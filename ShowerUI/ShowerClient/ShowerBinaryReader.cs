using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShowerUI.ShowerClient
{
    public class ShowerBinaryReader : BinaryReader
    {
        public ShowerBinaryReader(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen)
        {
            
        }

        public void ReadOK()
        {
            byte code = base.ReadByte();
            if (code != (byte)ShowerCodes.OK)
                throw new InvalidDataException();
        }

        public ShowerCodes ReadCode()
        {
            ShowerCodes code = (ShowerCodes)base.ReadByte();
            return code;
        }

        public async Task<ShowerCodes> ReadCodeAsync()
        {
            ShowerCodes code = await Task.Run(() => (ShowerCodes)base.ReadByte()).ConfigureAwait(false);
            return code;
        }
    }
}
