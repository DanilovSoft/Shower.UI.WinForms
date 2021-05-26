using System;
using System.Collections.Generic;
using System.Drawing;
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
            _writer.Send();

            int unmanagedSize = Marshal.SizeOf<T>();
            Span<byte> buf = stackalloc byte[unmanagedSize];
            _nstream.ReadBlock(buf);
            var value = MySerializer.Read<T>(buf);
            //var value2 = MySerializer.UnsafeRead<T>(buf.ToArray(), 0, unmanagedSize);
            return value;
        }

        /// <inheritdoc/>
        public ShowerConnection Write<T>(ShowerCodes code, Action<T> callback)
        {
            return this;
        }

        /// <summary>
        /// Уровень воды в микросекундах, без всякой фильтрации.
        /// </summary>
        /// <returns>-1 если у датчика уровня произошел таймаут.</returns>
        public Task<short> GetWaterLevelRawAsync(CancellationToken cancellationToken = default)
        {
            return RequestAsync<short>(ShowerCodes.GetWaterLevelRaw, cancellationToken);
        }

        /// <summary>
        /// Уровень воды в микросекундах, без всякой фильтрации.
        /// </summary>
        /// <returns>-1 если у датчика уровня произошел таймаут.</returns>
        public short GetWaterLevelRaw()
        {
            return Request<short>(ShowerCodes.GetWaterLevelRaw);
        }

        public Task<float> GetInternalTemperatureAsync(CancellationToken cancellationToken = default)
        {
            return RequestAsync<float>(ShowerCodes.GetInternalTemp, cancellationToken);
        }

        public float GetInternalTemperature()
        {
            return Request<float>(ShowerCodes.GetInternalTemp);
        }

        public async Task<bool> GetHeaterEnabledAsync(CancellationToken cancellationToken = default)
        {
            byte value = await RequestAsync<byte>(ShowerCodes.GetHeaterEnabled, cancellationToken).ConfigureAwait(false);
            return Convert.ToBoolean(value);
        }

        public bool GetHeaterEnabled()
        {
            byte value = Request<byte>(ShowerCodes.GetHeaterEnabled);
            return Convert.ToBoolean(value);
        }

        public Task<byte> GetMinutesLeftAsync(CancellationToken cancellationToken = default)
        {
            return RequestAsync<byte>(ShowerCodes.GetMinutesLeft, cancellationToken);
        }

        public byte GetMinutesLeft()
        {
            return Request<byte>(ShowerCodes.GetMinutesLeft);
        }

        public async Task<ushort> GetWaterLevelAsync(CancellationToken cancellationToken = default)
        {
            return await RequestAsync<ushort>(ShowerCodes.GetWaterLevel, cancellationToken).ConfigureAwait(false);
        }

        public byte GetWaterPercent()
        {
            return Request<byte>(ShowerCodes.GetWaterPercent);
        }

        public Task<byte> GetWaterPercentAsync(CancellationToken cancellationToken = default)
        {
            return RequestAsync<byte>(ShowerCodes.GetWaterPercent, cancellationToken);
        }

        public SetupModel GetTempChart()
        {
            _writer.Write(ShowerCodes.GetTempChart);
            _writer.End();
            _writer.Send();

            Span<byte> data = stackalloc byte[SetupModel.STEP_COUNT];
            _nstream.ReadBlock(data);

            var model = new SetupModel();
            model.ParseTemp(data);
            return model;
        }

        public Task<ushort> GetWaterLevelEmptyAsync(CancellationToken cancellationToken = default)
        {
            return RequestAsync<ushort>(ShowerCodes.GetWaterLevelEmptyUsec, cancellationToken);
        }

        public Task<ushort> GetWaterLevelFullAsync(CancellationToken cancellationToken = default)
        {
            return RequestAsync<ushort>(ShowerCodes.GetWaterLevelFullUsec, cancellationToken);
        }

        public async Task<T> RequestAsync<T>(ShowerCodes code, CancellationToken cancellationToken = default) where T : struct
        {
            _writer.Write(code);
            _writer.End();
            await _writer.SendAsync(cancellationToken).ConfigureAwait(false);
            int unmanagedSize = Marshal.SizeOf<T>();
            var buf = new byte[unmanagedSize];
            await _nstream.ReadBlockAsync(buf, 0, unmanagedSize, cancellationToken).ConfigureAwait(false);
            var value = MySerializer.Read<T>(buf);
            return value;
        }

        public bool GetWatchDogWasReset()
        {
            byte iwd = Request<byte>(ShowerCodes.GetWatchDogWasReset);
            bool result = Convert.ToBoolean(iwd);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        public float GetWaterTankVolumeLitre()
        {
            return Request<float>(ShowerCodes.GetWaterTankVolumeLitre);
        }

        /// <summary>
        /// 
        /// </summary>
        public Task<float> GetWaterTankVolumeLitreAsync(CancellationToken cancellationToken = default)
        {
            return RequestAsync<float>(ShowerCodes.GetWaterTankVolumeLitre, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        public float GetWaterHeaterPowerKWatt()
        {
            return Request<float>(ShowerCodes.GetWaterHeaterPowerKWatt);
        }

        /// <summary>
        /// 
        /// </summary>
        public Task<float> GetWaterHeaterPowerKWattAsync(CancellationToken cancellationToken = default)
        {
            return RequestAsync<float>(ShowerCodes.GetWaterHeaterPowerKWatt, cancellationToken);
        }

        /// <summary>
        /// Возвращает размер буфера медианного фильтра.
        /// </summary>
        public byte GetWaterLevelMedianSize()
        {
            return Request<byte>(ShowerCodes.GetWaterLevelMedianBufferSize);
        }

        /// <summary>
        /// Возвращает размер буфера медианного фильтра.
        /// </summary>
        public Task<byte> GetWaterLevelMedianSizeAsync()
        {
            return RequestAsync<byte>(ShowerCodes.GetWaterLevelMedianBufferSize);
        }

        /// <summary>
        /// Возвращает размер фильтра 'скользящее среднее' для медианы уровня воды.
        /// </summary>
        public Task<byte> GetWaterLevelAverageSizeAsync()
        {
            return RequestAsync<byte>(ShowerCodes.GetWaterLevelAverageBufferSize);
        }

        /// <summary>
        /// Возвращает размер буфера фильтра 'скользящее среднее'.
        /// </summary>
        public byte GetWaterLevelAverageFilterSize()
        {
            return Request<byte>(ShowerCodes.GetWaterLevelAverageBufferSize);
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
