using System.Text.Json.Serialization;
using Shower.Domain.RpcClient;

namespace Shower.Domain.Models;

/// <summary>
/// Параметры контроллера. Используется для импорта и экспорта в JSON.
/// </summary>
public sealed class PropertiesModel
{
    [JsonPropertyName("WaterLevelFullUsec")]
    public ushort? WaterLevelFullUsec { get; set; }

    [JsonPropertyName("WaterLevelEmptyUsec")]
    public ushort? WaterLevelEmptyUsec { get; set; }

    [JsonPropertyName("MinimumWaterHeatingLevel")]
    public byte? MinimumWaterHeatingLevel { get; set; }

    [JsonPropertyName("AbsoluteHeatingTimeLimit")]
    public byte? AbsoluteHeatingTimeLimit { get; set; }

    [JsonPropertyName("HeatingTimeLimit")]
    public byte? HeatingTimeLimit { get; set; }

    [JsonPropertyName("LightBrightness")]
    public byte? LightBrightness { get; set; }

    /// <summary>
    /// Мощность Wi-Fi от 40 до 82 с шагом в 0.25 dBm.
    /// </summary>
    /// <remarks>40..82</remarks>
    [JsonPropertyName("WiFiPower")]
    public byte? WiFiPower { get; set; }

    [JsonPropertyName("WaterLevelMeasureInterval")]
    public byte? WaterLevelMeasureInterval { get; set; }

    [JsonPropertyName("WaterLevelMedianBufferSize")]
    public byte? WaterLevelMedianBufferSize { get; set; }

    [JsonPropertyName("WaterLevelAverageBufferSize")]
    public byte? WaterLevelAverageBufferSize { get; set; }

    [JsonPropertyName("WaterValveCutOffPercent")]
    public byte? WaterValveCutOffPercent { get; set; }

    /// <summary>
    /// От 1 до 8.
    /// </summary>
    [JsonPropertyName("TempSensorInternalTempAverageSize")]
    public byte? TempSensorInternalTempAverageSize { get; set; }

    /// <summary>
    /// Объём воды полного бака (л).
    /// </summary>
    [JsonPropertyName("WaterTankVolumeLitre")]
    public float? WaterTankVolumeLitre { get; set; }

    /// <summary>
    /// Мощность ТЭНа с учётом КПД, кВт.
    /// </summary>
    [JsonPropertyName("WaterHeaterPowerKWatt")]
    public float? WaterHeaterPowerKWatt { get; set; }

    [JsonPropertyName("ButtonTimeMsec")]
    public byte? ButtonPressTimeMsec { get; set; }

    [JsonPropertyName("ButtonLongTimeMsec")]
    public ushort? ButtonLongTimeMsec { get; set; }

    /// <summary>
    /// Сработал ли сторожевой таймер в микроконтроллере с прошлого запуска.
    /// </summary>
    /// <remarks>Это свойство только для чтения.</remarks>
    [JsonIgnore]
    public bool? WatchDogWasReset { get; set; }

    [JsonPropertyName("WaterLevelErrorThreshhold")]
    public byte? WaterLevelErrorThreshhold { get; set; }

    /// <summary>
    /// Считывает все параметры с контроллера.
    /// </summary>
    public static async Task<PropertiesModel> LoadAsync(ShowerConnection con, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(con);

        return new PropertiesModel
        {
            WaterLevelFullUsec = await con.GetWaterLevelFullAsync(ct).ConfigureAwait(false),
            WaterLevelEmptyUsec = await con.GetWaterLevelEmptyAsync(ct).ConfigureAwait(false),
            MinimumWaterHeatingLevel = await con.GetMinimumWaterHeatingLevelAsync(ct).ConfigureAwait(false),
            AbsoluteHeatingTimeLimit = await con.GetAbsoluteHeatingTimeLimitAsync(ct).ConfigureAwait(false),
            HeatingTimeLimit = await con.GetHeatingTimeLimitAsync(ct).ConfigureAwait(false),
            LightBrightness = await con.GetLightBrightnessAsync(ct).ConfigureAwait(false),
            WiFiPower = await con.GetWiFiPowerAsync(ct).ConfigureAwait(false),
            WaterLevelMeasureInterval = (byte)(await con.GetWaterLevelMeasureIntervalAsync(ct).ConfigureAwait(false)).TotalMilliseconds,
            WaterLevelMedianBufferSize = await con.GetWaterLevelMedianSizeAsync(ct).ConfigureAwait(false),
            WaterLevelAverageBufferSize = await con.GetWaterLevelAverageSizeAsync(ct).ConfigureAwait(false),
            WaterValveCutOffPercent = await con.GetWaterValveCutOffPercentAsync(ct).ConfigureAwait(false),
            TempSensorInternalTempAverageSize = await con.GetTemperatureSensorInternalTempAverageSizeAsync(ct).ConfigureAwait(false),
            ButtonPressTimeMsec = await con.GetButtonPressTimeMsecAsync(ct).ConfigureAwait(false),
            ButtonLongTimeMsec = await con.GetButtonLongPressTimeMsecAsync(ct).ConfigureAwait(false),
            WatchDogWasReset = await con.GetWatchDogWasResetAsync(ct).ConfigureAwait(false),
            WaterTankVolumeLitre = await con.GetWaterTankVolumeLitreAsync(ct).ConfigureAwait(false),
            WaterHeaterPowerKWatt = await con.GetWaterHeaterPowerKWattAsync(ct).ConfigureAwait(false),
            WaterLevelErrorThreshhold = await con.GetWaterLevelErrorThresholdAsync(ct).ConfigureAwait(false),
        };
    }
}
