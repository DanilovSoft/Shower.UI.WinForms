using System.Text;

namespace Shower.Domain.RpcClient;

public sealed class ShowerBinaryReader(Stream input, Encoding encoding, bool leaveOpen) : BinaryReader(input, encoding, leaveOpen)
{
    /// <exception cref="InvalidDataException"/>
    public void ReadOK()
    {
        var code = ReadHeader();

        if (code != ShowerCode.OK)
        {
            throw new InvalidDataException($"Ожидался ответ 'OK' но получено значение '{code}'");
        }
    }

    public ShowerCode ReadHeader() => (ShowerCode)ReadByte();

    public Task<ShowerCode> ReadHeaderAsync(CancellationToken ct) => Task.Run(ReadHeader, ct);
}
