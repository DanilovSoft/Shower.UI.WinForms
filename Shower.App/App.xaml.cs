using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using Shower.App.Services;
using Shower.App.ViewModels;

namespace Shower.App;

public partial class App : Application
{
    private ServiceProvider? _services;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _services = ConfigureServices().BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateScopes = true,
            ValidateOnBuild = true,
        });

        DispatcherUnhandledException += OnDispatcherUnhandledException;

        var window = _services.GetRequiredService<MainWindow>();
        var viewModel = _services.GetRequiredService<MainViewModel>();
        window.Closing += (_, _) => viewModel.StopAll();

        MainWindow = window;
        window.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _services?.Dispose();
        base.OnExit(e);
    }

    private static ServiceCollection ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IDialogService, DialogService>();

        // Вкладки живут всё время работы приложения: каждая хранит свою запись и циклы опроса.
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<WaterTemperatureViewModel>();
        services.AddSingleton<WaterLevelViewModel>();
        services.AddSingleton<CalibrationViewModel>();
        services.AddSingleton<ParametersViewModel>();
        services.AddSingleton<DebugViewModel>();

        services.AddSingleton<Device>();
        services.AddSingleton<MainWindow>();

        return services;
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // Не даём приложению упасть из-за ошибки в обработчике команды — показываем её пользователю.
        var dialogs = _services?.GetService<IDialogService>() ?? new DialogService();
        dialogs.ShowError(e.Exception.Message, "Непредвиденная ошибка");
        e.Handled = true;
    }
}
