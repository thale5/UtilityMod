/*
 * Based on
 * City-Skylines-AdvancedRoadAnarchy
 * Copyright northfacts 2015
 * GNU GENERAL PUBLIC LICENSE Version 2
 * and
 * ToolsAnarchy
 * Copyright emf 2015
 */

using System;
using UnityEngine;

namespace UtilityMod
{
    class Anarchy : DetourUtility
    {
        public Anarchy()
        {
            Type netToolType = typeof(NetTool);
            Type netToolFineType; // set in case Fine Road Heights is detected
            bool fineRoadHeightsDetected = Util.FindFineRoadHeights(out netToolFineType);

            if (fineRoadHeightsDetected)
                Util.DebugPrint("Fine Road Heights was detected");
            else
                Util.DebugPrint("Fine Road Heights was not detected");

            init(fineRoadHeightsDetected ? netToolFineType : netToolType, "CanCreateSegment", 11);
            init(netToolType, "CheckCollidingSegments");
            init(typeof(BuildingTool), "CheckCollidingBuildings");
            init(typeof(BuildingTool), "CheckSpace");
        }

        private static ToolBase.ToolErrors CanCreateSegment(NetInfo segmentInfo, ushort startNode, ushort startSegment, ushort endNode, ushort endSegment, ushort upgrading, Vector3 startPos, Vector3 endPos, Vector3 startDir, Vector3 endDir, ulong[] collidingSegmentBuffer)
        {
            return ToolBase.ToolErrors.None;
        }

        public static bool CheckCollidingSegments(ulong[] segmentMask, ulong[] buildingMask, ushort upgrading)
        {
            return false;
        }

        public static bool CheckCollidingBuildings(ulong[] buildingMask, ulong[] segmentMask)
        {
            return false;
        }

        private ToolBase.ToolErrors CheckSpace(BuildingInfo info, int relocating, Vector3 pos, float minY, float maxY, float angle, int width, int length, bool test, ulong[] collidingSegmentBuffer, ulong[] collidingBuildingBuffer)
        {
            return ToolBase.ToolErrors.None;
        }
    }
}
