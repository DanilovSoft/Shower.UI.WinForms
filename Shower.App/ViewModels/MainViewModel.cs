using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shower.App.Services;

namespace Shower.App.ViewModels;

public sealed partial class MainViewModel : ObservableObject
{
    private readonly IDialogService _dialogs;
    private readonly Device _device;

    /// <summary>
    /// Выполняется команда, на время которой окно блокируется.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsIdle))]
    public partial bool IsBusy { get; set; }

    public MainViewModel(
        IDialogService dialogs,
        WaterTemperatureViewModel temperature,
        WaterLevelViewModel waterLevel,
        CalibrationViewModel calibration,
        ParametersViewModel parameters,
        DebugViewModel debug,
        Device device)
    {
        _dialogs = dialogs;

        Temperature = temperature;
        WaterLevel = waterLevel;
        Calibration = calibration;
        Parameters = parameters;
        Debug = debug;
        _device = device;

        // После записи параметров разница мощности ТЭНа на вкладке «Температура» считается от нового значения.
        Parameters.PropertiesLoaded += (_, model) => Temperature.SetDeviceHeaterPower(model.WaterHeaterPowerKWatt);
    }

    public WaterTemperatureViewModel Temperature { get; }

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
            await _device.RestartAsync(CancellationToken.None);
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
            var code = await _device.PingAsync(cts.Token);
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
