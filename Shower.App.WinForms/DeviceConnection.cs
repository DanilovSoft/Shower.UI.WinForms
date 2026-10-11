using Microsoft.Extensions.Options;
using Shower.Domain.RpcClient;

namespace ShowerUI;

/// <summary>
/// Подключение к контроллеру для WinForms-версии: здесь нет DI и appsettings.json, адрес задан в коде.
/// </summary>
internal static class DeviceConnection
{
    private static readonly ConnectionFactory Factory = new(Options.Create(new ShowerConnectionOptions
    {
        Host = "10.2.2.218",
        Port = 333,
    }));

    public static Task<ShowerConnection> ConnectAsync(CancellationToken ct) => Factory.ConnectAsync(ct);
}
