using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowerUI.Dto
{
    [DebuggerDisplay("{ToString(),nq}")]
    public sealed class InternalTempModel
    {
        public InternalTempModel()
        {

        }

        /// <summary>
        /// Число секунд от 0.
        /// </summary>
        public int Second { get; set; }

        /// <summary>
        /// Температура в баке.
        /// </summary>
        public float InternalTemp { get; set; }
        
        public bool HeaterEnabled { get; set; }
        
        public TimeSpan TimeLeft { get; set; }

        public override string ToString()
        {
            return $"Second = {Second}, InternalTemp = {InternalTemp}, HeaterEnabled = {HeaterEnabled}, TimeLeft = {TimeLeft}";
        }
    }
}
