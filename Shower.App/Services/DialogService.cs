using System.Windows;
using Microsoft.Win32;

namespace Shower.App.Services;

public sealed class DialogService : IDialogService
{
    public void ShowError(string message, string title = "Ошибка")
    {
        Show(message, title, MessageBoxImage.Error);
    }

    public void ShowInfo(string message, string title)
    {
        Show(message, title, MessageBoxImage.Information);
    }

    public string? OpenFile(string filter, string? fileName = null)
    {
        var dialog = new OpenFileDialog
        {
            Filter = filter,
            Multiselect = false,
            FileName = fileName ?? "",
        };
        return dialog.ShowDialog(Owner) == true ? dialog.FileName : null;
    }

    public string? SaveFile(string filter, string defaultExt, string? fileName = null)
    {
        var dialog = new SaveFileDialog
        {
            Filter = filter,
            DefaultExt = defaultExt,
            AddExtension = true,
            FileName = fileName ?? "",
        };
        return dialog.ShowDialog(Owner) == true ? dialog.FileName : null;
    }

    private static Window? Owner => Application.Current?.MainWindow;

    private static void Show(string message, string title, MessageBoxImage image)
    {
        if (Owner is { } owner)
        {
            MessageBox.Show(owner, message, title, MessageBoxButton.OK, image);
        }
        else
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, image);
        }
    }
}
