using System;
using System.Collections.Generic;
using System.Text;

namespace PixelGame.Core.Interfaces
{
    public interface IPGSerializable
    {
        public byte[] Serialize();

        public void Deserialize(byte[] bytes);
    }
}
