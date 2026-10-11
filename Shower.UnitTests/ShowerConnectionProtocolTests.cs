using System.Net;
using System.Net.Sockets;
using Shower.Domain.RpcClient;
using Xunit;

namespace Shower.UnitTests;

/// <summary>
/// Обмен с контроллером через loopback-сокет: тест играет роль контроллера.
/// </summary>
public sealed class ShowerConnectionProtocolTests : IAsyncLifetime
{
    private TcpListener _listener;
    private TcpClient _client;
    private Socket _device;
    private ShowerConnection _connection;

    public async Task InitializeAsync()
    {
        _listener = new TcpListener(IPAddress.Loopback, 0);
        _listener.Start();

        _client = new TcpClient();
        var accept = _listener.AcceptSocketAsync();
        await _client.ConnectAsync(IPAddress.Loopback, ((IPEndPoint)_listener.LocalEndpoint).Port);
        _device = await accept;
        _connection = new ShowerConnection(_client);
    }

    public Task DisposeAsync()
    {
        _connection.Dispose();
        _client.Dispose();
        _device.Dispose();
        _listener.Stop();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task RequestAsync_SendsCodeAndParsesLittleEndianResponse()
    {
        var request = _connection.GetWaterLevelEmptyAsync(CancellationToken.None);

        Assert.Equal(new byte[] { 2, (byte)ShowerCode.GetWaterLevelEmptyUsec }, await ReceiveAsync(2));
        await SendAsync(0x47, 0x07);

        Assert.Equal((ushort)0x0747, await request);
    }

    [Fact]
    public async Task RequestAsync_ParsesFloat()
    {
        var request = _connection.GetWaterHeaterPowerKWattAsync(CancellationToken.None);

        await ReceiveAsync(2);
        await SendAsync(BitConverter.GetBytes(1.25f));

        Assert.Equal(1.25f, await request);
    }

    [Fact]
    public async Task SetAsync_SendsArgumentAndAcceptsOk()
    {
        var request = _connection.SetWaterLevelFullUsecAsync(0x1234, CancellationToken.None);

        Assert.Equal(new byte[] { 4, (byte)ShowerCode.SetWaterLevelFullUsec, 0x34, 0x12 }, await ReceiveAsync(4));
        await SendAsync((byte)ShowerCode.OK);

        await request;
    }

    [Fact]
    public async Task SetAsync_ThrowsWhenResponseIsNotOk()
    {
        var request = _connection.SaveAsync(CancellationToken.None);

        await ReceiveAsync(2);
        await SendAsync(13);

        await Assert.ThrowsAsync<InvalidDataException>(() => request);
    }

    [Fact]
    public async Task SequentialRequests_ShareConnection()
    {
        var setRequest = _connection.SetWaterTankVolumeLitreAsync(37.5f, CancellationToken.None);
        Assert.Equal(new byte[] { 6, (byte)ShowerCode.SetWaterTankVolumeLitre, 0x00, 0x00, 0x16, 0x42 }, await ReceiveAsync(6));
        await SendAsync((byte)ShowerCode.OK);
        await setRequest;

        var pingRequest = _connection.PingAsync(CancellationToken.None);
        Assert.Equal(new byte[] { 2, (byte)ShowerCode.Ping }, await ReceiveAsync(2));
        await SendAsync((byte)ShowerCode.Ping);

        Assert.Equal(ShowerCode.Ping, await pingRequest);
    }

    [Fact]
    public async Task Cancellation_AbortsReadAndClosesConnection()
    {
        using var cts = new CancellationTokenSource();
        var request = _connection.GetWaterLevelRawAsync(cts.Token);

        await ReceiveAsync(2);
        cts.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => request);
        await Assert.ThrowsAsync<ObjectDisposedException>(() => _connection.PingAsync(CancellationToken.None));
    }

    [Fact]
    public async Task SetCurAPAsync_SendsAsciiCommand()
    {
        var request = _connection.SetCurAPAsync("abc", "0123456789", null, CancellationToken.None);

        var expectedPayload = "\"abc\",\"0123456789\""u8.ToArray();
        var received = await ReceiveAsync(2 + expectedPayload.Length);
        await SendAsync((byte)ShowerCode.OK);
        await request;

        Assert.Equal(received.Length, received[0]);
        Assert.Equal((byte)ShowerCode.SetCurAP, received[1]);
        Assert.Equal(expectedPayload, received[2..]);
    }

    private async Task<byte[]> ReceiveAsync(int count)
    {
        var buffer = new byte[count];
        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        for (var read = 0; read < count;)
        {
            read += await _device.ReceiveAsync(buffer.AsMemory(read), SocketFlags.None, timeout.Token);
        }
        return buffer;
    }

    private async Task SendAsync(params byte[] data)
    {
        await _device.SendAsync(data, SocketFlags.None);
    }
}
