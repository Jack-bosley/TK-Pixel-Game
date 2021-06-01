using System;
using System.Collections.Generic;
using System.Text;
using PixelGame.Core;
using PixelGame.Core.Interfaces;
using PixelGame.Core.Enums;

namespace PixelGame.Entity_Component
{
    public class Base : IDisposable, IPGSerializable
    {
        protected string id;
        protected SerializableType serializableType;
        protected bool isDisposed;

        protected Base()
        {
            id = EntityCounter.GetNextId();
            serializableType = SerializableType.Unknown;
        }
        ~Base()
        {
            if (!isDisposed)
                Dispose();
        }
        public virtual void Dispose()
        {
            isDisposed = true;
            GC.SuppressFinalize(this);
        }

        public string Id
        {
            get => id;
            protected set => id = value;
        }
        public SerializableType SerializableType
        {
            get => SerializableType;
            protected set => SerializableType = value;
        }

        public virtual byte[] Serialize()
        {
            throw new NotImplementedException();
        }
        public virtual void Deserialize(byte[] bytes)
        {
            throw new NotImplementedException();
        }
    }
}
