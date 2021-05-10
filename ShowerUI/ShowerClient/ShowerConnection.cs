using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShowerUI.ShowerClient
{
    public sealed class ShowerConnection : IDisposable
    {
        private const string Address = "10.2.2.11";
        private const int ReadTimeoutMsec = 10000;
        private TcpClient _tcp;
        private FixedNetworkStream _nstream;
        private bool _disposed;
        private ShowerBinaryReader _reader;
        private MyBinaryWriter _writer;

        public CancellationTokenRegistration RegisterToken(CancellationToken cancellationToken)
        {
            return cancellationToken.Register(() => 
            {
                _tcp.Dispose();
            }, false);
        }

        public async Task ConnectAsync()
        {
            _tcp = new TcpClient();
            try
            {
                await _tcp.ConnectAsync(Address, 333).ConfigureAwait(false);
            }
            catch
            {
                _tcp.Dispose();
                _tcp = null;
                throw;
            }
            
            _nstream = new FixedNetworkStream(_tcp.GetStream())
            {
                ReadTimeout = ReadTimeoutMsec,
                WriteTimeout = 5000
            };
            _writer = new MyBinaryWriter(_nstream);
            _reader = new ShowerBinaryReader(_nstream, Encoding.ASCII, true);
        }

        public static async Task<ShowerConnection> CreateConnectionAsync(CancellationToken cancellationToken)
        {
            var con = new ShowerConnection();

            using (cancellationToken.Register(() => { con.Dispose(); }))
            {
                try
                {
                    await con.ConnectAsync().ConfigureAwait(false);
                    return con;
                }
                catch
                {
                    con.Dispose();
                    cancellationToken.ThrowIfCancellationRequested();
                    throw;
                }
            }
        }
        
        public RequestBuilder BuildRequest()
        {
            return new RequestBuilder(this, _writer, _reader, _nstream);
        }

        public ShowerConnection Write<T>(ShowerCodes code, Action<T> callback)
        {
            return this;
        }

        public T Request<T>(ShowerCodes code) where T : struct
        {
            _writer.Write(code);
            _writer.End();
            int unmanagedSize = Marshal.SizeOf<T>();
            _writer.Send();
            byte[] buf = new byte[unmanagedSize];
            _nstream.ReadBlock(buf, 0, unmanagedSize);
            return Read<T>(buf, 0, unmanagedSize);
        }

        public Task<T> RequestAsync<T>(ShowerCodes code) where T : struct => RequestAsync<T>(code, CancellationToken.None);

        /// <summary>
        /// Уровень воды в микросекундах, без всякой фильтрации. -1 если у датчика уровня произошел таймаут.
        /// </summary>
        [DebuggerNonUserCode]
        public Task<short> GetWaterLevelRawAsync(CancellationToken cancellationToken)
        {
            return RequestAsync<short>(ShowerCodes.GetWaterLevelRaw, cancellationToken);
        }

        public Task<float> GetAverageInternalTemperatureAsync(CancellationToken cancellationToken)
        {
            return RequestAsync<float>(ShowerCodes.GetAverageInternalTemp, cancellationToken);
        }

        public async Task<bool> GetHeaterEnabledAsync(CancellationToken cancellationToken)
        {
            byte value = await RequestAsync<byte>(ShowerCodes.GetHeaterEnabled, cancellationToken).ConfigureAwait(false);
            return Convert.ToBoolean(value);
        }

        public Task<byte> GetTimeLeftAsync(CancellationToken cancellationToken)
        {
            return RequestAsync<byte>(ShowerCodes.GetTimeLeft, cancellationToken);
        }

        public async Task<ushort> GetWaterLevelAsync(CancellationToken cancellationToken)
        {
            return await RequestAsync<ushort>(ShowerCodes.GetWaterLevel, cancellationToken).ConfigureAwait(false);
        }

        public async Task<byte> GetWaterPercent(CancellationToken cancellationToken)
        {
            return await RequestAsync<byte>(ShowerCodes.GetWaterPercent, cancellationToken).ConfigureAwait(false);
        }

        public SetupModel GetTempChart()
        {
            _writer.Write(ShowerCodes.GetTempChart);
            _writer.End();
            _writer.Send();
            byte[] data = new byte[SetupModel.STEP_COUNT];
            _nstream.ReadBlock(data, 0, SetupModel.STEP_COUNT);

            SetupModel model = new SetupModel();
            model.ParseTemp(data);
            return model;
        }

        public Task<ushort> GetWaterLevelEmptyAsync(CancellationToken cancellationToken)
        {
            return RequestAsync<ushort>(ShowerCodes.GetWaterLevelEmpty, cancellationToken);
        }

        public Task<ushort> GetWaterLevelFullAsync(CancellationToken cancellationToken)
        {
            return RequestAsync<ushort>(ShowerCodes.GetWaterLevelFull, cancellationToken);
        }

        public async Task<T> RequestAsync<T>(ShowerCodes code, CancellationToken cancellationToken) where T : struct
        {
            _writer.Write(code);
            _writer.End();
            int unmanagedSize = Marshal.SizeOf<T>();
            await _writer.SendAsync(cancellationToken).ConfigureAwait(false);
            byte[] buf = new byte[unmanagedSize];
            await _nstream.ReadBlockAsync(buf, 0, unmanagedSize, cancellationToken).ConfigureAwait(false);
            return Read<T>(buf, 0, unmanagedSize);
        }

        private static T Read<T>(byte[] buf, int startIndex, int unmanagedSize)
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

        public void Dispose()
        {
            if (!_disposed)
            {
                _reader?.Dispose();
                _writer?.Dispose();
                _nstream?.Dispose();
                _tcp?.Dispose();

                _disposed = true;
            }
        }

        public class RequestBuilder
        {
            private readonly MyBinaryWriter _writer;
            private readonly ShowerBinaryReader _reader;
            private List<CallbackInfo> _callbacks;
            private FixedNetworkStream _nstream;
            private ShowerConnection _con;

            public RequestBuilder(ShowerConnection con, MyBinaryWriter writer, ShowerBinaryReader reader, FixedNetworkStream nstream)
            {
                _con = con;
                _writer = writer;
                _reader = reader;
                _nstream = nstream;
                _callbacks = new List<CallbackInfo>();
            }

            public RequestBuilder Write<T>(T value) where T : struct
            {
                int size = Marshal.SizeOf(value);
                byte[] arr = new byte[size];
                IntPtr ptr = Marshal.AllocHGlobal(size);

                try
                {
                    Marshal.StructureToPtr(value, ptr, true);
                    Marshal.Copy(ptr, arr, 0, size);
                }
                finally
                {
                    Marshal.FreeHGlobal(ptr);
                }

                _writer.Write(arr, 0, size);
                return this;
            }

            public RequestBuilder Write(ShowerCodes code)
            {
                _writer.Write(code);
                return this;
            }

            public RequestBuilder Write<T>(ShowerCodes code, Action<T> callback)
            {
                _writer.Write(code);
                _writer.End();
                _callbacks.Add(CallbackInfo.Create(callback));
                return this;
            }

            public RequestBuilder Result<T>(Action<T> callback)
            {
                _writer.End();
                _callbacks.Add(CallbackInfo.Create(callback));
                return this;
            }

            public void Send()
            {
                WriteEndAndSend();
            }

            public void ReadOK()
            {
                WriteEndAndSend();
                _reader.ReadOK();
            }

            private void WriteEndAndSend()
            {
                _writer.End();
                _writer.Send();
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

                public static CallbackInfo Create<T>(Action<T> callback)
                {
                    int unmanagedSize = Marshal.SizeOf<T>();

                    return new CallbackInfo(unmanagedSize, delegate(byte[] buf, ref int startIndex)
                    {
                        T obj = Read<T>(buf, startIndex, unmanagedSize);
                        startIndex += unmanagedSize;
                        callback(obj);
                    });
                }
            }
        }
    }
}
