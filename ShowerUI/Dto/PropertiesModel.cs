using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShowerUI.Dto
{
    internal sealed class PropertiesModel
    {
        public ushort? WaterLevelFullUsec { get; set; }
        public ushort? WaterLevelEmptyUsec { get; set; }
        public byte? MinimumWaterHeatingLevel { get; set; }
        public byte? AbsoluteHeatingTimeLimit { get; set; }
        public byte? HeatingTimeLimit { get; set; }
        public byte? LightBrightness { get; set; }
        /// <summary>
        /// Мощность Wi-Fi от 40 до 82 с шагом в 0.25 dBm.
        /// </summary>
        /// <remarks>40..82</remarks>
        public byte? WiFiPower { get; set; }
        public byte? WaterLevelMeasureInterval { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte? WaterLevelMedianBufferSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte? WaterLevelAverageBufferSize { get; set; }
        public byte? WaterValveCutOffPercent { get; set; }
        /// <summary>
        /// От 1 до 8.
        /// </summary>
        public byte? TempSensorInternalTempAverageSize { get; set; }
        /// <summary>
        /// Объём воды полного бака (л).
        /// </summary>
        public float? WaterTankVolumeLitre { get; set; }
        /// <summary>
        /// Мощность ТЭНа с учётом КПД, кВт.
        /// </summary>
        public float? WaterHeaterPowerKWatt { get; set; }
        public byte? ButtonTimeMsec { get; set; }

        /// <summary>
        /// Сработал ли сторожевой таймер в микроконтроллере с прошлого запуска.
        /// </summary>
        /// <remarks>Это свойство только для чтения.</remarks>
        [JsonIgnore]
        public bool? WatchDogWasReset { get; set; }
    }
}
