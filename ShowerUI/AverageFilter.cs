namespace ShowerUI
{
    internal sealed class AverageFilter
    {
        private readonly int[] _buffer;
        private readonly int _windowSize;
        private int _head;
        private int _sum;
        private int _initCounter;

        public AverageFilter(int windowSize)
        {
            _windowSize = windowSize;
            _buffer = new int[windowSize];
        }

        public bool IsInitialized { get; private set; }

        public int Add(int value)
        {
            _sum += (value - _buffer[_head]);
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
}
