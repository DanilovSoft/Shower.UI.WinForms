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
    /// <typeparam name="TResult">Тип ответа в протоколе: byte, short, ushort, float. Для логических значений используйте byte.</typeparam>
    public async Task<TResult> RequestAsync<TResult>(ShowerCode code, CancellationToken ct) where TResult : unmanaged
    {
        _request.Begin(code);
        var response = await ExchangeAsync(Unsafe.SizeOf<TResult>(), ct).ConfigureAwait(false);
        return MemoryMarshal.Read<TResult>(response.Span);
    }

    /// <summary>
    /// Создаёт пакет запросов, которые отправляются за один раундтрип.
    /// </summary>
    public ShowerBatch CreateBatch()
    {
        return new ShowerBatch(this);
    }

    internal async Task ExecuteBatchAsync(IReadOnlyList<BatchItem> items, CancellationToken ct)
    {
        if (items.Count == 0)
        {
            return;
        }

        _request.Begin(items[0].Code);
        var responseSize = items[0].ResponseSize;
        for (var i = 1; i < items.Count; i++)
        {
            _request.Append(items[i].Code);
            responseSize += items[i].ResponseSize;
        }

        var response = await ExchangeAsync(responseSize, ct).ConfigureAwait(false);

        var offset = 0;
        foreach (var item in items)
        {
            item.Read(response.Span.Slice(offset, item.ResponseSize));
            offset += item.ResponseSize;
        }
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

    public async Task<ShowerCode> PingAsync(CancellationToken ct)
    {
        return (ShowerCode)await RequestAsync<byte>(ShowerCode.Ping, ct).ConfigureAwait(false);
    }

    public Task SaveAsync(CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.Save, ct);
    }

    public Task ResetAsync(CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.Reset, ct);
    }

    public async Task<bool> GetWatchDogWasResetAsync(CancellationToken ct)
    {
        return Convert.ToBoolean(await RequestAsync<byte>(ShowerCode.GetWatchDogWasReset, ct).ConfigureAwait(false));
    }

    public Task<byte> GetWiFiPowerAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetWiFiPower, ct);
    }

    public Task SetWiFiPowerAsync(byte power, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWiFiPower, power, ct);
    }

    public Task SetCurAPAsync(string ssid, string password, PhysicalAddress? bsid, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetCurAP, CreateAccessPointCommand(ssid, password, bsid), ct);
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

    public Task<float> GetWaterTemperatureAsync(CancellationToken ct)
    {
        return RequestAsync<float>(ShowerCode.GetWaterTemperature, ct);
    }

    public async Task<bool> GetHeaterEnabledAsync(CancellationToken ct)
    {
        return Convert.ToBoolean(await RequestAsync<byte>(ShowerCode.GetHeaterEnabled, ct).ConfigureAwait(false));
    }

    public Task<byte> GetMinutesLeftAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetMinutesLeft, ct);
    }

    public Task<byte> GetMinimumWaterHeatingLevelAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetMinimumWaterHeatingLevel, ct);
    }

    public Task SetMinimumWaterHeatingLevelAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetMinimumWaterHeatingLevel, value, ct);
    }

    public Task<byte> GetAbsoluteHeatingTimeLimitAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetAbsoluteHeatingTimeLimit, ct);
    }

    public Task SetAbsoluteHeatingTimeLimitAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetAbsoluteHeatingTimeLimit, value, ct);
    }

    public Task<byte> GetHeatingTimeLimitAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetHeatingTimeLimit, ct);
    }

    public Task SetHeatingTimeLimitAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetHeatingTimeLimit, value, ct);
    }

    public Task<byte> GetTemperatureSensorInternalTempAverageSizeAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetTemperatureSensorInternalTempAverageSize, ct);
    }

    public Task SetTempSensorInternalTempAverageSizeAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetTemperatureSensorInternalTempAverageSize, value, ct);
    }

    public Task<float> GetWaterTankVolumeLitreAsync(CancellationToken ct)
    {
        return RequestAsync<float>(ShowerCode.GetWaterTankVolumeLitre, ct);
    }

    public Task SetWaterTankVolumeLitreAsync(float value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWaterTankVolumeLitre, value, ct);
    }

    public Task<float> GetWaterHeaterPowerKWattAsync(CancellationToken ct)
    {
        return RequestAsync<float>(ShowerCode.GetWaterHeaterPowerKWatt, ct);
    }

    public Task SetWaterHeaterPowerKWattAsync(float value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWaterHeaterPowerKWatt, value, ct);
    }

    /// <summary>
    /// Уровень воды в микросекундах, без всякой фильтрации.
    /// </summary>
    /// <returns>-1 если у датчика уровня произошел таймаут.</returns>
    public Task<short> GetWaterLevelRawAsync(CancellationToken ct)
    {
        return RequestAsync<short>(ShowerCode.GetWaterLevelRaw, ct);
    }

    public Task<ushort> GetWaterLevelAsync(CancellationToken ct)
    {
        return RequestAsync<ushort>(ShowerCode.GetWaterLevel, ct);
    }

    public Task<byte> GetWaterPercentAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetWaterPercent, ct);
    }

    public Task<ushort> GetWaterLevelFullAsync(CancellationToken ct)
    {
        return RequestAsync<ushort>(ShowerCode.GetWaterLevelFullUsec, ct);
    }

    public Task SetWaterLevelFullUsecAsync(ushort value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWaterLevelFullUsec, value, ct);
    }

    public Task<ushort> GetWaterLevelEmptyAsync(CancellationToken ct)
    {
        return RequestAsync<ushort>(ShowerCode.GetWaterLevelEmptyUsec, ct);
    }

    public Task SetWaterLevelEmptyUsecAsync(ushort value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWaterLevelEmptyUsec, value, ct);
    }

    public async Task<TimeSpan> GetWaterLevelMeasureIntervalAsync(CancellationToken ct)
    {
        var value = await RequestAsync<byte>(ShowerCode.GetWaterLevelMeasureInterval, ct).ConfigureAwait(false);
        return TimeSpan.FromMilliseconds(value);
    }

    public Task SetWaterLevelMeasureIntervalAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWaterLevelMeasureInterval, value, ct);
    }

    /// <summary>
    /// Возвращает размер буфера медианного фильтра.
    /// </summary>
    public Task<byte> GetWaterLevelMedianSizeAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetWaterLevelMedianBufferSize, ct);
    }

    public Task SetWaterLevelMedianBufferSizeAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWaterLevelMedianBufferSize, value, ct);
    }

    /// <summary>
    /// Возвращает размер фильтра 'скользящее среднее' для медианы уровня воды.
    /// </summary>
    public Task<byte> GetWaterLevelAverageSizeAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetWaterLevelAverageBufferSize, ct);
    }

    public Task SetWaterLevelAverageBufferSizeAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWaterLevelAverageBufferSize, value, ct);
    }

    public Task<byte> GetWaterLevelErrorThresholdAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetWaterLevelErrorThreshold, ct);
    }

    public Task SetWaterLevelErrorThresholdAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWaterLevelErrorThreshold, value, ct);
    }

    public Task<byte> GetWaterValveCutOffPercentAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetWaterValveCutOffPercent, ct);
    }

    public Task SetWaterValveCutOffPercentAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetWaterValveCutOffPercent, value, ct);
    }

    public Task<byte> GetLightBrightnessAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetLightBrightness, ct);
    }

    public Task SetLightBrightnessAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetLightBrightness, value, ct);
    }

    public Task<byte> GetButtonPressTimeMsecAsync(CancellationToken ct)
    {
        return RequestAsync<byte>(ShowerCode.GetButtonTimeMsec, ct);
    }

    public Task SetButtonTimeMsecAsync(byte value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetButtonTimeMsec, value, ct);
    }

    public Task<ushort> GetButtonLongPressTimeMsecAsync(CancellationToken ct)
    {
        return RequestAsync<ushort>(ShowerCode.GetButtonLongPressTimeMsec, ct);
    }

    public Task SetButtonLongPressTimeMsecAsync(ushort value, CancellationToken ct)
    {
        return ExecuteAsync(ShowerCode.SetButtonLongPressTimeMsec, value, ct);
    }
}
