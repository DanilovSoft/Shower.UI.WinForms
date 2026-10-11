using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.Json;
using CommunityToolkit.HighPerformance.Buffers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shower.App.Charts;
using Shower.App.Services;
using Shower.Domain.Heating;
using Shower.Domain.Models;
using Shower.Domain.RpcClient;

namespace Shower.App.ViewModels;

/// <summary>
/// Вкладка «Температура»: запись температуры в баке и прогноз времени нагрева.
/// </summary>
public sealed partial class WaterTemperatureViewModel : ObservableValidator
{
    private const int CalculationTemperatureLimit = 42;
    private const string TimeLeftPlaceholder = "__:__:__";
    private const string RecordFileFilter = "Json File|*.shower.txt";
    private const string SimulationFileFilter = "Json File|*.txt";

    // Должны совпадать с диапазоном ползунка в TemperatureView.xaml.
    private const double MinHeaterPower = 0.5;
    private const double MaxHeaterPower = 3;

    private static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(1);
    private static readonly TimeSpan ReconnectDelay = TimeSpan.FromSeconds(1);

    private readonly IDialogService _dialogs;
    private readonly Device _device;
    private readonly List<InternalTempModel> _records = new(600);
    private CancellationTokenSource? _cts;
    private HeatingTimeLeft? _calculator;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsBusy))]
    [NotifyCanExecuteChangedFor(nameof(StartCommand), nameof(StopCommand), nameof(LoadCommand), nameof(SimulateCommand))]
    private bool _isRecording;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsBusy))]
    [NotifyCanExecuteChangedFor(nameof(StartCommand), nameof(StopCommand), nameof(LoadCommand), nameof(SimulateCommand))]
    private bool _isSimulating;

    [ObservableProperty]
    private int _reconnectCount;

    [ObservableProperty]
    private string? _errorMessage;

    [ObservableProperty]
    private string _timeLeftText = TimeLeftPlaceholder;

    /// <summary>
    /// Через сколько минут после включения нагрева взять точку для прогноза. 0 — показывать время с контроллера.
    /// </summary>
    [ObservableProperty]
    private int _forecastStartMinute;

    /// <summary>
    /// Мощность ТЭНа на ползунке, кВт. Используется для прогноза, если <see cref="HasHeaterPower"/>.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HeaterPowerText), nameof(HeaterPowerDeltaText), nameof(IsHeaterPowerChanged))]
    [NotifyCanExecuteChangedFor(nameof(ResetHeaterPowerCommand))]
    private double _heaterPower = MinHeaterPower;

    /// <summary>
    /// Мощность ТЭНа задана: считана с контроллера или выбрана пользователем.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HeaterPowerText), nameof(HeaterPowerDeltaText), nameof(IsHeaterPowerChanged))]
    [NotifyCanExecuteChangedFor(nameof(ResetHeaterPowerCommand))]
    private bool _hasHeaterPower;

    /// <summary>
    /// Мощность ТЭНа, записанная в контроллер, кВт.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HeaterPowerDeltaText), nameof(IsHeaterPowerChanged))]
    [NotifyCanExecuteChangedFor(nameof(ResetHeaterPowerCommand))]
    private float? _deviceHeaterPower;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [CustomValidation(typeof(WaterTemperatureViewModel), nameof(ValidateTankVolume))]
    private string _tankVolumeText = "";

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [CustomValidation(typeof(WaterTemperatureViewModel), nameof(ValidateWaterLevel))]
    private string _waterLevelText = "";

    public WaterTemperatureViewModel(IDialogService dialogs, Device device)
    {
        _dialogs = dialogs;
        _device = device;
    }

    public TemperatureChart Chart { get; } = new();

    public bool IsBusy => IsRecording || IsSimulating;

    public string HeaterPowerText => HasHeaterPower ? FormatKWatt(RoundedHeaterPower) + " кВт" : "—";

    /// <summary>
    /// Насколько мощность на ползунке отличается от записанной в контроллер.
    /// </summary>
    public string HeaterPowerDeltaText
    {
        get
        {
            if (DeviceHeaterPower is not { } device)
            {
                return "В контроллере: нет данных";
            }

            var deviceText = $"В контроллере {FormatKWatt(device)} кВт";
            if (!HasHeaterPower)
            {
                return deviceText;
            }

            var delta = HeaterPowerDelta;
            if (delta == 0)
            {
                return deviceText + ", совпадает";
            }

            var percent = device > 0
                ? string.Create(CultureInfo.InvariantCulture, $" ({delta / device:+0.0%;-0.0%})")
                : "";
            return string.Create(CultureInfo.InvariantCulture, $"{deviceText}, разница {delta:+0.00;-0.00} кВт{percent}");
        }
    }

    /// <summary>
    /// Мощность на ползунке отличается от записанной в контроллер.
    /// </summary>
    public bool IsHeaterPowerChanged => HasHeaterPower && DeviceHeaterPower is not null && HeaterPowerDelta != 0;

    private double RoundedHeaterPower => Math.Round(HeaterPower, 2);

    private double HeaterPowerDelta => DeviceHeaterPower is { } device
        ? Math.Round(RoundedHeaterPower - Math.Round(device, 2), 2)
        : 0;

    public static ValidationResult? ValidateTankVolume(string text, ValidationContext context)
    {
        return TryParseTankVolume(text, out _)
            ? ValidationResult.Success
            : new ValidationResult("Введите число больше 1 и меньше 100");
    }

    public static ValidationResult? ValidateWaterLevel(string text, ValidationContext context)
    {
        return TryParseWaterLevel(text, out _)
            ? ValidationResult.Success
            : new ValidationResult("Введите число от 1 до 100");
    }

    /// <summary>
    /// Останавливает запись и симуляцию.
    /// </summary>
    [RelayCommand(CanExecute = nameof(IsBusy))]
    public void Stop()
    {
        _cts?.Cancel();
    }

    [RelayCommand(CanExecute = nameof(CanStart))]
    private async Task StartAsync()
    {
        var cts = _cts = new CancellationTokenSource();

        IsRecording = true;
        try
        {
            await RecordTemperature(cts.Token);
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            await cts.CancelAsync();
            IsRecording = false;
        }
    }

    private async Task RecordTemperature(CancellationToken ct)
    {
        ErrorMessage = null;
        ReconnectCount = 0;
        ClearRecords();

        var stopwatch = Stopwatch.StartNew();
        var wasConnectedOnce = false;

        while (!ct.IsCancellationRequested)
        {
            try
            {
                using var connection = await _device.CreateConnection(ct);

                if (wasConnectedOnce)
                {
                    ReconnectCount++;
                }

                wasConnectedOnce = true;
                ErrorMessage = null;

                await InitBeforeStartAsync(connection, ct);
                await RecordAsync(connection, stopwatch, ct);
            }
            catch (Exception e) when (!ct.IsCancellationRequested)
            {
                ErrorMessage = e.Message;
                await Task.Delay(ReconnectDelay, ct);
            }
        }
    }

    private bool CanStart() => !IsBusy;

    [RelayCommand]
    private void Clear()
    {
        if (IsSimulating)
        {
            Stop();
        }
        ClearRecords();
    }

    [RelayCommand]
    private void Save()
    {
        if (_dialogs.SaveFile(RecordFileFilter, "shower.txt") is not { } path)
        {
            return;
        }

        try
        {
            File.WriteAllText(path, JsonSerializer.Serialize(_records));
        }
        catch (Exception ex)
        {
            _dialogs.ShowError(ex.Message);
        }
    }

    [RelayCommand(CanExecute = nameof(CanStart))]
    private void Load()
    {
        if (_dialogs.OpenFile(RecordFileFilter) is not { } path)
        {
            return;
        }

        try
        {
            var records = ReadRecords(path);

            ClearRecords();
            _records.AddRange(records);
            Chart.AddRange(records);
            UpdateTimeLeft();
            _ = ReadDeviceSettingsAsync();
        }
        catch (Exception ex)
        {
            _dialogs.ShowError(ex.Message);
        }
    }

    [RelayCommand(CanExecute = nameof(CanStart))]
    private async Task SimulateAsync()
    {
        if (_dialogs.OpenFile(SimulationFileFilter) is not { } path)
        {
            return;
        }

        List<InternalTempModel> records;
        try
        {
            records = ReadRecords(path);
        }
        catch (Exception ex)
        {
            _dialogs.ShowError(ex.Message);
            return;
        }

        var cts = _cts = new CancellationTokenSource();
        IsSimulating = true;
        ClearRecords();
        _ = ReadDeviceSettingsAsync();

        try
        {
            foreach (var record in records)
            {
                AddRecord(record);
                await Task.Delay(10, cts.Token);
            }
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            cts.Cancel();
            IsSimulating = false;
        }
    }

    /// <summary>
    /// Возвращает ползунок мощности ТЭНа к значению из контроллера.
    /// </summary>
    [RelayCommand(CanExecute = nameof(IsHeaterPowerChanged))]
    private void ResetHeaterPower()
    {
        if (DeviceHeaterPower is { } device)
        {
            HeaterPower = ClampHeaterPower(device);
        }
    }

    /// <summary>
    /// Запоминает мощность ТЭНа из контроллера. Ползунок ставится на неё, если мощность ещё не была задана.
    /// </summary>
    public void SetDeviceHeaterPower(float? kWatt)
    {
        DeviceHeaterPower = kWatt;

        if (kWatt is { } device && !HasHeaterPower)
        {
            HeaterPower = ClampHeaterPower(device);
            HasHeaterPower = true;
            UpdateCalculator();
        }
    }

    partial void OnForecastStartMinuteChanged(int value) => UpdateTimeLeft();

    partial void OnHeaterPowerChanged(double value)
    {
        HasHeaterPower = true;
        UpdateCalculator();
    }

    partial void OnTankVolumeTextChanged(string value) => UpdateCalculator();

    partial void OnWaterLevelTextChanged(string value) => UpdateTimeLeft();

    private async Task InitBeforeStartAsync(ShowerConnection connection, CancellationToken ct)
    {
        var heaterPower = await _device.RequestAsync(connection.GetWaterHeaterPowerKWattAsync, ct);
        var tankVolume = await _device.RequestAsync(connection.GetWaterTankVolumeLitreAsync, ct);

        SetDeviceHeaterPower(heaterPower);
        TankVolumeText = tankVolume.ToString(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Считывает мощность ТЭНа и объём бака для прогноза по записи, загруженной из файла.
    /// </summary>
    private async Task ReadDeviceSettingsAsync()
    {
        try
        {
            using var cts = new CancellationTokenSource(Device.RequestTimeout);
            using var connection = await _device.CreateConnection(cts.Token);
            await InitBeforeStartAsync(connection, cts.Token);
        }
        catch (Exception ex)
        {
            var reason = ex is OperationCanceledException ? "устройство не ответило за отведённое время." : ex.Message;
            ErrorMessage = $"Не удалось считать мощность ТЭНа и объём бака: {reason}";
        }
    }

    private async Task RecordAsync(ShowerConnection connection, Stopwatch stopwatch, CancellationToken ct)
    {
        // Все показания запрашиваются одним пакетом — один раундтрип за опрос.
        var batch = connection.CreateBatch();
        var internalTemp = batch.Get<float>(ShowerCode.GetWaterTemperature);
        var heaterEnabled = batch.Get<byte>(ShowerCode.GetHeaterEnabled);
        var minutesLeft = batch.Get<byte>(ShowerCode.GetMinutesLeft);
        var waterPercent = batch.Get<byte>(ShowerCode.GetWaterPercent);

        while (!ct.IsCancellationRequested)
        {
            await _device.RequestAsync(batch.ExecuteAsync, ct);

            var second = (int)stopwatch.Elapsed.TotalSeconds;

            WaterLevelText = PercentToString(waterPercent);

            AddRecord(new InternalTempModel
            {
                Timestamp = DateTime.Now,
                Second = second,
                InternalTemp = internalTemp.Value,
                HeaterEnabled = heaterEnabled.Value != 0,
                TimeLeft = TimeSpan.FromMinutes(minutesLeft.Value),
            });

            await Task.Delay(PollInterval, ct);
        }
    }

    private static string PercentToString(BatchValue<byte> waterPercent)
    {
        Span<char> buffer = stackalloc char[4];

        // value to span
        if (!waterPercent.Value.TryFormat(buffer, out var charsWritten, provider: CultureInfo.InvariantCulture))
        {
            return waterPercent.Value.ToString(CultureInfo.InvariantCulture);
        }

        return StringPool.Shared.GetOrAdd(buffer[..charsWritten]);
    }

    private void AddRecord(InternalTempModel record)
    {
        _records.Add(record);
        Chart.Add(record);
        UpdateTimeLeft();
    }

    private void ClearRecords()
    {
        _records.Clear();
        Chart.Clear();
        TimeLeftText = TimeLeftPlaceholder;
    }

    private void UpdateCalculator()
    {
        _calculator = HasHeaterPower && TryParseTankVolume(TankVolumeText, out var volume)
            ? new HeatingTimeLeft(volume, (float)RoundedHeaterPower)
            : null;

        UpdateTimeLeft();
    }

    private void UpdateTimeLeft()
    {
        if (ForecastStartMinute == 0)
        {
            Chart.SetForecast(null);
            TimeLeftText = _records.Count > 0 ? FormatTimeLeft(_records[^1].TimeLeft) : TimeLeftPlaceholder;
            return;
        }

        HeatingForecast? forecast = null;
        if (_calculator is { } calculator && TryParseWaterLevel(WaterLevelText, out var waterPercent))
        {
            forecast = HeatingForecast.Calculate(_records, ForecastStartMinute, waterPercent / 100f, calculator, CalculationTemperatureLimit);
        }

        Chart.SetForecast(forecast);
        TimeLeftText = forecast is { } f ? FormatTimeLeft(f.TimeLeft) : TimeLeftPlaceholder;
    }

    private static List<InternalTempModel> ReadRecords(string path)
    {
        return JsonSerializer.Deserialize<List<InternalTempModel>>(File.ReadAllText(path))
            ?? throw new InvalidDataException("Файл не содержит записей.");
    }

    private static string FormatTimeLeft(TimeSpan timeLeft) => timeLeft.ToString("hh\\:mm\\:ss");

    private static double ClampHeaterPower(float kWatt) => Math.Clamp(Math.Round(kWatt, 2), MinHeaterPower, MaxHeaterPower);

    private static string FormatKWatt(double kWatt) => kWatt.ToString("0.00", CultureInfo.InvariantCulture);

    private static bool TryParseTankVolume(string? text, out float litre)
    {
        return TryParse(text, out litre) && litre > 1 && litre < 100;
    }

    private static bool TryParseWaterLevel(string? text, out float percent)
    {
        return TryParse(text, out percent) && percent >= 1 && percent <= 100;
    }

    private static bool TryParse(string? text, out float value)
    {
        return float.TryParse(text?.Replace(',', '.'), NumberStyles.Number, CultureInfo.InvariantCulture, out value);
    }
}
