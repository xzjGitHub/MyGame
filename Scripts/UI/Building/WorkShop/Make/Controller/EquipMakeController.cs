using System;
using System.Collections.Generic;


public enum MakeType
{
    Wuqi,
    FangJu,
    SiPing,
    None = 100
}

namespace WorkShop.EquipMake.Controller
{

    /// <summary>
    /// 装备制造逻辑
    /// </summary>
    public class EquipMakeController: IController
    {

        public Dictionary<MakeType,List<int>> m_dict = new Dictionary<MakeType,List<int>>();

        private void Init()
        {
            m_dict.Add(MakeType.Wuqi,new List<int>());
            m_dict[MakeType.Wuqi].Add(1001);
            m_dict[MakeType.Wuqi].Add(1002);
            m_dict[MakeType.Wuqi].Add(1003);
            m_dict[MakeType.Wuqi].Add(1004);

            m_dict.Add(MakeType.FangJu,new List<int>());
            m_dict[MakeType.FangJu].Add(1005);
            m_dict[MakeType.FangJu].Add(1006);
            //
            m_dict[MakeType.FangJu].Add(1007);

            m_dict.Add(MakeType.SiPing,new List<int>());
            m_dict[MakeType.SiPing].Add(1008);
            m_dict[MakeType.SiPing].Add(1009);
        }

        public Forge_template GetForge_Template(int forgeId)
        {
            return Forge_templateConfig.GetForge_template(forgeId);
        }

        private Core_lvup GetCore_Lvup()
        {
            int coreLevel = Core.Data.CoreSystem.Instance.GetLevel();
            return Core_lvupConfig.GetCore_lvup(coreLevel);
        }

        public int GetMaxItemLevel(int materialId,int type)
        {
            int researchLevel = WorkshopSystem.Instance.GetResearchLevel(type);
            Research_lvup research_Lvup = Research_lvupConfig.GetResearch_lvup(researchLevel);

            int researchAddMaxLevel =
               research_Lvup == null || research_Lvup.addItemLevel.Count < 2 ?
               0 : research_Lvup.addItemLevel[1];

            int materialAdd = 0;
            if(materialId != 0)
            {
                Material_template material = Material_templateConfig.GetMaterial_Template(materialId);
                materialAdd = material.addItemLevel[1];
            }

            Core_lvup core_Lvup = GetCore_Lvup();
            int coreAddLevel = core_Lvup.addItemLevel.Count < 2 ? 0 : core_Lvup.addItemLevel[1];
            int max = researchAddMaxLevel + materialAdd + coreAddLevel;

            return max;

        }

        public int GetMinItemLevel(int type)
        {
            int researchLevel = WorkshopSystem.Instance.GetResearchLevel(type);
            Research_lvup research_Lvup = Research_lvupConfig.GetResearch_lvup(researchLevel);

            int researchAddMinLevel =
                 research_Lvup == null || research_Lvup.addItemLevel.Count > 1 ?
                research_Lvup.addItemLevel[0] : 0;

            Core_lvup core_Lvup = GetCore_Lvup();

            int min = WorkshopSystem.Instance.GetEquipMakeMinLevel(type) +
                researchAddMinLevel +
                 core_Lvup.addItemLevel.Count > 1 ? core_Lvup.addItemLevel[0] : 0;
            return min;

        }



        private int GetFinalItemLevel(Research_lvup res,Material_template mat)
        {
            return Math.Min(res.finalItemLevel,mat.maxItemLevel);
        }

        private int GetRndItemLevel(Material_template mat,Parts_template part)
        {
            if(part == null)
                return 0;
            return Math.Min(part.rndItemLevel,mat.maxItemLevel);
        }

        private int GetMinUpgrade(Research_lvup res,Parts_template part)
        {
            if(part == null)
                return res.upgrade[0];
            return (res.upgrade[0] + part.addMinUpgrade);
        }

        private int GetMaxUpgrade(Research_lvup res,Material_template mat)
        {
            return (res.upgrade[1] + mat.addMaxUpgrade);
        }

        private float GetEquipRankBonus()
        {
            int coreLevel = Core.Data.CoreSystem.Instance.GetLevel();
            Core_lvup core = Core_lvupConfig.GetCore_lvup(coreLevel);
            return core.equipRankBonus;
        }

        private int GetBaseLevel(Material_template mat,int type)
        {
            int researchLevel = WorkshopSystem.Instance.GetResearchLevel(type);
            Research_lvup res = Research_lvupConfig.GetResearch_lvup(researchLevel);
            int baseItemLevel = Math.Min(mat.maxItemLevel,res.maxItemLevel[0]);
            return baseItemLevel;
        }


        private List<string> GetEndAttribute1(Parts_template part)
        {
            if(part == null)
                return null;
            return part.rndAttribute1;
        }


        public EquipAttribute CreatEquip(int type,int forgeId,ItemAttribute zcInfo,ItemAttribute fcInfo = null)
        {
            Forge_template forge = GetForge_Template(forgeId);
            int makeEquipId = forge.instance;

            Item_instance item = Item_instanceConfig.GetItemInstance(zcInfo.instanceID);
            Material_template mat = Material_templateConfig.GetMaterial_Template(zcInfo.instanceID);

            int researchLevel = WorkshopSystem.Instance.GetResearchLevel(type);
            Research_lvup res = Research_lvupConfig.GetResearch_lvup(researchLevel);

            Parts_template part = fcInfo == null ? null : Parts_templateConfig.GetParts_template(fcInfo.instanceID);

            string partName = part == null ? "" : part.preffix;
            string itemName = partName + mat.preffix + item.itemName;

            string iconName = (forgeId * 100 + researchLevel).ToString();
            int baseLevel = GetBaseLevel(mat,researchLevel);//todo ;
            int charLevelReq = mat.charLevelReq;
            int finalItemLevel = GetFinalItemLevel(res,mat);
            int rndItemLevel = GetRndItemLevel(mat,part);
            int minUpgrade = GetMinUpgrade(res,part);
            int maxUpgrade = GetMaxUpgrade(res,mat);
            float equipRankBonus = GetEquipRankBonus();
            List<string> rndAttribute1 = GetEndAttribute1(part);

            EquimentCreate ec = new EquimentCreate(makeEquipId,charLevelReq,
                itemName,iconName,rndAttribute1,ItemCreateType.Make
             ,baseLevel,mat.maxItemLevel,minUpgrade,maxUpgrade,rndItemLevel,equipRankBonus);

            EquipAttribute equip = ItemSystem.Instance.CreateItem(ec) as EquipAttribute;
            return equip;
        }

        /// <summary>
        /// 创建装备
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="costInfo">消耗的一个主材和多个辅材 主材是第一个 切记</param>
        /// <returns></returns>
        public void MakeEquip(int forgeId,ItemAttribute zcInfo,ItemAttribute fcInfo,EquipAttribute equip)
        {
            Forge_template forge = GetForge_Template(forgeId);
            ScriptSystem.Instance.SubGold((int)BuildingAttribute.Building.GetEquipMakeGoldCost(forge));
            ScriptSystem.Instance.SubMana(BuildingAttribute.Building.GetEquipMakeGoldCost(forge));

            ItemSystem.Instance.RemoveItem(zcInfo.itemID,forge.materialCost);
            ItemSystem.Instance.RemoveItem(fcInfo.itemID,forge.partsCost);

            ItemSystem.Instance.AddItem(equip.GetItemData());

            TipManager.Instance.ShowTip("获得新装备");
        }

        public bool CanMake(int forgeId,ItemAttribute zcInfo,ItemAttribute fcInfo)
        {
            Forge_template forge = GetForge_Template(forgeId);
            if(ScriptSystem.Instance.Gold < BuildingAttribute.Building.GetEquipMakeGoldCost(forge))
            {
                TipManager.Instance.ShowTip("金币不足");
                return false;
            }
            if(ScriptSystem.Instance.Mana < BuildingAttribute.Building.GetEquipMakeGoldCost(forge))
            {
                TipManager.Instance.ShowTip("魔力不足");
                return false;
            }
            if(zcInfo.sum < forge.materialCost)
            {
                TipManager.Instance.ShowTip("主材不足");
                return false;
            }

            if(fcInfo != null)
            {
                if(fcInfo.sum < forge.partsCost)
                {
                    TipManager.Instance.ShowTip("辅材不足");
                    return false;
                }
            }
            return true;
        }


        #region 接口

        public void Initialize()
        {
            Init();
        }

        public void Uninitialize()
        {
            m_dict.Clear();
        }

        #endregion
    }
}
