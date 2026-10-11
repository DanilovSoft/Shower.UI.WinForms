using System.Net.Sockets;

namespace Shower.Domain.RpcClient;

internal sealed class FixedNetworkStream : Stream
{
    private readonly Stream _nstream;
    private int _readTimeout;
    private int _writeTimeout;

    public FixedNetworkStream(NetworkStream nstream)
    {
        ArgumentNullException.ThrowIfNull(nstream);

        _nstream = nstream;
    }

    public override int ReadTimeout { get => _readTimeout; set => _readTimeout = value; }
    public override int WriteTimeout { get => _writeTimeout; set => _writeTimeout = value; }

    public override bool CanRead => _nstream.CanRead;

    public override bool CanSeek => _nstream.CanSeek;

    public override bool CanWrite => _nstream.CanWrite;

    public override long Length => _nstream.Length;

    public override long Position { get => _nstream.Position; set => _nstream.Position = value; }

    public override void Flush() => _nstream.Flush();

    public override int Read(byte[] buffer, int offset, int count) => _nstream.Read(buffer, offset, count);

    public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
    {
        // TO THINK закрывать соединение по токену?
        return _nstream.ReadAsync(buffer, cancellationToken);
    }

    public override long Seek(long offset, SeekOrigin origin) => _nstream.Seek(offset, origin);

    public override void SetLength(long value) => _nstream.SetLength(value);

    public override void Write(byte[] buffer, int offset, int count) => _nstream.Write(buffer, offset, count);

    public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => _nstream.WriteAsync(buffer, offset, count, cancellationToken);

    public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
    {
        // TO THINK закрывать соединение по токену?
        return _nstream.WriteAsync(buffer, cancellationToken);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _nstream.Dispose();
        }

        base.Dispose(disposing);
    }
}
