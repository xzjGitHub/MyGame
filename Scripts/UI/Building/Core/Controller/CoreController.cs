﻿using Core.Data;
using GameEventDispose;

namespace Core.Controller
{
    public partial class CoreController: IController
    {

        private Core_lvup m_coreUp = new Core_lvup();
        private int m_coreLevel;
        private Building_template m_bui = Building_templateConfig.GetBuildingTemplate((int)BuildingType.Core);


        public void Initialize()
        {
            EventDispatcher.Instance.ScriptTimeEvent.AddEventListener<ScriptTimeUpdateType,object>(
                EventId.ScriptTimeEvent,OnScriptTimeUpdateEvent);
            EventDispatcher.Instance.SystemEvent.AddEventListener<GameSystemEventType,int>(EventId.SystemEvent,AddPowerAndCheckLevelUp);

        }


        public void Uninitialize()
        {
            EventDispatcher.Instance.ScriptTimeEvent.RemoveEventListener<ScriptTimeUpdateType,object>(
                EventId.ScriptTimeEvent,OnScriptTimeUpdateEvent);
            EventDispatcher.Instance.SystemEvent.RemoveEventListener<GameSystemEventType,int>(EventId.SystemEvent,AddPowerAndCheckLevelUp);
        }

        private void DayAdd()
        {
            CoreSystem.Instance.UpdateMana(GetSecondProduce() * TimeUtil.DaySeconds);
        }

        private void OnScriptTimeUpdateEvent(ScriptTimeUpdateType arg1,object arg2)
        {
            if(arg1 == ScriptTimeUpdateType.Second)
            {
                SecondUpdate();
            }
            if(arg1 == ScriptTimeUpdateType.Day)
            {
                DayUpdate();
            }
            if(arg1 == ScriptTimeUpdateType.Month)
            {
                MonthUpdate();
            }
        }

        private void SecondUpdate()
        {
            CoreSystem.Instance.UpdateMana(GetSecondProduce());
        }

        private void DayUpdate()
        {
            int nowMana = (int)CoreSystem.Instance.GetMana();
            // LogHelper_MC.Log("核心产出魔力： " + nowMana);
            ScriptSystem.Instance.AddMana(nowMana);
            CoreSystem.Instance.ResetMana();
        }

        private void MonthUpdate()
        {
            Core_config core_Config = Core_configConfig.GetCore_Config();
            float random = UnityEngine.Random.Range(1 - core_Config.deviation,1 + core_Config.deviation);
            CoreSystem.Instance.UpdateEfficiency(random);
        }


        private int GetSecondProduce()
        {
            m_coreLevel = CoreSystem.Instance.GetLevel();
            m_coreUp = Core_lvupConfig.GetCore_lvup(m_coreLevel);

            int secondGet = (int)BuildingAttribute.Building.GetFinalRewardVaue(m_coreUp,m_bui);
            return secondGet;
        }


        public float GetMaxPower()
        {
            return Core_lvupConfig.GetMaxLevelNeedPower();
        }

        public float GetNowPower()
        {
            return CoreSystem.Instance.GetPower();
        }

    }
}
