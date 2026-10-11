using System.IO;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shower.App.Charts;
using Shower.App.Services;
using Shower.Domain.RpcClient;
using Shower.Domain.WaterLevel;

namespace Shower.App.ViewModels;

/// <summary>
/// Вкладка «Уровень воды»: запись показаний датчика и подбор параметров фильтров.
/// </summary>
public sealed partial class WaterLevelViewModel : ObservableObject
{
    private const string FileFilter = "Json File|*.txt";
    private static readonly TimeSpan PollInterval = TimeSpan.FromMilliseconds(40);
    private static readonly TimeSpan ReconnectDelay = TimeSpan.FromSeconds(3);
    private static readonly TimeSpan RebuildDelay = TimeSpan.FromMilliseconds(150);

    private readonly IDialogService _dialogs;
    private readonly Device _device;

    /// <summary>
    /// Номер точки на графике для каждого элемента <see cref="WaterLevelSession.UsecList"/>.
    /// </summary>
    private readonly List<int> _indices = new(30000);
    private WaterLevelSession _session = new();
    private WaterLevelFilterPipeline _pipeline;
    private int _nextPointIndex;
    private CancellationTokenSource? _cts;
    private CancellationTokenSource? _rebuildCts;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(StartCommand), nameof(StopCommand), nameof(LoadCommand))]
    public partial bool IsRecording { get; set; }

    [ObservableProperty]
    public partial int ReconnectCount { get; set; }

    [ObservableProperty]
    public partial string? ErrorMessage { get; set; }

    /// <summary>
    /// Показание датчика (мкс) для полного бака.
    /// </summary>
    [ObservableProperty]
    public partial ushort? MinWaterLevel { get; set; }

    /// <summary>
    /// Показание датчика (мкс) для пустого бака.
    /// </summary>
    [ObservableProperty]
    public partial ushort? MaxWaterLevel { get; set; }

    /// <summary>
    /// Положение ползунка медианного фильтра, размер окна = 2 * значение + 1.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MedianWindow))]
    public partial int MedianSlider { get; set; } = 1;

    [ObservableProperty]
    public partial int AverageWindow { get; set; } = 1;

    [ObservableProperty]
    public partial bool ShowRaw { get; set; } = true;

    [ObservableProperty]
    public partial bool ShowMedian { get; set; }

    [ObservableProperty]
    public partial bool ShowAverage { get; set; }

    [ObservableProperty]
    public partial bool ShowPercent { get; set; } = true;

    public WaterLevelViewModel(IDialogService dialogs, Device device)
    {
        _dialogs = dialogs;
        _device = device;
        _pipeline = CreatePipeline();
        UpdateVisibility();
    }

    // Должны совпадать с границами ползунков в WaterLevelView.xaml.
    private const int MedianSliderMin = 1;
    private const int MedianSliderMax = 128;
    private const int AverageWindowMin = 1;
    private const int AverageWindowMax = 129;

    public WaterLevelChart Chart { get; } = new();

    public int MedianWindow => WaterLevelFilterPipeline.MedianWindowFromSlider(MedianSlider);

    [RelayCommand(CanExecute = nameof(CanStart))]
    private async Task StartAsync()
    {
        var cts = _cts = new CancellationTokenSource();
        IsRecording = true;
        ErrorMessage = null;
        ReconnectCount = 0;
        var wasConnected = false;

        try
        {
            while (!cts.IsCancellationRequested)
            {
                try
                {
                    using var connection = await _device.ConnectAsync(cts.Token);
                    if (wasConnected)
                    {
                        ReconnectCount++;
                    }
                    wasConnected = true;
                    ErrorMessage = null;

                    await RecordAsync(connection, cts.Token);
                }
                catch when (cts.IsCancellationRequested)
                {
                    return;
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    try
                    {
                        await Task.Delay(ReconnectDelay, cts.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        return;
                    }
                }
            }
        }
        finally
        {
            cts.Cancel();
            IsRecording = false;
        }
    }

    private bool CanStart() => !IsRecording;

    [RelayCommand(CanExecute = nameof(IsRecording))]
    public void Stop()
    {
        _cts?.Cancel();
    }

    [RelayCommand]
    private void Clear()
    {
        _session.UsecList.Clear();
        _indices.Clear();
        _nextPointIndex = 0;
        _pipeline = CreatePipeline();
        Chart.Clear();
    }

    [RelayCommand]
    private void FitToData()
    {
        Chart.FitToData();
    }

    [RelayCommand]
    private void Save()
    {
        if (_session.UsecList.Count == 0)
        {
            _dialogs.ShowInfo("Нет данных для сохранения.", "Уровень воды");
            return;
        }

        if (_dialogs.SaveFile(FileFilter, "txt") is not { } path)
        {
            return;
        }

        try
        {
            File.WriteAllText(path, JsonSerializer.Serialize(_session.UsecList));
        }
        catch (Exception ex)
        {
            _dialogs.ShowError(ex.Message);
        }
    }

    [RelayCommand(CanExecute = nameof(CanStart))]
    private async Task LoadAsync()
    {
        if (_dialogs.OpenFile(FileFilter) is not { } path)
        {
            return;
        }

        ushort[] usecList;
        try
        {
            usecList = JsonSerializer.Deserialize<ushort[]>(File.ReadAllText(path))
                ?? throw new InvalidDataException("Файл не содержит данных.");
        }
        catch (Exception ex)
        {
            _dialogs.ShowError(ex.Message);
            return;
        }

        var session = new WaterLevelSession();
        session.UsecList.AddRange(usecList);

        // Калибровка в файле не хранится — берём текущую с контроллера.
        try
        {
            using var cts = new CancellationTokenSource(Device.RequestTimeout);
            using var connection = await _device.ConnectAsync(cts.Token);
            session.WaterLevelEmpty = await connection.GetWaterLevelEmptyAsync(cts.Token);
            session.WaterLevelFull = await connection.GetWaterLevelFullAsync(cts.Token);
        }
        catch (Exception ex)
        {
            session.WaterLevelEmpty = usecList.Length > 0 ? usecList.Max() : (ushort)0;
            session.WaterLevelFull = usecList.Length > 0 ? usecList.Min() : (ushort)0;
            _dialogs.ShowError(
                $"Не удалось получить калибровку датчика с контроллера: {ex.Message}\n\n" +
                "Диапазон графика определён по загруженным данным, проценты могут быть неточными.");
        }

        _session = session;
        _indices.Clear();
        _indices.AddRange(Enumerable.Range(0, usecList.Length));
        _nextPointIndex = usecList.Length;
        ApplyCalibration();

        await RebuildAsync(includeRaw: true, CancellationToken.None);
    }

    partial void OnMedianSliderChanged(int value) => ScheduleRebuild();

    partial void OnAverageWindowChanged(int value) => ScheduleRebuild();

    partial void OnShowRawChanged(bool value) => UpdateVisibility();

    partial void OnShowMedianChanged(bool value) => UpdateVisibility();

    partial void OnShowAverageChanged(bool value) => UpdateVisibility();

    partial void OnShowPercentChanged(bool value) => UpdateVisibility();

    private async Task RecordAsync(ShowerConnection connection, CancellationToken ct)
    {
        var empty = await _device.RequestAsync(connection.GetWaterLevelEmptyAsync, ct);
        var full = await _device.RequestAsync(connection.GetWaterLevelFullAsync, ct);
        var medianSize = await _device.RequestAsync(connection.GetWaterLevelMedianSizeAsync, ct);
        var averageSize = await _device.RequestAsync(connection.GetWaterLevelAverageSizeAsync, ct);

        _session.WaterLevelEmpty = empty;
        _session.WaterLevelFull = full;
        ApplyCalibration();

        // Ползунки показывают настройки фильтров контроллера.
        MedianSlider = Math.Clamp(WaterLevelFilterPipeline.SliderFromMedianBufferSize(medianSize), MedianSliderMin, MedianSliderMax);
        AverageWindow = Math.Clamp((int)averageSize, AverageWindowMin, AverageWindowMax);
        _rebuildCts?.Cancel();
        await RebuildAsync(includeRaw: false, ct);

        while (!ct.IsCancellationRequested)
        {
            await Task.Delay(PollInterval, ct);
            var rawUsec = await _device.RequestAsync(connection.GetWaterLevelRawAsync, ct);

            // -1 — у датчика произошёл таймаут.
            if (rawUsec != -1)
            {
                var usec = _session.InvertRaw(rawUsec);
                _session.UsecList.Add(usec);
                _indices.Add(_nextPointIndex);
                Chart.Add(_nextPointIndex, usec, _pipeline.Add(usec));
            }

            _nextPointIndex++;
        }
    }

    private void ApplyCalibration()
    {
        MinWaterLevel = _session.WaterLevelFull;
        MaxWaterLevel = _session.WaterLevelEmpty;
        Chart.SetUsecRange(_session.WaterLevelFull, _session.WaterLevelEmpty);
    }

    private async void ScheduleRebuild()
    {
        _rebuildCts?.Cancel();
        var cts = _rebuildCts = new CancellationTokenSource();

        try
        {
            // Ползунок меняется непрерывно — пересчитываем, когда пользователь остановится.
            await Task.Delay(RebuildDelay, cts.Token);
            await RebuildAsync(includeRaw: false, cts.Token);
        }
        catch (OperationCanceledException)
        {
        }
    }

    /// <summary>
    /// Пересчитывает фильтры по всем записанным данным с текущими размерами окон.
    /// </summary>
    private async Task RebuildAsync(bool includeRaw, CancellationToken ct)
    {
        var session = _session;
        var usecs = session.UsecList.ToArray();
        var pipeline = CreatePipeline();

        var samples = await Task.Run(() => Array.ConvertAll(usecs, pipeline.Add), ct);

        if (ct.IsCancellationRequested || session != _session)
        {
            return;
        }

        // Пока шёл пересчёт, запись могла добавить новые точки.
        var sampleList = new List<WaterLevelSample>(samples);
        for (var i = usecs.Length; i < session.UsecList.Count; i++)
        {
            sampleList.Add(pipeline.Add(session.UsecList[i]));
        }

        _pipeline = pipeline;

        if (includeRaw)
        {
            Chart.SetAll(_indices, session.UsecList, sampleList);
        }
        else
        {
            Chart.SetFiltered(_indices, sampleList);
        }
    }

    private WaterLevelFilterPipeline CreatePipeline()
    {
        return new WaterLevelFilterPipeline(_session, MedianWindow, AverageWindow);
    }

    private void UpdateVisibility()
    {
        Chart.SetVisibility(ShowRaw, ShowMedian, ShowAverage, ShowPercent);
    }
}
