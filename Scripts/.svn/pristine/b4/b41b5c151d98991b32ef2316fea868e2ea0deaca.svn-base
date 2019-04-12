using Core.Data;
using GameEventDispose;
using EventCenter;

namespace Core.Controller
{
    public partial class CoreController
    {
        private int m_level;
        private bool LevelUp;

        public int LastLevel;
        public float LastPower;

        public void AddPowerAndCheckLevelUp(GameSystemEventType type,int instanceId)
        {
            if(type != GameSystemEventType.MoJing)
                return;
            AddPower(instanceId);
        }


        private void AddPower(int instanceId)
        {
            Shard_template shard_Template = Shard_templateConfig.GetShard_Template(instanceId);
            CoreSystem.Instance.UpdatePower(shard_Template.tempCorePower);
            EventCenter.EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateCoreLevelInfo);

           // UnityEngine.Debug.LogError("当前power: " + CoreSystem.Instance.GetPower());
        }

        public bool CanLevelUp()
        {
            int nowLevel = CoreSystem.Instance.GetLevel();
            Core_config core_Config = Core_configConfig.GetCore_Config();
            if(nowLevel >= core_Config.maxCoreLevel)
            {
                return false;
            }
            float havePower = CoreSystem.Instance.GetPower();
            if(havePower < GetNextLevelNeed())
            {
                return false;
            }
            return true;
        }

        public void CoreLevelUp()
        {
            while(true)
            {
                if(CanLevelUp())
                {
                    LastLevel = CoreSystem.Instance.GetLevel();
                    LastPower = CoreSystem.Instance.GetPower();
                    CoreSystem.Instance.UpdatePower(-GetNextLevelNeed()); 
                    CoreSystem.Instance.Upgrade();
                }
                else
                {
                    break;
                }
            }
            CoreEventCenter.Instance.EmitCoreChange();
            EventCenter.EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateCoreLevelInfo);
        }

        public float GetNextLevelNeed()
        {
            Core_config core_Config = Core_configConfig.GetCore_Config();
            int nowLevel = CoreSystem.Instance.GetLevel();
            if(nowLevel >= core_Config.maxCoreLevel)
            {
                return -1;
            }
            Core_lvup next = Core_lvupConfig.GetCore_lvup(nowLevel);
            if(next == null)
                return -1;
            return next.lvupCorePower;
        }
    }
}
