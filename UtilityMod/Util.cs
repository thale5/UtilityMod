using ColossalFramework;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using System;
using System.Collections.Generic;
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
            long millis = now.Ticks / 10000;
            string s = String.Format("[UtilityMod] {0, -42} {1, 22} {2, 2}  {3}.{4}", String.Join(" ", args), now, Thread.CurrentThread.ManagedThreadId, millis / 1000, millis % 1000);
            DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, s);
        }

        public static void DebugPrint(params string[] args)
        {
            DateTime now = System.DateTime.Now;
            long millis = now.Ticks / 10000;
            string s = String.Format("[UtilityMod] {0, -42} {1, 22} {2, 2}  {3}.{4}", String.Join(" ", args), now, Thread.CurrentThread.ManagedThreadId, millis / 1000, millis % 1000);
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

        public static UIComponent FindVisible(UIComponent root, string searchName)
        {
            if (root != null)
            {
                if (root.cachedName == searchName && root.isVisible)
                    return root;

                IList<UIComponent> childs = root.components;

                if (childs != null)
                    for (int i = 0; i < childs.Count; i++)
                    {
                        UIComponent c = FindVisible(childs[i], searchName);

                        if (c != null)
                            return c;
                    }
            }

            return null;
        }
    }
}
