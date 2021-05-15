using System.Diagnostics;

namespace ShowerUI
{
    [DebuggerDisplay("{ExternalTemp} => {InternalTemp}")]
    public struct TemperatureStep
    {
        public int InternalTemp { get; set; }
        public int ExternalTemp { get; set; }
    }
}
