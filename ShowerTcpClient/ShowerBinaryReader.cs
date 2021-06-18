using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShowerTcpClient
{
    internal sealed class ShowerBinaryReader : BinaryReader
    {
        public ShowerBinaryReader(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen)
        {
            
        }

        /// <exception cref="InvalidDataException"/>
        public void ReadOK()
        {
            byte code = ReadByte();

            if (code != (byte)ShowerCodes.OK)
            {
                throw new InvalidDataException($"Ожидался ответ 'OK' но получено значение '{code}'");
            }
        }

        public ShowerCodes ReadCode()
        {
            ShowerCodes code = (ShowerCodes)ReadByte();
            return code;
        }

        public async Task<ShowerCodes> ReadCodeAsync()
        {
            ShowerCodes code = await Task.Run(() => (ShowerCodes)ReadByte()).ConfigureAwait(false);
            return code;
        }
    }
}
