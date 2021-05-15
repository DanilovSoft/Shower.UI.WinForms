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
        /// Порядковый номер от 0.
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Температура в баке.
        /// </summary>
        public float InternalTemp { get; set; }
        public bool HeaterEnabled { get; set; }
        public TimeSpan TimeLeft { get; set; }

        //public void Serialize(Stream stream)
        //{
        //    using (var bw = new BinaryWriter(stream, Encoding.UTF8, leaveOpen: true))
        //    {
        //        bw.Write(Number);   
        //        bw.Write(InternalTemp);   
        //        bw.Write(HeaterEnabled);   
        //        bw.Write(TimeLeft);   
        //    }
        //}
    }
}
