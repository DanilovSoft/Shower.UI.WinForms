using Shower.Domain.RpcClient;

namespace Shower.App.Services;

/// <summary>
/// Общие операции с контроллером душа.
/// </summary>
public static class Device
{
    /// <summary>
    /// Сколько ждать ответа на один запрос в циклах опроса.
    /// </summary>
    public static readonly TimeSpan RequestTimeout = TimeSpan.FromSeconds(10);

    /// <summary>
    /// Выполняет асинхронный запрос с таймаутом <see cref="RequestTimeout"/>.
    /// </summary>
    /// <exception cref="TimeoutException">Устройство не ответило вовремя.</exception>
    /// <exception cref="OperationCanceledException">Отменено через <paramref name="cancellationToken"/>.</exception>
    public static async Task<T> RequestAsync<T>(Func<CancellationToken, Task<T>> request, CancellationToken cancellationToken)
    {
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeout.CancelAfter(RequestTimeout);

        try
        {
            return await request(timeout.Token);
        }
        catch (OperationCanceledException ex) when (!cancellationToken.IsCancellationRequested)
        {
            throw new TimeoutException("Устройство не ответило за отведённое время.", ex);
        }
    }

    /// <summary>
    /// Выполняет синхронные запросы к контроллеру в фоновом потоке.
    /// </summary>
    public static async Task<T> RunAsync<T>(Func<ShowerConnection, T> action, CancellationToken cancellationToken = default)
    {
        using var connection = await ConnectionFactory.ConnectAsync(cancellationToken);
        return await Task.Run(() => action(connection), cancellationToken);
    }

    /// <inheritdoc cref="RunAsync{T}(Func{ShowerConnection, T}, CancellationToken)"/>
    public static Task RunAsync(Action<ShowerConnection> action, CancellationToken cancellationToken = default)
    {
        return RunAsync(connection =>
        {
            action(connection);
            return true;
        }, cancellationToken);
    }

    public static Task RestartAsync()
    {
        return RunAsync(connection => connection.Reset());
    }

    public static async Task<ShowerCodes> PingAsync(CancellationToken cancellationToken)
    {
        using var connection = await ConnectionFactory.ConnectAsync(cancellationToken);
        return await RequestAsync(connection.PingAsync, cancellationToken);
    }
}
