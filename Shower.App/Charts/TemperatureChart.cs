using ScottPlot;
using ScottPlot.Plottables;
using Shower.Domain.Heating;
using Shower.Domain.Models;

namespace Shower.App.Charts;

/// <summary>
/// Температура в баке: красным — при включённом нагреве, синим — при выключенном, чёрным — прогноз.
/// </summary>
public sealed class TemperatureChart : ChartModel
{
    private const double MinTemp = 15;
    private const double MaxTemp = 42;
    private const double MinVisibleSeconds = 60;

    private readonly List<double> _seconds = new(3600);
    private readonly List<double> _heaterOnTemps = new(3600);
    private readonly List<double> _heaterOffTemps = new(3600);
    private readonly List<double> _forecastSeconds = new(2);
    private readonly List<double> _forecastTemps = new(2);
    private readonly Scatter _forecast;
    private bool? _lastHeaterEnabled;

    public TemperatureChart()
    {
        var heaterOn = Plot.Add.SignalXY(_seconds, _heaterOnTemps, Colors.Red);
        heaterOn.LineWidth = 2;
        heaterOn.LegendText = "Нагрев включён";

        var heaterOff = Plot.Add.SignalXY(_seconds, _heaterOffTemps, Colors.DodgerBlue);
        heaterOff.LineWidth = 2;
        heaterOff.LegendText = "Нагрев выключен";

        _forecast = Plot.Add.ScatterLine(_forecastSeconds, _forecastTemps, Colors.Black);
        _forecast.LineWidth = 2;
        _forecast.LegendText = "Прогноз";

        HideAxis(Plot.Axes.Bottom);
        UpdateLimits();
    }

    public override void ApplyTheme(ChartTheme theme)
    {
        base.ApplyTheme(theme);
        // Чёрная линия прогноза не видна на тёмном фоне.
        _forecast.LineColor = theme.Foreground;
    }

    public void Add(InternalTempModel model)
    {
        AddPoint(model);
        UpdateLimits();
        RequestRefresh();
    }

    public void AddRange(IEnumerable<InternalTempModel> models)
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
        _forecastSeconds.Clear();
        _forecastTemps.Clear();

        if (forecast is { } f)
        {
            _forecastSeconds.Add(f.StartSecond);
            _forecastTemps.Add(f.StartTemp);
            _forecastSeconds.Add(f.EndSecond);
            _forecastTemps.Add(f.EndTemp);
        }

        UpdateLimits();
        RequestRefresh();
    }

    public void Clear()
    {
        _seconds.Clear();
        _heaterOnTemps.Clear();
        _heaterOffTemps.Clear();
        _forecastSeconds.Clear();
        _forecastTemps.Clear();
        _lastHeaterEnabled = null;

        UpdateLimits();
        RequestRefresh();
    }

    private void AddPoint(InternalTempModel model)
    {
        double temp = model.InternalTemp;

        // Точку переключения нагрева добавляем в обе серии, чтобы линия не прерывалась.
        bool toHeaterOn = model.HeaterEnabled || _lastHeaterEnabled == true;
        bool toHeaterOff = !model.HeaterEnabled || _lastHeaterEnabled == false;

        _seconds.Add(model.Second);
        _heaterOnTemps.Add(toHeaterOn ? temp : double.NaN);
        _heaterOffTemps.Add(toHeaterOff ? temp : double.NaN);
        _lastHeaterEnabled = model.HeaterEnabled;
    }

    private void UpdateLimits()
    {
        double maxSecond = MinVisibleSeconds;
        if (_seconds.Count > 0)
        {
            maxSecond = Math.Max(maxSecond, _seconds[^1]);
        }
        if (_forecastSeconds.Count > 0)
        {
            maxSecond = Math.Max(maxSecond, _forecastSeconds[^1]);
        }

        double minSecond = _seconds.Count > 0 ? _seconds[0] : 0;
        Plot.Axes.SetLimits(minSecond, maxSecond, MinTemp, MaxTemp);
    }
}
