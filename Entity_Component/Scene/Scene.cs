using System;
using System.Collections.Generic;
using System.Text;

using PixelGame.Core.Interfaces;
using PixelGame.Core.Logging;

namespace PixelGame.Entity_Component
{
    public class Scene : Base, IPGSerializable
    {
        public LayerStack layerStack;

        public Scene()
        {
            Logger.LogConsole("Scene initialized");
            layerStack = new LayerStack();
        }

        public void OnLoad()
        {
            //Logger.Log("Scene loaded");
        }
        public void OnUnload()
        {
            //Logger.Log("Scene unloaded");
        }
        public void OnDraw()
        {
            Logger.LogConsole("Scene draw");
        }
        public void OnEvent()
        {
            //Logger.Log("Scene event");
        }
        public void OnUpdate()
        {
            Logger.LogConsole("Scene update");
        }

        public override byte[] Serialize()
        {
            return new byte[0];
        }
        public override void Deserialize(byte[] bytes)
        {
        }
    }
}
