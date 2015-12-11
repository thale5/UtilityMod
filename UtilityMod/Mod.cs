using ICities;
using System;
using UnityEngine;

namespace UtilityMod
{
    public class Mod : IUserMod, ILoadingExtension
    {
        private static int scnt = 0;
        private int cnt = 0;
        private string GAME_OBJECT = "UtilityModObject";
        private Component script;
        private Controller controller;

        public Mod()
        {
            Util.DebugPrint("Mod constructor");
        }

        public string Name
        {
            get
            {
                Util.DebugPrint("Mod.Name");
                return "Utility Mod";
            }
        }

        public string Description
        {
            get { return "Private Utility Mod"; }
        }

        public void OnEnabled()
        {
            Util.DebugPrint("Mod.OnEnabled");
        }

        public void OnCreated(ILoading loading)
        {
            Util.DebugPrint("Mod.OnCreated", loading.loadingComplete.ToString(), scnt.ToString(), cnt.ToString());
            scnt++;
            cnt++;

            GameObject gameObject = CheckGameObject();
            bool recompiled = (gameObject != null);
            controller = new Controller(recompiled);

            if (!recompiled)
            {
                // Ordinary load or new game from the main menu. We are not yet in game.
                gameObject = new GameObject(GAME_OBJECT);
                script = gameObject.AddComponent<Script>();
                Util.DebugPrint("GameObject created: id =", gameObject.GetInstanceID().ToString());
            }
            else
            {
                // The mod dll was just recompiled while in game.
                script = gameObject.GetComponent("Script");
                SetAction();
            }

            Util.DebugPrint("Script id =", script.GetInstanceID().ToString());
        }

        public void OnLevelLoaded(LoadMode mode)
        {
            Util.DebugPrint("Mod.OnLevelLoaded");
            SetAction();
        }

        private void SetAction()
        {
            controller.InGame();

            if (script is Script)
                ((Script) script).updateAction = controller.Update;
            else
            {
                // The recompiled case.
                Type type = script.GetType();
                Action action = controller.Update;
                type.GetField("updateAction").SetValue(script, action);
            }
        }

        private void StopAction()
        {
            if (script is Script)
                ((Script) script).updateAction = Script.Empty;
            else
            {
                // The recompiled case.
                Type type = script.GetType();
                type.GetField("updateAction").SetValue(script, type.GetField("emptyAction").GetValue(script));
            }

            controller.Unloading();
        }

        public void OnLevelUnloading()
        {
            Util.DebugPrint("Mod.OnLevelUnloading");
        }

        // Called on exit to main menu / desktop. Not called on in-game load level or dll recompile.
        public void OnReleased()
        {
            Util.DebugPrint("Mod.OnReleased");
            GameObject gameObject = CheckGameObject();

            if (gameObject != null)
            {
                Util.DebugPrint("GameObject destroyed: id =", gameObject.GetInstanceID().ToString());
                GameObject.Destroy(gameObject);
            }
        }

        // Called just before the dll is recompiled. Also called when leaving game.
        public void OnDisabled()
        {
            Util.DebugPrint("Mod.OnDisabled");
            StopAction();
        }

        private GameObject CheckGameObject()
        {
            GameObject gameObject = GameObject.Find(GAME_OBJECT);

            if (gameObject != null)
                Util.DebugPrint("GameObject exists: id =", gameObject.GetInstanceID().ToString());
            else
                Util.DebugPrint("GameObject does not exist");

            return gameObject;
        }
    }
}
