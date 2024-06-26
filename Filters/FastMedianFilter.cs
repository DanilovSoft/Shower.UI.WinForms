﻿using System;
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
        public int WindowSize => _windowSize;
        public bool IsInitialized { get; private set; }

        public FastMedianFilter(int windowSize)
        {
            if (windowSize % 2 != 1)
                throw new ArgumentOutOfRangeException($"Параметр {nameof(windowSize)} должен быть не чётным");

            _windowSize = windowSize;
            _buffer = new ushort[windowSize];
            _halfSize = (windowSize / 2) + 1;
        }

        public ushort Add(ushort value)
        {
            _buffer[_tail] = value;
            _tail = (_tail + 1) % _windowSize;

            if (!IsInitialized)
            {
                _initCount++;
                if (_initCount >= _windowSize)
                    IsInitialized = true;
            }

            return Sort(_buffer, skip: _halfSize);
        }

        private static ushort Sort(ushort[] array, int skip)
        {
            var list = array.Select(x => (int)x).ToList();

            return (ushort)GetMedian(list);

            // первым этапом найдем абсолютное минимальное значение в массиве.
            bool firstTime = true;

            // любое значение на первом этапе.
            ushort successor = array[0];
            ushort minimum = array[0];

            //List<int> data = new List<int>();
            //data.Average();

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

        private static int GetMedian(List<int> data)
        {
            var res = data[0];
            double avg = data.Average();

            for (var index = 1; index < data.Count; index++)
            {
                var k0 = Math.Abs(res - avg);
                var k1 = Math.Abs(data[index] - avg);

                if (k0 > k1)
                    res = data[index];
            }

            return res;
        }
    }
}
