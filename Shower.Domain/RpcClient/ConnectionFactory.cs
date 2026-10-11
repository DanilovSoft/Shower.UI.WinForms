using System.Net;
using System.Net.Sockets;

namespace Shower.Domain.RpcClient;

public static class ConnectionFactory
{
    private static readonly IPEndPoint RemoteEP = new(IPAddress.Parse("10.2.2.218"), 333);

    public static async Task<ShowerConnection> ConnectAsync(CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var tcp = new TcpClient();
        try
        {
            await tcp.ConnectAsync(RemoteEP, ct).ConfigureAwait(false);

            var connection = new ShowerConnection(tcp);
            tcp = null;
            return connection;
        }
        finally
        {
            tcp?.Dispose();
        }
    }
}
