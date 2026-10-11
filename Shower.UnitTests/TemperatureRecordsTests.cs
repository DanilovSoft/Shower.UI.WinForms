using Shower.Domain.Models;
using Xunit;

namespace Shower.UnitTests;

public class TemperatureRecordsTests
{
    private static readonly DateTime Start = new(2026, 10, 1, 12, 0, 0);

    [Theory]
    [InlineData(1, false)]
    [InlineData(4.9, false)]
    [InlineData(5, true)]
    [InlineData(30, true)]
    public void IsGap_ComparesIntervalWithThreshold(double seconds, bool expected)
    {
        var previous = new InternalTempModel { Timestamp = Start };
        var current = new InternalTempModel { Timestamp = Start.AddSeconds(seconds) };

        Assert.Equal(expected, TemperatureRecords.IsGap(previous, current));
    }

    [Fact]
    public void RestoreTimestamps_CountsBackFromEnd()
    {
        var records = new List<InternalTempModel>
        {
            new() { Timestamp = default, Second = 0, InternalTemp = 20 },
            new() { Timestamp = default, Second = 10, InternalTemp = 21 },
            new() { Timestamp = default, Second = 25, InternalTemp = 22 },
        };
        var end = Start.AddMinutes(5);

        var restored = TemperatureRecords.RestoreTimestamps(records, end);

        Assert.Equal(new[] { end.AddSeconds(-25), end.AddSeconds(-15), end }, restored.Select(r => r.Timestamp));
        Assert.Equal(new[] { 20f, 21f, 22f }, restored.Select(r => r.InternalTemp));
    }

    [Fact]
    public void RestoreTimestamps_KeepsRecordsWithTimestamps()
    {
        var records = new List<InternalTempModel> { new() { Timestamp = Start, Second = 0 } };

        Assert.Same(records, TemperatureRecords.RestoreTimestamps(records, Start.AddHours(1)));
    }
}
