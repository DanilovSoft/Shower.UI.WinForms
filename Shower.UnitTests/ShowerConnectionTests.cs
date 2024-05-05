using System.Net.NetworkInformation;
using Shower.Domain.RpcClient;
using Xunit;

namespace Shower.UnitTests;

public class ShowerConnectionTests
{
    [Fact]
    public void EscapingSSID_ReturnsEscaped()
    {
        const string ssid = "abc/,";
        const string password = @"0123456789""/";

        var escapedSsid = ESP8266Helper.EscapeString(ssid);
        var escapedPass = ESP8266Helper.EscapeString(password);

        const string expectedSsid = "abc///,";
        const string expectedPass = @"0123456789/""//";

        Assert.Equal(expectedSsid, escapedSsid);
        Assert.Equal(expectedPass, escapedPass);
    }

    [Fact]
    public void SetCurAP_ReturnsCommand()
    {
        // AT+CWJAP_CUR="abc","0123456789"
        // Если SSID – это "ab/,c", а пароль — это "0123456789"/", то AT+CWJAP_CUR="abc///,", "0123456789/"//"
        // Если SSID с названием "abc" несколько, то нужную SSID можно найти с помощью BSSID, т.е. примерно так: AT+CWJAP_CUR="abc", "0123456789", "ca:d7:19:d8:a6:44"

        var bsid = "CA:D7:19:D8:A6:44".Replace(":", "");

        var command1 = ESP8266Helper.CreateSetCurrentAPCommand("abc", "0123456789");
        var command2 = ESP8266Helper.CreateSetCurrentAPCommand("abc", "0123456789", PhysicalAddress.Parse(bsid));

        Assert.Equal("\"abc\",\"0123456789\"", command1);
        Assert.Equal("\"abc\",\"0123456789\",\"ca:d7:19:d8:a6:44\"", command2);
    }
}
