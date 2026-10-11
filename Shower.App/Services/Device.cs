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
    public Task<ShowerConnection> CreateConnection(CancellationToken ct) => _connectionFactory.CreateConnection(ct);

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
    /// Открывает соединение и выполняет на нём серию запросов. Вся серия ограничена таймаутом <see cref="RequestTimeout"/>.
    /// </summary>
    /// <exception cref="TimeoutException">Устройство не ответило вовремя.</exception>
    public async Task<T> RunAsync<T>(Func<ShowerConnection, CancellationToken, Task<T>> action, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(action);

        await using var connection = await CreateConnection(ct).ConfigureAwait(false);
        return await RequestAsync(t => action(connection, t), ct).ConfigureAwait(false);
    }

    /// <inheritdoc cref="RunAsync{T}(Func{ShowerConnection, CancellationToken, Task{T}}, CancellationToken)"/>
    public Task RunAsync(Func<ShowerConnection, CancellationToken, Task> action, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(action);

        return RunAsync(async (connection, t) =>
        {
            await action(connection, t).ConfigureAwait(false);
            return true;
        }, ct);
    }

    public Task RestartAsync(CancellationToken ct)
    {
        return RunAsync((connection, t) => connection.ResetAsync(t), ct);
    }

    public async Task<ShowerCode> PingAsync(CancellationToken cancellationToken)
    {
        using var connection = await CreateConnection(cancellationToken).ConfigureAwait(false);
        return await RequestAsync(connection.PingAsync, cancellationToken).ConfigureAwait(false);
    }
}
