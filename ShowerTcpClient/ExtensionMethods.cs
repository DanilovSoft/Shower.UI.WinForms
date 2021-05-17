using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShowerTcpClient
{
    public static class ExtensionMethods
    {
        public static void ReadBlock(this Stream stream, byte[] buf, int offset, int count)
        {
            int n;
            while ((count -= n = stream.Read(buf, offset, count)) > 0)
            {
                if (n == 0)
                {
                    throw new EndOfStreamException();
                }

                offset += n;
            }
        }

        public static void ReadBlock(this Stream stream, Span<byte> buffer)
        {
            while (buffer.Length > 0)
            {
                int n = stream.Read(buffer);
                if (n == 0)
                {
                    throw new EndOfStreamException();
                }

                buffer = buffer.Slice(n);
            }
        }

        public static int Read(this Stream stream, Span<byte> buffer)
        {
            byte[] sharedBuffer = ArrayPool<byte>.Shared.Rent(buffer.Length);
            try
            {
                int numRead = stream.Read(sharedBuffer, 0, buffer.Length);
                if ((uint)numRead > (uint)buffer.Length)
                {
                    throw new IOException("StreamTooLong");
                }

                sharedBuffer.AsSpan(0, numRead).CopyTo(buffer);
                return numRead;
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(sharedBuffer);
            }
        }
    }
}
