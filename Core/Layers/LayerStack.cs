using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PixelGame.Core.Logging;

namespace PixelGame.Core.Layers
{
    public class LayerStack
    {
        private const int OVERLAY_OFFSET = 1024;

        private Dictionary<int, Layer> layers;
        private int layerCount, overlayCount;


        public LayerStack()
        {
            layers = new Dictionary<int, Layer>();
        }

        public int MaxLayers
        {
            get => OVERLAY_OFFSET;
        }
        public int LayerCount
        {
            get => layerCount;
        }
        public int OverlayCount
        {
            get => overlayCount;
        }


        public void AddLayer(Layer layer)
        {
            if (layerCount < MaxLayers)
            {
                layers.Add(layerCount, layer);
                layerCount++;
            }
            else
            {
                Logger.LogException(new Exception($"Maximum layer count ({MaxLayers}) has been reached"));
            }
        }

        public void AddOverlay(Layer layer)
        {
            layers.Add(OVERLAY_OFFSET + overlayCount, layer);
            overlayCount++;
        }

    }
}
