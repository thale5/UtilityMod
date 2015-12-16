using ColossalFramework.UI;
using System;
using UnityEngine;

namespace UtilityMod
{
    /// <summary>
    /// Available utilities.
    /// </summary>
    public enum Id
    {
        Anarchy,
        Angles,
        Water,
        Test,
        Dev,
        Snap,
        Size    // end marker
    };

    /// <summary>
    /// This class controls the utilities.
    /// </summary>
    public class Controller
    {
        Utility[] utilities = new Utility[(int) Id.Size];
        readonly bool recompiled;

        public Controller(bool recompiled)
        {
            Util.DebugPrint("Controller constructor");
            this.recompiled = recompiled;
        }

        public void InGame()
        {
            Util.DebugPrint("Controller.InGame");
        }

        public void Update()
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    Toggle(Id.Anarchy);
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                    Toggle(Id.Angles);
                else if (Input.GetKeyDown(KeyCode.W))
                    Toggle(Id.Water);
                else if (Input.GetKeyDown(KeyCode.T))
                    Toggle(Id.Test);
                else if (Input.GetKeyDown(KeyCode.D))
                    Toggle(Id.Dev);
                else if (Input.GetKeyDown(KeyCode.S))
                    Toggle(Id.Snap);
            }
        }

        public void Unloading()
        {
            Util.DebugPrint("Controller.Unloading");

            for (int i = 0; i < utilities.Length; i++)
                if (utilities[i] != null)
                {
                    utilities[i].Cleanup();
                    utilities[i] = null;
                }
        }

        void Toggle(Id id)
        {
            Utility utility = utilities[(int) id];

            // Lazy initialization on the first use.
            if (utility == null)
            {
                Util.DebugPrint("Creating", id.ToString());
                Type type = Type.GetType(GetType().Namespace + "." + id.ToString());
                utility = utilities[(int) id] = (Utility) Activator.CreateInstance(type);
                utility.id = id;
            }

            utility.Toggle();
        }
    }
}
