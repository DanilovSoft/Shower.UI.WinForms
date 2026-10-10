namespace Shower.App.Services;

/// <summary>
/// Окна сообщений и выбора файлов, чтобы ViewModel не зависели от WPF.
/// </summary>
public interface IDialogService
{
    void ShowError(string message, string title = "Ошибка");

    void ShowInfo(string message, string title);

    /// <returns>Путь к выбранному файлу или <see langword="null"/>, если пользователь отказался.</returns>
    string? OpenFile(string filter, string? fileName = null);

    /// <returns>Путь к файлу или <see langword="null"/>, если пользователь отказался.</returns>
    string? SaveFile(string filter, string defaultExt, string? fileName = null);
}
