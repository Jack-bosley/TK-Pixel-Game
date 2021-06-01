using System;
using System.Collections.Generic;
using System.Text;

namespace PixelGame.Entity_Component
{
    public class SerializationHelper
    {
        public byte[] Serialize<T>(T entityComponent) 
            where T : Base
        {
            return entityComponent.Serialize();
        }


        public void Deserialize<T>(T entityComponent, byte[] data) 
            where T : Base
        {
            entityComponent.Deserialize(data);
        }
    }
}
