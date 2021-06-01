using System;
using System.Collections.Generic;
using System.Text;

namespace PixelGame.Entity_Component
{
    public class LayerStack : Base
    {
        private const int MAX_LAYER_COUNT = 1024;
        private int layerCount, overlayCount;

        private Dictionary<int, Layer> layers;

        public LayerStack()
        {
            layers = new Dictionary<int, Layer>();
        }

        public int MaxLayers
        {
            get => MAX_LAYER_COUNT;
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
                layers.Add(LayerCount, layer);
                layerCount++;
            }
            else
                throw new Exception($"Cannot exceed maximum layer count {MaxLayers}");
        }
        public void AddOverlay(Layer layer)
        {
            layers.Add(MaxLayers + OverlayCount, layer);
            overlayCount++;
        }
    }
}
