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

        // Все параметры запрашиваются одним пакетом — один раундтрип вместо 18.
        var batch = con.CreateBatch();
        var watchDogWasReset = batch.Get<byte>(ShowerCode.GetWatchDogWasReset);
        var waterLevelFull = batch.Get<ushort>(ShowerCode.GetWaterLevelFullUsec);
        var waterLevelEmpty = batch.Get<ushort>(ShowerCode.GetWaterLevelEmptyUsec);
        var minimumWaterHeatingLevel = batch.Get<byte>(ShowerCode.GetMinimumWaterHeatingLevel);
        var absoluteHeatingTimeLimit = batch.Get<byte>(ShowerCode.GetAbsoluteHeatingTimeLimit);
        var heatingTimeLimit = batch.Get<byte>(ShowerCode.GetHeatingTimeLimit);
        var lightBrightness = batch.Get<byte>(ShowerCode.GetLightBrightness);
        var wiFiPower = batch.Get<byte>(ShowerCode.GetWiFiPower);
        var waterLevelMeasureInterval = batch.Get<byte>(ShowerCode.GetWaterLevelMeasureInterval);
        var waterLevelMedianBufferSize = batch.Get<byte>(ShowerCode.GetWaterLevelMedianBufferSize);
        var waterLevelAverageBufferSize = batch.Get<byte>(ShowerCode.GetWaterLevelAverageBufferSize);
        var waterValveCutOffPercent = batch.Get<byte>(ShowerCode.GetWaterValveCutOffPercent);
        var tempSensorInternalTempAverageSize = batch.Get<byte>(ShowerCode.GetWaterTemperatureSensorAverageSize);
        var buttonPressTimeMsec = batch.Get<byte>(ShowerCode.GetButtonTimeMsec);
        var buttonLongTimeMsec = batch.Get<ushort>(ShowerCode.GetButtonLongPressTimeMsec);
        var waterTankVolumeLitre = batch.Get<float>(ShowerCode.GetWaterTankVolumeLitre);
        var waterHeaterPowerKWatt = batch.Get<float>(ShowerCode.GetWaterHeaterPowerKWatt);
        var waterLevelErrorThreshold = batch.Get<byte>(ShowerCode.GetWaterLevelErrorThreshold);

        await batch.ExecuteAsync(ct).ConfigureAwait(false);

        return new PropertiesModel
        {
            WaterLevelFullUsec = waterLevelFull.Value,
            WaterLevelEmptyUsec = waterLevelEmpty.Value,
            MinimumWaterHeatingLevel = minimumWaterHeatingLevel.Value,
            AbsoluteHeatingTimeLimit = absoluteHeatingTimeLimit.Value,
            HeatingTimeLimit = heatingTimeLimit.Value,
            LightBrightness = lightBrightness.Value,
            WiFiPower = wiFiPower.Value,
            WaterLevelMeasureInterval = waterLevelMeasureInterval.Value,
            WaterLevelMedianBufferSize = waterLevelMedianBufferSize.Value,
            WaterLevelAverageBufferSize = waterLevelAverageBufferSize.Value,
            WaterValveCutOffPercent = waterValveCutOffPercent.Value,
            TempSensorInternalTempAverageSize = tempSensorInternalTempAverageSize.Value,
            ButtonPressTimeMsec = buttonPressTimeMsec.Value,
            ButtonLongTimeMsec = buttonLongTimeMsec.Value,
            WatchDogWasReset = watchDogWasReset.Value != 0,
            WaterTankVolumeLitre = waterTankVolumeLitre.Value,
            WaterHeaterPowerKWatt = waterHeaterPowerKWatt.Value,
            WaterLevelErrorThreshhold = waterLevelErrorThreshold.Value,
        };
    }
}
