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

    [ObservableProperty]
    private string? _pingResult;

    [ObservableProperty]
    private string _ssid = "";

    [ObservableProperty]
    private string _password = "";

    /// <summary>
    /// MAC-адрес (BSSID) точки доступа, необязательный.
    /// </summary>
    [ObservableProperty]
    private string _mac = "";

    [ObservableProperty]
    private string? _wiFiResult;

    public DebugViewModel(IDialogService dialogs)
    {
        _dialogs = dialogs;
    }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task PingAsync(CancellationToken token)
    {
        PingResult = "Ожидание ответа…";
        try
        {
            var code = await Device.PingAsync(token);
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

            await Device.RunAsync(connection =>
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
