using Shower.Domain.Filters;
using Xunit;

namespace Shower.UnitTests;

public class MedianFilterTest
{
    [Theory]
    [InlineData([new int[] { 10, 150, 15, 20, 25, 30, 35, 400, 15, 50 }, 3, new int[] { 15, 20, 20, 25, 30, 35, 35, 50 }])]
    [InlineData([new int[] { 50, 15, 400, 35, 30, 25, 20, 15, 150, 10 }, 5, new int[] { 35, 30, 30, 25, 25, 20 }])]
    [InlineData([new int[] { 50, 15, 400, 35, 30, 25, 20, 15, 150, 10 }, 7, new int[] { 30, 25, 30, 25 }])]
    [InlineData([new int[] { 50, 15, 400, 35, 30, 25, 20, 15, 150, 10 }, 9, new int[] { 30, 25 }])]
    public void MedianFilter_Success(int[] input, int windowSize, int[] expected)
    {
        var filter = new MedianFilter<int>(windowSize);

        var rawOutput = input.Select(filter.Add).ToArray();

        var filtered = rawOutput
            .Skip(filter.InitSize)
            .ToArray();

        Assert.Equal(expected, filtered);
    }
}
