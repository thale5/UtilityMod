using ColossalFramework.UI;

namespace UtilityMod
{
    class Snap : Utility
    {
        readonly UIPanel optionsBar;
        UIMultiStateButton snapButton;

        public Snap() : base(false)
        {
            this.optionsBar = UIView.Find<UIPanel>("OptionsBar");
        }

        protected override void Deploy()
        {
            Click();
        }

        protected override void Revert(bool cleaningUp)
        {
            if (!cleaningUp)
                Click();
        }

        void Click()
        {
            UIMultiStateButton snap = CheckSnapButton();

            if (snap != null)
                snap.SimulateClick();
        }

        UIMultiStateButton CheckSnapButton()
        {
            if (snapButton == null || !snapButton.isVisible)
            {
                snapButton = Util.FindVisible(optionsBar, "SnappingToggle") as UIMultiStateButton;
                Util.DebugPrint("Snap button looked up");
            }

            return snapButton;
        }
    }
}
