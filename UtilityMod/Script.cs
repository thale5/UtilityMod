using System;
using UnityEngine;

namespace UtilityMod
{
    /// <summary>
    /// This is the only class that cannot be replaced while in game. Unity wants to use the original
    /// version in any case. Therefore, the class is small and simple so that there is no need
    /// to modify it while in game.
    /// </summary>
    public class Script : MonoBehaviour
    {
        public Action updateAction = Empty;
        public Action emptyAction = Empty;

        public void Awake()
        {
            DontDestroyOnLoad(this);
            Util.DebugPrint("Script.Awake");
        }

        public void Start()
        {
            Util.DebugPrint("Script.Start: script id =", GetInstanceID().ToString());
        }

        public void Update()
        {
            if (Input.GetKey(KeyCode.LeftAlt))
                updateAction();
        }

        public void OnDestroy()
        {
            Util.DebugPrint("Script.OnDestroy: script id =", GetInstanceID().ToString());
        }

        public static void Empty()
        {
        }
    }
}
