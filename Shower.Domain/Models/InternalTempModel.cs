using System.Diagnostics;

namespace Shower.Domain.Models;

/// <summary>
/// Одно измерение температуры в баке.
/// </summary>
[DebuggerDisplay("{ToString(),nq}")]
public sealed class InternalTempModel
{
    /// <summary>
    /// Число секунд от начала записи.
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
