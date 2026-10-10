using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Shower.Domain;

/// <summary>
/// Преобразование мощности Wi-Fi между значением контроллера (шаги по 0.25 dBm) и dBm.
/// </summary>
public static class WiFiPower
{
    public const byte MinValue = 40;
    public const byte MaxValue = 82;
    private const double Step = 0.25;

    public static double ToDbm(byte rawValue)
    {
        if (rawValue < MinValue || rawValue > MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(rawValue), $"Значение должно быть в диапазоне от {MinValue} до {MaxValue}");
        }
        return rawValue * Step;
    }

    public static string FormatDbm(byte rawValue)
    {
        return ToDbm(rawValue).ToString(CultureInfo.InvariantCulture);
    }

    /// <param name="text">Мощность в dBm, разделитель — точка.</param>
    /// <param name="rawValue">Значение для контроллера, от 40 до 82.</param>
    /// <param name="error">Описание ошибки.</param>
    public static bool TryParseDbm(string? text, out byte rawValue, [NotNullWhen(false)] out string? error)
    {
        rawValue = default;

        if (!double.TryParse(text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double dbm))
        {
            error = "Введите число, например 20.5";
            return false;
        }

        if (dbm % Step != 0)
        {
            error = "Число должно быть кратно 0.25";
            return false;
        }

        double raw = dbm / Step;
        if (raw < MinValue || raw > MaxValue)
        {
            error = $"Число должно быть в диапазоне от {ToDbm(MinValue).ToString(CultureInfo.InvariantCulture)} до {ToDbm(MaxValue).ToString(CultureInfo.InvariantCulture)}";
            return false;
        }

        rawValue = (byte)raw;
        error = null;
        return true;
    }
}
