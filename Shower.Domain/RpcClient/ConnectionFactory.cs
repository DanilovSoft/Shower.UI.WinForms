using System.Net.Sockets;
using Microsoft.Extensions.Options;

namespace Shower.Domain.RpcClient;

public sealed class ConnectionFactory(IOptions<ShowerConnectionOptions> options)
{
    private readonly ShowerConnectionOptions _options = options.Value;

    public async Task<ShowerConnection> ConnectAsync(CancellationToken ct)
    {
        var tcp = new TcpClient
        {
            ReceiveTimeout = 10_000, // 10 секунд
            SendTimeout = 10_000 // 10 секунд
        };

        try
        {

            await tcp.ConnectAsync(_options.Host, _options.Port, ct).ConfigureAwait(false);

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
