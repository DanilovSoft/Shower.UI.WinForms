namespace Shower.Domain.Filters;

//public sealed class MedianFilter
//{
//    private readonly ushort[] _window;
//    private readonly ushort[] _windowCopy;
//    private readonly int _halfSize; // половина размера буффера
//    private readonly int _windowSize;
//    private int _tail = 0;
//    private int _initCount;

//    public MedianFilter(int windowSize)
//    {
//        if (windowSize % 2 != 1)
//        {
//            throw new ArgumentOutOfRangeException(nameof(windowSize), "Параметр должен быть не чётным");
//        }

//        _windowSize = windowSize;
//        _window = new ushort[windowSize];
//        _windowCopy = new ushort[windowSize];
//        _halfSize = windowSize / 2;
//    }

//    public bool IsInitialized { get; private set; }

//    public ushort Add(ushort value)
//    {
//        _window[_tail++ % _windowSize] = value;

//        if (!IsInitialized)
//        {
//            _initCount++;
//            if (_initCount > _halfSize)
//            {
//                IsInitialized = true;
//            }
//        }

//        _window.AsSpan().CopyTo(_windowCopy);
//        _windowCopy.AsSpan().Sort();
//        return _windowCopy[_halfSize];
//    }
//}

public struct MedianFilter<T> where T : struct, IComparable<T>
{
    private readonly T[] _window;
    private readonly T[] _sortedWindow;
    private int _initCount;
    private int _tail;

    public MedianFilter(int windowSize)
    {
        if (windowSize % 2 != 1)
        {
            throw new ArgumentOutOfRangeException(nameof(windowSize), "Window size should be odd");
        }

        _window = new T[windowSize];
        _sortedWindow = new T[windowSize];
    }

    public readonly int MinInitSize => _window.Length / 2; // значения будут правильными уже при заполненности буфера на половину.
    public readonly int InitSize => _window.Length - 1;
    public bool IsInitialized { get; private set; }

    public T Add(T value)
    {
        _window[_tail] = value;
        _tail = (_tail + 1) % _window.Length;

        if (!IsInitialized)
        {
            _initCount++;
            if (_initCount > InitSize)
            {
                IsInitialized = true;
            }
        }

        _window.AsSpan().CopyTo(_sortedWindow);
        _sortedWindow.AsSpan().Sort();
        return _sortedWindow[_sortedWindow.Length / 2];
    }
}
