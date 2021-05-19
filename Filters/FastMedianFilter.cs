using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters
{
    public class FastMedianFilter
    {
        private readonly ushort[] _buffer;
        private readonly int _halfSize; // половина размера буффера
        private readonly int _windowSize;
        private int _tail = 0;
        private int _initCount;

        public FastMedianFilter(int size)
        {
            if (size % 2 != 1)
            {
                throw new ArgumentOutOfRangeException($"Параметр {nameof(size)} должен быть не чётным");
            }

            _windowSize = size;
            _buffer = new ushort[size];
            _halfSize = (size / 2) + 1;
        }

        public int WindowSize => _windowSize;
        /// <summary>
        /// Фильтр считается инициализированным когда его буффер был полностью заполнен.
        /// </summary>
        public bool IsInitialized { get; private set; }

        public ushort Add(ushort value)
        {
            _buffer[_tail] = value;
            _tail = (_tail + 1) % _windowSize;

            if (!IsInitialized)
            {
                _initCount++;
                if (_initCount >= _windowSize)
                {
                    IsInitialized = true;
                }
            }

            return Sort(_buffer, skip: _halfSize);
        }

        private static ushort Sort(ushort[] array, int skip)
        {
            int[] list = array.Select(x => (int)x).ToArray();

            return (ushort)GetMedian(list);

            // первым этапом найдем абсолютное минимальное значение в массиве.
            bool firstTime = true;

            // любое значение на первом этапе.
            ushort successor = array[0];
            ushort minimum = array[0];

            while (skip-- > 0)
            {
                bool hasMinimum = false;
                ushort firstMinimum = 0;

                for (int j = 0; j < array.Length; j++) // из-за 2 этапа искать всегда с начала массива.
                {
                    ushort v = array[j];

                    if (!firstTime)
                    {
                        if (v > successor)
                        {
                            if (hasMinimum)
                            {
                                if (v <= firstMinimum)
                                {
                                    minimum = v;
                                    firstMinimum = v;
                                }
                                else
                                {
                                    minimum = firstMinimum;
                                }
                            }
                            else
                            {
                                firstMinimum = v;
                                hasMinimum = true;
                            }
                        }
                    }
                    else
                    {
                        if (v < minimum)
                            minimum = v;
                    }
                }

                // абсолютный минимум найден.
                firstTime = false;
                successor = minimum;
            }
            return successor;
        }

        private static int GetMedian(int[] data)
        {
            var res = data[0];
            double avg = data.Average();

            for (var index = 1; index < data.Length; index++)
            {
                var k0 = Math.Abs(res - avg);
                var k1 = Math.Abs(data[index] - avg);

                if (k0 > k1)
                {
                    res = data[index];
                }
            }

            return res;
        }
    }
}
