
namespace UtilityMod
{
    public abstract class Utility
    {
        bool deployed = false;

        public bool Toggle()
        {
            if (deployed)
                Revert();
            else
                Deploy();

            deployed = !deployed;
            return deployed;
        }

        protected abstract void Deploy();

        protected abstract void Revert();

        protected virtual void DoCleanup() {}

        public void Cleanup()
        {
            if (deployed)
                Revert();

            deployed = false;
            DoCleanup();
        }
    }
}
