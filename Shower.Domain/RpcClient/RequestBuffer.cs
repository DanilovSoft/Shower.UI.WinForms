using System.Buffers.Binary;
using System.Text;

namespace Shower.Domain.RpcClient;

/// <summary>
/// Собирает запрос к контроллеру в памяти и отправляет его одним куском.
/// </summary>
/// <remarks>
/// Формат: каждое сообщение начинается с байта длины, который учитывает и сам себя,
/// затем код команды и аргументы в little-endian. В одном запросе может быть несколько сообщений подряд,
/// контроллер отвечает на них по очереди. Весь запрос — не больше <see cref="MaxLength"/> байт.
/// </remarks>
internal sealed class RequestBuffer
{
    private const int MaxLength = 200;

    private readonly byte[] _buffer = new byte[MaxLength + 1];

    /// <summary>
    /// Позиция байта длины текущего сообщения.
    /// </summary>
    private int _messageStart;

    /// <summary>
    /// Позиция следующего байта данных.
    /// </summary>
    private int _position = 1;

    /// <summary>
    /// Начинает новый запрос, отбрасывая недописанный предыдущий.
    /// </summary>
    public void Begin(ShowerCode code)
    {
        _messageStart = 0;
        _position = 1;
        Write((byte)code);
    }

    /// <summary>
    /// Завершает текущее сообщение и начинает следующее в том же запросе.
    /// </summary>
    public void Append(ShowerCode code)
    {
        EndMessage();
        _messageStart = _position;
        Next(1);
        Write((byte)code);
    }

    public void Write(byte value)
    {
        Next(sizeof(byte))[0] = value;
    }

    public void Write(ushort value)
    {
        BinaryPrimitives.WriteUInt16LittleEndian(Next(sizeof(ushort)), value);
    }

    public void Write(float value)
    {
        BinaryPrimitives.WriteSingleLittleEndian(Next(sizeof(float)), value);
    }

    public void WriteAscii(string value)
    {
        Encoding.ASCII.GetBytes(value, Next(Encoding.ASCII.GetByteCount(value)));
    }

    /// <summary>
    /// Завершает последнее сообщение. Возвращает готовый к отправке запрос.
    /// </summary>
    public ReadOnlyMemory<byte> Complete()
    {
        EndMessage();
        return _buffer.AsMemory(0, _position);
    }

    private void EndMessage()
    {
        _buffer[_messageStart] = (byte)(_position - _messageStart);
    }

    private Span<byte> Next(int size)
    {
        if (_position + size > MaxLength)
        {
            throw new InvalidOperationException("Maximum buffer length reached");
        }

        var span = _buffer.AsSpan(_position, size);
        _position += size;
        return span;
    }
}
