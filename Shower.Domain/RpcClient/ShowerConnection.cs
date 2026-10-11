using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace Shower.Domain.RpcClient;

/// <remarks>Не потокобезопасный</remarks>
public sealed class ShowerConnection : IShowerConnection, IDisposable, IAsyncDisposable
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
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _reader.Dispose();
        _writer.Dispose();
        _socketStream.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _reader.Dispose();
        await _writer.DisposeAsync().ConfigureAwait(false);
        await _socketStream.DisposeAsync().ConfigureAwait(false);
    }

    public T Request<T>(ShowerCode code) where T : struct
    {
        _writer.WriteHeader(code);
        _writer.WriteEndOfMessage();
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
    public ShowerConnection Write<T>(ShowerCode code, Action<T> callback)
    {
        return this;
    }

    /// <summary>
    /// Уровень воды в микросекундах, без всякой фильтрации.
    /// </summary>
    /// <returns>-1 если у датчика уровня произошел таймаут.</returns>
    public Task<short> GetWaterLevelRawAsync(CancellationToken cancellationToken = default)
    {
        return RequestAsync<short>(ShowerCode.GetWaterLevelRaw, cancellationToken);
    }

    public void SetWaterLevelFullUsec(ushort value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCode.SetWaterLevelFullUsec)
            .WriteUInt16(value)
            .SendAndReadOK();
    }

    public Task<ShowerCode> PingAsync(CancellationToken ct)
    {
        return CreateRequestBuilder()
            .WriteHeader(ShowerCode.Ping)
            .ReadHeaderAsync(ct)
            .AsTask();
    }

    /// <summary>
    /// Уровень воды в микросекундах, без всякой фильтрации.
    /// </summary>
    /// <returns>-1 если у датчика уровня произошел таймаут.</returns>
    public short GetWaterLevelRaw()
    {
        return Request<short>(ShowerCode.GetWaterLevelRaw);
    }

    public void SetWaterLevelEmptyUsec(ushort value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCode.SetWaterLevelEmptyUsec)
            .WriteUInt16(value)
            .SendAndReadOK();
    }

    public Task<float> GetWaterTemperatureAsync(CancellationToken ct)
    {
        return RequestAsync<float>(ShowerCode.GetWaterTemperature, ct);
    }

    public float GetInternalTemperature()
    {
        return Request<float>(ShowerCode.GetWaterTemperature);
    }

    public void SetMinimumWaterHeatingLevel(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCode.SetMinimumWaterHeatingLevel)
            .WriteUInt8(value)
            .SendAndReadOK();
    }

    public async Task<bool> GetHeaterEnabledAsync(CancellationToken ct)
    {
        var value = await RequestAsync<byte>(ShowerCode.GetHeaterEnabled, ct).ConfigureAwait(false);
        return Convert.ToBoolean(value);
    }

    public bool GetHeaterEnabled()
    {
        var value = Request<byte>(ShowerCode.GetHeaterEnabled);
        return Convert.ToBoolean(value);
    }

    public void SetAbsoluteHeatingTimeLimit(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCode.SetAbsoluteHeatingTimeLimit)
            .WriteUInt8(value)
            .SendAndReadOK();
    }

    public Task<byte> GetMinutesLeftAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetMinutesLeft, ct);
    }

    public byte GetMinutesLeft()
    {
        return Request<byte>(ShowerCode.GetMinutesLeft);
    }

    public void SetHeatingTimeLimit(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCode.SetHeatingTimeLimit)
            .WriteUInt8(value)
            .SendAndReadOK();
    }

    public async Task<ushort> GetWaterLevelAsync(CancellationToken ct)
    {
        return await RequestAsync<ushort>(ShowerCode.GetWaterLevel, ct).ConfigureAwait(false);
    }

    public byte GetWaterPercent()
    {
        return Request<byte>(ShowerCode.GetWaterPercent);
    }

    public void SetLightBrightness(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCode.SetLightBrightness)
            .WriteUInt8(value)
            .SendAndReadOK();
    }

    public Task<byte> GetWaterPercentAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetWaterPercent, ct);
    }

    public SetupModel GetTempChart()
    {
        _writer.WriteHeader(ShowerCode.GetTemperatureChart);
        _writer.WriteEndOfMessage();
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
            .WriteHeader(ShowerCode.SetWiFiPower)
            .WriteUInt8(power)
            .SendAndReadOK();
    }

    public void SetWaterLevelMeasureInterval(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCode.SetWaterLevelMeasureInterval)
            .WriteUInt8(value)
            .SendAndReadOK();
    }

    public Task<ushort> GetWaterLevelEmptyAsync(CancellationToken ct)
    {
        return RequestAsync<ushort>(ShowerCode.GetWaterLevelEmptyUsec, ct);
    }

    public Task<ushort> GetWaterLevelFullAsync(CancellationToken ct)
    {
        return RequestAsync<ushort>(ShowerCode.GetWaterLevelFullUsec, ct);
    }

    public void SetWaterLevelMedianBufferSize(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCode.SetWaterLevelMedianBufferSize)
            .WriteUInt8(value)
            .SendAndReadOK();
    }

    public async Task<T> RequestAsync<T>(ShowerCode code, CancellationToken ct) where T : struct
    {
        _writer.WriteHeader(code);
        _writer.WriteEndOfMessage();

        var unmanagedSize = Marshal.SizeOf<T>();
        var buffer = new byte[unmanagedSize];

        await _writer.SendAsync(ct).ConfigureAwait(false);
        await _socketStream.ReadExactlyAsync(buffer, ct).ConfigureAwait(false);

        return MySerializer.Read<T>(buffer);
    }

    public void SetWaterLevelAverageBufferSize(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCode.SetWaterLevelAverageBufferSize)
            .WriteUInt8(value)
            .SendAndReadOK();
    }

    public bool GetWatchDogWasReset()
    {
        var iwd = Request<byte>(ShowerCode.GetWatchDogWasReset);
        var result = Convert.ToBoolean(iwd);
        return result;
    }

    public void SetWaterValveCutOffPercent(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCode.SetWaterValveCutOffPercent)
            .WriteUInt8(value)
            .SendAndReadOK();
    }

    /// <summary>
    /// 
    /// </summary>
    public float GetWaterTankVolumeLitre()
    {
        return Request<float>(ShowerCode.GetWaterTankVolumeLitre);
    }

    public void SetTempSensorInternalTempAverageSize(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCode.SetTemperatureSensorInternalTempAverageSize)
            .WriteUInt8(value)
            .SendAndReadOK();
    }

    /// <summary>
    /// 
    /// </summary>
    public Task<float> GetWaterTankVolumeLitreAsync(CancellationToken ct)
    {
        return RequestAsync<float>(ShowerCode.GetWaterTankVolumeLitre, ct);
    }

    /// <summary>
    /// 
    /// </summary>
    public float GetWaterHeaterPowerKWatt()
    {
        return Request<float>(ShowerCode.GetWaterHeaterPowerKWatt);
    }

    public void SetWaterTankVolumeLitre(float value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCode.SetWaterTankVolumeLitre)
            .WriteFloat(value)
            .SendAndReadOK();
    }

    /// <summary>
    /// 
    /// </summary>
    public Task<float> GetWaterHeaterPowerKWattAsync(CancellationToken ct)
    {
        return RequestAsync<float>(ShowerCode.GetWaterHeaterPowerKWatt, ct);
    }

    /// <summary>
    /// Возвращает размер буфера медианного фильтра.
    /// </summary>
    public byte GetWaterLevelMedianSize()
    {
        return Request<byte>(ShowerCode.GetWaterLevelMedianBufferSize);
    }

    /// <summary>
    /// Возвращает размер буфера медианного фильтра.
    /// </summary>
    public Task<byte> GetWaterLevelMedianSizeAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetWaterLevelMedianBufferSize, ct);
    }

    public void Save()
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCode.Save)
            .SendAndReadOK();
    }

    /// <summary>
    /// Возвращает размер фильтра 'скользящее среднее' для медианы уровня воды.
    /// </summary>
    public Task<byte> GetWaterLevelAverageSizeAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetWaterLevelAverageBufferSize, ct);
    }

    /// <summary>
    /// Возвращает размер буфера фильтра 'скользящее среднее'.
    /// </summary>
    public byte GetWaterLevelAverageFilterSize()
    {
        return Request<byte>(ShowerCode.GetWaterLevelAverageBufferSize);
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
            .WriteHeader(ShowerCode.SetCurAP)
            .WriteString(espCommand)
            .SendAndReadOK();
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
            .WriteHeader(ShowerCode.SetDefAP)
            .WriteString(espCommand)
            .SendAndReadOK();
    }

    public byte GetWaterLevelErrorThreshhold()
    {
        return Request<byte>(ShowerCode.GetWaterLevelErrorThreshold);
    }

    public void SetWaterLevelErrorThreshold(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCode.SetWaterLevelErrorThreshold)
            .WriteUInt8(value)
            .SendAndReadOK();
    }

    public void Reset()
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCode.Reset)
            .SendAndReadOK();
    }

    public void SetWaterHeaterPowerKWatt(float value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCode.SetWaterHeaterPowerKWatt)
            .WriteFloat(value)
            .SendAndReadOK();
    }

    public void SetButtonTimeMsec(byte value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCode.SetButtonTimeMsec)
            .WriteUInt8(value)
            .SendAndReadOK();
    }

    public ushort GetButtonLongPressTimeMsec()
    {
        return Request<ushort>(ShowerCode.GetButtonLongPressTimeMsec);
    }

    public void SetButtonLongPressTimeMsec(ushort value)
    {
        CreateRequestBuilder()
            .WriteHeader(ShowerCode.SetButtonLongPressTimeMsec)
            .WriteUInt16(value)
            .SendAndReadOK();
    }

    public Task<ushort> GetWaterLevelFullUsec(CancellationToken ct)
    {
        return RequestAsync<ushort>(ShowerCode.GetWaterLevelFullUsec, ct);
    }

    public ushort GetWaterLevelEmptyUsec()
    {
        return Request<ushort>(ShowerCode.GetWaterLevelEmptyUsec);
    }

    public byte GetMinimumWaterHeatingLevel()
    {
        return Request<byte>(ShowerCode.GetMinimumWaterHeatingLevel);
    }

    public byte GetAbsoluteHeatingTimeLimit()
    {
        return Request<byte>(ShowerCode.GetAbsoluteHeatingTimeLimit);
    }

    public byte GetHeatingTimeLimit()
    {
        return Request<byte>(ShowerCode.GetHeatingTimeLimit);
    }

    public byte GetLightBrightness()
    {
        return Request<byte>(ShowerCode.GetLightBrightness);
    }

    public byte GetWiFiPower()
    {
        return Request<byte>(ShowerCode.GetWiFiPower);
    }

    public byte GetWaterLevelMeasureInterval()
    {
        return Request<byte>(ShowerCode.GetWaterLevelMeasureInterval);
    }

    public async Task<TimeSpan> GetWaterLevelMeasureIntervalAsync(CancellationToken ct)
    {
        var value = await RequestAsync<byte>(ShowerCode.GetWaterLevelMeasureInterval, ct).ConfigureAwait(false);
        return TimeSpan.FromMilliseconds(value);
    }

    public byte GetWaterValveCutOffPercent()
    {
        return Request<byte>(ShowerCode.GetWaterValveCutOffPercent);
    }

    public byte GetTemperatureSensorInternalTempAverageSize()
    {
        return Request<byte>(ShowerCode.GetTemperatureSensorInternalTempAverageSize);
    }

    public byte GetButtonPressTimeMsec()
    {
        return Request<byte>(ShowerCode.GetButtonTimeMsec);
    }

    private RequestBuilder CreateRequestBuilder()
    {
        return new RequestBuilder(this, _writer, _reader, _socketStream);
    }
}
