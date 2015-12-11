using ColossalFramework.UI;
using System;
using UnityEngine;

namespace UtilityMod
{
    /// <summary>
    /// Available utilities.
    /// </summary>
    enum Id
    {
        Anarchy,
        Angles,
        Water,
        Test,
        Size
    };

    /// <summary>
    /// This class controls the utilities.
    /// </summary>
    public class Controller
    {
        Utility[] utilities = new Utility[(int) Id.Size];
        UILabel[] labels = new UILabel[(int) Id.Size];
        readonly bool recompiled;

        const float LABEL_POS_X = 0.15f;
        const float LABEL_POS_Y = 0.97f;
        const float LABEL_SPACING = 0.05f;

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
                if (Input.GetKeyDown(KeyCode.L))
                    Toggle(Id.Anarchy);
                else if (Input.GetKeyDown(KeyCode.K))
                    Toggle(Id.Angles);
                else if (Input.GetKeyDown(KeyCode.W))
                    Toggle(Id.Water);
                else if (Input.GetKeyDown(KeyCode.T))
                    Toggle(Id.Test);
            }
        }

        public void Unloading()
        {
            Util.DebugPrint("Controller.Unloading");

            for (int i = 0; i < utilities.Length; i++)
            {
                if (labels[i] != null)
                {
                    UnityEngine.Object.Destroy(labels[i]);
                    labels[i] = null;
                }

                if (utilities[i] != null)
                {
                    utilities[i].Cleanup();
                    utilities[i] = null;
                }
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
            }

            bool enabled = utility.Toggle();
            SetVisible(id, enabled);
        }

        void SetVisible(Id id, bool visible)
        {
            UILabel label = labels[(int) id];

            // Lazy initialization on the first use.
            if (label == null)
            {
                Util.DebugPrint("Creating", id.ToString(), "label");
                float pos_x = LABEL_POS_X;
                UIView view = ColossalFramework.UI.UIView.GetAView();
                label = labels[(int) id] = (UILabel) view.AddUIComponent(typeof(UILabel));
                label.transformPosition = new Vector3(pos_x, LABEL_POS_Y - (int) id * LABEL_SPACING);
                label.textColor = new Color32(240, 240, 240, 192);
                label.autoSize = false;
                label.textScale = 1.4f;
                label.dropShadowColor = new Color32(64, 64, 64, 64);
                label.dropShadowOffset = new Vector2(2f, -1.5f);
                label.useDropShadow = true;
                label.text = id.ToString();
            }

            label.isVisible = visible;
            label.enabled = visible;
        }
    }
}
