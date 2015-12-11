using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace UtilityMod
{
    public class DetourUtility : Utility
    {
        const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public;
        List<Detour> detours = new List<Detour>();

        protected void init(System.Type fromType, string fromMethod, System.Type toType, string toMethod, int args = -1)
        {
            try
            {
                MethodInfo from = GetMethod(fromType, fromMethod, args), to = GetMethod(toType, toMethod);

                if (from == null)
                    Util.DebugPrint(GetType().ToString(), "reflection failed:", fromMethod);
                else if (to == null)
                    Util.DebugPrint(GetType().ToString(), "reflection failed:", toMethod);
                else
                    detours.Add(new Detour(from, to));
            }
            catch (Exception e)
            {
                Util.DebugPrint(GetType().ToString(), "reflection failed");
                UnityEngine.Debug.LogException(e);
            }
        }

        protected void init(System.Type fromType, string fromMethod, string toMethod, int args = -1)
        {
            init(fromType, fromMethod, GetType(), toMethod, args);
        }

        protected void init(System.Type fromType, string fromMethod, int args = -1)
        {
            init(fromType, fromMethod, GetType(), fromMethod, args);
        }

        MethodInfo GetMethod(System.Type type, string method, int args = -1)
        {
            return args < 0 ? type.GetMethod(method, FLAGS) :
                              type.GetMethods(FLAGS).Single(m => m.Name == method && m.GetParameters().Length == args);
        }

        protected override void Deploy()
        {
            foreach (Detour d in detours)
                d.Deploy();
        }

        protected override void Revert()
        {
            foreach (Detour d in detours)
                d.Revert();
        }

        protected override void DoCleanup()
        {
            detours.Clear();
        }
    }

    class Detour
    {
        MethodInfo from, to;
        bool deployed = false;
        RedirectCallsState state;

        public Detour(MethodInfo from, MethodInfo to)
        {
            this.from = from;
            this.to = to;
        }

        public void Deploy()
        {
            try
            {
                if (!deployed)
                    state = RedirectionHelper.RedirectCalls(from, to);

                deployed = true;
            }
            catch (Exception e)
            {
                Util.DebugPrint("Detour of", from.Name, "->", to.Name, "failed");
                UnityEngine.Debug.LogException(e);
            }
        }

        public void Revert()
        {
            try
            {
                if (deployed)
                    RedirectionHelper.RevertRedirect(from, state);

                deployed = false;
            }
            catch (Exception e)
            {
                Util.DebugPrint("Revert of", from.Name, "failed");
                UnityEngine.Debug.LogException(e);
            }
        }
    }
}
