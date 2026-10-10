namespace Shower.Domain.Filters;

/// <summary>
/// Фильтр «скользящее среднее».
/// </summary>
public sealed class AverageFilter
{
    private readonly int[] _buffer;
    private readonly int _windowSize;
    private int _head;
    private int _sum;
    private int _initCounter;

    public AverageFilter(int windowSize)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(windowSize);

        _windowSize = windowSize;
        _buffer = new int[windowSize];
    }

    /// <summary>
    /// Буфер заполнен и возвращаемые значения корректны.
    /// </summary>
    public bool IsInitialized { get; private set; }

    /// <returns>Среднее значение по окну, округлённое до целого.</returns>
    public int Add(int value)
    {
        _sum += value - _buffer[_head];
        _buffer[_head] = value;
        _head = (_head + 1) % _windowSize;

        if (!IsInitialized)
        {
            _initCounter++;
            if (_initCounter == _windowSize)
            {
                IsInitialized = true;
            }
        }

        return (int)Math.Round(_sum / (double)_windowSize);
    }
}
