using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
    public class AverageFilter
    {
        private readonly ushort[] _buffer;
        private readonly int _windowSize;
        public bool IsInitialized;
        private int _head;
        private int _sum;
        private int _initC;

        public AverageFilter(int windowSize)
        {
            _windowSize = windowSize;
            _buffer = new ushort[windowSize];
        }

        public ushort AddNextValue(ushort value)
        {
            _sum += (value - _buffer[_head]);
            _buffer[_head] = value;
            _head = (_head + 1) % _windowSize;


            if(!IsInitialized)
            {
                _initC++;
                if(_initC >= _windowSize)
                {
                    IsInitialized = true;
                }
            }

            return (ushort)(_sum / _windowSize);
        }
    }
}
