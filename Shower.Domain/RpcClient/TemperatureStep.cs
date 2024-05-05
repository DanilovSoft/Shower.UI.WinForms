using System.Diagnostics;

namespace Shower.Domain.RpcClient;

[DebuggerDisplay("{" + nameof(ExternalTemp) + "} => {" + nameof(InternalTemp) + "}")]
public struct TemperatureStep
{
    public int InternalTemp { get; set; }
    public int ExternalTemp { get; set; }
}
