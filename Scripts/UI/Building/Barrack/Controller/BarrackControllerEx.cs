using System;
using System.Collections.Generic;
using Barrack.Data;
using College.Research.Data;

namespace Barrack.Controller
{
    public partial class BarrackController
    {
        public Action<int,bool> CharCanWork;


        private void UpdateDay()
        {
            HR_config hR_Config = HR_configConfig.GetHR_Config();
            float cost = BuildingAttribute.Building.GetUnitManaCost(hR_Config);

            List<WorkCharInfo> goldList = BarrackSystem.Instance.GetCharStatusList(CharStatus.GoldProduce);
            List<WorkCharInfo> researchList = BarrackSystem.Instance.GetCharStatusList(CharStatus.EquipResearch);
            List<WorkCharInfo> enchantList = BarrackSystem.Instance.GetCharStatusList(CharStatus.EnchantResearch);

            int max = goldList.Count > researchList.Count ? goldList.Count : researchList.Count;
            max = max > enchantList.Count ? max : enchantList.Count;

            //扣除规则   a列表第一个 b第列表第一个 c列表第一个 依次类推
            for (int i = 0; i < max; i++)
            {
                if (goldList.Count > i)
                {
                    SubMana(goldList[i], cost);
                }
                if(researchList.Count > i)
                {
                    SubMana(researchList[i],cost);
                }
                if(enchantList.Count > i)
                {
                    SubMana(enchantList[i],cost);
                }
            }
            AddGold();
        }

        private void SubMana(WorkCharInfo info,float cost)
        {
            if(NeedSubMana(info.Status))
            {
                if(ScriptSystem.Instance.Mana >= cost)
                {
                    ScriptSystem.Instance.SubMana(cost);
                    BarrackSystem.Instance.UpdateCharInfo(info.CharId,true);
                }
                else
                {
                    BarrackSystem.Instance.UpdateCharInfo(info.CharId,false);
                }

                if(CharCanWork != null)
                {
                    CharCanWork(info.CharId,ScriptSystem.Instance.Mana >= cost);
                }
            }
        }

        private bool NeedSubMana(CharStatus useCharType)
        {
            switch(useCharType)
            {
                case CharStatus.GoldProduce:
                    return true;
                case CharStatus.EnchantResearch:
                    return ResearchLabSystem.Instance.GetResearchingInfoList().Count>0;
                case CharStatus.EquipResearch:
                    return WorkshopSystem.Instance.GetNowWorkResearchList().Count > 0;
                default:
                    return false;
            }
        }

        private void AddGold()
        {
            int getGold = GetGold();
            ScriptSystem.Instance.AddGold(getGold);
        }


        public List<CharAttribute> GetCharList()
        {
            int value = (int)CharStatus.Idle | (int)CharStatus.GoldProduce |
                (int)CharStatus.EquipResearch | (int)CharStatus.EnchantResearch;
            List<CharAttribute> list = CharSystem.Instance.GetCharinCharListByStatusAndPlayerType(value,PlayerType.All);

            return list;
        }
    }
}