using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShowerUI
{
    public sealed class WaterLevelSession
    {
        public List<ushort> UsecList { get; } = new(30000); // 20 минут
        public CancellationTokenSource Cts { get; } = new();
        public ushort WaterLevelEmpty { get; set; }
        public ushort WaterLevelFull { get; set; }
        public int LastPointIndex { get; set; }
        public bool Running { get; set; }

        // From 0.0 to 50
        public float GetPoint(ushort usec)
        {
            var usecRange = (ushort)(WaterLevelEmpty - WaterLevelFull);

            // Поправка на выход из диаппазона.
            ClampRawRange(ref usec);

            // Смещение.
            usec -= WaterLevelFull;

            // Уровень воды в микросекундах.
            usec = (ushort)(usecRange - usec);

            double tmp = usec * (100 / 2d);

            // Сколько пунктов из 50.
            double point = tmp / usecRange;

            var pointf = (float)point;

            return pointf;
        }

        public void ClampRawRange(ref ushort rawValue)
        {
            if (rawValue < WaterLevelFull)
            {
                rawValue = WaterLevelFull;
            }
            else if (rawValue > WaterLevelEmpty)
            {
                rawValue = WaterLevelEmpty;
            }
        }

        /// <returns>От 0 до 99.</returns>
        public byte CalcPercent(int usec)
        {
            var invertedUsec = (ushort)(WaterLevelEmpty - (usec - WaterLevelFull));

            var pointf = GetPoint(invertedUsec);

            var point = (byte)pointf;
            //byte point = (byte)Math.Round(pointf);

            int percent = point * 2;

            //percent = (100 - percent);

            if (percent > 99)
            {
                percent = 99;
            }

            return (byte)percent;
        }
    }
}
