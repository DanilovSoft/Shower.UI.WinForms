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
        public static T Read<T>(byte[] buf, int startIndex, int unmanagedSize)
        {
            IntPtr pnt = Marshal.AllocHGlobal(unmanagedSize);
            //MemoryMarshal.TryRead<T>()
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
