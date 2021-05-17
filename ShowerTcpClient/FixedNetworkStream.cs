using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShowerTcpClient
{
    internal sealed class FixedNetworkStream : Stream
    {
        private readonly NetworkStream _nstream;
        private int _readTimeout;
        private int _writeTimeout;

        public FixedNetworkStream(NetworkStream nstream)
        {
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

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            using (var linked = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                linked.CancelAfter(ReadTimeout);

                try
                {
                    using (linked.Token.Register(() => _nstream.Close(), false))
                    {
                        return await _nstream.ReadAsync(buffer, offset, count, linked.Token).ConfigureAwait(false);
                    }
                }
                catch (Exception ex) when (linked.IsCancellationRequested && !cancellationToken.IsCancellationRequested)
                {
                    throw new TimeoutException("Превышено время ожидания операции", ex);
                }
                catch (Exception ex) when (cancellationToken.IsCancellationRequested)
                {
                    throw new OperationCanceledException("Операция отменена пользователем", ex, cancellationToken);
                }
            }
        }

        public async Task ReadBlockAsync(byte[] buf, int offset, int count, CancellationToken cancellationToken)
        {
            int n;
            while ((count -= n = await ReadAsync(buf, offset, count, cancellationToken).ConfigureAwait(false)) > 0)
            {
                if (n == 0)
                {
                    throw new EndOfStreamException();
                }

                offset += n;
            }
        }

        public override long Seek(long offset, SeekOrigin origin) => _nstream.Seek(offset, origin);

        public override void SetLength(long value) => _nstream.SetLength(value);

        public override void Write(byte[] buffer, int offset, int count) => _nstream.Write(buffer, offset, count);

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => _nstream.WriteAsync(buffer, offset, count, cancellationToken);

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _nstream.Dispose();
            }
        }
    }
}
