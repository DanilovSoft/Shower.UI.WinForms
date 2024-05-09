using System.Runtime.InteropServices;

namespace Shower.Domain.RpcClient;

internal static class MySerializer
{
    public static T Read<T>(ReadOnlySpan<byte> buf) where T : struct
    {
        var value = MemoryMarshal.Read<T>(buf);
        return value;
    }
}
