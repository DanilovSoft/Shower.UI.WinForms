namespace Shower.Domain.WaterLevel;

/// <summary>
/// Записанные показания датчика уровня воды и калибровка, с которой они были получены.
/// </summary>
public sealed class WaterLevelSession
{
    /// <summary>
    /// Инвертированные показания датчика в микросекундах (см. <see cref="InvertRaw"/>).
    /// </summary>
    public List<ushort> UsecList { get; } = new(30000); // 20 минут

    /// <summary>
    /// Показание датчика (мкс), соответствующее пустому баку.
    /// </summary>
    public ushort WaterLevelEmpty { get; set; }

    /// <summary>
    /// Показание датчика (мкс), соответствующее полному баку.
    /// </summary>
    public ushort WaterLevelFull { get; set; }

    /// <summary>
    /// Переворачивает показание датчика так, чтобы большее значение соответствовало большему уровню воды.
    /// </summary>
    public ushort InvertRaw(int rawUsec)
    {
        return (ushort)(WaterLevelEmpty - (rawUsec - WaterLevelFull));
    }

    /// <returns>От 0.0 до 50.</returns>
    public float GetPoint(ushort usec)
    {
        var usecRange = (ushort)(WaterLevelEmpty - WaterLevelFull);

        // Поправка на выход из диаппазона.
        ClampRawRange(ref usec);

        // Смещение.
        usec -= WaterLevelFull;

        // Уровень воды в микросекундах.
        usec = (ushort)(usecRange - usec);

        double tmp = usec * (100 / 2d);

        // Сколько пунктов из 50.
        double point = tmp / usecRange;

        return (float)point;
    }

    public void ClampRawRange(ref ushort rawValue)
    {
        if (rawValue < WaterLevelFull)
        {
            rawValue = WaterLevelFull;
        }
        else if (rawValue > WaterLevelEmpty)
        {
            rawValue = WaterLevelEmpty;
        }
    }

    /// <param name="usec">Инвертированное значение датчика (см. <see cref="InvertRaw"/>).</param>
    /// <returns>От 0 до 99.</returns>
    public byte CalcPercent(int usec)
    {
        var invertedUsec = InvertRaw(usec);
        var point = (byte)GetPoint(invertedUsec);
        int percent = point * 2;

        if (percent > 99)
        {
            percent = 99;
        }

        return (byte)percent;
    }
}
