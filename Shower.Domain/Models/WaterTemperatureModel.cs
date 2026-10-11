using System.Diagnostics;

namespace Shower.Domain.Models;

/// <summary>
/// Одно измерение температуры в баке.
/// </summary>
[DebuggerDisplay("{ToString(),nq}")]
public sealed class WaterTemperatureModel
{
    /// <summary>
    /// Момент времени когда были полученны данные.
    /// </summary>
    public required DateTime Timestamp { get; init; }

    /// <summary>
    /// Число секунд от начала записи.
    /// </summary>
    public int Second { get; set; }

    /// <summary>
    /// Температура в баке.
    /// </summary>
    public float WaterTemperature { get; set; }

    public bool HeaterEnabled { get; set; }

    public TimeSpan TimeLeft { get; set; }

    public override string ToString()
    {
        return $"WaterTemp = {WaterTemperature}, HeaterEnabled = {HeaterEnabled}, TimeLeft = {TimeLeft}";
    }
}
