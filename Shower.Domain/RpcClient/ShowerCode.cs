namespace Shower.Domain.RpcClient;

/// <summary>
/// Коды команд протокола контроллера душа и коды его ответов.
/// </summary>
/// <remarks>
/// Все значения передаются в little-endian. На команды Set* контроллер отвечает одним байтом <see cref="OK"/>.
/// Коды с пометкой «не используется» приложение не отправляет, их формат по прошивке не проверялся.
/// </remarks>
public enum ShowerCode
{
    /// <summary>
    /// Нет команды.
    /// </summary>
    None = 0,

    /// <summary>
    /// Записывает показание датчика уровня для полного бака.
    /// </summary>
    /// <remarks>Аргумент: ushort, мкс.</remarks>
    SetWaterLevelFullUsec = 1,

    /// <summary>
    /// Записывает показание датчика уровня для пустого бака.
    /// </summary>
    /// <remarks>Аргумент: ushort, мкс.</remarks>
    SetWaterLevelEmptyUsec = 2,

    /// <summary>
    /// Показание датчика уровня для полного бака: время отражения сигнала, когда бак полностью заполнен водой.
    /// </summary>
    /// <remarks>Ответ: ushort, мкс.</remarks>
    GetWaterLevelFullUsec = 3,

    /// <summary>
    /// Показание датчика уровня для пустого бака: время отражения сигнала, когда бак полностью пустой.
    /// </summary>
    /// <remarks>Ответ: ushort, мкс.</remarks>
    GetWaterLevelEmptyUsec = 4,

    /// <summary>
    /// Таблица температуры воды в баке в зависимости от температуры воздуха.
    /// </summary>
    /// <remarks>
    /// Ответ: 25 байт — температура в баке (°C) для температуры воздуха от 15 до 39 °C (см. <see cref="SetupModel"/>).
    /// Сейчас не используется.
    /// </remarks>
    GetTemperatureChart = 5,

    /// <summary>
    /// Записывает таблицу температуры воды в баке.
    /// </summary>
    /// <remarks>Не используется.</remarks>
    SetTemperatureChart = 6,

    /// <summary>
    /// Проверка связи.
    /// </summary>
    /// <remarks>Ответ: byte — тот же код <see cref="Ping"/>.</remarks>
    Ping = 7,

    /// <summary>
    /// Уровень воды.
    /// </summary>
    /// <remarks>Ответ: ushort.</remarks>
    GetWaterLevel = 8,

    /// <summary>
    /// Уровень воды в баке в процентах.
    /// </summary>
    /// <remarks>Ответ: byte, %.</remarks>
    GetWaterPercent = 9,

    /// <summary>
    /// Температура воздуха.
    /// </summary>
    /// <remarks>Не используется.</remarks>
    GetAirTemperature = 10,

    /// <summary>
    /// Температура воды в баке.
    /// </summary>
    /// <remarks>Ответ: float, °C.</remarks>
    GetWaterTemperature = 11,

    /// <summary>
    /// Усреднённая температура внутри бака.
    /// </summary>
    /// <remarks>Не используется.</remarks>
    GetAverageInternalTemperature = 12,

    /// <summary>
    /// Усреднённая температура воздуха.
    /// </summary>
    /// <remarks>Не используется.</remarks>
    GetAverageExternalTemperature = 13,

    /// <summary>
    /// Уровень воды в баке, ниже которого нагреватель не включается.
    /// </summary>
    /// <remarks>Ответ: byte, %.</remarks>
    GetMinimumWaterHeatingLevel = 14,

    /// <inheritdoc cref="GetMinimumWaterHeatingLevel"/>
    /// <remarks>Аргумент: byte, %.</remarks>
    SetMinimumWaterHeatingLevel = 15,

    /// <summary>
    /// Максимальное время нагрева: если за это время вода не нагрелась до нужной температуры,
    /// нагрев прекращается и контроллер переходит в аварийное состояние.
    /// </summary>
    /// <remarks>Ответ: byte, минуты.</remarks>
    GetHeatingTimeLimit = 18,

    /// <inheritdoc cref="GetHeatingTimeLimit"/>
    /// <remarks>Аргумент: byte, минуты.</remarks>
    SetHeatingTimeLimit = 19,

    /// <summary>
    /// Яркость светодиодного освещения.
    /// </summary>
    /// <remarks>Ответ: byte, от 0 до 100%.</remarks>
    GetLightBrightness = 20,

    /// <inheritdoc cref="GetLightBrightness"/>
    /// <remarks>Аргумент: byte, от 0 до 100%.</remarks>
    SetLightBrightness = 21,

    /// <summary>
    /// Примерное время до окончания нагрева.
    /// </summary>
    /// <remarks>Ответ: byte, минуты.</remarks>
    GetMinutesLeft = 22,

    /// <summary>
    /// Прогресс нагрева.
    /// </summary>
    /// <remarks>Не используется.</remarks>
    GetHeatingProgress = 23,

    /// <summary>
    /// Вода нагрета.
    /// </summary>
    /// <remarks>Не используется.</remarks>
    GetWaterHeated = 24,

    /// <summary>
    /// Нагрев прекращён по превышению <see cref="GetHeatingTimeLimit"/>.
    /// </summary>
    /// <remarks>Не используется.</remarks>
    GetHeatingTimeoutState = 25,

    /// <summary>
    /// Нагреватель сейчас включён.
    /// </summary>
    /// <remarks>Ответ: byte, 0 или 1.</remarks>
    GetHeaterEnabled = 26,

    /// <summary>
    /// Абсолютное время нагрева: если за это время автомат нагревателя не был физически выключен,
    /// нагрев прекращается и контроллер переходит в аварийное состояние.
    /// </summary>
    /// <remarks>Ответ: byte, часы.</remarks>
    GetAbsoluteHeatingTimeLimit = 27,

    /// <inheritdoc cref="GetAbsoluteHeatingTimeLimit"/>
    /// <remarks>Аргумент: byte, часы.</remarks>
    SetAbsoluteHeatingTimeLimit = 28,

    /// <summary>
    /// Мощность WiFi в единицах по 0.25 dBm.
    /// </summary>
    /// <remarks>От 40 до 82 (10..20.5 dBm).</remarks>
    GetWiFiPower = 29,

    /// <summary>
    /// Мощность WiFi в единицах по 0.25 dBm.
    /// </summary>
    /// <remarks>От 40 до 82 (10..20.5 dBm).</remarks>
    SetWiFiPower = 30,

    /// <summary>
    /// Нагрев прекращён по превышению <see cref="GetAbsoluteHeatingTimeLimit"/>.
    /// </summary>
    /// <remarks>Не используется.</remarks>
    GetAbsoluteTimeoutStatus = 31,

    /// <summary>
    /// Контроллер был перезапущен сторожевым таймером.
    /// </summary>
    /// <remarks>Ответ: byte, 0 или 1.</remarks>
    GetWatchDogWasReset = 32,

    /// <summary>
    /// Есть основное питание.
    /// </summary>
    /// <remarks>Не используется.</remarks>
    GetHasMainPower = 33,

    /// <summary>
    /// Предел нагрева.
    /// </summary>
    /// <remarks>Не используется.</remarks>
    GetHeatingLimit = 34,

    /// <summary>
    /// Подключает Wi-Fi к точке доступа только в текущей сессии.
    /// </summary>
    /// <remarks>
    /// Аргумент: ASCII-строка вида <c>"ssid","password"</c> или <c>"ssid","password","ca:d7:19:d8:a6:44"</c>
    /// (см. <see cref="ESP8266Helper.CreateSetCurrentAPCommand"/>).
    /// </remarks>
    SetCurAP = 35,

    /// <summary>
    /// Подключает Wi-Fi к точке доступа и сохраняет её как точку по умолчанию.
    /// </summary>
    /// <remarks>Аргумент: как у <see cref="SetCurAP"/>.</remarks>
    SetDefAP = 36,

    /// <summary>
    /// Пауза перед повторным измерением расстояния до воды в баке. Рекомендуется не меньше 60 мсек.
    /// </summary>
    /// <remarks>Ответ: byte, мсек.</remarks>
    GetWaterLevelMeasureInterval = 37,

    /// <inheritdoc cref="GetWaterLevelMeasureInterval"/>
    /// <remarks>Аргумент: byte, мсек.</remarks>
    SetWaterLevelMeasureInterval = 38,

    /// <summary>
    /// Уровень воды, при котором прекращается набор воды в бак.
    /// </summary>
    /// <remarks>Ответ: byte, %.</remarks>
    GetWaterValveCutOffPercent = 39,

    /// <inheritdoc cref="GetWaterValveCutOffPercent"/>
    /// <remarks>Аргумент: byte, %.</remarks>
    SetWaterValveCutOffPercent = 40,

    /// <summary>
    /// Размер буфера фильтра «скользящее среднее» для медианы показаний датчика уровня воды.
    /// </summary>
    /// <remarks>Ответ: byte, от 1 до 129.</remarks>
    GetWaterLevelAverageBufferSize = 45,

    /// <inheritdoc cref="GetWaterLevelAverageBufferSize"/>
    /// <remarks>Аргумент: byte, от 1 до 129.</remarks>
    SetWaterLevelAverageBufferSize = 46,

    /// <summary>
    /// Размер буфера фильтра «скользящее среднее» для датчика температуры внутри бака.
    /// </summary>
    /// <remarks>Ответ: byte, от 1 до 8.</remarks>
    GetWaterTemperatureSensorAverageSize = 47,

    /// <inheritdoc cref="GetWaterTemperatureSensorAverageSize"/>
    /// <remarks>Аргумент: byte, от 1 до 8.</remarks>
    SetWaterTemperatureSensorAverageSize = 48,

    /// <summary>
    /// Мкс на градус для датчика уровня воды.
    /// </summary>
    /// <remarks>Не используется.</remarks>
    GetWaterLevelUsecPerDeg = 49,

    /// <inheritdoc cref="GetWaterLevelUsecPerDeg"/>
    SetWaterLevelUsecPerDeg = 50,

    /// <summary>
    /// Таймаут клапана набора воды, секунды.
    /// </summary>
    /// <remarks>Не используется.</remarks>
    GetWaterValveTimeoutSec = 51,

    /// <inheritdoc cref="GetWaterValveTimeoutSec"/>
    SetWaterValveTimeoutSec = 52,

    /// <summary>
    /// Показание датчика уровня воды без фильтрации.
    /// </summary>
    /// <remarks>Ответ: short, мкс; -1, если у датчика произошёл таймаут.</remarks>
    GetWaterLevelRaw = 53,

    /// <summary>
    /// Время нажатия кнопки для её срабатывания (антидребезг).
    /// </summary>
    /// <remarks>Ответ: byte, от 5 до 50 мсек.</remarks>
    GetButtonTimeMsec = 54,

    /// <inheritdoc cref="GetButtonTimeMsec"/>
    /// <remarks>Аргумент: byte, от 5 до 50 мсек.</remarks>
    SetButtonTimeMsec = 55,

    /// <summary>
    /// Нижняя граница температуры.
    /// </summary>
    /// <remarks>Не используется.</remarks>
    GetTemperatureLowerBound = 56,

    /// <summary>
    /// Верхняя граница температуры.
    /// </summary>
    /// <remarks>Не используется.</remarks>
    GetTemperatureUpperBound = 57,

    /// <summary>
    /// Размер буфера медианного фильтра для сырых показаний датчика уровня воды.
    /// </summary>
    /// <remarks>Ответ: byte, от 1 до 255.</remarks>
    GetWaterLevelMedianBufferSize = 58,

    /// <inheritdoc cref="GetWaterLevelMedianBufferSize"/>
    /// <remarks>Аргумент: byte, от 1 до 255.</remarks>
    SetWaterLevelMedianBufferSize = 59,

    /// <summary>
    /// Объём воды в полном баке. Используется для расчёта времени нагрева.
    /// </summary>
    /// <remarks>Ответ: float, литры.</remarks>
    GetWaterTankVolumeLitre = 60,

    /// <inheritdoc cref="GetWaterTankVolumeLitre"/>
    /// <remarks>Аргумент: float, литры.</remarks>
    SetWaterTankVolumeLitre = 61,

    /// <summary>
    /// Мощность ТЭНа с учётом КПД. Используется для расчёта времени нагрева.
    /// </summary>
    /// <remarks>Ответ: float, кВт.</remarks>
    GetWaterHeaterPowerKWatt = 62,

    /// <inheritdoc cref="GetWaterHeaterPowerKWatt"/>
    /// <remarks>Аргумент: float, кВт.</remarks>
    SetWaterHeaterPowerKWatt = 63,

    /// <summary>
    /// Время удержания кнопки, после которого нажатие считается длительным.
    /// </summary>
    /// <remarks>Ответ: ushort, от 1000 до 10000 мсек.</remarks>
    GetButtonLongPressTimeMsec = 70,

    /// <inheritdoc cref="GetButtonLongPressTimeMsec"/>
    /// <remarks>Аргумент: ushort, от 1000 до 10000 мсек.</remarks>
    SetButtonLongPressTimeMsec = 71,

    /// <summary>
    /// Число неудачных измерений уровня воды, после которого на индикаторе отображаются прочерки.
    /// </summary>
    /// <remarks>Ответ: byte, от 1 до 255.</remarks>
    GetWaterLevelErrorThreshold = 80,

    /// <inheritdoc cref="GetWaterLevelErrorThreshold"/>
    /// <remarks>Аргумент: byte, от 1 до 255.</remarks>
    SetWaterLevelErrorThreshold = 81,

    /// <summary>
    /// Счётчик переполнений при измерении уровня воды.
    /// </summary>
    /// <remarks>Ответ: ushort.</remarks>
    GetWaterLevelOverflowCounter = 82,

    /// <summary>
    /// Счётчик измерений уровня воды, отброшенных из-за шума.
    /// </summary>
    /// <remarks>Ответ: ushort.</remarks>
    GetWaterLevelNoiseErrorCounter = 83,

    /// <summary>
    /// Гистерезис нагревателя: на сколько градусов вода должна остыть ниже целевой температуры, чтобы нагрев включился снова.
    /// </summary>
    /// <remarks>Ответ: float, °C.</remarks>
    GetHeaterHysteresis = 84,

    /// <inheritdoc cref="GetHeaterHysteresis"/>
    /// <remarks>Аргумент: float, °C.</remarks>
    SetHeaterHysteresis = 85,

    /// <summary>
    /// Ответ контроллера: команда выполнена.
    /// </summary>
    OK = 200,

    /// <summary>
    /// Ответ контроллера на неизвестный код команды.
    /// </summary>
    UnknownCode = 253,

    /// <summary>
    /// Перезапускает контроллер.
    /// </summary>
    /// <remarks>Ответ: <see cref="OK"/>.</remarks>
    Reset = 254,

    /// <summary>
    /// Сохраняет записанные параметры в памяти контроллера. Отправляется после серии команд Set*.
    /// </summary>
    /// <remarks>Ответ: <see cref="OK"/>.</remarks>
    Save = 255,
}
