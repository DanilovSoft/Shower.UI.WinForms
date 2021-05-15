using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShowerTcpClient
{
    internal sealed class MyBinaryWriter : BinaryWriter
    {
        private const byte MaxLength = 200;
        private readonly Stream _baseStream;
        private long _lastPosition;

        public MyBinaryWriter(Stream output) : base(new MemoryStream(), Encoding.ASCII, true)
        {
            _baseStream = output;
            OutStream.Position = 1;
        }

        public void Write(ShowerCodes code)
        {
            Write((byte)code);
        }

        public void End()
        {
            if (OutStream.Length > MaxLength)
            {
                throw new InvalidOperationException("Maximum buffer length reached");
            }

            var curPos = OutStream.Position;
            byte length = (byte)(curPos - _lastPosition);
            OutStream.Position = _lastPosition;
            Write(length);
            OutStream.Position = curPos + 1;
            _lastPosition = curPos;
        }

        /// <summary>
        /// Отправляет подготовленный запрос из памяти в TCP сокет
        /// </summary>
        public void Send()
        {
            OutStream.Position = 0;
            OutStream.CopyTo(_baseStream);
            _baseStream.Flush();
            OutStream.SetLength(1);
            OutStream.Position = 1;
            _lastPosition = 0;
        }

        /// <exception cref="OperationCanceledException"/>
        public async Task SendAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            OutStream.Position = 0;
            await OutStream.CopyToAsync(_baseStream, bufferSize: 81920, cancellationToken).ConfigureAwait(false);
            await _baseStream.FlushAsync(cancellationToken).ConfigureAwait(false);
            OutStream.SetLength(1);
            OutStream.Position = 1;
            _lastPosition = 0;
        }
    }
}
