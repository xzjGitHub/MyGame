using Barrack.Data;
using College.Research.Data;
using Core.Data;
using GameEventDispose;

namespace Barrack.Controller
{
    public partial class BarrackController: IController
    {
        public void Initialize()
        {
            EventDispatcher.Instance.ScriptTimeEvent.AddEventListener<ScriptTimeUpdateType,object>(
                EventId.ScriptTimeEvent,OnScriptTimeUpdateEvent);
        }

        public void Uninitialize()
        {
            EventDispatcher.Instance.ScriptTimeEvent.RemoveEventListener<ScriptTimeUpdateType,object>(
                EventId.ScriptTimeEvent,OnScriptTimeUpdateEvent);
        }


        private void OnScriptTimeUpdateEvent(ScriptTimeUpdateType arg1,object arg2)
        {
            if(arg1 == ScriptTimeUpdateType.Second)
            {
                SecondUpdateTrain();
            }
            if(arg1 == ScriptTimeUpdateType.Day)
            {
                UpdateDay();
                DayUpdateTrain();
            }
        }

        public bool RemoveCharFromWork(int charId)
        {
            if(BarrackSystem.Instance.HavIn(charId))
            {
                BarrackSystem.Instance.RemoveCharFromUse(charId);
                CharSystem.Instance.SetCharSate(charId,CharStatus.Idle);
                return true;
            }
            return false;
        }


        public bool AddCharToWork(int charId,CharStatus status)
        {
            WorkCharInfo info = BarrackSystem.Instance.GetWorkCharInfo(charId);
            if(info != null)
            {
                info.Status = status;
                return true;
            }
            if (CanAddChar())
            {
                WorkCharInfo workCharInfo = new WorkCharInfo(charId,status,true);
                BarrackSystem.Instance.AddCharToUse(workCharInfo);
                CharSystem.Instance.SetCharSate(charId,status);
                return true;
            }
            return false;
        }

        public bool UpdateCharUseInfo(int charId,CharStatus status)
        {
            if(status == CharStatus.Idle)
            {
                return RemoveCharFromWork(charId);
            }
            return AddCharToWork(charId,status); ;
        }

        private bool CanAddChar()
        {
            if(BarrackSystem.Instance.GetCurrentWorkNum() >= GetMaxWorkChar())
            {
                TipManager.Instance.ShowTip("超过最大工作上限，无法继续加入角色");
                return false;
            }
            return true;
        }

        public int GetMaxWorkChar()
        {
            return GetCharList().Count;
        }

        public bool CanSelectChar()
        {
            return BarrackSystem.Instance.GetCurrentWorkNum() < GetMaxWorkChar();
        }

        public int GetGold(int charNum)
        {
            HR_config hR_Config = HR_configConfig.GetHR_Config();
            int coreLevel = CoreSystem.Instance.GetLevel();
            Core_lvup core_Lvup = Core_lvupConfig.GetCore_lvup(coreLevel);
            //HR_config.baseGoldOutput float * 金币产出的角色数 * （ 1 + Core_lvup. resourceOutPutBonus ）；
            float value = hR_Config.baseGoldOutput * charNum * (1 + core_Lvup.resourceOutPutBonus);
            return (int)value;
        }

        public int GetGold()
        {
            int charCount = BarrackSystem.Instance.GetUserTypeCharCount(CharStatus.GoldProduce);
            return GetGold(charCount);
        }


        public float GetAllManaCost()
        {
            HR_config hR_Config = HR_configConfig.GetHR_Config();
            int goldCount = BarrackSystem.Instance.GetCharStatusList(CharStatus.GoldProduce).Count;
            int resCount = 0;
            if (ResearchLabSystem.Instance.GetResearchingInfoList().Count > 0)
            {
                resCount= BarrackSystem.Instance.GetCharStatusList(CharStatus.EquipResearch).Count;
            }
            int enchantCount = 0;
            if (WorkshopSystem.Instance.GetNowWorkResearchList().Count > 0)
            {
                resCount = BarrackSystem.Instance.GetCharStatusList(CharStatus.EnchantResearch).Count;
            }
            return (goldCount+resCount+enchantCount) * BuildingAttribute.Building.GetUnitManaCost(hR_Config);
        }

        public float GetResearchOrEnchantEff(int charNum)
        {
            HR_config hR_Config = HR_configConfig.GetHR_Config();
            return hR_Config.researchBonus * charNum;
        }
    }
}