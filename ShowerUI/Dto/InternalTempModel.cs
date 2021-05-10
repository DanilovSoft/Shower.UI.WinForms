using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowerUI.Dto
{
    [Serializable]
    public class InternalTempModel
    {
        public InternalTempModel()
        {

        }

        /// <summary>
        /// Время в секундах.
        /// </summary>
        public int Time { get; set; }
        public float AverageInternalTemp { get; set; }
        public bool HeaterEnabled { get; set; }
        public int TimeLeft { get; set; }

        public void Serialize(Stream stream)
        {
            using (var bw = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true))
            {
                bw.Write(Time);   
                bw.Write(AverageInternalTemp);   
                bw.Write(HeaterEnabled);   
                bw.Write(TimeLeft);   
            }
        }
    }
}
