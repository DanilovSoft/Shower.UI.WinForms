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
    public Task SendAsync(CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        OutStream.Position = 0;

        var copyTask = OutStream.CopyToAsync(_baseStream, bufferSize: 81920, cancellationToken);
        if (!copyTask.IsCompletedSuccessfully)
        {
            return WaitCopy(copyTask, cancellationToken);
        }

        var flushTask = _baseStream.FlushAsync(cancellationToken);
        if (!flushTask.IsCompletedSuccessfully)
        {
            return WaitFlush(flushTask);
        }

        OnComplete();
        return Task.CompletedTask;

        void OnComplete()
        {
            OutStream.SetLength(1);
            OutStream.Position = 1;
            _lastPosition = 0;
        }

        async Task WaitCopy(Task copyTask, CancellationToken ct)
        {
            await copyTask.ConfigureAwait(false);
            await _baseStream.FlushAsync(ct).ConfigureAwait(false);
            OnComplete();
        }

        async Task WaitFlush(Task flushTask)
        {
            await flushTask.ConfigureAwait(false);
            OnComplete();
        }
    }
}
