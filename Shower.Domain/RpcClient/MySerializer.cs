using System.Runtime.InteropServices;

namespace Shower.Domain.RpcClient;

internal static class MySerializer
{
    public static T Read<T>(ReadOnlySpan<byte> source) where T : struct
    {
        return MemoryMarshal.Read<T>(source);
    }
}
