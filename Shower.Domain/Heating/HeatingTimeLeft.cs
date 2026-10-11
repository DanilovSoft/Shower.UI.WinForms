namespace Shower.Domain.Heating;

public sealed class HeatingTimeLeft
{
    private const float Q = 0.00117f;

    /// <summary>
    /// Объём воды полного бака в литрах.
    /// </summary>
    private readonly float _tankVolumeLitre;

    /// <summary>
    /// Электрическая мощность нагревательного элемента — ТЭНа с учётом его КПД, кВТ.
    /// </summary>
    private readonly float _heaterPowerKWatt;

    /// <param name="tankVolumeLitre">Объём воды полного бака в литрах.</param>
    /// <param name="heaterPowerKWatt">Мощность ТЭНа в киловатах с учётом его КПД.</param>
    public HeatingTimeLeft(float tankVolumeLitre, float heaterPowerKWatt)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(tankVolumeLitre);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(heaterPowerKWatt);

        _tankVolumeLitre = tankVolumeLitre;
        _heaterPowerKWatt = heaterPowerKWatt;
    }

    /// <param name="cylinderHeightMil">Высота цилиндра в миллиметрах.</param>
    /// <param name="cylinderDiameterMil">Диамметр цилиндра в миллиметрах.</param>
    /// <returns>Объём цилиндра в литрах.</returns>
    public static double CalcCylinderVolume(float cylinderHeightMil, float cylinderDiameterMil)
    {
        // Формула расчёта объема цилиндра в литрах V = Pi * (r^2) * h
        // В 1 литре - 1млн кубических мм.
        double tankVolume = Math.PI * Math.Pow(cylinderDiameterMil / 2d, 2) * cylinderHeightMil / 1_000_000d;
        return tankVolume;
    }

    /// <param name="cylinderHeight">Высота цилиндра в метрах.</param>
    /// <param name="cylinderDiameter">Диамметр цилиндра в метрах.</param>
    /// <returns>Объём цилиндра в литрах.</returns>
    public static double CalcCylinderVolume(double cylinderHeight, double cylinderDiameter)
    {
        double radius = cylinderDiameter / 2d;

        // Формула расчёта объема цилиндра в кубических метрах V = Pi * (r^2) * h
        double tankVolumeM = Math.PI * Math.Pow(radius, 2) * cylinderHeight;

        // Переведём в литры.
        double tankVolumeLitre = tankVolumeM * 1_000;
        return tankVolumeLitre;
    }

    /// <param name="currentTemp">Текущая температура воды в баке.</param>
    /// <param name="targetTemperature">Целевая темпаратура.</param>
    /// <param name="waterLevel">Уровень воды в баке, от 0 до 1.</param>
    /// <returns>Время до окончания нагрева.</returns>
    public TimeSpan CalcTimeLeft(float currentTemp, float targetTemperature, float waterLevel)
    {
        //  Формула расчета времени нагрева T = 0,00117 * V * (tк - tн) / W
        //  Т – время нагрева воды, час
        //  V – объем водонагревательного бака(л)
        //  tк – конечная температура воды, °С(обычно 40°C)
        //  tн – начальная температура воды, °С
        //  W – электрическая мощность нагревательного элемента — ТЭНа, кВТ

        // Учитываем что бак может быть не полным.
        float tankVolume = _tankVolumeLitre * waterLevel;

        double timeH = Q * tankVolume * (targetTemperature - currentTemp) / _heaterPowerKWatt;
        return TimeSpan.FromHours(timeH);
    }
}
