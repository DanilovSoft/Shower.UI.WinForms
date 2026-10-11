using System.IO;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shower.App.Services;
using Shower.Domain.Models;

namespace Shower.App.ViewModels;

/// <summary>
/// Вкладка «Параметры»: чтение, изменение и запись настроек контроллера, импорт и экспорт в файл.
/// </summary>
public sealed partial class ParametersViewModel : ObservableObject
{
    private const string FileFilter = "Json File|*.shower.txt";
    private const string DefaultFileName = "Parameters";

    private static readonly JsonSerializerOptions ExportOptions = new() { WriteIndented = true };

    private readonly IDialogService _dialogs;
    private CancellationTokenSource? _loadCts;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEditable))]
    [NotifyCanExecuteChangedFor(nameof(LoadCommand), nameof(SaveCommand), nameof(ClearCommand), nameof(RestartCommand),
        nameof(ExportCommand), nameof(ImportCommand), nameof(CancelCommand))]
    public partial bool IsLoading { get; private set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEditable))]
    [NotifyCanExecuteChangedFor(nameof(LoadCommand), nameof(SaveCommand), nameof(ClearCommand), nameof(RestartCommand),
        nameof(ExportCommand), nameof(ImportCommand))]
    public partial bool IsBusy { get; private set; }

    /// <summary>
    /// Сработал ли сторожевой таймер контроллера с прошлого запуска. <see langword="null"/> — неизвестно.
    /// </summary>
    [ObservableProperty]
    public partial bool? WatchDogWasReset { get; set; }

    public ParametersViewModel(IDialogService dialogs)
    {
        _dialogs = dialogs;

        LeftColumn =
        [
            new ParameterViewModel<ushort>("Уровень полного бака (μs)",
                "Число микросекунд соответствующие расстоянию когда бак полностью заполнен водой",
                m => m.WaterLevelFullUsec, (m, v) => m.WaterLevelFullUsec = v, (c, v) => c.SetWaterLevelFullUsec(v)),
            new ParameterViewModel<ushort>("Уровень пустого бака (μs)",
                "Число микросекунд соответствующие расстоянию когда бак полностью пустой",
                m => m.WaterLevelEmptyUsec, (m, v) => m.WaterLevelEmptyUsec = v, (c, v) => c.SetWaterLevelEmptyUsec(v)),
            new ParameterViewModel<byte>("Минимальный уровень воды (%)",
                "Уровень воды в баке ниже которого нагреватель не будет включаться",
                m => m.MinimumWaterHeatingLevel, (m, v) => m.MinimumWaterHeatingLevel = v, (c, v) => c.SetMinimumWaterHeatingLevel(v)),
            new ParameterViewModel<byte>("Максимальное время нагрева (мин)",
                "Если на протяжении заданного времени не была достигнута желаемая температура воды в баке, то нагрев прекращается и алгоритм переходит в аварийное состояние",
                m => m.HeatingTimeLimit, (m, v) => m.HeatingTimeLimit = v, (c, v) => c.SetHeatingTimeLimit(v)),
            new ParameterViewModel<byte>("Абсолютное время нагрева (ч)",
                "Если на протяжении заданного времени, автомат нагревателя не был физически выключен то нагрев прекратится и контроллер перейдёт в аварийное состояние",
                m => m.AbsoluteHeatingTimeLimit, (m, v) => m.AbsoluteHeatingTimeLimit = v, (c, v) => c.SetAbsoluteHeatingTimeLimit(v)),
            new ParameterViewModel<byte>("Яркость освещения (%)",
                "Яркость светодиодного освещения от 0 до 100%",
                m => m.LightBrightness, (m, v) => m.LightBrightness = v, (c, v) => c.SetLightBrightness(v)),
            new WiFiPowerParameterViewModel("Мощность Wi-Fi (10..20.5 dBm)",
                "Мощность передатчика Wi-Fi с шагом 0.25 dBm"),
            new ParameterViewModel<byte>("Время срабатывания кнопок (msec)",
                "Время нажатия кнопки для её срабатывания (антидребезг), от 5 до 50 мсек.",
                m => m.ButtonPressTimeMsec, (m, v) => m.ButtonPressTimeMsec = v, (c, v) => c.SetButtonTimeMsec(v)),
            new ParameterViewModel<ushort>("Долгое нажатие кнопок (msec)",
                "Время зажатой кнопки для определения длительного нажатия, от 1000 до 10000 мсек.",
                m => m.ButtonLongTimeMsec, (m, v) => m.ButtonLongTimeMsec = v, (c, v) => c.SetButtonLongPressTimeMsec(v)),
        ];

        RightColumn =
        [
            new ParameterViewModel<byte>("Интервал замера уровня воды (msec)",
                "Длительность паузы перед повторным измерением расстояния до воды в баке. Рекомендуется не меньше 60 мсек",
                m => m.WaterLevelMeasureInterval, (m, v) => m.WaterLevelMeasureInterval = v, (c, v) => c.SetWaterLevelMeasureInterval(v)),
            new ParameterViewModel<byte>("Медианный фильтр для уровня воды",
                "Размер буфера медианного фильтра для RAW показаний датчика уровня воды (от 1 до 255)",
                m => m.WaterLevelMedianBufferSize, (m, v) => m.WaterLevelMedianBufferSize = v, (c, v) => c.SetWaterLevelMedianBufferSize(v)),
            new ParameterViewModel<byte>("Скользящее среднее для уровня воды",
                "Размер буфера фильтра 'скользящее среднее' для значений медианы датчика уровня воды (от 1 до 129)",
                m => m.WaterLevelAverageBufferSize, (m, v) => m.WaterLevelAverageBufferSize = v, (c, v) => c.SetWaterLevelAverageBufferSize(v)),
            new ParameterViewModel<byte>("Порог ошибок уровня воды",
                "Число неудачных измерений уровня воды после которого на индикаторе отобразятся прочерки (от 1 до 255)",
                m => m.WaterLevelErrorThreshhold, (m, v) => m.WaterLevelErrorThreshhold = v, (c, v) => c.SetWaterLevelErrorThreshold(v)),
            new ParameterViewModel<byte>("Порог отключения набора воды (%)",
                null,
                m => m.WaterValveCutOffPercent, (m, v) => m.WaterValveCutOffPercent = v, (c, v) => c.SetWaterValveCutOffPercent(v)),
            new ParameterViewModel<byte>("Скользящее среднее для температуры в баке",
                "Размер буфера фильтра 'скользящее среднее' для значений датчика температуры внутри бака (от 1 до 8)",
                m => m.TempSensorInternalTempAverageSize, (m, v) => m.TempSensorInternalTempAverageSize = v, (c, v) => c.SetTempSensorInternalTempAverageSize(v)),
            new ParameterViewModel<float>("Объём воды полного бака (л)",
                null,
                m => m.WaterTankVolumeLitre, (m, v) => m.WaterTankVolumeLitre = v, (c, v) => c.SetWaterTankVolumeLitre(v)),
            new ParameterViewModel<float>("Мощность ТЭНа с учётом КПД (кВт)",
                null,
                m => m.WaterHeaterPowerKWatt, (m, v) => m.WaterHeaterPowerKWatt = v, (c, v) => c.SetWaterHeaterPowerKWatt(v)),
        ];

        All = [.. LeftColumn, .. RightColumn];
    }

    public IReadOnlyList<ParameterViewModel> LeftColumn { get; }

    public IReadOnlyList<ParameterViewModel> RightColumn { get; }

    public IReadOnlyList<ParameterViewModel> All { get; }

    /// <summary>
    /// Параметры считаны с контроллера.
    /// </summary>
    public event Action<PropertiesModel>? PropertiesLoaded;

    public bool IsEditable => !IsLoading && !IsBusy;

    [RelayCommand(CanExecute = nameof(IsEditable))]
    private async Task LoadAsync()
    {
        var cts = _loadCts = new CancellationTokenSource();
        IsLoading = true;

        try
        {
            var model = await Device.RunAsync(PropertiesModel.Load, cts.Token);
            if (!cts.IsCancellationRequested)
            {
                SetOriginal(model);
                PropertiesLoaded?.Invoke(model);
            }
        }
        catch when (cts.IsCancellationRequested)
        {
        }
        catch (Exception ex)
        {
            _dialogs.ShowError(ex.Message);
        }
        finally
        {
            cts.Cancel();
            IsLoading = false;
        }
    }

    [RelayCommand(CanExecute = nameof(IsLoading))]
    private void Cancel()
    {
        _loadCts?.Cancel();
        IsLoading = false;
    }

    [RelayCommand(CanExecute = nameof(IsEditable))]
    private async Task SaveAsync()
    {
        if (All.FirstOrDefault(p => p.HasChanges && p.HasErrors) is { } invalid)
        {
            _dialogs.ShowError($"Исправьте значение параметра «{invalid.Caption}».");
            return;
        }

        var changed = All.Where(p => p.HasChanges).ToList();
        var written = new List<ParameterViewModel>(changed.Count);

        IsBusy = true;
        try
        {
            await Device.RunAsync(connection =>
            {
                foreach (var parameter in changed)
                {
                    parameter.Write(connection);
                    written.Add(parameter);
                }
                connection.Save();
            });
        }
        catch (Exception ex)
        {
            _dialogs.ShowError(ex.Message);
            return;
        }
        finally
        {
            foreach (var parameter in written)
            {
                parameter.AcceptChanges();
            }
            IsBusy = false;
        }

        await LoadAsync();
    }

    [RelayCommand(CanExecute = nameof(IsEditable))]
    private void Clear()
    {
        SetOriginal(null);
    }

    [RelayCommand(CanExecute = nameof(IsEditable))]
    private async Task RestartAsync()
    {
        IsBusy = true;
        try
        {
            await Device.RestartAsync();
        }
        catch (Exception ex)
        {
            _dialogs.ShowError(ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand(CanExecute = nameof(IsEditable))]
    private void Export()
    {
        var model = new PropertiesModel();
        foreach (var parameter in All)
        {
            parameter.Export(model);
        }

        if (_dialogs.SaveFile(FileFilter, "shower.txt", DefaultFileName) is not { } path)
        {
            return;
        }

        try
        {
            File.WriteAllText(path, JsonSerializer.Serialize(model, ExportOptions));
        }
        catch (Exception ex)
        {
            _dialogs.ShowError(ex.Message);
        }
    }

    [RelayCommand(CanExecute = nameof(IsEditable))]
    private void Import()
    {
        if (_dialogs.OpenFile(FileFilter, DefaultFileName) is not { } path)
        {
            return;
        }

        try
        {
            var model = JsonSerializer.Deserialize<PropertiesModel>(File.ReadAllText(path))
                ?? throw new InvalidDataException("Файл не содержит параметров.");

            // Значения отличающиеся от считанных с контроллера подсвечиваются и будут записаны по кнопке «Записать».
            foreach (var parameter in All)
            {
                parameter.SetPending(model);
            }
        }
        catch (Exception ex)
        {
            _dialogs.ShowError(ex.Message);
        }
    }

    private void SetOriginal(PropertiesModel? model)
    {
        foreach (var parameter in All)
        {
            parameter.SetOriginal(model);
        }
        WatchDogWasReset = model?.WatchDogWasReset;
    }
}
