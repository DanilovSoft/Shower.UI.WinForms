using System.Runtime.InteropServices;

namespace Shower.Domain.RpcClient;

internal static class MySerializer
{
    public static T Read<T>(ReadOnlySpan<byte> buf) where T : struct
    {
        var value = MemoryMarshal.Read<T>(buf);
        return value;
    }

    [Obsolete]
    public static T UnsafeRead<T>(byte[] buf, int startIndex, int unmanagedSize) where T : struct
    {
        var pnt = Marshal.AllocHGlobal(unmanagedSize);
        try
        {
            Marshal.Copy(buf, startIndex, pnt, unmanagedSize);
            return Marshal.PtrToStructure<T>(pnt);
        }
        finally
        {
            Marshal.FreeHGlobal(pnt);
        }
    }
}
