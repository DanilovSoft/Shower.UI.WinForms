using ScottPlot;
using ScottPlot.Interactivity;

namespace Shower.App.Charts;

/// <summary>
/// Модель графика: владеет <see cref="ScottPlot.Plot"/> и данными серий.
/// Представление подключает её к WpfPlot через <see cref="ChartBinding"/>.
/// </summary>
/// <remarks>Все методы вызываются из UI-потока.</remarks>
public abstract class ChartModel
{
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
