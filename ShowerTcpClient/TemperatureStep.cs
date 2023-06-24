using System.Diagnostics;

namespace ShowerTcpClient;

[DebuggerDisplay("{" + nameof(ExternalTemp) + "} => {" + nameof(InternalTemp) + "}")]
public struct TemperatureStep
{
    public int InternalTemp { get; set; }
    public int ExternalTemp { get; set; }
}
