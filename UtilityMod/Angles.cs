/*
 * Based on
 * SharpJunctionAngles
 * Copyright Thaok 2015
 * GNU GENERAL PUBLIC LICENSE Version 2
 */

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityMod
{
    class Angles : DetourUtility
    {
        NetInfo[] netInfos = {};

        public Angles()
        {
            Type netToolType = typeof(NetTool);
            Type netToolFineType;
            bool fineRoadHeightsDetected = Util.FindFineRoadHeights(out netToolFineType);
            Type inUseToolType = fineRoadHeightsDetected ? netToolFineType : netToolType;

            init(inUseToolType, "CanAddSegment");
            init(inUseToolType, "CanAddNode", 4);
            init(inUseToolType, "CanAddNode", "CanAddNode5", 5);
            init(inUseToolType, "CheckStartAndEnd");
            init(typeof(NetInfo), "GetMinNodeDistance");
            netInfos = findBendingNetInfos();
        }

        NetInfo[] findBendingNetInfos()
        {
            NetInfo[] netInfos = Resources.FindObjectsOfTypeAll(typeof(NetInfo)) as NetInfo[];
            List<NetInfo> bendingNetInfos = new List<NetInfo>(110);

            for (int i = 0; i < netInfos.Length; ++i)
                if (netInfos[i].m_enableBendingSegments)
                    bendingNetInfos.Add(netInfos[i]);

            return bendingNetInfos.ToArray();
        }

        protected override void Deploy()
        {
            base.Deploy();

            for (int i = 0; i < netInfos.Length; ++i)
                netInfos[i].m_enableBendingSegments = false;
        }

        protected override void Revert()
        {
            base.Revert();

            for (int i = 0; i < netInfos.Length; ++i)
                netInfos[i].m_enableBendingSegments = true;
        }

        protected override void DoCleanup()
        {
            base.DoCleanup();
            netInfos = new NetInfo[0];
        }

        private static bool CanAddSegment(ushort nodeID, Vector3 direction, ulong[] collidingSegmentBuffer, ushort ignoreSegment)
        {
            return true;
        }

        private static bool CanAddNode(ushort segmentID, ushort nodeID, Vector3 position, Vector3 direction)
        {
            return true;
        }

        private static bool CanAddNode5(ushort segmentID, Vector3 position, Vector3 direction, bool checkDirection, ulong[] collidingSegmentBuffer)
        {
            return true;
        }

        private static bool CheckStartAndEnd(ushort upgrading, ushort startSegment, ushort startNode, ushort endSegment, ushort endNode, ulong[] collidingSegmentBuffer)
        {
            return true;
        }

        public float GetMinNodeDistance()
        {
            return 3f;
        }
    }
}
