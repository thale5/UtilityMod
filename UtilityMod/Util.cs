using ColossalFramework;
using ColossalFramework.Plugins;
using System;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace UtilityMod
{
    public class Util
    {
        public static void PanelPrint(params string[] args)
        {
            DateTime now = System.DateTime.Now;
            string s = String.Format("[UtilityMod] {0, -51} {1, 22} {2, 2}  {3}", String.Join(" ", args), now, Thread.CurrentThread.ManagedThreadId, (now.Ticks / 10000) / 1000f);
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, s);
        }

        public static void DebugPrint(params string[] args)
        {
            DateTime now = System.DateTime.Now;
            string s = String.Format("[UtilityMod] {0, -51} {1, 22} {2, 2}  {3}", String.Join(" ", args), now, Thread.CurrentThread.ManagedThreadId, (now.Ticks / 10000) / 1000f);
            Debug.Log(s);
        }

        public static void EnableLogChannel()
        {
            CODebugBase<LogChannel>.EnableChannels(LogChannel.All);
            CODebugBase<LogChannel>.verbose = true;
        }

        public static void EnableInternalLogChannel()
        {
            CODebugBase<InternalLogChannel>.EnableChannels(InternalLogChannel.All);
            CODebugBase<InternalLogChannel>.verbose = true;
        }

        public static bool FindFineRoadHeights(out Type netToolFineType)
        {
            if (AppDomain.CurrentDomain.GetAssemblies().Any(q => q.FullName.Contains("FineRoadHeights")))
            {
                netToolFineType = Type.GetType("NetToolFine, FineRoadHeights");

                if (UnityEngine.Object.FindObjectOfType(netToolFineType) as ToolBase)
                    return true;
            }

            netToolFineType = null;
            return false;
        }
    }
}
