using System;
using System.Collections.Generic;
using System.Text;
using PixelGame.Core.Interfaces;

namespace PixelGame.Entity_Component
{
    public class Scene : Base, IPGSerializable
    {
        public LayerStack layerStack;

        public Scene()
        {
            layerStack = new LayerStack();
        }

        public void OnDraw()
        {
        }
        public void OnEvent()
        {
        }
        public void OnLoad()
        {
        }
        public void OnUnload()
        {
        }
        public void OnUpdate()
        {
        }

        public byte[] Serialize()
        {
            return new byte[0];
        }
        public void Deserialize(byte[] bytes)
        {
        }
    }
}
