namespace Filters;

public sealed class MedianFilter
{
    private readonly ushort[] _window;
    private readonly ushort[] _windowCopy;
    private readonly int _halfSize; // половина размера буффера
    private readonly int _windowSize;
    private int _tail = 0;
    private int _initCount;

    public MedianFilter(int windowSize)
    {
        if (windowSize % 2 != 1)
        {
            throw new ArgumentOutOfRangeException(nameof(windowSize), "Параметр должен быть не чётным");
        }

        _windowSize = windowSize;
        _window = new ushort[windowSize];
        _windowCopy = new ushort[windowSize];
        _halfSize = windowSize / 2;
    }

    public bool IsInitialized { get; private set; }

    public ushort Add(ushort value)
    {
        _window[_tail++ % _windowSize] = value;

        if (!IsInitialized)
        {
            _initCount++;
            if (_initCount > _halfSize)
            {
                IsInitialized = true;
            }
        }

        _window.AsSpan().CopyTo(_windowCopy);
        _windowCopy.AsSpan().Sort();
        return _windowCopy[_halfSize];
    }
}
