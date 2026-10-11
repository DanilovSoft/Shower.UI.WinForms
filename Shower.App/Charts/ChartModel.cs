using ScottPlot;
using ScottPlot.Interactivity;
using ScottPlot.Plottables;

namespace Shower.App.Charts;

/// <summary>
/// Модель графика: владеет <see cref="ScottPlot.Plot"/> и данными серий.
/// Представление подключает её к WpfPlot через <see cref="ChartBinding"/>.
/// </summary>
/// <remarks>Все методы вызываются из UI-потока.</remarks>
public abstract class ChartModel
{
    /// <summary>
    /// На каком расстоянии от точки данных (в пикселях) показывается её значение.
    /// </summary>
    private const float HoverRadius = 12;

    private readonly List<HoverSeries> _hoverSeries = [];
    private Marker? _hoverMarker;
    private Text? _hoverLabel;
    private ChartTheme? _theme;

    public Plot Plot { get; } = new();

    /// <summary>
    /// Данные изменились и график нужно перерисовать.
    /// </summary>
    public event EventHandler? RefreshRequested;

    /// <summary>
    /// Настраивает реакцию графика на мышь. По умолчанию взаимодействие отключено.
    /// </summary>
    public virtual void ConfigureInput(UserInputProcessor input)
    {
        input.Disable();
    }

    /// <summary>
    /// Пользователь начал масштабировать или прокручивать график.
    /// </summary>
    public virtual void OnUserInteraction()
    {
    }

    /// <summary>
    /// Подстраивает цвета осей, сетки и легенды под светлую или тёмную тему.
    /// </summary>
    public virtual void ApplyTheme(ChartTheme theme)
    {
        Plot.FigureBackground.Color = Colors.Transparent;
        Plot.DataBackground.Color = Colors.Transparent;
        Plot.Axes.Color(theme.Foreground);
        Plot.Grid.MajorLineColor = theme.Grid;
        Plot.Legend.BackgroundColor = theme.LegendBackground;
        Plot.Legend.OutlineColor = theme.Grid;
        Plot.Legend.FontColor = theme.Foreground;

        _theme = theme;
        if (_hoverLabel is not null)
        {
            ApplyHoverTheme(_hoverLabel, theme);
        }
        if (_hoverMarker is not null)
        {
            _hoverMarker.MarkerLineColor = theme.Foreground;
        }
    }

    /// <summary>
    /// Показывает значение ближайшей к курсору точки данных или скрывает подсказку, если рядом точек нет.
    /// </summary>
    /// <param name="mouse">Положение курсора в пикселях графика.</param>
    /// <returns><see langword="true"/>, если подсказка изменилась и график нужно перерисовать.</returns>
    public bool UpdateHover(Pixel mouse)
    {
        if (FindNearest(mouse) is not { } nearest)
        {
            return HideHover();
        }

        var (series, x, y) = nearest;
        var marker = _hoverMarker ??= CreateHoverMarker();
        var label = _hoverLabel ??= CreateHoverLabel();
        var text = series.Format(x, y);

        if (marker.IsVisible && marker.Location == new Coordinates(x, y) && label.LabelText == text)
        {
            return false;
        }

        marker.Axes.XAxis = label.Axes.XAxis = series.Plottable.Axes.XAxis;
        marker.Axes.YAxis = label.Axes.YAxis = series.Plottable.Axes.YAxis;
        marker.Location = label.Location = new Coordinates(x, y);
        marker.MarkerFillColor = series.Color;
        label.LabelText = text;

        // Подпись не должна уходить за правый и верхний край графика.
        var pixel = Plot.GetPixel(new Coordinates(x, y), marker.Axes.XAxis, marker.Axes.YAxis);
        var dataRect = Plot.LastRender.DataRect;
        bool left = pixel.X > dataRect.HorizontalCenter;
        bool below = pixel.Y < dataRect.Top + 40;
        label.LabelAlignment = (left, below) switch
        {
            (false, false) => Alignment.LowerLeft,
            (true, false) => Alignment.LowerRight,
            (false, true) => Alignment.UpperLeft,
            (true, true) => Alignment.UpperRight,
        };
        label.OffsetX = left ? -8 : 8;
        label.OffsetY = below ? 8 : -8;

        marker.IsVisible = label.IsVisible = true;
        Plot.MoveToTop(marker);
        Plot.MoveToTop(label);
        return true;
    }

    /// <summary>
    /// Скрывает подсказку со значением.
    /// </summary>
    /// <returns><see langword="true"/>, если подсказка была видна и график нужно перерисовать.</returns>
    public bool HideHover()
    {
        if (_hoverMarker is not { IsVisible: true } marker)
        {
            return false;
        }

        marker.IsVisible = false;
        _hoverLabel!.IsVisible = false;
        return true;
    }

    /// <summary>
    /// Регистрирует серию, значения которой показываются при наведении курсора.
    /// </summary>
    /// <param name="plottable">Серия на графике: от неё берутся оси и видимость.</param>
    /// <param name="xs">Значения X, по возрастанию.</param>
    /// <param name="ys">Значения Y; <see cref="double.NaN"/> — разрыв линии.</param>
    /// <param name="color">Цвет маркера точки.</param>
    /// <param name="format">Текст подсказки по координатам точки.</param>
    protected void AddHoverSeries(IPlottable plottable, IReadOnlyList<double> xs, IReadOnlyList<double> ys, Color color, Func<double, double, string> format)
    {
        _hoverSeries.Add(new HoverSeries(plottable, xs, ys, color, format));
    }

    private (HoverSeries Series, double X, double Y)? FindNearest(Pixel mouse)
    {
        (HoverSeries Series, double X, double Y)? nearest = null;
        double nearestDistance = HoverRadius;

        foreach (var series in _hoverSeries)
        {
            if (!series.Plottable.IsVisible || series.Xs.Count == 0)
            {
                continue;
            }

            var xAxis = series.Plottable.Axes.XAxis;
            var yAxis = series.Plottable.Axes.YAxis;
            var index = LowerBound(series.Xs, Plot.GetCoordinates(mouse, xAxis, yAxis).X);

            // Ближайшая по X точка — слева или справа от курсора.
            for (var i = Math.Max(index - 1, 0); i <= Math.Min(index, series.Xs.Count - 1); i++)
            {
                double x = series.Xs[i];
                double y = series.Ys[i];
                if (double.IsNaN(y))
                {
                    continue;
                }

                var pixel = Plot.GetPixel(new Coordinates(x, y), xAxis, yAxis);
                double distance = Math.Sqrt(Math.Pow(pixel.X - mouse.X, 2) + Math.Pow(pixel.Y - mouse.Y, 2));
                if (distance <= nearestDistance)
                {
                    nearestDistance = distance;
                    nearest = (series, x, y);
                }
            }
        }

        return nearest;
    }

    /// <summary>
    /// Индекс первого элемента, не меньшего <paramref name="value"/>.
    /// </summary>
    private static int LowerBound(IReadOnlyList<double> sorted, double value)
    {
        int low = 0;
        int high = sorted.Count;
        while (low < high)
        {
            int middle = (low + high) / 2;
            if (sorted[middle] < value)
            {
                low = middle + 1;
            }
            else
            {
                high = middle;
            }
        }
        return low;
    }

    private Marker CreateHoverMarker()
    {
        var marker = Plot.Add.Marker(0, 0, MarkerShape.FilledCircle, 11);
        marker.MarkerLineWidth = 2;
        marker.IsVisible = false;
        if (_theme is { } theme)
        {
            marker.MarkerLineColor = theme.Foreground;
        }
        return marker;
    }

    private Text CreateHoverLabel()
    {
        var label = Plot.Add.Text("", 0, 0);
        label.LabelFontSize = 14;
        label.LabelPadding = 4;
        label.LabelBorderWidth = 1;
        label.LabelBorderRadius = 3;
        label.IsVisible = false;
        if (_theme is { } theme)
        {
            ApplyHoverTheme(label, theme);
        }
        return label;
    }

    private static void ApplyHoverTheme(Text label, ChartTheme theme)
    {
        label.LabelFontColor = theme.Foreground;
        label.LabelBackgroundColor = theme.LegendBackground;
        label.LabelBorderColor = theme.Grid;
    }

    protected void RequestRefresh()
    {
        RefreshRequested?.Invoke(this, EventArgs.Empty);
    }

    protected static void HideAxis(IAxis axis)
    {
        axis.TickLabelStyle.IsVisible = false;
        axis.MajorTickStyle.Length = 0;
        axis.MinorTickStyle.Length = 0;
    }
}

/// <summary>
/// Серия, значения которой показываются при наведении курсора.
/// </summary>
internal sealed record HoverSeries(IPlottable Plottable, IReadOnlyList<double> Xs, IReadOnlyList<double> Ys, Color Color, Func<double, double, string> Format);

/// <param name="Foreground">Цвет текста и осей.</param>
/// <param name="Grid">Цвет линий сетки.</param>
/// <param name="LegendBackground">Фон легенды.</param>
public readonly record struct ChartTheme(Color Foreground, Color Grid, Color LegendBackground)
{
    public static ChartTheme FromForeground(byte r, byte g, byte b)
    {
        bool isDark = (0.2126 * r + 0.7152 * g + 0.0722 * b) / 255 > 0.5;
        var foreground = new Color(r, g, b);
        var grid = new Color(r, g, b, 40);
        var legendBackground = isDark ? new Color(32, 32, 32, 220) : new Color(255, 255, 255, 220);
        return new ChartTheme(foreground, grid, legendBackground);
    }
}
