using System;
using System.Collections.Generic;
using GameEventDispose;

namespace WorkShop.EquipResearch.Controller
{
    public class EquipResearchController: IController
    {
        private float m_manaCost;
        private EquipResearchInfo m_researchInfo;

        // id   状态（1：添加 2：时间到了 3:暂停 4：取消 5:移除
        public Action<EquipResearchInfo,int> EquipResearchChange;

        //[id,总时间，已经用了的时间,活得的经验]
        public Action<string,float,int,float> CountDownAction;

        private int temp;
        private Game_config m_gameConfig;

        private void AddResTime()
        {
            WorkshopSystem.Instance.AddRessTime(TimeUtil.DaySeconds);
        }

        private void SecondUpdate()
        {
            if(WorkshopSystem.Instance.GetResearchList().Count <= 0)
                return;
            for(int i = 0; i < WorkshopSystem.Instance.GetResearchList().Count; i++)
            {
                m_researchInfo = WorkshopSystem.Instance.GetResearchList()[i];
                if(m_researchInfo.SubManaSuc && (m_researchInfo.HaveUseTime < m_researchInfo.NeedTime))
                {
                    m_researchInfo.HaveUseTime++;

                    temp = m_researchInfo.HaveUseTime % TimeUtil.DaySeconds;

                    if(temp == 0)
                    {
                        DayAdd(m_researchInfo);
                    }
                }
            }
        }

        private void DayAdd(EquipResearchInfo equipResearch)
        {
            //item里面来
            EquipAttribute equipAttribute = ItemSystem.Instance.GetEquipAttribute(equipResearch.EquipId);

            Equip_template equip = Equip_templateConfig.GetEquip_template(equipAttribute.equipRnd.templateID);

            Item_instance item = Item_instanceConfig.GetItemInstance(equipAttribute.instanceID);

            ER_template eR_Template = ER_templateConfig.GetER_template(item.ERTemplate);

            int nowLevel = WorkshopSystem.Instance.GetResearchLevel(equip.REType);
            if(nowLevel >= Research_lvupConfig.GetMaxLevel())
                return;

            equipAttribute.game_config = m_gameConfig;
            equipAttribute.hr_config = HR_configConfig.GetHR_Config();

            int currentLevel = WorkshopSystem.Instance.GetResearchLevel(equip.REType);
            int REExpReward = 0;

            if(eR_Template.activeRELevel.Count >= 2 && eR_Template.REExpReward.Count >= 2)
            {
                if(currentLevel <= eR_Template.activeRELevel[0])
                {
                    REExpReward = eR_Template.REExpReward[1];
                }
                if(currentLevel > eR_Template.activeRELevel[0] && currentLevel <= eR_Template.activeRELevel[1])
                {
                    REExpReward = eR_Template.REExpReward[0];
                }
            }
            equipAttribute.ERExpReward = REExpReward;
            equipResearch.Exp += equipAttribute.dailyERExp;
     
            WorkshopSystem.Instance.AddExp(equip.REType,equipAttribute.dailyERExp);

            if(equipResearch.HaveUseTime >= equipResearch.NeedTime)
            {
                ResearchTimeOut(equipResearch);
            }

            if(CountDownAction != null)
            {
                CountDownAction(equipResearch.WorkId,equipResearch.NeedTime,
                    equipResearch.HaveUseTime,equipResearch.Exp);
            }

          //  UnityEngine.Debug.LogError("到达一天" + "  添加exp: " + equipAttribute.dailyERExp);
        }

        private void DayCostMana()
        {
            if(WorkshopSystem.Instance.GetResearchList().Count <= 0)
                return;
            EquipResearchInfo info = null;
            int cout = WorkshopSystem.Instance.GetResearchList().Count;
            for(int i = 0; i < cout; i++)
            {
                info = WorkshopSystem.Instance.GetResearchList()[i];
                m_manaCost = GetManaCost();

                bool manaEnough = ScriptSystem.Instance.Mana >= m_manaCost;
                if(manaEnough && cout > 1)
                {
                    ScriptSystem.Instance.SubMana((int)m_manaCost);
                    // LogHelperLSK.LogError("扣除消耗： " + m_manaCost);
                }
                info.SubManaSuc = manaEnough;
            }
        }

        //时间到了
        public void ResearchTimeOut(EquipResearchInfo equipResearch)
        {
            //判断是否升级
            EquipAttribute equipAttribute = ItemSystem.Instance.GetEquipAttribute(equipResearch.EquipId);
            Equip_template equip = Equip_templateConfig.GetEquip_template(equipAttribute.equipRnd.templateID);
            Item_instance item = Item_instanceConfig.GetItemInstance(equipAttribute.instanceID);

            ER_template eR_Template = ER_templateConfig.GetER_template(item.ERTemplate);

            int nowLevel = WorkshopSystem.Instance.GetResearchLevel(equip.REType);
            if(nowLevel < Research_lvupConfig.GetMaxLevel())
            {
                float nowExp = WorkshopSystem.Instance.GetExp(equip.REType);
                int level = Research_lvupConfig.GetNowLevelByExp(nowExp,nowLevel);
                if(level != 0)
                {
                    WorkshopSystem.Instance.SetLevel(equip.REType,level);
                    UnityEngine.Debug.LogError("reType: " + equip.REType + "  当前等级: " + level);
                }
            }

            if(EquipResearchChange != null)
            {
                EquipResearchChange(equipResearch,2);
            }
        }

        public void ResearchEnd(EquipResearchInfo info)
        {
            //扣除装备
            ItemSystem.Instance.RemoveItem(info.EquipId,1);
            if(EquipResearchChange != null)
            {
                EquipResearchChange(info,5);
            }
            WorkshopSystem.Instance.RemoveResearch(info.WorkId);
        }

        ////eR_Template.REType 替换成 equip_template.ERType
        ////最小制造等级提升：
        //private void CheckAddMinEquipMakeLevel(int equipInstanceId)
        //{
        //    int randomNum = UnityEngine.Random.Range(0,1000);
        //    int addMinLevel = 0;
        //    ER_template eR_Template = ER_templateConfig.GetER_template(equipInstanceId);
        //    if(randomNum >= m_gameConfig.minLvRewardChance)
        //    {
        //        //几率奖励的等级
        //        float randomValue = UnityEngine.Random.Range(1 - m_gameConfig.researchDeviation,1 + m_gameConfig.researchDeviation);
        //        addMinLevel += (int)(eR_Template.addMinItemLevel * randomValue);
        //        WorkshopSystem.Instance.AddMinEquipMakeLevel(eR_Template.REType,addMinLevel);
        //        // LogHelperLSK.Log("提升最小制造等级： " + addMinLevel);
        //    }
        //}

        public void CancelResearch(EquipResearchInfo equipResearch)
        {
            EquipAttribute attr = ItemSystem.Instance.GetEquipAttribute(equipResearch.EquipId);
            attr.SetEquipState(EquipState.Idle);
            if(EquipResearchChange != null)
            {
                EquipResearchChange(m_researchInfo,4);
            }
            WorkshopSystem.Instance.RemoveResearch(equipResearch.WorkId);
        }

        public EquipResearchInfo Research(EquipAttribute attr)
        {

            Item_instance item = Item_instanceConfig.GetItemInstance(attr.instanceID);
            ER_template eR_Template = ER_templateConfig.GetER_template(item.ERTemplate);

            Equip_template equip = Equip_templateConfig.GetEquip_template(attr.equipRnd.templateID);
            EquipResearchInfo info = new EquipResearchInfo();
            info.EquipId = attr.itemID;
            info.NeedTime = GetErTime();
            info.WorkId = attr.itemID.ToString();
            int level = WorkshopSystem.Instance.GetResearchLevel(equip.REType);
            info.Exp = 0;
            info.Level = level;
            float cost = GetManaCost();
            if(ScriptSystem.Instance.Mana >= cost)
            {
                ScriptSystem.Instance.SubMana((int)cost);
                info.SubManaSuc = true;
            }
            else
            {
                info.SubManaSuc = false;
            }

            WorkshopSystem.Instance.AddResearch(info);
            attr.SetEquipState(EquipState.Researching);
            if(EquipResearchChange != null)
            {
                EquipResearchChange(info,1);
            }

            return info;
        }

        public bool CanResearch(EquipAttribute attr)
        {
            if(WorkshopSystem.Instance.GetResearchList().Count >= GetMaxWorkNum())
            {
                TipManager.Instance.ShowTip("超过并行研究上限");
                return false;
            }
            return true;
        }

        public float GetManaCost()
        {
            HR_config hR_Config = HR_configConfig.GetHR_Config();
            return BuildingAttribute.Building.GetUnitManaCost(hR_Config);
        }

        private float GetErTime()
        {
            UnityEngine.Debug.LogError("needTime: " + m_gameConfig.productionCycle * TimeUtil.DaySeconds);
            return m_gameConfig.productionCycle * TimeUtil.DaySeconds;
        }

        public List<ItemAttribute> GetEquipList()
        {
            List<ItemAttribute> all = ItemSystem.Instance.GetAllEquip();
            return all;
        }

        public int GetMaxWorkNum()
        {
            int coreLevel = Core.Data.CoreSystem.Instance.GetLevel();
            Core_lvup core_Lvup = Core_lvupConfig.GetCore_lvup(coreLevel);
            return core_Lvup.workshopLine;
            //todo 
            // return 3;
        }

        public int GetCorrentNum()
        {
            return WorkshopSystem.Instance.GetResearchList().Count;
        }

        #region 接口
        public void Initialize()
        {
            m_gameConfig = Game_configConfig.GetGame_Config();
            EventDispatcher.Instance.ScriptTimeEvent.AddEventListener<ScriptTimeUpdateType,object>(EventId.ScriptTimeEvent,OnScriptTimeUpdateEvent);

        }

        public void Uninitialize()
        {
            EventDispatcher.Instance.ScriptTimeEvent.RemoveEventListener<ScriptTimeUpdateType,object>(EventId.ScriptTimeEvent,OnScriptTimeUpdateEvent);
        }

        #endregion

        public static string GetName(int type)
        {
            switch(type)
            {
                case 1:
                    return "金属武器研究";
                case 2:
                    return "灵木武器研究";
                case 3:
                    return "防具研究";
                case 4:
                    return "饰品研究";
                default:
                    return "";
            }
        }

        private void OnScriptTimeUpdateEvent(ScriptTimeUpdateType arg1,object arg2)
        {
            if(arg1 == ScriptTimeUpdateType.Second)
            {
                SecondUpdate();
            }
            if(arg1 == ScriptTimeUpdateType.Day)
            {
                DayCostMana();
            }
        }
    }
}
