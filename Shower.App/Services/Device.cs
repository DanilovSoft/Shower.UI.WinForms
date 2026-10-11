using Shower.Domain.RpcClient;

namespace Shower.App.Services;

/// <summary>
/// Общие операции с контроллером душа.
/// </summary>
/// <remarks>Singleton</remarks>
public sealed class Device
{
    /// <summary>
    /// Сколько ждать ответа на один запрос в циклах опроса.
    /// </summary>
    public static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(10);

    private readonly ConnectionFactory _connectionFactory;

    public Device(ConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    /// <summary>
    /// Открывает новое соединение с контроллером.
    /// </summary>
    public Task<ShowerConnection> ConnectAsync(CancellationToken ct)
    {
        return _connectionFactory.ConnectAsync(ct);
    }

    /// <summary>
    /// Выполняет асинхронный запрос с таймаутом <see cref="RequestTimeout"/>.
    /// </summary>
    /// <exception cref="TimeoutException">Устройство не ответило вовремя.</exception>
    /// <exception cref="OperationCanceledException">Отменено через <paramref name="ct"/>.</exception>
    public async Task<T> RequestAsync<T>(Func<CancellationToken, Task<T>> request, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(request);

        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(ct);
        timeout.CancelAfter(RequestTimeout);

        try
        {
            return await request(timeout.Token).ConfigureAwait(false);
        }
        catch (OperationCanceledException ex) when (!ct.IsCancellationRequested)
        {
            throw new TimeoutException("Устройство не ответило за отведённое время.", ex);
        }
    }

    /// <summary>
    /// Выполняет синхронные запросы к контроллеру в фоновом потоке.
    /// </summary>
    public async Task<T> RunAsync<T>(Func<ShowerConnection, CancellationToken, T> action, CancellationToken ct)
    {
        using var connection = await ConnectAsync(ct).ConfigureAwait(false);
        return await Task.Run(() => action(connection, ct), ct).ConfigureAwait(false);
    }

    /// <inheritdoc cref="RunAsync{T}(Func{ShowerConnection, T}, CancellationToken)"/>
    public Task RunAsync(Action<ShowerConnection> action, CancellationToken ct)
    {
        return RunAsync((connection, ct) =>
        {
            action(connection);
            return true;
        }, ct);
    }

    public Task RestartAsync(CancellationToken ct)
    {
        return RunAsync((connection) => connection.Reset(), ct);
    }

    public async Task<ShowerCode> PingAsync(CancellationToken cancellationToken)
    {
        using var connection = await ConnectAsync(cancellationToken).ConfigureAwait(false);
        return await RequestAsync(connection.PingAsync, cancellationToken).ConfigureAwait(false);
    }
}
