using System.Buffers;

namespace Shower.Domain.RpcClient;

public static class ExtensionMethods
{
    [Obsolete("Use ReadExactly")]
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

    [Obsolete("Use ReadExactly")]
    public static void ReadBlock(this Stream stream, Span<byte> buffer)
    {
        while (buffer.Length > 0)
        {
            var n = stream.Read(buffer);
            if (n == 0)
            {
                throw new EndOfStreamException();
            }

            buffer = buffer.Slice(n);
        }
    }

    //public static int Read(this Stream stream, Span<byte> buffer)
    //{
    //    var sharedBuffer = ArrayPool<byte>.Shared.Rent(buffer.Length);
    //    try
    //    {
    //        var numRead = stream.Read(sharedBuffer, 0, buffer.Length);
    //        if ((uint)numRead > (uint)buffer.Length)
    //        {
    //            throw new IOException("StreamTooLong");
    //        }

    //        sharedBuffer.AsSpan(0, numRead).CopyTo(buffer);
    //        return numRead;
    //    }
    //    finally
    //    {
    //        ArrayPool<byte>.Shared.Return(sharedBuffer);
    //    }
    //}
}
