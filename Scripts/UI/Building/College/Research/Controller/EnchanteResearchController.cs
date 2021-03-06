﻿using System;
using System.Collections.Generic;
using College.Research.Data;
using Core.Data;
using GameEventDispose;

namespace College.Research.Controller
{
    /// <summary>
    /// 附魔研究
    /// </summary>
    public class EnchanteResearchController: IController
    {
        private ResearchingInfo m_resInfo;
        private List<ResearchingInfo> m_info = new List<ResearchingInfo>();

        //[id,总时间，已经用了的时间,已经增加的经验]
        public Action<string,float,int,int> CountDownAction;
        // id   状态（1：添加 2：时间到了 3:取消 4：结束
        public Action<ResearchingInfo,int> ResearchChange;

        private Game_config m_gameConfig;

        public void Initialize()
        {
            m_gameConfig = Game_configConfig.GetGame_Config();
            EventDispatcher.Instance.ScriptTimeEvent.AddEventListener<ScriptTimeUpdateType,object>(EventId.ScriptTimeEvent,OnScriptTimeUpdateEvent);
        }

        public void Uninitialize()
        {
            EventDispatcher.Instance.ScriptTimeEvent.RemoveEventListener<ScriptTimeUpdateType,object>(EventId.ScriptTimeEvent,OnScriptTimeUpdateEvent);
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

        private float manaCost;
        private int m_temp;

        private void AddResearchingsTime()
        {
            ResearchLabSystem.Instance.AddResearchingsTime(TimeUtil.DaySeconds);
        }

        private void SecondUpdate()
        {
            m_info = ResearchLabSystem.Instance.GetResearchingInfoList();
            for(int i = 0; i < m_info.Count; i++)
            {
                m_resInfo = m_info[i];
                if(m_resInfo.SubManaSuc && m_resInfo.HaveUseTime < m_resInfo.NeedTime)
                {
                    m_resInfo.HaveUseTime++;

                    m_temp = m_resInfo.HaveUseTime % TimeUtil.DaySeconds;

                    if(m_temp == 0)
                    {
                        DayAdd(m_resInfo);
                    }

                    if(CountDownAction != null)
                    {
                        CountDownAction(m_resInfo.Id,m_resInfo.NeedTime,m_resInfo.HaveUseTime,(int)m_resInfo.Exp);
                    }

                    if(m_resInfo.HaveUseTime >= m_resInfo.NeedTime)
                    {
                        ResearchEnd(m_resInfo);
                        if(ResearchChange != null)
                        {
                            ResearchChange(m_resInfo,2);
                        }
                    }
                }
            }
        }

        private void DayAdd(ResearchingInfo info)
        {
            MR_template rare = MR_templateConfig.GetTemplate(info.Data.instanceID);
            int nowLevel = (int)ResearchLabSystem.Instance.GetReseachLvel(rare.enchantType);

            int enchantExpReward = 0;
            if(nowLevel <= rare.activeEnchantLevel[0])
            {
                enchantExpReward = rare.enchantExpReward[1];
            }
            else if(nowLevel > rare.activeEnchantLevel[0]
                     && nowLevel < rare.activeEnchantLevel[1])
            {
                enchantExpReward = rare.enchantExpReward[0];
            }
            else
            {
                enchantExpReward = 0;
            }

            int addExp = RareMaterialAttribute.Instance.GetDailExp(enchantExpReward,
                GetGame_Config(),GetCorrentNum(),GetHR_Config());

            ResearchLabSystem.Instance.UpdateExp(rare.enchantType,addExp);
            info.Exp += addExp;

            UnityEngine.Debug.Log("附魔添加经验： " + addExp);
        }

        private void DayCostMana()
        {
            if(ResearchLabSystem.Instance.GetResearchingInfoList().Count <= 0)
                return;
            int cout = ResearchLabSystem.Instance.GetResearchingInfoList().Count;
            for(int i = 0; i < cout; i++)
            {
                ResearchingInfo info = ResearchLabSystem.Instance.GetResearchingInfoList()[i];
                float m_manaCost = GetManaCost();

                bool manaEnough = ScriptSystem.Instance.Mana >= m_manaCost;
                if(manaEnough && cout > 1)
                {
                    ScriptSystem.Instance.SubMana((int)m_manaCost);
                    LogHelper_MC.Log("扣除消耗： " + m_manaCost);
                }
                info.SubManaSuc = manaEnough;
            }
        }

        public void EndResearch(ResearchingInfo info)
        {
            if(ResearchChange != null)
            {
                ResearchChange(info,4);
            }
            ResearchLabSystem.Instance.RemoveRearch(info.Id);
        }

        private void ResearchEnd(ResearchingInfo info)
        {
            CheckLevelUp(info.Data.instanceID);
            CheckMinEnchantLevel(info.Data.instanceID);
        }

        private void CheckLevelUp(int rareId)
        {
            MR_template rare = MR_templateConfig.GetTemplate(rareId);

            int nowLevel = (int)ResearchLabSystem.Instance.GetReseachLvel(rare.enchantType);
            if(nowLevel < Research_lvupConfig.GetMaxLevel())
            {
                int nowExp = ResearchLabSystem.Instance.GetReseachExp(rare.enchantType);
                int level = Research_lvupConfig.GetNowLevelByExp(nowExp,nowLevel);
                ResearchLabSystem.Instance.UpdateReseachLevel(rare.enchantType,level);

                UnityEngine.Debug.LogError("附魔等级提升：  " + level + " nowExp: " + nowExp);
            }
        }

        private void CheckMinEnchantLevel(int rareId)
        {
            Game_config game_Config = GetGame_Config();
            int ran = UnityEngine.Random.Range(0,10000);
            if(ran >= game_Config.minLvRewardChance)
            {
                UnityEngine.Debug.Log("提升最小附魔等级失败");
                return;
            }
            MR_template rare = MR_templateConfig.GetTemplate(rareId);
            float random = UnityEngine.Random.Range(1 - game_Config.researchDeviation,1 + game_Config.researchDeviation);
            int minLevel = (int)(rare.addMinEnchantLevel * random);
            ResearchLabSystem.Instance.AddMinLevel(rare.enchantType,minLevel);
        }


        public bool CanReseach(ItemAttribute attr)
        {
            MR_template rare = MR_templateConfig.GetTemplate(attr.instanceID);
            int haveResCount = ResearchLabSystem.Instance.GetResearchingInfoList().Count;
            if(haveResCount >= GetMaxWorkNum())
            {
                TipManager.Instance.ShowTip("已经达到同时研究上限");
                return false;
            }

            int haveCount = ItemSystem.Instance.GetItemNum(attr.itemID);
            if(haveCount < rare.researchCost)
            {
                TipManager.Instance.ShowTip("数量不够");
                return false;
            }
            return true;
        }

        public void Research(ItemAttribute attr)
        {
            MR_template rare = MR_templateConfig.GetTemplate(attr.instanceID);

            ResearchingInfo researchingInfo = new ResearchingInfo();
            researchingInfo.Id = Utility.GenerateOnlyId();
            researchingInfo.Data = attr.GetItemData();
            researchingInfo.Level = (int)ResearchLabSystem.Instance.GetReseachLvel(rare.enchantType);
            researchingInfo.MinLevel = ResearchLabSystem.Instance.GetMinLeve(rare.enchantType);
            researchingInfo.Exp = 0;
            researchingInfo.EffectId = -1;
            researchingInfo.NeedTime = GetResTime();

            float cost = GetManaCost();
            bool canCost = ScriptSystem.Instance.Mana >= cost;
            if(canCost)
            {
                ScriptSystem.Instance.SubMana((int)cost);
            }
            researchingInfo.SubManaSuc = canCost;
            ResearchLabSystem.Instance.AddRearch(researchingInfo);

            if(ResearchChange != null)
            {
                ResearchChange(researchingInfo,1);
            }
            ItemSystem.Instance.RemoveItem(attr.itemID,rare.researchCost);
        }

        public void CancelReseach(ResearchingInfo info)
        {
            MR_template rare = MR_templateConfig.GetTemplate(info.Data.instanceID);
            ItemSystem.Instance.AddItem(info.Data.itemID,info.Data.instanceID,rare.researchCost);
            ResearchLabSystem.Instance.RemoveRearch(info.Id);
            if(ResearchChange != null)
            {
                ResearchChange(info,3);
            }
        }

        public float GetManaCost()
        {
            HR_config hR_Config = HR_configConfig.GetHR_Config();
            return BuildingAttribute.Building.GetUnitManaCost(hR_Config);
        }

        private float GetResTime()
        {
            return m_gameConfig.productionCycle * TimeUtil.DaySeconds;

            // return 15;
        }

        public int GetCorrentNum()
        {
            return ResearchLabSystem.Instance.GetResearchingInfoList().Count;
        }

        public int GetMaxWorkNum()
        {
            int corelevel = CoreSystem.Instance.GetLevel();
            Core_lvup core_lvup = Core_lvupConfig.GetCore_lvup(corelevel);
            return core_lvup.academyLine;
        }

        private Game_config GetGame_Config()
        {
            return Game_configConfig.GetGame_Config();
        }

        private HR_config GetHR_Config()
        {
            return HR_configConfig.GetHR_Config();
        }

        public static string GetName(int type)
        {
            switch(type)
            {
                case 1:
                    return "天使研究";
                case 2:
                    return "恶魔研究";
                case 3:
                    return "元素研究";
                case 4:
                    return "巨龙研究";
                default:
                    return "天使恶魔";
            }
        }
    }

}
