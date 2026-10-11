using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace Shower.Domain.RpcClient;

/// <remarks>Не потокобезопасный</remarks>
public sealed class ShowerConnection : IShowerConnection, IDisposable
{
    private readonly Stream _socketStream;
    private readonly ShowerBinaryReader _reader;
    private readonly MyBinaryWriter _writer;
    private bool _disposed;

    public ShowerConnection(TcpClient connection)
    {
        ArgumentNullException.ThrowIfNull(connection);

        _socketStream = connection.GetStream();
        _writer = new MyBinaryWriter(_socketStream);
        _reader = new ShowerBinaryReader(_socketStream, Encoding.ASCII, leaveOpen: true);
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _disposed = true;
            _reader.Dispose();
            _writer.Dispose();
            _socketStream.Dispose();
        }
    }

    public T Request<T>(ShowerCodes code) where T : struct
    {
        _writer.Write(code);
        _writer.WriteEnd();
        _writer.Send();

        var unmanagedSize = Marshal.SizeOf<T>();
        Span<byte> buffer = GetRequestBuffer(unmanagedSize, stackalloc byte[256]);

        _socketStream.ReadExactly(buffer);
        return MySerializer.Read<T>(buffer);
    }

    private static Span<byte> GetRequestBuffer(int size, Span<byte> buffer)
    {
        return buffer.Length >= size
            ? buffer[..size]
            : new byte[size];
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
        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.SetWaterLevelFullUsec)
            .WriteUInt16(value)
            .ReadOK();
    }

    public ShowerCodes Ping(CancellationToken cancellationToken = default)
    {
        return CreateRequestBuilder()
            .WriteHeader(ShowerCodes.Ping)
            .ReadCodeAsync(cancellationToken).GetAwaiter().GetResult();
    }

    public Task<ShowerCodes> PingAsync(CancellationToken ct)
    {
        return CreateRequestBuilder()
            .WriteHeader(ShowerCodes.Ping)
            .ReadCodeAsync(ct);
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
        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.SetWaterLevelEmptyUsec)
            .WriteUInt16(value)
            .ReadOK();
    }

    public Task<float> GetWaterTemperatureAsync(CancellationToken ct)
    {
        return RequestAsync<float>(ShowerCodes.GetWaterTemperature, ct);
    }

    public float GetInternalTemperature()
    {
        return Request<float>(ShowerCodes.GetWaterTemperature);
    }

    public void SetMinimumWaterHeatingLevel(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.SetMinimumWaterHeatingLevel)
            .WriteUInt8(value)
            .ReadOK();
    }

    public async Task<bool> GetHeaterEnabledAsync(CancellationToken ct)
    {
        var value = await RequestAsync<byte>(ShowerCodes.GetHeaterEnabled, ct).ConfigureAwait(false);
        return Convert.ToBoolean(value);
    }

    public bool GetHeaterEnabled()
    {
        var value = Request<byte>(ShowerCodes.GetHeaterEnabled);
        return Convert.ToBoolean(value);
    }

    public void SetAbsoluteHeatingTimeLimit(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.SetAbsoluteHeatingTimeLimit)
            .WriteUInt8(value)
            .ReadOK();
    }

    public Task<byte> GetMinutesLeftAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCodes.GetMinutesLeft, ct);
    }

    public byte GetMinutesLeft()
    {
        return Request<byte>(ShowerCodes.GetMinutesLeft);
    }

    public void SetHeatingTimeLimit(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.SetHeatingTimeLimit)
            .WriteUInt8(value)
            .ReadOK();
    }

    public async Task<ushort> GetWaterLevelAsync(CancellationToken ct)
    {
        return await RequestAsync<ushort>(ShowerCodes.GetWaterLevel, ct).ConfigureAwait(false);
    }

    public byte GetWaterPercent()
    {
        return Request<byte>(ShowerCodes.GetWaterPercent);
    }

    public void SetLightBrightness(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.SetLightBrightness)
            .WriteUInt8(value)
            .ReadOK();
    }

    public Task<byte> GetWaterPercentAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCodes.GetWaterPercent, ct);
    }

    public SetupModel GetTempChart()
    {
        _writer.Write(ShowerCodes.GetTemperatureChart);
        _writer.WriteEnd();
        _writer.Send();

        Span<byte> data = stackalloc byte[SetupModel.STEP_COUNT];
        _socketStream.ReadExactly(data);

        var model = new SetupModel();
        model.ParseTemp(data);
        return model;
    }

    public void SetWiFiPower(byte power)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.SetWiFiPower)
            .WriteUInt8(power)
            .ReadOK();
    }

    public void SetWaterLevelMeasureInterval(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.SetWaterLevelMeasureInterval)
            .WriteUInt8(value)
            .ReadOK();
    }

    public Task<ushort> GetWaterLevelEmptyAsync(CancellationToken ct)
    {
        return RequestAsync<ushort>(ShowerCodes.GetWaterLevelEmptyUsec, ct);
    }

    public Task<ushort> GetWaterLevelFullAsync(CancellationToken ct)
    {
        return RequestAsync<ushort>(ShowerCodes.GetWaterLevelFullUsec, ct);
    }

    public void SetWaterLevelMedianBufferSize(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.SetWaterLevelMedianBufferSize)
            .WriteUInt8(value)
            .ReadOK();
    }

    public async Task<T> RequestAsync<T>(ShowerCodes code, CancellationToken ct) where T : struct
    {
        _writer.Write(code);
        _writer.WriteEnd();

        var unmanagedSize = Marshal.SizeOf<T>();
        var buffer = new byte[unmanagedSize];

        await _writer.SendAsync(ct).ConfigureAwait(false);
        await _socketStream.ReadExactlyAsync(buffer, ct).ConfigureAwait(false);

        return MySerializer.Read<T>(buffer);
    }

    public void SetWaterLevelAverageBufferSize(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.SetWaterLevelAverageBufferSize)
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
        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.SetWaterValveCutOffPercent)
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
        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.SetTemperatureSensorInternalTempAverageSize)
            .WriteUInt8(value)
            .ReadOK();
    }

    /// <summary>
    /// 
    /// </summary>
    public Task<float> GetWaterTankVolumeLitreAsync(CancellationToken ct)
    {
        return RequestAsync<float>(ShowerCodes.GetWaterTankVolumeLitre, ct);
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
        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.SetWaterTankVolumeLitre)
            .WriteFloat(value)
            .ReadOK();
    }

    /// <summary>
    /// 
    /// </summary>
    public Task<float> GetWaterHeaterPowerKWattAsync(CancellationToken ct)
    {
        return RequestAsync<float>(ShowerCodes.GetWaterHeaterPowerKWatt, ct);
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
    public Task<byte> GetWaterLevelMedianSizeAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCodes.GetWaterLevelMedianBufferSize, ct);
    }

    public void Save()
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.Save)
            .ReadOK();
    }

    /// <summary>
    /// Возвращает размер фильтра 'скользящее среднее' для медианы уровня воды.
    /// </summary>
    public Task<byte> GetWaterLevelAverageSizeAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCodes.GetWaterLevelAverageBufferSize, ct);
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
        ArgumentNullException.ThrowIfNull(ssid);
        ArgumentNullException.ThrowIfNull(password);

        if (ssid.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(ssid));

        if (password.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(password));

        // Полная команда на контроллере: AT+CWJAP_CUR="abc","0123456789"
        var espCommand = ESP8266Helper.CreateSetCurrentAPCommand(ssid, password, bsid);

        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.SetCurAP)
            .WriteString(espCommand)
            .ReadOK();
    }

    public void SetDefAP(string ssid, string password, PhysicalAddress? bsid = null)
    {
        ArgumentNullException.ThrowIfNull(ssid);
        ArgumentNullException.ThrowIfNull(password);

        if (ssid.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(ssid));

        if (password.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(password));

        // Полная команда на контроллере: AT+CWJAP_CUR="abc","0123456789"
        var espCommand = ESP8266Helper.CreateSetCurrentAPCommand(ssid, password, bsid);

        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.SetDefAP)
            .WriteString(espCommand)
            .ReadOK();
    }

    public byte GetWaterLevelErrorThreshhold()
    {
        return Request<byte>(ShowerCodes.GetWaterLevelErrorThreshold);
    }

    public void SetWaterLevelErrorThreshold(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.SetWaterLevelErrorThreshold)
            .WriteUInt8(value)
            .ReadOK();
    }

    public void Reset()
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.Reset)
            .ReadOK();
    }

    public void SetWaterHeaterPowerKWatt(float value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.SetWaterHeaterPowerKWatt)
            .WriteFloat(value)
            .ReadOK();
    }

    public void SetButtonTimeMsec(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.SetButtonTimeMsec)
            .WriteUInt8(value)
            .ReadOK();
    }

    public ushort GetButtonLongPressTimeMsec()
    {
        return Request<ushort>(ShowerCodes.GetButtonLongPressTimeMsec);
    }

    public void SetButtonLongPressTimeMsec(ushort value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCodes.SetButtonLongPressTimeMsec)
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

    public async Task<TimeSpan> GetWaterLevelMeasureIntervalAsync(CancellationToken ct)
    {
        var value = await RequestAsync<byte>(ShowerCodes.GetWaterLevelMeasureInterval, ct).ConfigureAwait(false);
        return TimeSpan.FromMilliseconds(value);
    }

    public byte GetWaterValveCutOffPercent()
    {
        return Request<byte>(ShowerCodes.GetWaterValveCutOffPercent);
    }

    public byte GetTemperatureSensorInternalTempAverageSize()
    {
        return Request<byte>(ShowerCodes.GetTemperatureSensorInternalTempAverageSize);
    }

    public byte GetButtonPressTimeMsec()
    {
        return Request<byte>(ShowerCodes.GetButtonTimeMsec);
    }

    private RequestBuilder CreateRequestBuilder()
    {
        return new RequestBuilder(this, _writer, _reader, _socketStream);
    }
}
