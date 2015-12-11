/*
 * Based on
 * No Water Check
 * Copyright boformer 2015
 */

using System;
using UnityEngine;

namespace UtilityMod
{
    class Water : DetourUtility
    {
        public Water()
        {
            init(typeof(TerrainManager), "HasWater", 1);
        }

        public bool HasWater(Vector2 position)
        {
            return false;
        }
    }
}
