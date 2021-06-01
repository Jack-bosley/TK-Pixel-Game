using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;

namespace PixelGame.Entity_Component
{
    public class Transform : BaseComponent
    {
        private Vector3 position, rotation, scale;
        

        public Transform() : this(0, 0, 0) 
        {
            this.IsRemovable = false;
            this.IsUnique = true;
        }
        public Transform(Vector3 position) : base()
        {
            this.position = position;
            this.rotation = position;
            this.scale = position;
        }
        public Transform(float x, float y, float z) : base()
        {
            this.position = new Vector3(x, y, z);
            this.rotation = new Vector3();
            this.scale = new Vector3();
        }


        public Vector3 Position
        {
            get => position;
            set => position = value;
        }

        public Vector3 Rotation
        {
            get => rotation;
            set => rotation = value;
        }

        public Vector3 Scale
        {
            get => scale;
            set => scale = value;
        }

        public override byte[] Serialize()
        {
            return base.Serialize();
        }

        public override void Deserialize(byte[] bytes)
        {
            base.Deserialize(bytes);
        }
    }
}
