using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class LayerHelpers
{
    public static LayerMask IndexToLayerMask(int layerIndex)
    {
        return 1 << layerIndex;
    }

    public static bool CheckLayer(int layerIndex, LayerMask layerMask)
    {
        return (IndexToLayerMask(layerIndex) & layerMask) != 0;
    }

    public static LayerMask GetCollisionLayerMask(int layer)
    {
        int layerCount = 32;
        int mask = 0;

        for (int i = 0; i < layerCount; i++)
        {
            if (!Physics2D.GetIgnoreLayerCollision(layer, i)) mask |= 1 << i;
        }

        return mask;
    }
}
