using UnityEngine;

namespace Utilities
{
    public static class LayerMaskExtensions
    {
        public static bool HasLayerWihin(this LayerMask layerMask, int otherMask)
        {
            return (1 << otherMask | layerMask) == layerMask;
        }    
        
        public static bool HasLayerWihin(this LayerMask layerMask, LayerMask otherMask)
        {
            return (otherMask | layerMask) == layerMask;
        }    
    }
}