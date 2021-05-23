﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowerTcpClient
{
    public enum ShowerCodes : byte
    {
        None = 0,
        SetWaterLevelFullUsec = 1,
        SetWaterLevelEmptyUsec = 2,
        GetWaterLevelFullUsec = 3,
        GetWaterLevelEmptyUsec = 4,
        GetTempChart = 5,
        SetTempChart = 6,
        Ping = 7,
        GetWaterLevel = 8,
        GetWaterPercent = 9,
        GetExternalTemp = 10,
        GetInternalTemp = 11,
        GetAverageInternalTemp = 12,
        GetAverageExternalTemp = 13,
        GetMinimumWaterHeatingLevel = 14,
        SetMinimumWaterHeatingLevel = 15,

        GetHeatingTimeLimit = 18,
        SetHeatingTimeLimit = 19,
        /// <summary>
        /// От 0 до 100%.
        /// </summary>
        GetLightBrightness = 20,
        SetLightBrightness = 21,
        /// <summary>
        /// Примерное время до окончания нагрева.
        /// </summary>
        GetMinutesLeft = 22,
        GetHeatingProgress = 23,
        GetWaterHeated = 24,
        GetHeatingTimeoutState = 25,
        GetHeaterEnabled = 26,
        GetAbsoluteHeatingTimeLimit = 27,
        SetAbsoluteHeatingTimeLimit = 28,
        /// <summary>
        /// Мощность WiFi в единицах по 0.25 dBm.
        /// </summary>
        /// <remarks>От 40 до 82 (10..20.5 dBm).</remarks>
        GetWiFiPower = 29,
        /// <summary>
        /// Мощность WiFi в единицах по 0.25 dBm.
        /// </summary>
        /// <remarks>От 40 до 82 (10..20.5 dBm).</remarks>
        SetWiFiPower = 30,
        GetAbsoluteTimeoutStatus = 31,
        GetWatchDogWasReset = 32,
        GetHasMainPower = 33,
        GetHeatingLimit = 34,
        SetCurAP = 35,
        SetDefAP = 36,

        GetWaterLevelMeasureInterval = 37,
        SetWaterLevelMeasureInterval = 38,

        GetWaterValveCutOffPercent = 39,
        SetWaterValveCutOffPercent = 40,

        GetWaterLevelRingBufferSize = 45,
        SetWaterLevelRingBufferSize = 46,

        GetTempSensorInternalTempBufferSize = 47,
        SetTempSensorInternalTempBufferSize = 48,

        GetWaterLevelUsecPerDeg = 49,
        SetWaterLevelUsecPerDeg = 50,

        GetWaterValveTimeoutSec = 51,
        SetWaterValveTimeoutSec = 52,

        GetWaterLevelRaw = 53,

        GetButtonTimeMsec = 54,
        SetButtonTimeMsec = 55,

        OK = 200,
        Reset = 254,
        Save = 255,
    }
}