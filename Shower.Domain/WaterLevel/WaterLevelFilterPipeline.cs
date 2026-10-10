using Shower.Domain.Filters;

namespace Shower.Domain.WaterLevel;

/// <summary>
/// Результат обработки одного показания датчика уровня воды.
/// </summary>
/// <param name="Median">Значение медианного фильтра, если фильтр уже заполнен.</param>
/// <param name="Average">Скользящее среднее медианы, если оба фильтра уже заполнены.</param>
/// <param name="Percent">Уровень воды 0..99%, рассчитанный по <paramref name="Average"/>.</param>
public readonly record struct WaterLevelSample(ushort? Median, int? Average, byte? Percent);

/// <summary>
/// Цепочка обработки показаний датчика: медиана → скользящее среднее → проценты.
/// </summary>
public sealed class WaterLevelFilterPipeline
{
    private readonly WaterLevelSession _session;
    private readonly AverageFilter _average;
    private MedianFilter<ushort> _median; // Изменяемая структура — не делать readonly.

    /// <param name="session">Калибровка, по которой рассчитываются проценты.</param>
    /// <param name="medianWindowSize">Размер окна медианного фильтра, нечётное число.</param>
    /// <param name="averageWindowSize">Размер окна скользящего среднего.</param>
    public WaterLevelFilterPipeline(WaterLevelSession session, int medianWindowSize, int averageWindowSize)
    {
        ArgumentNullException.ThrowIfNull(session);

        _session = session;
        _median = new MedianFilter<ushort>(medianWindowSize);
        _average = new AverageFilter(averageWindowSize);
    }

    /// <summary>
    /// Размер окна медианного фильтра по положению ползунка: всегда нечётный.
    /// </summary>
    public static int MedianWindowFromSlider(int sliderValue) => sliderValue * 2 + 1;

    /// <summary>
    /// Положение ползунка по размеру буфера медианного фильтра на контроллере.
    /// </summary>
    public static int SliderFromMedianBufferSize(int bufferSize)
    {
        // Число должно быть не чётным.
        if (bufferSize % 2 != 1)
        {
            bufferSize++;
        }
        return (bufferSize - 1) / 2;
    }

    public WaterLevelSample Add(ushort usec)
    {
        ushort median = _median.Add(usec);
        if (!_median.IsInitialized)
        {
            return default;
        }

        int average = _average.Add(median);
        if (!_average.IsInitialized)
        {
            return new WaterLevelSample(median, null, null);
        }

        return new WaterLevelSample(median, average, _session.CalcPercent(average));
    }
}
