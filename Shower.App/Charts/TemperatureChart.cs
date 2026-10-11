using System.Globalization;
using ScottPlot;
using ScottPlot.Plottables;
using Shower.Domain.Heating;
using Shower.Domain.Models;

namespace Shower.App.Charts;

/// <summary>
/// Температура в баке: красным — при включённом нагреве, синим — при выключенном, чёрным — прогноз.
/// Периоды без связи с контроллером остаются пропуском в линии и закрашиваются серым.
/// </summary>
/// <remarks>По оси X — время измерения в формате OADate (дни), как требует ось времени ScottPlot.</remarks>
public sealed class TemperatureChart : ChartModel
{
    private const double MinTemp = 15;
    private const double MaxTemp = 42;
    private static readonly double MinVisibleDays = TimeSpan.FromSeconds(60).TotalDays;
    private static readonly Color GapColor = Colors.Gray.WithAlpha(0.25);

    private readonly List<double> _times = new(3600);
    private readonly List<double> _heaterOnTemps = new(3600);
    private readonly List<double> _heaterOffTemps = new(3600);
    private readonly List<double> _forecastTimes = new(2);
    private readonly List<double> _forecastTemps = new(2);
    private readonly Scatter _forecast;
    private readonly List<HorizontalSpan> _gaps = [];
    private WaterTemperatureModel? _last;

    public TemperatureChart()
    {
        var heaterOn = Plot.Add.SignalXY(_times, _heaterOnTemps, Colors.Red);
        heaterOn.LineWidth = 2;
        heaterOn.LegendText = "Нагрев включён";
        AddHoverSeries(heaterOn, _times, _heaterOnTemps, Colors.Red, FormatTemperature);

        var heaterOff = Plot.Add.SignalXY(_times, _heaterOffTemps, Colors.DodgerBlue);
        heaterOff.LineWidth = 2;
        heaterOff.LegendText = "Нагрев выключен";
        AddHoverSeries(heaterOff, _times, _heaterOffTemps, Colors.DodgerBlue, FormatTemperature);

        _forecast = Plot.Add.ScatterLine(_forecastTimes, _forecastTemps, Colors.Black);
        _forecast.LineWidth = 2;
        _forecast.LegendText = "Прогноз";
        AddHoverSeries(_forecast, _forecastTimes, _forecastTemps, Colors.Gray, (time, temp) => "Прогноз\n" + FormatTemperature(time, temp));

        var timeAxis = Plot.Axes.DateTimeTicksBottom();
        ((ScottPlot.TickGenerators.DateTimeAutomatic)timeAxis.TickGenerator).LabelFormatter =
            static time => time.ToString("HH:mm", CultureInfo.InvariantCulture);
        UpdateLimits();
    }

    private static string FormatTemperature(double time, double temp)
    {
        return string.Create(CultureInfo.InvariantCulture, $"{DateTime.FromOADate(time):HH:mm:ss}\n{temp:0.00} °C");
    }

    public override void ApplyTheme(ChartTheme theme)
    {
        base.ApplyTheme(theme);
        // Чёрная линия прогноза не видна на тёмном фоне.
        _forecast.LineColor = theme.Foreground;
    }

    public void Add(WaterTemperatureModel model)
    {
        AddPoint(model);
        UpdateLimits();
        RequestRefresh();
    }

    public void AddRange(IEnumerable<WaterTemperatureModel> models)
    {
        foreach (var model in models)
        {
            AddPoint(model);
        }
        UpdateLimits();
        RequestRefresh();
    }

    public void SetForecast(HeatingForecast? forecast)
    {
        _forecastTimes.Clear();
        _forecastTemps.Clear();

        if (forecast is { } f)
        {
            _forecastTimes.Add(f.StartTime.ToOADate());
            _forecastTemps.Add(f.StartTemp);
            _forecastTimes.Add(f.EndTime.ToOADate());
            _forecastTemps.Add(f.EndTemp);
        }

        UpdateLimits();
        RequestRefresh();
    }

    public void Clear()
    {
        _times.Clear();
        _heaterOnTemps.Clear();
        _heaterOffTemps.Clear();
        _forecastTimes.Clear();
        _forecastTemps.Clear();
        _last = null;

        foreach (var gap in _gaps)
        {
            Plot.Remove(gap);
        }
        _gaps.Clear();

        UpdateLimits();
        RequestRefresh();
    }

    private void AddPoint(WaterTemperatureModel model)
    {
        double time = model.Timestamp.ToOADate();

        if (_last is { } last && TemperatureRecords.IsGap(last, model))
        {
            AddGap(last.Timestamp.ToOADate(), time);
            _last = null;
        }

        // Точку переключения нагрева добавляем в обе серии, чтобы линия не прерывалась.
        double temp = model.WaterTemperature;
        bool toHeaterOn = model.HeaterEnabled || _last?.HeaterEnabled == true;
        bool toHeaterOff = !model.HeaterEnabled || _last?.HeaterEnabled == false;

        _times.Add(time);
        _heaterOnTemps.Add(toHeaterOn ? temp : double.NaN);
        _heaterOffTemps.Add(toHeaterOff ? temp : double.NaN);
        _last = model;
    }

    /// <summary>
    /// Разрывает линию между двумя измерениями и закрашивает промежуток.
    /// </summary>
    private void AddGap(double start, double end)
    {
        _times.Add((start + end) / 2);
        _heaterOnTemps.Add(double.NaN);
        _heaterOffTemps.Add(double.NaN);

        var gap = Plot.Add.HorizontalSpan(start, end, GapColor);
        gap.LineWidth = 0;
        if (_gaps.Count == 0)
        {
            gap.LegendText = "Нет связи";
        }
        _gaps.Add(gap);
    }

    private void UpdateLimits()
    {
        double minTime = _times.Count > 0 ? _times[0] : DateTime.Now.ToOADate();
        double maxTime = minTime + MinVisibleDays;
        if (_times.Count > 0)
        {
            maxTime = Math.Max(maxTime, _times[^1]);
        }
        if (_forecastTimes.Count > 0)
        {
            maxTime = Math.Max(maxTime, _forecastTimes[^1]);
        }

        Plot.Axes.SetLimits(minTime, maxTime, MinTemp, MaxTemp);
    }
}
