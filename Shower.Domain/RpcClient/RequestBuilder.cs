using System.Runtime.InteropServices;
using System.Text;

namespace Shower.Domain.RpcClient;

public class RequestBuilder
{
    private readonly List<CallbackInfo> _callbacks = [];
    private readonly MyBinaryWriter _writer;
    private readonly ShowerBinaryReader _reader;
    private readonly FixedNetworkStream _nstream;
    private readonly ShowerConnection _con;

    internal RequestBuilder(ShowerConnection con, MyBinaryWriter writer, ShowerBinaryReader reader, FixedNetworkStream nstream)
    {
        _con = con;
        _writer = writer;
        _reader = reader;
        _nstream = nstream;
    }

    public RequestBuilder WriteFloat(float value)
    {
        return Write(value);
    }

    public RequestBuilder WriteUInt16(ushort value)
    {
        return Write(value);
    }

    public RequestBuilder WriteUInt8(byte value)
    {
        return Write(value);
    }

    private RequestBuilder Write<T>(T value) where T : struct
    {
        var sizeBytes = Marshal.SizeOf(value);
        var output = new byte[sizeBytes];
        var ptr = Marshal.AllocHGlobal(sizeBytes);

        try
        {
            Marshal.StructureToPtr(value, ptr, true);
            Marshal.Copy(ptr, output, 0, sizeBytes);
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }

        _writer.Write(output, 0, sizeBytes);
        return this;
    }

    public RequestBuilder Write(ShowerCodes code)
    {
        _writer.Write(code);
        return this;
    }

    private RequestBuilder Write<T>(ShowerCodes code, Action<T> callback) where T : struct
    {
        _writer.Write(code);
        _writer.End();
        _callbacks.Add(CallbackInfo.Create(callback));
        return this;
    }

    public RequestBuilder WriteString(string value)
    {
        var str = Encoding.ASCII.GetBytes(value);
        var sizeBytes = str.Length;

        _writer.Write(str, 0, sizeBytes);
        return this;
    }

    public RequestBuilder Result<T>(Action<T> callback) where T : struct
    {
        _writer.End();
        _callbacks.Add(CallbackInfo.Create(callback));
        return this;
    }

    public void Send()
    {
        WriteEndAndSend();
    }

    /// <summary>
    /// Добавляет заголовок в стрим, отправляет все накопленные данные и читает один байт ответа.
    /// </summary>
    /// <exception cref="InvalidDataException"/>
    public void ReadOK()
    {
        WriteEndAndSend();
        _reader.ReadOK();
    }

    public async Task<ShowerCodes> ReadCodeAsync(CancellationToken cancellationToken = default)
    {
        using (cancellationToken.Register(() => { _con.Dispose(); }))
        {
            try
            {
                WriteEndAndSend();
                return await _reader.ReadCodeAsync().ConfigureAwait(false);
            }
            catch when (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
                throw;
            }
        }
    }

    //public void Execute()
    //{
    //    _writer.Send();

    //    if (_callbacks.Count == 0)
    //    {
    //        return;
    //    }

    //    var totalSize = _callbacks.Sum(x => x.UnmanagedSize);
    //    var buffer = new byte[totalSize];
    //    Read(buffer);

    //    var startIndex = 0;
    //    for (var i = 0; i < _callbacks.Count; i++)
    //    {
    //        _callbacks[i].ReadPrimitiveCallback(buffer, ref startIndex);
    //    }
    //}

    public Task ExecuteAsync() => ExecuteAsync(CancellationToken.None);

    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        await _writer.SendAsync(cancellationToken).ConfigureAwait(false);
        var totalSize = _callbacks.Sum(x => x.UnmanagedSize);
        var buffer = new byte[totalSize];
        await ReadExactlyAsync(buffer, cancellationToken).ConfigureAwait(false);

        var startIndex = 0;
        for (var i = 0; i < _callbacks.Count; i++)
        {
            _callbacks[i].ReadPrimitiveCallback(buffer, ref startIndex);
        }
    }

    /// <summary>
    /// Добавляет заголовок в стрим и отправляет весь стрим и сбрасывает его размер.
    /// </summary>
    private void WriteEndAndSend()
    {
        _writer.End();
        _writer.Send();
    }

    private ValueTask ReadExactlyAsync(Memory<byte> buffer, CancellationToken ct)
    {
        return _nstream.ReadExactlyAsync(buffer, ct);
    }

    private void Read(Span<byte> buffer)
    {
        _nstream.ReadExactly(buffer);
    }

    private readonly struct CallbackInfo
    {
        public delegate void ReadPrimitiveCallbackHandler(byte[] buffer, ref int startIndex);
        public readonly ReadPrimitiveCallbackHandler ReadPrimitiveCallback;
        public readonly int UnmanagedSize;

        private CallbackInfo(int unmanagedSize, ReadPrimitiveCallbackHandler callback)
        {
            UnmanagedSize = unmanagedSize;
            ReadPrimitiveCallback = callback;
        }

        public static CallbackInfo Create<T>(Action<T> callback) where T : struct
        {
            var unmanagedSize = Marshal.SizeOf<T>();

            return new CallbackInfo(unmanagedSize, delegate (byte[] buf, ref int startIndex)
            {
                var obj = MySerializer.Read<T>(buf.AsSpan(startIndex, unmanagedSize));
                startIndex += unmanagedSize;
                callback(obj);
            });
        }
    }
}
