using Shower.Domain.Filters;
using Xunit;

namespace Shower.UnitTests;

public class AverageFilterTests
{
    [Fact]
    public void Add_InitializedAfterWindowFilled()
    {
        var filter = new AverageFilter(3);

        filter.Add(3);
        filter.Add(4);
        Assert.False(filter.IsInitialized);

        var average = filter.Add(5);
        Assert.True(filter.IsInitialized);
        Assert.Equal(4, average);
    }

    [Fact]
    public void Add_SlidesWindow()
    {
        var filter = new AverageFilter(3);

        var output = new[] { 3, 4, 5, 6, 7 }.Select(filter.Add).ToArray()[2..];

        Assert.Equal(new[] { 4, 5, 6 }, output);
    }

    [Theory]
    [InlineData(1, 2, 2)] // 1.5 → 2
    [InlineData(2, 3, 2)] // 2.5 → 2 (банковское округление)
    [InlineData(2, 5, 4)] // 3.5 → 4
    public void Add_RoundsAverage(int first, int second, int expected)
    {
        var filter = new AverageFilter(2);

        filter.Add(first);
        var average = filter.Add(second);

        Assert.Equal(expected, average);
    }

    [Fact]
    public void Constructor_ZeroWindow_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new AverageFilter(0));
    }
}
