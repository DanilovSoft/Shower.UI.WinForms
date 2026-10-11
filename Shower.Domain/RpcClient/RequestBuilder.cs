using System.Runtime.InteropServices;
using System.Text;

namespace Shower.Domain.RpcClient;

public sealed class RequestBuilder
{
    private readonly MyBinaryWriter _writer;
    private readonly ShowerBinaryReader _reader;
    private readonly Stream _nstream;
    private readonly ShowerConnection _con;

    public RequestBuilder(ShowerConnection con, MyBinaryWriter writer, ShowerBinaryReader reader, Stream nstream)
    {
        ArgumentNullException.ThrowIfNull(con);
        ArgumentNullException.ThrowIfNull(writer);
        ArgumentNullException.ThrowIfNull(reader);
        ArgumentNullException.ThrowIfNull(nstream);

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

    public RequestBuilder WriteHeader(ShowerCode code)
    {
        _writer.WriteHeader(code);
        return this;
    }

    public RequestBuilder WriteString(string value)
    {
        var str = Encoding.ASCII.GetBytes(value);
        var sizeBytes = str.Length;

        _writer.Write(str, 0, sizeBytes);
        return this;
    }

    /// <summary>
    /// Добавляет заголовок в стрим, отправляет все накопленные данные и читает один байт ответа.
    /// </summary>
    /// <exception cref="InvalidDataException"/>
    public void SendAndReadOK()
    {
        WriteEndAndSend();
        _reader.ReadOK();
    }

    public async ValueTask<ShowerCode> ReadHeaderAsync(CancellationToken ct)
    {
        using var _ = ct.Register(_con.Dispose);

        try
        {
            WriteEndAndSend();
            return await _reader.ReadHeaderAsync(ct).ConfigureAwait(false);
        }
        catch when (ct.IsCancellationRequested)
        {
            ct.ThrowIfCancellationRequested();
            throw;
        }
    }

    /// <summary>
    /// Добавляет заголовок в стрим и отправляет весь стрим и сбрасывает его размер.
    /// </summary>
    private void WriteEndAndSend()
    {
        _writer.WriteEndOfMessage();
        _writer.Send();
    }
}
