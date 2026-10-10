using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shower.App.Services;

namespace Shower.App.ViewModels;

public sealed partial class MainViewModel : ObservableObject
{
    private readonly IDialogService _dialogs;

    /// <summary>
    /// Выполняется команда, на время которой окно блокируется.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsIdle))]
    private bool _isBusy;

    public MainViewModel(IDialogService dialogs)
    {
        _dialogs = dialogs;

        Temperature = new TemperatureViewModel(dialogs);
        WaterLevel = new WaterLevelViewModel(dialogs);
        Calibration = new CalibrationViewModel();
        Parameters = new ParametersViewModel(dialogs);
        Debug = new DebugViewModel(dialogs);
    }

    public TemperatureViewModel Temperature { get; }

    public WaterLevelViewModel WaterLevel { get; }

    public CalibrationViewModel Calibration { get; }

    public ParametersViewModel Parameters { get; }

    public DebugViewModel Debug { get; }

    public bool IsIdle => !IsBusy;

    /// <summary>
    /// Останавливает все циклы опроса контроллера.
    /// </summary>
    public void StopAll()
    {
        Temperature.Stop();
        WaterLevel.Stop();
        Calibration.Stop();
    }

    [RelayCommand]
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

    [RelayCommand]
    private async Task PingAsync()
    {
        IsBusy = true;
        try
        {
            using var cts = new CancellationTokenSource(Device.RequestTimeout);
            var code = await Device.PingAsync(cts.Token);
            _dialogs.ShowInfo($"Ответ контроллера: {code}", "Ping");
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
}
