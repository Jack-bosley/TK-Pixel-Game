using System;
using System.Collections.Generic;
using System.Text;

namespace PixelGame.Core.Layers
{
    public class Layer
    {
        public readonly string name;

        public Layer()
        {
            this.name = "layer";
        }
        public Layer(string name)
        {
            this.name = name;
        }

        public virtual void Update()
        {

        }

        public virtual void Event()
        {

        }
    }
}
