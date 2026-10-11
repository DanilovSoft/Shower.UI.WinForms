using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shower.App.Charts;
using Shower.App.Services;
using Shower.Domain.Filters;
using Shower.Domain.RpcClient;

namespace Shower.App.ViewModels;

/// <summary>
/// Вкладка «Калибровка уровня»: живой график сырых показаний датчика и счётчики его ошибок.
/// </summary>
public sealed partial class CalibrationViewModel : ObservableObject
{
    // Должно совпадать с границей ползунка в CalibrationView.xaml.
    private const int MaxMeasureIntervalMsec = 200;

    private static readonly TimeSpan RetryDelay = TimeSpan.FromSeconds(3);
    private static readonly TimeSpan ErrorCountersInterval = TimeSpan.FromSeconds(1);

    private CancellationTokenSource? _cts;
    private Device _device;

    public CalibrationViewModel(Device device)
    {
        _device = device;
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(StartCommand), nameof(StopCommand))]
    private bool _isRunning;

    [ObservableProperty]
    private string? _errorMessage;

    /// <summary>
    /// Пауза между запросами показаний, мс.
    /// </summary>
    [ObservableProperty]
    private int _measureIntervalMsec = 100;

    /// <summary>
    /// Медиана длительности одного цикла запроса.
    /// </summary>
    [ObservableProperty]
    private string _elapsedText = "__";

    [ObservableProperty]
    private string _errorCountersText = "Ошибки:";

    public CalibrationChart Chart { get; } = new();

    [RelayCommand(CanExecute = nameof(CanStart))]
    private async Task StartAsync()
    {
        var cts = _cts = new CancellationTokenSource();
        IsRunning = true;
        ErrorMessage = null;

        var stopwatch = Stopwatch.StartNew();
        var elapsedFilter = new MedianFilter<ushort>(9);

        try
        {
            while (!cts.IsCancellationRequested)
            {
                using var iterationCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token);
                var countersTask = PollErrorCountersAsync(iterationCts.Token);

                try
                {
                    using var connection = await _device.ConnectAsync(cts.Token);

                    var measureInterval = await _device.RequestAsync(connection.GetWaterLevelMeasureIntervalAsync, cts.Token);
                    MeasureIntervalMsec = Math.Clamp((int)measureInterval.TotalMilliseconds, 0, MaxMeasureIntervalMsec);

                    var waterLevelEmpty = await _device.RequestAsync(t => connection.RequestAsync<ushort>(ShowerCode.GetWaterLevelEmptyUsec, t), cts.Token);
                    Chart.SetCalibration(waterLevelEmpty);
                    ErrorMessage = null;

                    var requestTimer = new Stopwatch();
                    while (!cts.IsCancellationRequested)
                    {
                        requestTimer.Restart();
                        await Task.Delay(MeasureIntervalMsec, cts.Token);
                        var usec = await _device.RequestAsync(connection.GetWaterLevelRawAsync, cts.Token);
                        requestTimer.Stop();

                        var elapsed = elapsedFilter.Add((ushort)Math.Min(requestTimer.ElapsedMilliseconds, ushort.MaxValue));
                        if (elapsedFilter.IsInitialized)
                        {
                            ElapsedText = $"{elapsed} мсек";
                        }

                        Chart.Add(stopwatch.Elapsed.TotalSeconds, usec);
                    }
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
                        await Task.Delay(RetryDelay, cts.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        return;
                    }
                }
                finally
                {
                    iterationCts.Cancel();
                    await countersTask;
                }
            }
        }
        finally
        {
            cts.Cancel();
            IsRunning = false;
        }
    }

    private bool CanStart() => !IsRunning;

    [RelayCommand(CanExecute = nameof(IsRunning))]
    public void Stop()
    {
        _cts?.Cancel();
    }

    /// <summary>
    /// Раз в секунду запрашивает счётчики ошибок датчика по отдельному соединению.
    /// </summary>
    private async Task PollErrorCountersAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try
            {
                using var connection = await _device.ConnectAsync(ct);
                using var timer = new PeriodicTimer(ErrorCountersInterval);

                while (await timer.WaitForNextTickAsync(ct))
                {
                    var overflowCount = await _device.RequestAsync(t => connection.RequestAsync<ushort>(ShowerCode.GetWaterLevelOverflowCounter, t), ct);
                    var noiseErrors = await _device.RequestAsync(t => connection.RequestAsync<ushort>(ShowerCode.GetWaterLevelNoiseErrorCounter, t), ct);

                    ErrorCountersText = $"Ошибки: Overflow: {overflowCount}, Noise: {noiseErrors}";
                }
            }
            catch when (ct.IsCancellationRequested)
            {
                return;
            }
            catch
            {
                // Ошибку подключения показывает основной цикл, здесь просто повторяем.
                try
                {
                    await Task.Delay(RetryDelay, ct);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }
    }
}
