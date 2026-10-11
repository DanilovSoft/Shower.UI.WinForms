using System.Net.NetworkInformation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shower.App.Services;

namespace Shower.App.ViewModels;

/// <summary>
/// Вкладка «Отладка»: проверка связи и подключение контроллера к точке доступа Wi-Fi.
/// </summary>
public sealed partial class DebugViewModel : ObservableObject
{
    private readonly IDialogService _dialogs;
    private readonly Device _device;

    [ObservableProperty]
    public partial string? PingResult { get; private set; }

    [ObservableProperty]
    public partial string Ssid { get; private set; } = "";

    [ObservableProperty]
    public partial string Password { get; private set; } = "";

    /// <summary>
    /// MAC-адрес (BSSID) точки доступа, необязательный.
    /// </summary>
    [ObservableProperty]
    public partial string Mac { get; private set; } = "";

    [ObservableProperty]
    public partial string? WiFiResult { get; private set; }

    public DebugViewModel(IDialogService dialogs, Device device)
    {
        _dialogs = dialogs;
        _device = device;
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task PingAsync(CancellationToken token)
    {
        PingResult = "Ожидание ответа…";
        try
        {
            var code = await _device.PingAsync(token);
            PingResult = $"Ответ: {code}";
        }
        catch when (token.IsCancellationRequested)
        {
            PingResult = "Отменено";
        }
        catch (Exception ex)
        {
            PingResult = $"Ошибка: {ex.Message}";
        }
    }

    /// <summary>
    /// Подключение к точке доступа только в текущей сессии.
    /// </summary>
    [RelayCommand]
    private Task SetCurrentAccessPointAsync()
    {
        return SetAccessPointAsync(isDefault: false);
    }

    /// <summary>
    /// Подключение к точке доступа с сохранением данных как данных по умолчанию.
    /// </summary>
    [RelayCommand]
    private Task SetDefaultAccessPointAsync()
    {
        return SetAccessPointAsync(isDefault: true);
    }

    private async Task SetAccessPointAsync(bool isDefault)
    {
        WiFiResult = null;
        try
        {
            var ssid = Ssid;
            var password = Password;
            var bssid = string.IsNullOrWhiteSpace(Mac)
                ? null
                : PhysicalAddress.Parse(Mac.Trim().Replace(":", "").Replace("-", ""));

            await _device.RunAsync(connection =>
            {
                if (isDefault)
                {
                    connection.SetDefAP(ssid, password, bssid);
                }
                else
                {
                    connection.SetCurAP(ssid, password, bssid);
                }
            });

            WiFiResult = "Команда выполнена";
        }
        catch (Exception ex)
        {
            _dialogs.ShowError(ex.Message);
        }
    }
}
