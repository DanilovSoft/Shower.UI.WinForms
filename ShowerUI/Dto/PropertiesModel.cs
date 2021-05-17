using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowerUI.Dto
{
    internal sealed class PropertiesModel
    {
        public ushort WaterLevelFullUsec { get; set; }
        public ushort WaterLevelEmptyUsec { get; set; }
        public byte MinimumWaterHeatingLevel { get; set; }
        public bool WatchDogWasReset { get; set; }
        public byte AbsoluteHeatingTimeLimit { get; set; }
        public byte HeatingTimeLimit { get; set; }
        public byte LightBrightness { get; set; }
        public byte WiFiPower { get; set; }
        public byte WaterLevelMeasureInterval { get; set; }
        public byte WaterLevelRingBufferSize { get; set; }
        public byte WaterValveCutOffPercent { get; set; }
        public byte TempSensorInternalTempBufferSize { get; set; }
        public float WaterLevelUsecPerDeg { get; set; }
        public byte ButtonTimeMsec { get; set; }
    }
}
