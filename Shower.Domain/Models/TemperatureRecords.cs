namespace Shower.Domain.Models;

/// <summary>
/// Операции над записью температуры в баке.
/// </summary>
public static class TemperatureRecords
{
    /// <summary>
    /// Интервал между соседними измерениями, начиная с которого считается, что связь с контроллером терялась.
    /// Опрос идёт раз в секунду, а переподключение занимает несколько секунд.
    /// </summary>
    public static readonly TimeSpan GapThreshold = TimeSpan.FromSeconds(5);

    /// <summary>
    /// Между измерениями был разрыв связи.
    /// </summary>
    public static bool IsGap(InternalTempModel previous, InternalTempModel current)
    {
        ArgumentNullException.ThrowIfNull(previous);
        ArgumentNullException.ThrowIfNull(current);

        return current.Timestamp - previous.Timestamp >= GapThreshold;
    }

    /// <summary>
    /// Восстанавливает <see cref="InternalTempModel.Timestamp"/> для записей из старых файлов, где было только
    /// <see cref="InternalTempModel.Second"/>: последнее измерение считается сделанным в момент <paramref name="end"/>.
    /// </summary>
    /// <returns>Исходный список, если у всех записей уже есть время.</returns>
    public static List<InternalTempModel> RestoreTimestamps(List<InternalTempModel> records, DateTime end)
    {
        ArgumentNullException.ThrowIfNull(records);

        if (records.Count == 0 || records.TrueForAll(r => r.Timestamp != default))
        {
            return records;
        }

        var lastSecond = records[^1].Second;
        return records.ConvertAll(r => new InternalTempModel
        {
            Timestamp = r.Timestamp != default ? r.Timestamp : end.AddSeconds(r.Second - lastSecond),
            Second = r.Second,
            InternalTemp = r.InternalTemp,
            HeaterEnabled = r.HeaterEnabled,
            TimeLeft = r.TimeLeft,
        });
    }
}
