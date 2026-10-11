using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Shower.Domain.RpcClient;

/// <remarks>
/// Не потокобезопасный. Если обмен с контроллером прервался (ошибка сети, таймаут, отмена),
/// соединение закрывается: в сокете мог остаться недочитанный ответ.
/// </remarks>
public sealed class ShowerConnection : IShowerConnection, IDisposable, IAsyncDisposable
{
    private readonly Stream _stream;
    private readonly RequestBuffer _request = new();
    private readonly byte[] _response = new byte[64];
    private bool _disposed;

    public ShowerConnection(TcpClient connection)
    {
        ArgumentNullException.ThrowIfNull(connection);

        _stream = connection.GetStream();
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _stream.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return ValueTask.CompletedTask;
        }

        _disposed = true;
        return _stream.DisposeAsync();
    }

    /// <inheritdoc/>
    public ShowerConnection Write<T>(ShowerCode code, Action<T> callback)
    {
        return this;
    }

    /// <summary>
    /// Запрашивает значение фиксированного размера.
    /// </summary>
    /// <typeparam name="T">Тип ответа в протоколе: byte, short, ushort, float. Для логических значений используйте byte.</typeparam>
    public T Request<T>(ShowerCode code) where T : unmanaged
    {
        _request.Begin(code);
        return MemoryMarshal.Read<T>(Exchange(Unsafe.SizeOf<T>()));
    }

    /// <inheritdoc cref="Request{T}(ShowerCode)"/>
    public async Task<TResult> RequestAsync<TResult>(ShowerCode code, CancellationToken ct) where TResult : unmanaged
    {
        _request.Begin(code);
        var response = await ExchangeAsync(Unsafe.SizeOf<TResult>(), ct).ConfigureAwait(false);
        return MemoryMarshal.Read<TResult>(response.Span);
    }

    private void Execute(ShowerCode code)
    {
        _request.Begin(code);
        CheckOK(Exchange(1));
    }

    private void Execute(ShowerCode code, byte value)
    {
        _request.Begin(code);
        _request.Write(value);
        CheckOK(Exchange(1));
    }

    private void Execute(ShowerCode code, ushort value)
    {
        _request.Begin(code);
        _request.Write(value);
        CheckOK(Exchange(1));
    }

    private void Execute(ShowerCode code, float value)
    {
        _request.Begin(code);
        _request.Write(value);
        CheckOK(Exchange(1));
    }

    private void Execute(ShowerCode code, string ascii)
    {
        _request.Begin(code);
        _request.WriteAscii(ascii);
        CheckOK(Exchange(1));
    }

    private async Task ExecuteAsync(ShowerCode code, CancellationToken ct)
    {
        _request.Begin(code);
        CheckOK((await ExchangeAsync(1, ct).ConfigureAwait(false)).Span);
    }

    private async Task ExecuteAsync(ShowerCode code, byte value, CancellationToken ct)
    {
        _request.Begin(code);
        _request.Write(value);
        CheckOK((await ExchangeAsync(1, ct).ConfigureAwait(false)).Span);
    }

    private async Task ExecuteAsync(ShowerCode code, ushort value, CancellationToken ct)
    {
        _request.Begin(code);
        _request.Write(value);
        CheckOK((await ExchangeAsync(1, ct).ConfigureAwait(false)).Span);
    }

    private async Task ExecuteAsync(ShowerCode code, float value, CancellationToken ct)
    {
        _request.Begin(code);
        _request.Write(value);
        CheckOK((await ExchangeAsync(1, ct).ConfigureAwait(false)).Span);
    }

    private async Task ExecuteAsync(ShowerCode code, string ascii, CancellationToken ct)
    {
        _request.Begin(code);
        _request.WriteAscii(ascii);
        CheckOK((await ExchangeAsync(1, ct).ConfigureAwait(false)).Span);
    }

    /// <summary>
    /// Отправляет подготовленный запрос и читает ответ заданного размера.
    /// </summary>
    private ReadOnlySpan<byte> Exchange(int responseSize)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        var request = _request.Complete();
        var response = GetResponseBuffer(responseSize);
        try
        {
            _stream.Write(request.Span);
            _stream.ReadExactly(response.Span);
        }
        catch
        {
            Dispose();
            throw;
        }
        return response.Span;
    }

    /// <inheritdoc cref="Exchange(int)"/>
    private async ValueTask<ReadOnlyMemory<byte>> ExchangeAsync(int responseSize, CancellationToken ct)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        var request = _request.Complete();
        var response = GetResponseBuffer(responseSize);
        try
        {
            await _stream.WriteAsync(request, ct).ConfigureAwait(false);
            await _stream.ReadExactlyAsync(response, ct).ConfigureAwait(false);
        }
        catch
        {
            await DisposeAsync().ConfigureAwait(false);
            throw;
        }
        return response;
    }

    private Memory<byte> GetResponseBuffer(int size)
    {
        return _response.Length >= size
            ? _response.AsMemory(0, size)
            : new byte[size];
    }

    /// <exception cref="InvalidDataException"/>
    private static void CheckOK(ReadOnlySpan<byte> response)
    {
        var code = (ShowerCode)response[0];
        if (code != ShowerCode.OK)
        {
            throw new InvalidDataException($"Ожидался ответ 'OK' но получено значение '{code}'");
        }
    }

    public ShowerCode Ping()
    {
        return (ShowerCode)Request<byte>(ShowerCode.Ping);
    }

    public async Task<ShowerCode> PingAsync(CancellationToken ct)
    {
        return (ShowerCode)await RequestAsync<byte>(ShowerCode.Ping, ct).ConfigureAwait(false);
    }

    public void Save()
    {
        Execute(ShowerCode.Save);
    }

    public Task SaveAsync(CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.Save, ct);
    }

    public void Reset()
    {
        Execute(ShowerCode.Reset);
    }

    public Task ResetAsync(CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.Reset, ct);
    }

    public bool GetWatchDogWasReset()
    {
        return Convert.ToBoolean(Request<byte>(ShowerCode.GetWatchDogWasReset));
    }

    public async Task<bool> GetWatchDogWasResetAsync(CancellationToken ct)
    {
        return Convert.ToBoolean(await RequestAsync<byte>(ShowerCode.GetWatchDogWasReset, ct).ConfigureAwait(false));
    }

    public SetupModel GetTempChart()
    {
        _request.Begin(ShowerCode.GetTemperatureChart);
        return ParseTempChart(Exchange(SetupModel.STEP_COUNT));
    }

    public async Task<SetupModel> GetTempChartAsync(CancellationToken ct)
    {
        _request.Begin(ShowerCode.GetTemperatureChart);
        var response = await ExchangeAsync(SetupModel.STEP_COUNT, ct).ConfigureAwait(false);
        return ParseTempChart(response.Span);
    }

    private static SetupModel ParseTempChart(ReadOnlySpan<byte> data)
    {
        var model = new SetupModel();
        model.ParseTemp(data);
        return model;
    }

    public byte GetWiFiPower()
    {
        return Request<byte>(ShowerCode.GetWiFiPower);
    }

    public Task<byte> GetWiFiPowerAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetWiFiPower, ct);
    }

    public void SetWiFiPower(byte power)
    {
        Execute(ShowerCode.SetWiFiPower, power);
    }

    public Task SetWiFiPowerAsync(byte power, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWiFiPower, power, ct);
    }

    public void SetCurAP(string ssid, string password, PhysicalAddress? bsid = null)
    {
        Execute(ShowerCode.SetCurAP, CreateAccessPointCommand(ssid, password, bsid));
    }

    public Task SetCurAPAsync(string ssid, string password, PhysicalAddress? bsid, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetCurAP, CreateAccessPointCommand(ssid, password, bsid), ct);
    }

    public void SetDefAP(string ssid, string password, PhysicalAddress? bsid = null)
    {
        Execute(ShowerCode.SetDefAP, CreateAccessPointCommand(ssid, password, bsid));
    }

    public Task SetDefAPAsync(string ssid, string password, PhysicalAddress? bsid, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetDefAP, CreateAccessPointCommand(ssid, password, bsid), ct);
    }

    private static string CreateAccessPointCommand(string ssid, string password, PhysicalAddress? bsid)
    {
        ArgumentNullException.ThrowIfNull(ssid);
        ArgumentNullException.ThrowIfNull(password);

        if (ssid.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(ssid));

        if (password.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(password));

        // Полная команда на контроллере: AT+CWJAP_CUR="abc","0123456789"
        return ESP8266Helper.CreateSetCurrentAPCommand(ssid, password, bsid);
    }

    public float GetInternalTemperature()
    {
        return Request<float>(ShowerCode.GetWaterTemperature);
    }

    public Task<float> GetWaterTemperatureAsync(CancellationToken ct)
    {
        return RequestAsync<float>(ShowerCode.GetWaterTemperature, ct);
    }

    public bool GetHeaterEnabled()
    {
        return Convert.ToBoolean(Request<byte>(ShowerCode.GetHeaterEnabled));
    }

    public async Task<bool> GetHeaterEnabledAsync(CancellationToken ct)
    {
        return Convert.ToBoolean(await RequestAsync<byte>(ShowerCode.GetHeaterEnabled, ct).ConfigureAwait(false));
    }

    public byte GetMinutesLeft()
    {
        return Request<byte>(ShowerCode.GetMinutesLeft);
    }

    public Task<byte> GetMinutesLeftAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetMinutesLeft, ct);
    }

    public byte GetMinimumWaterHeatingLevel()
    {
        return Request<byte>(ShowerCode.GetMinimumWaterHeatingLevel);
    }

    public Task<byte> GetMinimumWaterHeatingLevelAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetMinimumWaterHeatingLevel, ct);
    }

    public void SetMinimumWaterHeatingLevel(byte value)
    {
        Execute(ShowerCode.SetMinimumWaterHeatingLevel, value);
    }

    public Task SetMinimumWaterHeatingLevelAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetMinimumWaterHeatingLevel, value, ct);
    }

    public byte GetAbsoluteHeatingTimeLimit()
    {
        return Request<byte>(ShowerCode.GetAbsoluteHeatingTimeLimit);
    }

    public Task<byte> GetAbsoluteHeatingTimeLimitAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetAbsoluteHeatingTimeLimit, ct);
    }

    public void SetAbsoluteHeatingTimeLimit(byte value)
    {
        Execute(ShowerCode.SetAbsoluteHeatingTimeLimit, value);
    }

    public Task SetAbsoluteHeatingTimeLimitAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetAbsoluteHeatingTimeLimit, value, ct);
    }

    public byte GetHeatingTimeLimit()
    {
        return Request<byte>(ShowerCode.GetHeatingTimeLimit);
    }

    public Task<byte> GetHeatingTimeLimitAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetHeatingTimeLimit, ct);
    }

    public void SetHeatingTimeLimit(byte value)
    {
        Execute(ShowerCode.SetHeatingTimeLimit, value);
    }

    public Task SetHeatingTimeLimitAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetHeatingTimeLimit, value, ct);
    }

    public byte GetTemperatureSensorInternalTempAverageSize()
    {
        return Request<byte>(ShowerCode.GetTemperatureSensorInternalTempAverageSize);
    }

    public Task<byte> GetTemperatureSensorInternalTempAverageSizeAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetTemperatureSensorInternalTempAverageSize, ct);
    }

    public void SetTempSensorInternalTempAverageSize(byte value)
    {
        Execute(ShowerCode.SetTemperatureSensorInternalTempAverageSize, value);
    }

    public Task SetTempSensorInternalTempAverageSizeAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetTemperatureSensorInternalTempAverageSize, value, ct);
    }

    public float GetWaterTankVolumeLitre()
    {
        return Request<float>(ShowerCode.GetWaterTankVolumeLitre);
    }

    public Task<float> GetWaterTankVolumeLitreAsync(CancellationToken ct)
    {
        return RequestAsync<float>(ShowerCode.GetWaterTankVolumeLitre, ct);
    }

    public void SetWaterTankVolumeLitre(float value)
    {
        Execute(ShowerCode.SetWaterTankVolumeLitre, value);
    }

    public Task SetWaterTankVolumeLitreAsync(float value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWaterTankVolumeLitre, value, ct);
    }

    public float GetWaterHeaterPowerKWatt()
    {
        return Request<float>(ShowerCode.GetWaterHeaterPowerKWatt);
    }

    public Task<float> GetWaterHeaterPowerKWattAsync(CancellationToken ct)
    {
        return RequestAsync<float>(ShowerCode.GetWaterHeaterPowerKWatt, ct);
    }

    public void SetWaterHeaterPowerKWatt(float value)
    {
        Execute(ShowerCode.SetWaterHeaterPowerKWatt, value);
    }

    public Task SetWaterHeaterPowerKWattAsync(float value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWaterHeaterPowerKWatt, value, ct);
    }

    /// <summary>
    /// Уровень воды в микросекундах, без всякой фильтрации.
    /// </summary>
    /// <returns>-1 если у датчика уровня произошел таймаут.</returns>
    public short GetWaterLevelRaw()
    {
        return Request<short>(ShowerCode.GetWaterLevelRaw);
    }

    /// <inheritdoc cref="GetWaterLevelRaw"/>
    public Task<short> GetWaterLevelRawAsync(CancellationToken ct)
    {
        return RequestAsync<short>(ShowerCode.GetWaterLevelRaw, ct);
    }

    public Task<ushort> GetWaterLevelAsync(CancellationToken ct)
    {
        return RequestAsync<ushort>(ShowerCode.GetWaterLevel, ct);
    }

    public byte GetWaterPercent()
    {
        return Request<byte>(ShowerCode.GetWaterPercent);
    }

    public Task<byte> GetWaterPercentAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetWaterPercent, ct);
    }

    public ushort GetWaterLevelFullUsec()
    {
        return Request<ushort>(ShowerCode.GetWaterLevelFullUsec);
    }

    public Task<ushort> GetWaterLevelFullAsync(CancellationToken ct)
    {
        return RequestAsync<ushort>(ShowerCode.GetWaterLevelFullUsec, ct);
    }

    public void SetWaterLevelFullUsec(ushort value)
    {
        Execute(ShowerCode.SetWaterLevelFullUsec, value);
    }

    public Task SetWaterLevelFullUsecAsync(ushort value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWaterLevelFullUsec, value, ct);
    }

    public ushort GetWaterLevelEmptyUsec()
    {
        return Request<ushort>(ShowerCode.GetWaterLevelEmptyUsec);
    }

    public Task<ushort> GetWaterLevelEmptyAsync(CancellationToken ct)
    {
        return RequestAsync<ushort>(ShowerCode.GetWaterLevelEmptyUsec, ct);
    }

    public void SetWaterLevelEmptyUsec(ushort value)
    {
        Execute(ShowerCode.SetWaterLevelEmptyUsec, value);
    }

    public Task SetWaterLevelEmptyUsecAsync(ushort value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWaterLevelEmptyUsec, value, ct);
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

    public void SetWaterLevelMeasureInterval(byte value)
    {
        Execute(ShowerCode.SetWaterLevelMeasureInterval, value);
    }

    public Task SetWaterLevelMeasureIntervalAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWaterLevelMeasureInterval, value, ct);
    }

    /// <summary>
    /// Возвращает размер буфера медианного фильтра.
    /// </summary>
    public byte GetWaterLevelMedianSize()
    {
        return Request<byte>(ShowerCode.GetWaterLevelMedianBufferSize);
    }

    /// <inheritdoc cref="GetWaterLevelMedianSize"/>
    public Task<byte> GetWaterLevelMedianSizeAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetWaterLevelMedianBufferSize, ct);
    }

    public void SetWaterLevelMedianBufferSize(byte value)
    {
        Execute(ShowerCode.SetWaterLevelMedianBufferSize, value);
    }

    public Task SetWaterLevelMedianBufferSizeAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWaterLevelMedianBufferSize, value, ct);
    }

    /// <summary>
    /// Возвращает размер фильтра 'скользящее среднее' для медианы уровня воды.
    /// </summary>
    public byte GetWaterLevelAverageFilterSize()
    {
        return Request<byte>(ShowerCode.GetWaterLevelAverageBufferSize);
    }

    /// <inheritdoc cref="GetWaterLevelAverageFilterSize"/>
    public Task<byte> GetWaterLevelAverageSizeAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetWaterLevelAverageBufferSize, ct);
    }

    public void SetWaterLevelAverageBufferSize(byte value)
    {
        Execute(ShowerCode.SetWaterLevelAverageBufferSize, value);
    }

    public Task SetWaterLevelAverageBufferSizeAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWaterLevelAverageBufferSize, value, ct);
    }

    public byte GetWaterLevelErrorThreshhold()
    {
        return Request<byte>(ShowerCode.GetWaterLevelErrorThreshold);
    }

    public Task<byte> GetWaterLevelErrorThresholdAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetWaterLevelErrorThreshold, ct);
    }

    public void SetWaterLevelErrorThreshold(byte value)
    {
        Execute(ShowerCode.SetWaterLevelErrorThreshold, value);
    }

    public Task SetWaterLevelErrorThresholdAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWaterLevelErrorThreshold, value, ct);
    }

    public byte GetWaterValveCutOffPercent()
    {
        return Request<byte>(ShowerCode.GetWaterValveCutOffPercent);
    }

    public Task<byte> GetWaterValveCutOffPercentAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetWaterValveCutOffPercent, ct);
    }

    public void SetWaterValveCutOffPercent(byte value)
    {
        Execute(ShowerCode.SetWaterValveCutOffPercent, value);
    }

    public Task SetWaterValveCutOffPercentAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWaterValveCutOffPercent, value, ct);
    }

    public byte GetLightBrightness()
    {
        return Request<byte>(ShowerCode.GetLightBrightness);
    }

    public Task<byte> GetLightBrightnessAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetLightBrightness, ct);
    }

    public void SetLightBrightness(byte value)
    {
        Execute(ShowerCode.SetLightBrightness, value);
    }

    public Task SetLightBrightnessAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetLightBrightness, value, ct);
    }

    public byte GetButtonPressTimeMsec()
    {
        return Request<byte>(ShowerCode.GetButtonTimeMsec);
    }

    public Task<byte> GetButtonPressTimeMsecAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetButtonTimeMsec, ct);
    }

    public void SetButtonTimeMsec(byte value)
    {
        Execute(ShowerCode.SetButtonTimeMsec, value);
    }

    public Task SetButtonTimeMsecAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetButtonTimeMsec, value, ct);
    }

    public ushort GetButtonLongPressTimeMsec()
    {
        return Request<ushort>(ShowerCode.GetButtonLongPressTimeMsec);
    }

    public Task<ushort> GetButtonLongPressTimeMsecAsync(CancellationToken ct)
    {
        return RequestAsync<ushort>(ShowerCode.GetButtonLongPressTimeMsec, ct);
    }

    public void SetButtonLongPressTimeMsec(ushort value)
    {
        Execute(ShowerCode.SetButtonLongPressTimeMsec, value);
    }

    public Task SetButtonLongPressTimeMsecAsync(ushort value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetButtonLongPressTimeMsec, value, ct);
    }
}
