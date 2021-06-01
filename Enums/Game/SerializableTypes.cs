using System;
using System.Collections.Generic;
using System.Text;

namespace PixelGame.Core.Enums
{
    public enum SerializableType
    {
        Unknown = 0,
        None = 1,

        // Architectural
        Scene = 100000,
        LayerStack = 1,
        Layer = 2,

        // Entities
        Player = 200000,


        // Components
        Transform = 300000,
    }
}
