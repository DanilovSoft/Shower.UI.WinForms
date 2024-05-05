using System.Text;

namespace Shower.Domain.RpcClient;

internal sealed class ShowerBinaryReader : BinaryReader
{
    public ShowerBinaryReader(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen)
    {

    }

    /// <exception cref="InvalidDataException"/>
    public void ReadOK()
    {
        var code = ReadByte();

        if (code != (byte)ShowerCodes.OK)
        {
            throw new InvalidDataException($"Ожидался ответ 'OK' но получено значение '{code}'");
        }
    }

    public ShowerCodes ReadCode()
    {
        var code = (ShowerCodes)ReadByte();
        return code;
    }

    public async Task<ShowerCodes> ReadCodeAsync()
    {
        var code = await Task.Run(() => (ShowerCodes)ReadByte()).ConfigureAwait(false);
        return code;
    }
}
