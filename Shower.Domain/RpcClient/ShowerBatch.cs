using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Shower.Domain.RpcClient;

/// <summary>
/// Несколько запросов к контроллеру, которые отправляются одним пакетом: один раундтрип вместо N.
/// </summary>
/// <remarks>
/// Пакет можно выполнять повторно — значения обновляются при каждом <see cref="ExecuteAsync"/>.
/// </remarks>
/// <example>
/// <code>
/// var batch = connection.CreateBatch();
/// var temperature = batch.Get&lt;float&gt;(ShowerCode.GetWaterTemperature);
/// var heater = batch.Get&lt;byte&gt;(ShowerCode.GetHeaterEnabled);
/// await batch.ExecuteAsync(ct);
/// Console.WriteLine($"{temperature.Value} {heater.Value}");
/// </code>
/// </example>
public sealed class ShowerBatch
{
    private readonly ShowerConnection _connection;
    private readonly List<BatchItem> _items = [];

    internal ShowerBatch(ShowerConnection connection)
    {
        _connection = connection;
    }

    /// <summary>
    /// Добавляет в пакет запрос значения фиксированного размера.
    /// </summary>
    /// <typeparam name="T">Тип ответа в протоколе: byte, short, ushort, float. Для логических значений используйте byte.</typeparam>
    /// <returns>Ячейка, в которой появится значение после <see cref="ExecuteAsync"/>.</returns>
    public BatchValue<T> Get<T>(ShowerCode code) where T : unmanaged
    {
        var value = new BatchValue<T>(code);
        _items.Add(value);
        return value;
    }

    /// <summary>
    /// Отправляет все запросы пакета и заполняет их значения.
    /// </summary>
    /// <exception cref="InvalidOperationException">Запросы не помещаются в один пакет.</exception>
    public Task ExecuteAsync(CancellationToken ct)
    {
        return _connection.ExecuteBatchAsync(_items, ct);
    }
}

/// <summary>
/// Запрос в составе <see cref="ShowerBatch"/>.
/// </summary>
public abstract class BatchItem
{
    private protected BatchItem(ShowerCode code)
    {
        Code = code;
    }

    internal ShowerCode Code { get; }

    internal abstract int ResponseSize { get; }

    internal abstract void Read(ReadOnlySpan<byte> response);
}

/// <summary>
/// Значение, полученное в составе <see cref="ShowerBatch"/>.
/// </summary>
public sealed class BatchValue<T> : BatchItem where T : unmanaged
{
    private T _value;
    private bool _hasValue;

    internal BatchValue(ShowerCode code) : base(code)
    {
    }

    /// <exception cref="InvalidOperationException">Пакет ещё не выполнен.</exception>
    public T Value => _hasValue
        ? _value
        : throw new InvalidOperationException("Пакет ещё не выполнен.");

    internal override int ResponseSize => Unsafe.SizeOf<T>();

    internal override void Read(ReadOnlySpan<byte> response)
    {
        _value = MemoryMarshal.Read<T>(response);
        _hasValue = true;
    }
}
