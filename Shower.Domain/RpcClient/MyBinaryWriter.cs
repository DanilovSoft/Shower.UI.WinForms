using System.Text;

namespace Shower.Domain.RpcClient;

internal sealed class MyBinaryWriter : BinaryWriter
{
    private const byte MaxLength = 200;
    private readonly Stream _baseStream;
    private long _lastPosition;

    public MyBinaryWriter(Stream output) : base(new MemoryStream(), Encoding.ASCII, leaveOpen: true)
    {
        _baseStream = output;
        OutStream.Position = 1;
    }

    public void Write(ShowerCodes code)
    {
        Write((byte)code);
    }

    /// <summary>
    /// Записывает в начало стрима 1 байт: размер последующих данных в стриме (до 200 байт).
    /// </summary>
    public void End()
    {
        if (OutStream.Length > MaxLength)
        {
            throw new InvalidOperationException("Maximum buffer length reached");
        }

        var curPos = OutStream.Position;
        var length = (byte)(curPos - _lastPosition);
        OutStream.Position = _lastPosition;
        Write(length);
        OutStream.Position = curPos + 1;
        _lastPosition = curPos;
    }

    /// <summary>
    /// Отправляет подготовленный запрос из памяти в TCP сокет
    /// </summary>
    public void Send()
    {
        OutStream.Position = 0;
        OutStream.CopyTo(_baseStream);
        _baseStream.Flush();
        OutStream.SetLength(1);
        OutStream.Position = 1;
        _lastPosition = 0;
    }

    /// <exception cref="OperationCanceledException"/>
    public async Task SendAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        OutStream.Position = 0;
        await OutStream.CopyToAsync(_baseStream, bufferSize: 81920, cancellationToken).ConfigureAwait(false);
        await _baseStream.FlushAsync(cancellationToken).ConfigureAwait(false);
        OutStream.SetLength(1);
        OutStream.Position = 1;
        _lastPosition = 0;
    }
}
