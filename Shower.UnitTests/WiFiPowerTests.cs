using Shower.Domain;
using Xunit;

namespace Shower.UnitTests;

public class WiFiPowerTests
{
    [Theory]
    [InlineData("10", 40)]
    [InlineData("20.5", 82)]
    [InlineData("15.25", 61)]
    public void TryParseDbm_Valid(string text, byte expected)
    {
        Assert.True(WiFiPower.TryParseDbm(text, out var raw, out var error));
        Assert.Equal(expected, raw);
        Assert.Null(error);
    }

    [Theory]
    [InlineData("15.1")] // не кратно 0.25
    [InlineData("9.75")] // меньше минимума
    [InlineData("21")]   // больше максимума
    [InlineData("abc")]
    [InlineData("")]
    [InlineData(null)]
    public void TryParseDbm_Invalid(string text)
    {
        Assert.False(WiFiPower.TryParseDbm(text, out _, out var error));
        Assert.False(string.IsNullOrEmpty(error));
    }

    [Theory]
    [InlineData(40, "10")]
    [InlineData(82, "20.5")]
    [InlineData(61, "15.25")]
    public void FormatDbm(byte raw, string expected)
    {
        Assert.Equal(expected, WiFiPower.FormatDbm(raw));
    }

    [Fact]
    public void ToDbm_OutOfRange_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => WiFiPower.ToDbm(39));
        Assert.Throws<ArgumentOutOfRangeException>(() => WiFiPower.ToDbm(83));
    }
}
