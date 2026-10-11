using System.Globalization;
using ScottPlot;

namespace Shower.App.Charts;

/// <summary>
/// Сырые показания датчика уровня за последние 30 секунд.
/// </summary>
public sealed class CalibrationChart : ChartModel
{
    private const int DataIntervalSec = 30;
    private const int MaxPoints = DataIntervalSec * 1000 / 100;

    private readonly List<double> _seconds = new(MaxPoints + 1);
    private readonly List<double> _usecs = new(MaxPoints + 1);

    public CalibrationChart()
    {
        var series = Plot.Add.SignalXY(_seconds, _usecs, new Color(240, 0, 0));
        series.LineWidth = 2;
        AddHoverSeries(series, _seconds, _usecs, new Color(240, 0, 0),
            static (second, usec) => string.Create(CultureInfo.InvariantCulture, $"{second:0.0} с\n{usec:0} мкс"));

        HideAxis(Plot.Axes.Bottom);
        SetUsecRange(0, 10);
    }

    /// <summary>
    /// Задаёт диапазон оси Y по калибровке датчика.
    /// </summary>
    public void SetCalibration(ushort waterLevelEmpty)
    {
        const double fullUsec = 116;
        double emptyUsec = waterLevelEmpty * 1.10; // +10%

        SetUsecRange(fullUsec, emptyUsec);
        Clear();
    }

    /// <param name="second">Время измерения в секундах.</param>
    /// <param name="usec">Показание датчика, -1 — датчик не ответил (разрыв линии).</param>
    public void Add(double second, short usec)
    {
        _seconds.Add(second);
        _usecs.Add(usec != -1 ? usec : double.NaN);

        if (_seconds.Count > MaxPoints)
        {
            _seconds.RemoveAt(0);
            _usecs.RemoveAt(0);
        }

        UpdateLimitsX();
        RequestRefresh();
    }

    public void Clear()
    {
        _seconds.Clear();
        _usecs.Clear();
        UpdateLimitsX();
        RequestRefresh();
    }

    private void SetUsecRange(double min, double max)
    {
        Plot.Axes.SetLimitsY(min, max);
    }

    private void UpdateLimitsX()
    {
        double last = _seconds.Count > 0 ? _seconds[^1] : 0;
        double first = _seconds.Count > 0 ? _seconds[0] : 0;
        Plot.Axes.SetLimitsX(Math.Min(first, last - DataIntervalSec), last);
    }
}
