using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
    public class MedianFilter
    {
        private readonly ushort[] _buffer;
        private readonly ushort[] _bufferCopy;
        private readonly int _halfSize; // половина размера буффера
        private int _tail = 0;
        private int _initCount;
        public int WindowSize { get; }
        public bool IsInitialized { get; private set; }

        public MedianFilter(int windowSize)
        {
            if (windowSize % 2 != 1)
                throw new ArgumentOutOfRangeException($"Параметр {nameof(windowSize)} должен быть не чётным");

            WindowSize = windowSize;
            _buffer = new ushort[windowSize];
            _bufferCopy = new ushort[windowSize];
            _halfSize = (windowSize / 2) + 1;
        }

        public ushort Add(ushort value)
        {
            _buffer[_tail] = value;
            _tail = (_tail + 1) % WindowSize;

            if (!IsInitialized)
            {
                _initCount++;
                if (_initCount >= WindowSize)
                    IsInitialized = true;
            }

            Array.Copy(_buffer, _bufferCopy, _buffer.Length);
            Array.Sort(_bufferCopy);
            return _bufferCopy[_halfSize - 1];
        }
    }
}
