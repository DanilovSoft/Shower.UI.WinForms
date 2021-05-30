using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShowerTcpClient
{
    public class RequestBuilder
    {
        private readonly List<CallbackInfo> _callbacks = new();
        private readonly MyBinaryWriter _writer;
        private readonly ShowerBinaryReader _reader;
        private readonly FixedNetworkStream _nstream;
        private readonly ShowerConnection _con;

        internal RequestBuilder(ShowerConnection con, MyBinaryWriter writer, ShowerBinaryReader reader, FixedNetworkStream nstream)
        {
            _con = con;
            _writer = writer;
            _reader = reader;
            _nstream = nstream;
        }

        public RequestBuilder Write<T>(T value) where T : struct
        {
            int sizeBytes = Marshal.SizeOf(value);
            byte[] output = new byte[sizeBytes];
            IntPtr ptr = Marshal.AllocHGlobal(sizeBytes);

            try
            {
                Marshal.StructureToPtr(value, ptr, true);
                Marshal.Copy(ptr, output, 0, sizeBytes);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }

            _writer.Write(output, 0, sizeBytes);
            return this;
        }

        public RequestBuilder Write(ShowerCodes code)
        {
            _writer.Write(code);
            return this;
        }

        public RequestBuilder Write<T>(ShowerCodes code, Action<T> callback) where T : struct
        {
            _writer.Write(code);
            _writer.End();
            _callbacks.Add(CallbackInfo.Create(callback));
            return this;
        }

        public RequestBuilder Write(string value)
        {
            byte[] str = Encoding.ASCII.GetBytes(value);
            int sizeBytes = str.Length;
            
            _writer.Write(str, 0, sizeBytes);
            return this;
        }

        public RequestBuilder Result<T>(Action<T> callback) where T : struct
        {
            _writer.End();
            _callbacks.Add(CallbackInfo.Create(callback));
            return this;
        }

        public void Send()
        {
            WriteEndAndSend();
        }

        /// <summary>
        /// Добавляет заголовок в стрим, отправляет все накопленные данные и читает один байт ответа.
        /// </summary>
        public void ReadOK()
        {
            WriteEndAndSend();
            _reader.ReadOK();
        }

        public async Task<ShowerCodes> ReadCodeAsync(CancellationToken cancellationToken)
        {
            using (cancellationToken.Register(() => { _con.Dispose(); }))
            {
                try
                {
                    WriteEndAndSend();
                    return await _reader.ReadCodeAsync().ConfigureAwait(false);
                }
                catch when (cancellationToken.IsCancellationRequested)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    throw;
                }
            }
        }

        public void Execute()
        {
            _writer.Send();

            if (_callbacks.Count > 0)
            {
                int totalSize = _callbacks.Sum(x => x.UnmanagedSize);
                byte[] buf = new byte[totalSize];
                Read(buf, totalSize);
                int startIndex = 0;
                for (int i = 0; i < _callbacks.Count; i++)
                {
                    _callbacks[i].ReadPrimitiveCallback(buf, ref startIndex);
                }
            }
        }

        public Task ExecuteAsync() => ExecuteAsync(CancellationToken.None);

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await _writer.SendAsync(cancellationToken).ConfigureAwait(false);
            int totalSize = _callbacks.Sum(x => x.UnmanagedSize);
            byte[] buf = new byte[totalSize];
            await ReadAsync(buf, totalSize, cancellationToken).ConfigureAwait(false);
            int startIndex = 0;
            for (int i = 0; i < _callbacks.Count; i++)
            {
                _callbacks[i].ReadPrimitiveCallback(buf, ref startIndex);
            }
        }

        /// <summary>
        /// Добавляет заголовок в стрим и отправляет весь стрим и сбрасывает его размер.
        /// </summary>
        private void WriteEndAndSend()
        {
            _writer.End();
            _writer.Send();
        }

        private async Task ReadAsync(byte[] buf, int count, CancellationToken cancellationToken)
        {
            await _nstream.ReadBlockAsync(buf, 0, count, cancellationToken).ConfigureAwait(false);
        }

        private void Read(byte[] buf, int count)
        {
            _nstream.ReadBlock(buf, 0, count);
        }

        private struct CallbackInfo
        {
            public delegate void ReadPrimitiveCallbackHandler(byte[] buffer, ref int startIndex);
            public readonly ReadPrimitiveCallbackHandler ReadPrimitiveCallback;
            public readonly int UnmanagedSize;

            private CallbackInfo(int unmanagedSize, ReadPrimitiveCallbackHandler callback)
            {
                UnmanagedSize = unmanagedSize;
                ReadPrimitiveCallback = callback;
            }

            public static CallbackInfo Create<T>(Action<T> callback) where T : struct
            {
                int unmanagedSize = Marshal.SizeOf<T>();

                return new CallbackInfo(unmanagedSize, delegate (byte[] buf, ref int startIndex)
                {
                    T obj = MySerializer.Read<T>(buf.AsSpan(startIndex, unmanagedSize));
                    startIndex += unmanagedSize;
                    callback(obj);
                });
            }
        }
    }
}
