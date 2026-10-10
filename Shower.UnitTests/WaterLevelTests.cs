using Shower.Domain.WaterLevel;
using Xunit;

namespace Shower.UnitTests;

public class WaterLevelTests
{
    private static WaterLevelSession CreateSession() => new()
    {
        WaterLevelEmpty = 2000,
        WaterLevelFull = 200,
    };

    [Theory]
    [InlineData(200, 2000)]
    [InlineData(2000, 200)]
    [InlineData(1100, 1100)]
    public void InvertRaw_MirrorsRange(int raw, ushort expected)
    {
        Assert.Equal(expected, CreateSession().InvertRaw(raw));
    }

    [Theory]
    [InlineData(2000, 99)] // Полный бак, 100% ограничивается 99.
    [InlineData(1100, 50)]
    [InlineData(200, 0)]   // Пустой бак.
    [InlineData(2200, 99)] // За пределами калибровки — ограничивается.
    public void CalcPercent_ReturnsLevel(int invertedUsec, byte expected)
    {
        Assert.Equal(expected, CreateSession().CalcPercent(invertedUsec));
    }

    [Fact]
    public void Pipeline_ReturnsValuesWhenFiltersInitialized()
    {
        var session = CreateSession();
        var pipeline = new WaterLevelFilterPipeline(session, medianWindowSize: 3, averageWindowSize: 2);

        var samples = new ushort[] { 1100, 1100, 1100, 1100 }.Select(pipeline.Add).ToArray();

        Assert.Equal(default, samples[0]);
        Assert.Equal(default, samples[1]);
        Assert.Equal(new WaterLevelSample(1100, null, null), samples[2]);
        Assert.Equal(new WaterLevelSample(1100, 1100, 50), samples[3]);
    }

    [Fact]
    public void Pipeline_MedianRemovesSpike()
    {
        var pipeline = new WaterLevelFilterPipeline(CreateSession(), medianWindowSize: 3, averageWindowSize: 1);

        var samples = new ushort[] { 1000, 1000, 1900, 1000 }.Select(pipeline.Add).ToArray();

        Assert.Equal((ushort)1000, samples[2].Median);
        Assert.Equal((ushort)1000, samples[3].Median);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(4, 2)] // Чётный размер увеличивается до нечётного.
    [InlineData(5, 2)]
    [InlineData(255, 127)]
    public void SliderFromMedianBufferSize_ReturnsSliderPosition(int bufferSize, int expected)
    {
        Assert.Equal(expected, WaterLevelFilterPipeline.SliderFromMedianBufferSize(bufferSize));
    }

    [Theory]
    [InlineData(1, 3)]
    [InlineData(2, 5)]
    [InlineData(128, 257)]
    public void MedianWindowFromSlider_ReturnsOddWindow(int slider, int expected)
    {
        Assert.Equal(expected, WaterLevelFilterPipeline.MedianWindowFromSlider(slider));
    }
}
