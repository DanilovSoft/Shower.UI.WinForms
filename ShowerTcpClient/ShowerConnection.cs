using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static ShowerTcpClient.ConnectionHelper;

namespace ShowerTcpClient
{
    public sealed class ShowerConnection : IShowerConnection, IDisposable
    {
        private const int ReadTimeoutMsec = 10000;
        private readonly FixedNetworkStream _nstream;
        private readonly ShowerBinaryReader _reader;
        private readonly MyBinaryWriter _writer;
        private bool _disposed;

        internal ShowerConnection(TcpClient connection)
        {
            _nstream = new FixedNetworkStream(connection.GetStream())
            {
                ReadTimeout = ReadTimeoutMsec,
                WriteTimeout = 5000
            };
            _writer = new MyBinaryWriter(_nstream);
            _reader = new ShowerBinaryReader(_nstream, Encoding.ASCII, leaveOpen: true);
        }

        public RequestBuilder BuildRequest()
        {
            return new RequestBuilder(this, _writer, _reader, _nstream);
        }

        public T Request<T>(ShowerCodes code) where T : struct
        {
            _writer.Write(code);
            _writer.End();
            int unmanagedSize = Marshal.SizeOf<T>();
            _writer.Send();
            byte[] buf = new byte[unmanagedSize];
            _nstream.ReadBlock(buf, 0, unmanagedSize);
            return MySerializer.Read<T>(buf, 0, unmanagedSize);
        }

        /// <inheritdoc/>
        public ShowerConnection Write<T>(ShowerCodes code, Action<T> callback)
        {
            return this;
        }

        public Task<T> RequestAsync<T>(ShowerCodes code) where T : struct => RequestAsync<T>(code, CancellationToken.None);

        /// <summary>
        /// Уровень воды в микросекундах, без всякой фильтрации. -1 если у датчика уровня произошел таймаут.
        /// </summary>
        public Task<short> GetWaterLevelRawAsync(CancellationToken cancellationToken)
        {
            return RequestAsync<short>(ShowerCodes.GetWaterLevelRaw, cancellationToken);
        }

        public Task<float> GetInternalTemperatureAsync(CancellationToken cancellationToken)
        {
            return RequestAsync<float>(ShowerCodes.GetInternalTemp, cancellationToken);
        }

        public async Task<bool> GetHeaterEnabledAsync(CancellationToken cancellationToken)
        {
            byte value = await RequestAsync<byte>(ShowerCodes.GetHeaterEnabled, cancellationToken).ConfigureAwait(false);
            return Convert.ToBoolean(value);
        }

        public Task<byte> GetMinutesLeftAsync(CancellationToken cancellationToken)
        {
            return RequestAsync<byte>(ShowerCodes.GetMinutesLeft, cancellationToken);
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

            var model = new SetupModel();
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
            return MySerializer.Read<T>(buf, 0, unmanagedSize);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _reader.Dispose();
                _writer.Dispose();
                _nstream.Dispose();
            }
        }
    }
}
