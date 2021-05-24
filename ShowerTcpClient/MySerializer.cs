using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShowerTcpClient
{
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
            IntPtr pnt = Marshal.AllocHGlobal(unmanagedSize);
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
}
