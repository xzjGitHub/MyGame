using College.Research.Data;

namespace College.Enchant.Controller
{
    public class EquipEnchanteController: IController
    {
        #region 接口
        public void Initialize() { }

        public void Uninitialize() { }

        #endregion

        public bool CanFomo(ItemAttribute attr)
        {
            MR_template rare = MR_templateConfig.GetTemplate(attr.instanceID);
            if(ScriptSystem.Instance.Gold < rare.goldCost)
            {
                TipManager.Instance.ShowTip("金币不足");
                return false;
            }
            if(ScriptSystem.Instance.Mana < rare.manaCost)
            {
                TipManager.Instance.ShowTip("魔力不足");
                return false;
            }
            if(attr.sum < rare.enchantCost)
            {
                TipManager.Instance.ShowTip("素材不足");
                return false;
            }
            return true;
        }

        public int GetFinalLevel(int rareId)
        {
            Item_instance item = Item_instanceConfig.GetItemInstance(rareId);
            MR_template rare = MR_templateConfig.GetTemplate(rareId);
            int researchLevel = (int)ResearchLabSystem.Instance.GetReseachLvel(rare.enchantType);
            Research_lvup res = Research_lvupConfig.GetResearch_lvup(researchLevel);
            int level = UnityEngine.Mathf.Min(item.maxItemLevel,res.maxItemLevel.Count > 1 ? res.maxItemLevel[1] : 0);
            return level;
        }


        public void Enchant(int equipId,int rareId,int itemId)
        {
            Item_instance item = Item_instanceConfig.GetItemInstance(rareId);

            ItemSystem.Instance.EquipEnchant(equipId,rareId,item.maxItemLevel);

            MR_template rare = MR_templateConfig.GetTemplate(rareId);
            ScriptSystem.Instance.SubGold(rare.goldCost);
            ScriptSystem.Instance.SubMana(rare.manaCost);

            ItemSystem.Instance.RemoveItem(itemId,rare.enchantCost);
        }

    }
}
