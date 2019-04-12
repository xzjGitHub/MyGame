using System;


namespace Core.Controller
{
    public class CoreEventCenter: Singleton<CoreEventCenter>
    {
        private CoreEventCenter() { }

        public Action CoreChange;

        public void EmitCoreChange()
        {
            if(CoreChange != null)
            {
                CoreChange();
            }
        }

        public Action ShowCoreInfo;
        public void EmitShowCoreInfo()
        {
            if(ShowCoreInfo != null)
            {
                ShowCoreInfo();
            }
        }
    }
}

