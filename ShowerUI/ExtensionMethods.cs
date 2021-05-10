using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShowerUI
{
    public static class Utils
    {
        public static float? TryParse(string value)
        {
            return (float.TryParse(value, out float f) ? f : (float?)null);
        }

        public static void ReadBlock(this Stream stream, byte[] buf, int offset, int count)
        {
            int n;
            while ((count -= n = stream.Read(buf, offset, count)) > 0)
            {
                if (n == 0)
                    throw new EndOfStreamException();

                offset += n;
            }
        }
    }
}
