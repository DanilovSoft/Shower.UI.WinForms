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

    /// <summary>
    /// Выполняет асинхронный запрос с таймаутом <see cref="RequestTimeout"/>.
    /// </summary>
    /// <exception cref="TimeoutException">Устройство не ответило вовремя.</exception>
    /// <exception cref="OperationCanceledException">Отменено через <paramref name="ct"/>.</exception>
    public async Task<T> RequestAsync<T>(Func<CancellationToken, Task<T>> request, CancellationToken ct)
    {
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(ct);
        timeout.CancelAfter(RequestTimeout);

        try
        {
            return await request(timeout.Token);
        }
        catch (OperationCanceledException ex) when (!ct.IsCancellationRequested)
        {
            throw new TimeoutException("Устройство не ответило за отведённое время.", ex);
        }
    }

    /// <summary>
    /// Выполняет синхронные запросы к контроллеру в фоновом потоке.
    /// </summary>
    public async Task<T> RunAsync<T>(Func<ShowerConnection, T> action, CancellationToken ct)
    {
        using var connection = await ConnectionFactory.ConnectAsync(ct);
        return await Task.Run(() => action(connection), ct);
    }

    /// <inheritdoc cref="RunAsync{T}(Func{ShowerConnection, T}, CancellationToken)"/>
    public Task RunAsync(Action<ShowerConnection> action, CancellationToken ct = default)
    {
        return RunAsync(connection =>
        {
            action(connection);
            return true;
        }, ct);
    }

    public Task RestartAsync()
    {
        return RunAsync(connection => connection.Reset());
    }

    public async Task<ShowerCodes> PingAsync(CancellationToken cancellationToken)
    {
        using var connection = await ConnectionFactory.ConnectAsync(cancellationToken);
        return await RequestAsync(connection.PingAsync, cancellationToken);
    }
}
