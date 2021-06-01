using System;
using System.Collections.Generic;
using System.Text;

namespace PixelGame.Entity_Component
{
    public class Layer : Base
    {
        public readonly string name;

        public Layer()
        {
            name = "layer";
        }
        public Layer(string name)
        {
            this.name = name;
        }

        public virtual void Event()
        {

        }
        public virtual void Update()
        {

        }
    }
}
