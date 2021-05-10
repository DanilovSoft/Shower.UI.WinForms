using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShowerUI.ShowerClient
{
    public class MyBinaryWriter : BinaryWriter
    {
        long _lastPosition;
        Stream baseStream;
        const byte MaxLength = 200;

        public MyBinaryWriter(Stream output) : base(new MemoryStream(), Encoding.ASCII, true)
        {
            baseStream = output;
            OutStream.Position = 1;
        }

        public void Write(ShowerCodes code)
        {
            Write((byte)code);
        }

        public void End()
        {
            if (OutStream.Length > MaxLength)
                throw new InvalidOperationException("Maximum buffer length reached");

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
            OutStream.CopyTo(baseStream);
            baseStream.Flush();
            OutStream.SetLength(1);
            OutStream.Position = 1;
            _lastPosition = 0;
        }

        public Task SendAsync() => SendAsync(CancellationToken.None);

        public async Task SendAsync(CancellationToken cancellationToken)
        {
            OutStream.Position = 0;
            await OutStream.CopyToAsync(baseStream).ConfigureAwait(false);
            await baseStream.FlushAsync().ConfigureAwait(false);
            OutStream.SetLength(1);
            OutStream.Position = 1;
            _lastPosition = 0;
        }
    }
}
