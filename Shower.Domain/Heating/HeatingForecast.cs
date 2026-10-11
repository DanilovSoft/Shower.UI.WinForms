using Shower.Domain.Models;

namespace Shower.Domain.Heating;

/// <summary>
/// Прогноз окончания нагрева по записанной кривой температуры.
/// </summary>
/// <param name="StartTime">Момент записи, от которого строится прогноз.</param>
/// <param name="StartTemp">Температура в баке в точке начала прогноза.</param>
/// <param name="EndTime">Момент, когда ожидается достижение целевой температуры.</param>
/// <param name="EndTemp">Целевая температура.</param>
/// <param name="TimeLeft">Оставшееся время нагрева от точки начала прогноза.</param>
public readonly record struct HeatingForecast(DateTime StartTime, float StartTemp, DateTime EndTime, float EndTemp, TimeSpan TimeLeft)
{
    /// <param name="records">Записи температуры, упорядоченные по времени.</param>
    /// <param name="startMinute">Через сколько минут после включения нагрева взять точку для прогноза.</param>
    /// <param name="waterLevel">Уровень воды в баке, от 0 до 1.</param>
    /// <param name="calculator">Калькулятор времени нагрева.</param>
    /// <param name="limitTemp">Целевая температура.</param>
    /// <returns><see langword="null"/> если в записи нет подходящей точки.</returns>
    public static HeatingForecast? Calculate(
        IReadOnlyList<InternalTempModel> records,
        int startMinute,
        float waterLevel,
        HeatingTimeLeft calculator,
        float limitTemp)
    {
        ArgumentNullException.ThrowIfNull(records);
        ArgumentNullException.ThrowIfNull(calculator);

        if (startMinute <= 0)
        {
            return null;
        }

        if (records.FirstOrDefault(x => x.HeaterEnabled) is not { } heaterOnPoint)
        {
            return null;
        }

        var time = heaterOnPoint.Timestamp.AddMinutes(startMinute);
        if (records.FirstOrDefault(x => x.HeaterEnabled && x.Timestamp >= time) is not { } point)
        {
            return null;
        }

        var timeLeft = calculator.CalcTimeLeft(point.InternalTemp, limitTemp, waterLevel);
        return new HeatingForecast(point.Timestamp, point.InternalTemp, point.Timestamp + timeLeft, limitTemp, timeLeft);
    }
}
