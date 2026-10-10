using ScottPlot;
using ScottPlot.AxisRules;
using ScottPlot.Interactivity;
using ScottPlot.Plottables;
using ScottPlot.TickGenerators;
using Shower.Domain.WaterLevel;

namespace Shower.App.Charts;

/// <summary>
/// Показания датчика уровня воды: RAW, медиана, скользящее среднее (левая скрытая ось, мкс)
/// и уровень в процентах (правая ось). По X — номер измерения, масштабируется колесом мыши.
/// </summary>
public sealed class WaterLevelChart : ChartModel
{
    private const double MinVisiblePoints = 100;

    private readonly Series _raw;
    private readonly Series _median;
    private readonly Series _average;
    private readonly Series _percent;
    private double _minUsec;
    private double _maxUsec = 9.5;

    public WaterLevelChart()
    {
        _raw = new Series(Plot, "Usec", Colors.DodgerBlue);
        _median = new Series(Plot, "Median", Colors.Violet);
        _percent = new Series(Plot, "%", Colors.Red);
        _average = new Series(Plot, "Avg", Colors.MediumSeaGreen);

        _percent.Signal.Axes.YAxis = Plot.Axes.Right;

        // Шкала микросекунд не отображается, сетка и подписи — по процентам.
        HideAxis(Plot.Axes.Left);
        HideAxis(Plot.Axes.Bottom);
        Plot.Axes.Right.TickGenerator = new NumericFixedInterval(10);
        Plot.Grid.YAxis = Plot.Axes.Right;

        Plot.Axes.Rules.Add(new LockedVertical(Plot.Axes.Right, 0, 99));
        ApplyUsecRange();

        Plot.ShowLegend(Alignment.UpperLeft);
        FitToData();
    }

    /// <summary>
    /// Ось X следует за новыми данными. Сбрасывается, когда пользователь масштабирует график.
    /// </summary>
    public bool FollowData { get; private set; } = true;

    public override void ConfigureInput(UserInputProcessor input)
    {
        input.DoubleLeftClickBenchmark(false);
    }

    public override void OnUserInteraction()
    {
        FollowData = false;
    }

    public void SetUsecRange(ushort fullUsec, ushort emptyUsec)
    {
        _minUsec = fullUsec;
        _maxUsec = emptyUsec;
        ApplyUsecRange();
        RequestRefresh();
    }

    public void SetVisibility(bool raw, bool median, bool average, bool percent)
    {
        _raw.Signal.IsVisible = raw;
        _median.Signal.IsVisible = median;
        _average.Signal.IsVisible = average;
        _percent.Signal.IsVisible = percent;
        RequestRefresh();
    }

    /// <summary>
    /// Добавляет очередное измерение во все серии.
    /// </summary>
    public void Add(int index, ushort usec, WaterLevelSample sample)
    {
        AddPoint(index, usec, sample);
        if (FollowData)
        {
            FitToData();
        }
        RequestRefresh();
    }

    /// <summary>
    /// Полностью перестраивает все серии.
    /// </summary>
    public void SetAll(IReadOnlyList<int> indices, IReadOnlyList<ushort> usecs, IReadOnlyList<WaterLevelSample> samples)
    {
        _raw.Clear();
        ReplaceFiltered(indices, samples);

        for (var i = 0; i < indices.Count; i++)
        {
            _raw.Add(indices[i], usecs[i]);
        }

        FitToData();
        RequestRefresh();
    }

    /// <summary>
    /// Перестраивает только отфильтрованные серии (медиана, среднее, проценты).
    /// </summary>
    public void SetFiltered(IReadOnlyList<int> indices, IReadOnlyList<WaterLevelSample> samples)
    {
        ReplaceFiltered(indices, samples);
        RequestRefresh();
    }

    public void Clear()
    {
        _raw.Clear();
        _median.Clear();
        _average.Clear();
        _percent.Clear();
        FollowData = true;
        FitToData();
        RequestRefresh();
    }

    /// <summary>
    /// Показывает все данные и включает слежение за новыми.
    /// </summary>
    public void FitToData()
    {
        FollowData = true;

        double first = _raw.Xs.Count > 0 ? _raw.Xs[0] : 0;
        double last = _raw.Xs.Count > 0 ? _raw.Xs[^1] : 0;
        Plot.Axes.SetLimitsX(first, Math.Max(last, first + MinVisiblePoints));
        RequestRefresh();
    }

    private void ReplaceFiltered(IReadOnlyList<int> indices, IReadOnlyList<WaterLevelSample> samples)
    {
        _median.Clear();
        _average.Clear();
        _percent.Clear();

        for (var i = 0; i < samples.Count; i++)
        {
            AddFiltered(indices[i], samples[i]);
        }
    }

    private void AddPoint(int index, ushort usec, WaterLevelSample sample)
    {
        _raw.Add(index, usec);
        AddFiltered(index, sample);
    }

    private void AddFiltered(int index, WaterLevelSample sample)
    {
        if (sample.Median is { } median)
        {
            _median.Add(index, median);
        }
        if (sample.Average is { } average)
        {
            _average.Add(index, average);
        }
        if (sample.Percent is { } percent)
        {
            _percent.Add(index, percent);
        }
    }

    private void ApplyUsecRange()
    {
        Plot.Axes.Rules.RemoveAll(rule => rule is LockedVertical locked && locked.YAxis == Plot.Axes.Left);
        Plot.Axes.Rules.Add(new LockedVertical(Plot.Axes.Left, _minUsec, _maxUsec));
        Plot.Axes.SetLimitsY(_minUsec, _maxUsec, Plot.Axes.Left);
        Plot.Axes.SetLimitsY(0, 99, Plot.Axes.Right);
    }

    private sealed class Series
    {
        public Series(Plot plot, string name, Color color)
        {
            Signal = plot.Add.SignalXY(Xs, Ys, color);
            Signal.LegendText = name;
        }

        public List<double> Xs { get; } = new(30000);
        public List<double> Ys { get; } = new(30000);
        public SignalXY Signal { get; }

        public void Add(double x, double y)
        {
            Xs.Add(x);
            Ys.Add(y);
        }

        public void Clear()
        {
            Xs.Clear();
            Ys.Clear();
        }
    }
}
