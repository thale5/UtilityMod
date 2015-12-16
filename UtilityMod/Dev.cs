
namespace UtilityMod
{
    class Dev : Utility
    {
        public Dev() : base(false) { }

        protected override void Deploy()
        {
            ToolController toolController = ToolsModifierControl.toolController;
            toolController.m_enableDevUI = true;
        }

        protected override void Revert(bool cleaningUp)
        {
            ToolController toolController = ToolsModifierControl.toolController;
            toolController.m_enableDevUI = false;
        }
    }
}
