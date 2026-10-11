using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ScottPlot.WPF;

namespace Shower.App.Charts;

/// <summary>
/// Подключает <see cref="ChartModel"/> из ViewModel к контролу <see cref="WpfPlot"/>.
/// </summary>
/// <example><c>&lt;scottplot:WpfPlot charts:ChartBinding.Chart="{Binding Chart}" /&gt;</c></example>
public static class ChartBinding
{
    public static readonly DependencyProperty ChartProperty = DependencyProperty.RegisterAttached(
        "Chart", typeof(ChartModel), typeof(ChartBinding), new PropertyMetadata(null, OnChartChanged));

    private static readonly DependencyProperty AttachmentProperty = DependencyProperty.RegisterAttached(
        "Attachment", typeof(Attachment), typeof(ChartBinding));

    public static ChartModel? GetChart(DependencyObject obj) => (ChartModel?)obj.GetValue(ChartProperty);

    public static void SetChart(DependencyObject obj, ChartModel? value) => obj.SetValue(ChartProperty, value);

    private static void OnChartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not WpfPlot control)
        {
            throw new InvalidOperationException($"{nameof(ChartBinding)} применим только к {nameof(WpfPlot)}.");
        }

        (control.GetValue(AttachmentProperty) as Attachment)?.Dispose();
        control.ClearValue(AttachmentProperty);

        if (e.NewValue is ChartModel chart)
        {
            control.SetValue(AttachmentProperty, new Attachment(control, chart));
        }
    }

    private sealed class Attachment : IDisposable
    {
        private static readonly DependencyPropertyDescriptor ForegroundDescriptor =
            DependencyPropertyDescriptor.FromProperty(Control.ForegroundProperty, typeof(WpfPlot));

        private readonly WpfPlot _control;
        private readonly ChartModel _chart;
        private bool _subscribed;

        public Attachment(WpfPlot control, ChartModel chart)
        {
            _control = control;
            _chart = chart;

            control.Reset(chart.Plot);
            chart.ConfigureInput(control.UserInputProcessor);

            control.Loaded += OnLoaded;
            control.Unloaded += OnUnloaded;

            if (control.IsLoaded)
            {
                Subscribe();
            }
        }

        public void Dispose()
        {
            _control.Loaded -= OnLoaded;
            _control.Unloaded -= OnUnloaded;
            Unsubscribe();
        }

        private void OnLoaded(object sender, RoutedEventArgs e) => Subscribe();

        private void OnUnloaded(object sender, RoutedEventArgs e) => Unsubscribe();

        private void Subscribe()
        {
            if (_subscribed)
            {
                return;
            }
            _subscribed = true;

            _chart.RefreshRequested += OnRefreshRequested;
            ForegroundDescriptor.AddValueChanged(_control, OnForegroundChanged);
            _control.PreviewMouseWheel += OnUserInput;
            _control.PreviewMouseDown += OnUserInput;
            _control.MouseMove += OnMouseMove;
            _control.MouseLeave += OnMouseLeave;

            ApplyTheme();
            _control.Refresh();
        }

        private void Unsubscribe()
        {
            if (!_subscribed)
            {
                return;
            }
            _subscribed = false;

            _chart.RefreshRequested -= OnRefreshRequested;
            ForegroundDescriptor.RemoveValueChanged(_control, OnForegroundChanged);
            _control.PreviewMouseWheel -= OnUserInput;
            _control.PreviewMouseDown -= OnUserInput;
            _control.MouseMove -= OnMouseMove;
            _control.MouseLeave -= OnMouseLeave;
        }

        private void OnRefreshRequested(object? sender, EventArgs e) => _control.Refresh();

        private void OnUserInput(object sender, MouseEventArgs e) => _chart.OnUserInteraction();

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            // ScottPlot работает в физических пикселях, WPF — в независимых от DPI единицах.
            var position = e.GetPosition(_control);
            var scale = _control.DisplayScale;
            if (_chart.UpdateHover(new ScottPlot.Pixel(position.X * scale, position.Y * scale)))
            {
                _control.Refresh();
            }
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (_chart.HideHover())
            {
                _control.Refresh();
            }
        }

        private void OnForegroundChanged(object? sender, EventArgs e)
        {
            ApplyTheme();
            _control.Refresh();
        }

        private void ApplyTheme()
        {
            // Цвет текста наследуется от окна и меняется вместе с темой Windows.
            var color = (_control.Foreground as SolidColorBrush)?.Color ?? Colors.Black;
            _chart.ApplyTheme(ChartTheme.FromForeground(color.R, color.G, color.B));
        }
    }
}
