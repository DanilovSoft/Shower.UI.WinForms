using System.Net.NetworkInformation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shower.App.Services;

namespace Shower.App.ViewModels;

/// <summary>
/// Вкладка «Отладка»: проверка связи и подключение контроллера к точке доступа Wi-Fi.
/// </summary>
public sealed partial class DebugViewModel(IDialogService dialogs, Device device) : ObservableObject
{
    [ObservableProperty]
    public partial string? PingResult { get; set; }

    [ObservableProperty]
    public partial string Ssid { get; set; } = "";

    [ObservableProperty]
    public partial string Password { get; set; } = "";

    /// <summary>
    /// MAC-адрес (BSSID) точки доступа, необязательный.
    /// </summary>
    [ObservableProperty]
    public partial string Mac { get; set; } = "";

    [ObservableProperty]
    public partial string? WiFiResult { get; set; }

    [RelayCommand(IncludeCancelCommand = true)]
    private async Task PingAsync(CancellationToken token)
    {
        PingResult = "Ожидание ответа…";
        try
        {
            var code = await device.PingAsync(token);
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
        return SetAccessPointAsync(isDefault: false, CancellationToken.None);
    }

    /// <summary>
    /// Подключение к точке доступа с сохранением данных как данных по умолчанию.
    /// </summary>
    [RelayCommand]
    private Task SetDefaultAccessPointAsync()
    {
        return SetAccessPointAsync(isDefault: true, CancellationToken.None);
    }

    private async Task SetAccessPointAsync(bool isDefault, CancellationToken ct)
    {
        WiFiResult = null;
        try
        {
            var ssid = Ssid;
            var password = Password;
            var bssid = string.IsNullOrWhiteSpace(Mac)
                ? null
                : PhysicalAddress.Parse(Mac.Trim().Replace(":", "", StringComparison.InvariantCulture).Replace("-", "", StringComparison.InvariantCulture));

            await device.ConnectAndExecute((connection, ct) => isDefault
                ? connection.SetDefAPAsync(ssid, password, bssid, ct)
                : connection.SetCurAPAsync(ssid, password, bssid, ct),
                ct);

            WiFiResult = "Команда выполнена";
        }
        catch (Exception ex)
        {
            dialogs.ShowError(ex.Message);
        }
    }
}
