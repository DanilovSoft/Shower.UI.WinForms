using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace Shower.Domain.RpcClient;

public sealed class ShowerConnection : IShowerConnection, IDisposable
{
    private const int ReadTimeoutMsec = 10_000;
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

    public T Request<T>(ShowerCodes code) where T : struct
    {
        _writer.Write(code);
        _writer.End();
        _writer.Send();

        var unmanagedSize = Marshal.SizeOf<T>();
        Span<byte> buffer = unmanagedSize <= 256 ? stackalloc byte[256] : new byte[unmanagedSize];
        buffer = buffer[..unmanagedSize];

        _nstream.ReadExactly(buffer);
        return MySerializer.Read<T>(buffer);
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

    public void SetWaterLevelFullUsec(ushort value)
    {
        BuildRequest()
            .Write(ShowerCodes.SetWaterLevelFullUsec)
            .WriteUInt16(value)
            .ReadOK();
    }

    public ShowerCodes Ping(CancellationToken cancellationToken = default)
    {
        return BuildRequest()
            .Write(ShowerCodes.Ping)
            .ReadCodeAsync(cancellationToken).GetAwaiter().GetResult();
    }

    public async Task<ShowerCodes> PingAsync(CancellationToken cancellationToken = default)
    {
        return await BuildRequest()
            .Write(ShowerCodes.Ping)
            .ReadCodeAsync(cancellationToken);
    }

    /// <summary>
    /// Уровень воды в микросекундах, без всякой фильтрации.
    /// </summary>
    /// <returns>-1 если у датчика уровня произошел таймаут.</returns>
    public short GetWaterLevelRaw()
    {
        return Request<short>(ShowerCodes.GetWaterLevelRaw);
    }

    public void SetWaterLevelEmptyUsec(ushort value)
    {
        BuildRequest()
            .Write(ShowerCodes.SetWaterLevelEmptyUsec)
            .WriteUInt16(value)
            .ReadOK();
    }

    public Task<float> GetInternalTemperatureAsync(CancellationToken cancellationToken = default)
    {
        return RequestAsync<float>(ShowerCodes.GetInternalTemp, cancellationToken);
    }

    public float GetInternalTemperature()
    {
        return Request<float>(ShowerCodes.GetInternalTemp);
    }

    public void SetMinimumWaterHeatingLevel(byte value)
    {
        BuildRequest()
            .Write(ShowerCodes.SetMinimumWaterHeatingLevel)
            .WriteUInt8(value)
            .ReadOK();
    }

    public async Task<bool> GetHeaterEnabledAsync(CancellationToken cancellationToken = default)
    {
        var value = await RequestAsync<byte>(ShowerCodes.GetHeaterEnabled, cancellationToken).ConfigureAwait(false);
        return Convert.ToBoolean(value);
    }

    public bool GetHeaterEnabled()
    {
        var value = Request<byte>(ShowerCodes.GetHeaterEnabled);
        return Convert.ToBoolean(value);
    }

    public void SetAbsoluteHeatingTimeLimit(byte value)
    {
        BuildRequest()
            .Write(ShowerCodes.SetAbsoluteHeatingTimeLimit)
            .WriteUInt8(value)
            .ReadOK();
    }

    public Task<byte> GetMinutesLeftAsync(CancellationToken cancellationToken = default)
    {
        return RequestAsync<byte>(ShowerCodes.GetMinutesLeft, cancellationToken);
    }

    public byte GetMinutesLeft()
    {
        return Request<byte>(ShowerCodes.GetMinutesLeft);
    }

    public void SetHeatingTimeLimit(byte value)
    {
        BuildRequest()
            .Write(ShowerCodes.SetHeatingTimeLimit)
            .WriteUInt8(value)
            .ReadOK();
    }

    public async Task<ushort> GetWaterLevelAsync(CancellationToken cancellationToken = default)
    {
        return await RequestAsync<ushort>(ShowerCodes.GetWaterLevel, cancellationToken).ConfigureAwait(false);
    }

    public byte GetWaterPercent()
    {
        return Request<byte>(ShowerCodes.GetWaterPercent);
    }

    public void SetLightBrightness(byte value)
    {
        BuildRequest()
            .Write(ShowerCodes.SetLightBrightness)
            .WriteUInt8(value)
            .ReadOK();
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
        _nstream.ReadExactly(data);

        var model = new SetupModel();
        model.ParseTemp(data);
        return model;
    }

    public void SetWiFiPower(byte power)
    {
        BuildRequest()
            .Write(ShowerCodes.SetWiFiPower)
            .WriteUInt8(power)
            .ReadOK();
    }

    public void SetWaterLevelMeasureInterval(byte value)
    {
        BuildRequest()
            .Write(ShowerCodes.SetWaterLevelMeasureInterval)
            .WriteUInt8(value)
            .ReadOK();
    }

    public Task<ushort> GetWaterLevelEmptyAsync(CancellationToken cancellationToken = default)
    {
        return RequestAsync<ushort>(ShowerCodes.GetWaterLevelEmptyUsec, cancellationToken);
    }

    public Task<ushort> GetWaterLevelFullAsync(CancellationToken cancellationToken = default)
    {
        return RequestAsync<ushort>(ShowerCodes.GetWaterLevelFullUsec, cancellationToken);
    }

    public void SetWaterLevelMedianBufferSize(byte value)
    {
        BuildRequest()
            .Write(ShowerCodes.SetWaterLevelMedianBufferSize)
            .WriteUInt8(value)
            .ReadOK();
    }

    public async Task<T> RequestAsync<T>(ShowerCodes code, CancellationToken cancellationToken = default) where T : struct
    {
        _writer.Write(code);
        _writer.End();
        
        var unmanagedSize = Marshal.SizeOf<T>();
        var buffer = new byte[unmanagedSize];

        await _writer.SendAsync(cancellationToken).ConfigureAwait(false);
        await _nstream.ReadExactlyAsync(buffer, cancellationToken).ConfigureAwait(false);
        
        return MySerializer.Read<T>(buffer);
    }

    public void SetWaterLevelAverageBufferSize(byte value)
    {
        BuildRequest()
            .Write(ShowerCodes.SetWaterLevelAverageBufferSize)
            .WriteUInt8(value)
            .ReadOK();
    }

    public bool GetWatchDogWasReset()
    {
        var iwd = Request<byte>(ShowerCodes.GetWatchDogWasReset);
        var result = Convert.ToBoolean(iwd);
        return result;
    }

    public void SetWaterValveCutOffPercent(byte value)
    {
        BuildRequest()
            .Write(ShowerCodes.SetWaterValveCutOffPercent)
            .WriteUInt8(value)
            .ReadOK();
    }

    /// <summary>
    /// 
    /// </summary>
    public float GetWaterTankVolumeLitre()
    {
        return Request<float>(ShowerCodes.GetWaterTankVolumeLitre);
    }

    public void SetTempSensorInternalTempAverageSize(byte value)
    {
        BuildRequest()
            .Write(ShowerCodes.SetTempSensorInternalTempAverageSize)
            .WriteUInt8(value)
            .ReadOK();
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

    public void SetWaterTankVolumeLitre(float value)
    {
        BuildRequest()
            .Write(ShowerCodes.SetWaterTankVolumeLitre)
            .WriteFloat(value)
            .ReadOK();
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

    public void Save()
    {
        BuildRequest()
            .Write(ShowerCodes.Save)
            .ReadOK();
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

    public void SetCurAP(string ssid, string password, PhysicalAddress? bsid = null)
    {
        if (ssid == null)
            throw new ArgumentNullException(nameof(ssid));

        if (password == null)
            throw new ArgumentNullException(nameof(password));

        if (ssid.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(ssid));

        if (password.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(password));

        // Полная команда на контроллере: AT+CWJAP_CUR="abc","0123456789"
        var espCommand = ESP8266Helper.CreateSetCurrentAPCommand(ssid, password, bsid);

        BuildRequest()
            .Write(ShowerCodes.SetCurAP)
            .WriteString(espCommand)
            .ReadOK();
    }

    public void SetDefAP(string ssid, string password, PhysicalAddress? bsid = null)
    {
        if (ssid == null)
            throw new ArgumentNullException(nameof(ssid));

        if (password == null)
            throw new ArgumentNullException(nameof(password));

        if (ssid.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(ssid));

        if (password.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(password));

        // Полная команда на контроллере: AT+CWJAP_CUR="abc","0123456789"
        var espCommand = ESP8266Helper.CreateSetCurrentAPCommand(ssid, password, bsid);

        BuildRequest()
            .Write(ShowerCodes.SetDefAP)
            .WriteString(espCommand)
            .ReadOK();
    }

    public byte GetWaterLevelErrorThreshhold()
    {
        return Request<byte>(ShowerCodes.GetWaterLevelErrorThreshold);
    }

    public void SetWaterLevelErrorThreshold(byte value)
    {
        BuildRequest()
            .Write(ShowerCodes.SetWaterLevelErrorThreshold)
            .WriteUInt8(value)
            .ReadOK();
    }

    public void Reset()
    {
        BuildRequest()
            .Write(ShowerCodes.Reset)
            .ReadOK();
    }

    public void SetWaterHeaterPowerKWatt(float value)
    {
        BuildRequest()
            .Write(ShowerCodes.SetWaterHeaterPowerKWatt)
            .WriteFloat(value)
            .ReadOK();
    }

    public void SetButtonTimeMsec(byte value)
    {
        BuildRequest()
            .Write(ShowerCodes.SetButtonTimeMsec)
            .WriteUInt8(value)
            .ReadOK();
    }

    public ushort GetButtonLongPressTimeMsec()
    {
        return Request<ushort>(ShowerCodes.GetButtonLongPressTimeMsec);
    }

    public void SetButtonLongPressTimeMsec(ushort value)
    {
        BuildRequest()
            .Write(ShowerCodes.SetButtonLongPressTimeMsec)
            .WriteUInt16(value)
            .ReadOK();
    }

    public ushort GetWaterLevelFullUsec()
    {
        return Request<ushort>(ShowerCodes.GetWaterLevelFullUsec);
    }

    public ushort GetWaterLevelEmptyUsec()
    {
        return Request<ushort>(ShowerCodes.GetWaterLevelEmptyUsec);
    }

    public byte GetMinimumWaterHeatingLevel()
    {
        return Request<byte>(ShowerCodes.GetMinimumWaterHeatingLevel);
    }

    public byte GetAbsoluteHeatingTimeLimit()
    {
        return Request<byte>(ShowerCodes.GetAbsoluteHeatingTimeLimit);
    }

    public byte GetHeatingTimeLimit()
    {
        return Request<byte>(ShowerCodes.GetHeatingTimeLimit);
    }

    public byte GetLightBrightness()
    {
        return Request<byte>(ShowerCodes.GetLightBrightness);
    }

    public byte GetWiFiPower()
    {
        return Request<byte>(ShowerCodes.GetWiFiPower);
    }

    public byte GetWaterLevelMeasureInterval()
    {
        return Request<byte>(ShowerCodes.GetWaterLevelMeasureInterval);
    }

    public byte GetWaterValveCutOffPercent()
    {
        return Request<byte>(ShowerCodes.GetWaterValveCutOffPercent);
    }

    public byte GetTempSensorInternalTempAverageSize()
    {
        return Request<byte>(ShowerCodes.GetTempSensorInternalTempAverageSize);
    }

    public byte GetButtonPressTimeMsec()
    {
        return Request<byte>(ShowerCodes.GetButtonTimeMsec);
    }

    private RequestBuilder BuildRequest()
    {
        return new RequestBuilder(this, _writer, _reader, _nstream);
    }
}
