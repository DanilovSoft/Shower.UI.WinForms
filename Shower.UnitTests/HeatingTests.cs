using Shower.Domain.Heating;
using Shower.Domain.Models;
using Xunit;

namespace Shower.UnitTests;

public class HeatingTests
{
    [Fact]
    public void CalcTimeLeft_FullTank()
    {
        var calculator = new HeatingTimeLeft(tankVolumeLitre: 50, heaterPowerKWatt: 2);

        // T = 0.00117 * 50 * (40 - 20) / 2 = 0.585 ч.
        var timeLeft = calculator.CalcTimeLeft(internalTemp: 20, limitTemp: 40, waterLevel: 1);

        Assert.Equal(0.585 * 3600, timeLeft.TotalSeconds, precision: 0);
    }

    [Fact]
    public void CalcTimeLeft_HalfTankTakesHalfTime()
    {
        var calculator = new HeatingTimeLeft(tankVolumeLitre: 50, heaterPowerKWatt: 2);

        var full = calculator.CalcTimeLeft(20, 40, 1);
        var half = calculator.CalcTimeLeft(20, 40, 0.5f);

        Assert.Equal(full.TotalSeconds / 2, half.TotalSeconds, precision: 0);
    }

    [Fact]
    public void CalcCylinderVolume_ReturnsLitres()
    {
        // Цилиндр 297 мм высотой и 400 мм в диаметре — около 37.3 л.
        Assert.Equal(37.32, HeatingTimeLeft.CalcCylinderVolume(0.297, 0.4), precision: 2);
        Assert.Equal(37.32, HeatingTimeLeft.CalcCylinderVolume(297f, 400f), precision: 2);
    }

    [Fact]
    public void Forecast_UsesPointAfterStartMinute()
    {
        // Нагрев включается на 10-й секунде, температура растёт на 0.01° в секунду.
        var records = Enumerable.Range(0, 300)
            .Select(s => new InternalTempModel { Second = s, InternalTemp = 20 + s * 0.01f, HeaterEnabled = s >= 10 })
            .ToList();
        var calculator = new HeatingTimeLeft(50, 2);

        var forecast = HeatingForecast.Calculate(records, startMinute: 1, waterLevel: 1, calculator, limitTemp: 40);

        Assert.NotNull(forecast);
        Assert.Equal(70, forecast.Value.StartSecond);
        Assert.Equal(20.7f, forecast.Value.StartTemp, precision: 3);
        Assert.Equal(40f, forecast.Value.EndTemp);
        Assert.Equal(calculator.CalcTimeLeft(records[70].InternalTemp, 40, 1), forecast.Value.TimeLeft);
        Assert.Equal(70 + (int)forecast.Value.TimeLeft.TotalSeconds, forecast.Value.EndSecond);
    }

    [Fact]
    public void Forecast_ZeroMinute_ReturnsNull()
    {
        var records = new List<InternalTempModel> { new() { Second = 0, InternalTemp = 20, HeaterEnabled = true } };

        Assert.Null(HeatingForecast.Calculate(records, 0, 1, new HeatingTimeLeft(50, 2), 40));
    }

    [Fact]
    public void Forecast_HeaterNeverEnabled_ReturnsNull()
    {
        var records = Enumerable.Range(0, 300)
            .Select(s => new InternalTempModel { Second = s, InternalTemp = 20, HeaterEnabled = false })
            .ToList();

        Assert.Null(HeatingForecast.Calculate(records, 1, 1, new HeatingTimeLeft(50, 2), 40));
    }

    [Fact]
    public void Forecast_RecordTooShort_ReturnsNull()
    {
        var records = Enumerable.Range(0, 30)
            .Select(s => new InternalTempModel { Second = s, InternalTemp = 20, HeaterEnabled = true })
            .ToList();

        Assert.Null(HeatingForecast.Calculate(records, 1, 1, new HeatingTimeLeft(50, 2), 40));
    }
}
