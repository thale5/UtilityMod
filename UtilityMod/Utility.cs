using ColossalFramework.UI;
using UnityEngine;

namespace UtilityMod
{
    public abstract class Utility
    {
        public Id id { get; set; }
        UILabel label;
        bool deployed = false;
        readonly bool hasLabel;

        const float LABEL_OFFSET_X = 0.16f;
        const float LABEL_OFFSET_Y = 0.97f;
        const float LABEL_SPACING_X = 0.16f;
        const float LABEL_SPACING_Y = -0.05f;
        const int ROWS = 3;

        public Utility() : this(true) { }
        
        public Utility(bool hasLabel)
        {
            this.hasLabel = hasLabel;
        }

        public void Toggle()
        {
            if (deployed)
                Revert(false);
            else
                Deploy();

            deployed = !deployed;

            if (hasLabel)
                SetVisible();
        }

        protected abstract void Deploy();

        protected abstract void Revert(bool cleaningUp);

        protected virtual void DoCleanup() {}

        public void Cleanup()
        {
            if (deployed)
                Revert(true);

            if (label != null)
                UnityEngine.Object.Destroy(label);

            deployed = false;
            label = null;
            DoCleanup();
        }

        void SetVisible()
        {
            // Lazy initialization on the first use.
            if (label == null)
            {
                Util.DebugPrint("Creating", id.ToString(), "label");
                UIView view = ColossalFramework.UI.UIView.GetAView();
                label = (UILabel) view.AddUIComponent(typeof(UILabel));
                float x = (int) id / ROWS * LABEL_SPACING_X + LABEL_OFFSET_X, y = (int) id % ROWS * LABEL_SPACING_Y + LABEL_OFFSET_Y;
                label.transformPosition = new Vector3(x, y);
                label.textColor = new Color32(240, 240, 240, 192);
                label.autoSize = false;
                label.textScale = 1.4f;
                label.dropShadowColor = new Color32(64, 64, 64, 64);
                label.dropShadowOffset = new Vector2(2f, -1.5f);
                label.useDropShadow = true;
                label.text = id.ToString();
            }

            label.isVisible = deployed;
            label.enabled = deployed;
        }
    }
}
