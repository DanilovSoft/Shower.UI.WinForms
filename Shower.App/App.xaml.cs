using System.Windows;
using System.Windows.Threading;
using Shower.App.Services;
using Shower.App.ViewModels;

namespace Shower.App;

public partial class App : Application
{
    private readonly DialogService _dialogs = new();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        DispatcherUnhandledException += OnDispatcherUnhandledException;

        var viewModel = new MainViewModel(_dialogs);
        var window = new MainWindow { DataContext = viewModel };
        window.Closing += (_, _) => viewModel.StopAll();

        MainWindow = window;
        window.Show();
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // Не даём приложению упасть из-за ошибки в обработчике команды — показываем её пользователю.
        _dialogs.ShowError(e.Exception.Message, "Непредвиденная ошибка");
        e.Handled = true;
    }
}
