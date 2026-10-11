using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Numerics;
using CommunityToolkit.Mvvm.ComponentModel;
using Shower.Domain;
using Shower.Domain.Models;
using Shower.Domain.RpcClient;

namespace Shower.App.ViewModels;

/// <summary>
/// Один параметр контроллера: подпись, подсказка, редактируемый текст и признак изменения.
/// </summary>
public abstract partial class ParameterViewModel : ObservableObject, INotifyDataErrorInfo
{
    private string _originalText = "";
    private string? _error;

    [ObservableProperty]
    private string _text = "";

    [ObservableProperty]
    private bool _hasChanges;

    protected ParameterViewModel(string caption, string? hint)
    {
        Caption = caption;
        Hint = string.IsNullOrEmpty(hint) ? null : hint;
    }

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public string Caption { get; }

    public string? Hint { get; }

    public bool HasErrors => _error != null;

    public IEnumerable GetErrors(string? propertyName)
    {
        return (propertyName is null or nameof(Text)) && _error != null ? new[] { _error } : Array.Empty<string>();
    }

    /// <summary>
    /// Значение, считанное с контроллера. Сбрасывает признак изменения.
    /// </summary>
    public abstract void SetOriginal(PropertiesModel? model);

    /// <summary>
    /// Значение из импортированного файла. Помечается как изменённое, если отличается от считанного.
    /// </summary>
    public abstract void SetPending(PropertiesModel model);

    /// <summary>
    /// Записывает текущее значение в модель для экспорта.
    /// </summary>
    public abstract void Export(PropertiesModel model);

    /// <summary>
    /// Записывает текущее значение в контроллер.
    /// </summary>
    public abstract Task WriteAsync(ShowerConnection connection, CancellationToken ct);

    /// <summary>
    /// Текущее значение записано в контроллер.
    /// </summary>
    public void AcceptChanges()
    {
        _originalText = Text;
        HasChanges = false;
    }

    protected void SetOriginalText(string text)
    {
        _originalText = text;
        Text = text;
        HasChanges = false;
        Validate(text);
    }

    /// <returns>Текст ошибки или <see langword="null"/>, если значение корректно.</returns>
    protected abstract string? ValidateText(string text);

    partial void OnTextChanged(string value)
    {
        HasChanges = value != _originalText;
        Validate(value);
    }

    private void Validate(string text)
    {
        // Пустое значение допустимо только пока параметр не загружен.
        var error = text.Length == 0 && _originalText.Length == 0 ? null : ValidateText(text);
        if (error == _error)
        {
            return;
        }

        _error = error;
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Text)));
        OnPropertyChanged(nameof(HasErrors));
    }
}

/// <summary>
/// Числовой параметр контроллера.
/// </summary>
/// <typeparam name="T">Тип значения в протоколе контроллера.</typeparam>
public class ParameterViewModel<T> : ParameterViewModel where T : struct, INumber<T>
{
    private readonly Func<PropertiesModel, T?> _get;
    private readonly Action<PropertiesModel, T?> _set;
    private readonly Func<ShowerConnection, T, CancellationToken, Task> _write;

    public ParameterViewModel(
        string caption,
        string? hint,
        Func<PropertiesModel, T?> get,
        Action<PropertiesModel, T?> set,
        Func<ShowerConnection, T, CancellationToken, Task> write)
        : base(caption, hint)
    {
        _get = get;
        _set = set;
        _write = write;
    }

    /// <summary>
    /// Текущее значение или <see langword="null"/>, если текст пустой или некорректный.
    /// </summary>
    public T? Value => TryParse(Text, out var value, out _) ? value : null;

    public override void SetOriginal(PropertiesModel? model)
    {
        SetOriginalText(model is null ? "" : FormatNullable(_get(model)));
    }

    public override void SetPending(PropertiesModel model)
    {
        if (_get(model) is { } value)
        {
            Text = Format(value);
        }
    }

    public override void Export(PropertiesModel model)
    {
        _set(model, Value);
    }

    public override Task WriteAsync(ShowerConnection connection, CancellationToken ct)
    {
        if (!TryParse(Text, out var value, out var error))
        {
            throw new FormatException($"{Caption}: {error}");
        }
        return _write(connection, value, ct);
    }

    protected virtual string Format(T value)
    {
        return value.ToString(null, CultureInfo.InvariantCulture);
    }

    protected virtual bool TryParse(string text, out T value, out string? error)
    {
        var isFloat = typeof(T) == typeof(float) || typeof(T) == typeof(double);
        var styles = isFloat ? NumberStyles.Float : NumberStyles.Integer;

        if (T.TryParse(text.Trim().Replace(',', '.'), styles, CultureInfo.InvariantCulture, out value))
        {
            error = null;
            return true;
        }

        error = isFloat
            ? "Введите число, например 2.5"
            : $"Введите целое число от 0 до {MaxValue}";
        return false;
    }

    protected override string? ValidateText(string text)
    {
        return TryParse(text, out _, out var error) ? null : error;
    }

    private static string MaxValue => typeof(T) == typeof(byte) ? byte.MaxValue.ToString() : ushort.MaxValue.ToString();

    private string FormatNullable(T? value) => value is { } v ? Format(v) : "";
}

/// <summary>
/// Мощность Wi-Fi: в контроллере хранится в шагах по 0.25 dBm (40..82), редактируется в dBm.
/// </summary>
public sealed class WiFiPowerParameterViewModel : ParameterViewModel<byte>
{
    public WiFiPowerParameterViewModel(string caption, string? hint)
        : base(caption, hint, m => m.WiFiPower, (m, v) => m.WiFiPower = v, (c, v, ct) => c.SetWiFiPowerAsync(v, ct))
    {
    }

    protected override string Format(byte value)
    {
        // Значение вне диапазона всё равно показываем — валидация подсветит ошибку.
        return value is >= WiFiPower.MinValue and <= WiFiPower.MaxValue
            ? WiFiPower.FormatDbm(value)
            : (value * 0.25).ToString(CultureInfo.InvariantCulture);
    }

    protected override bool TryParse(string text, out byte value, out string? error)
    {
        return WiFiPower.TryParseDbm(text.Trim().Replace(',', '.'), out value, out error);
    }
}
