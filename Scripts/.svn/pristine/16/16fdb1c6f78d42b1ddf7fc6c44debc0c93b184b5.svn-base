using System.Collections.Generic;

namespace WorkShop.Recast.Controller
{

    /// <summary>
    /// 遗物重铸逻辑
    /// </summary>
    public class EquipRecastController: IController
    {
        public bool CanRecast(int craftID)
        {
           // return true;
            Craft_template craft_Template = Craft_templateConfig.GetCraft_template(craftID);
            if(ScriptSystem.Instance.Mana < craft_Template.manaCost)
            {
                TipManager.Instance.ShowTip("魔力不足");
                return false;
            }
            if(ScriptSystem.Instance.Gold < craft_Template.goldCost)
            {
                TipManager.Instance.ShowTip("金币不足");
                return false;
            }

            List<List<int>> cost = craft_Template.itemCost;
            for(int i = 0; i < cost.Count; i++)
            {
                if(ItemSystem.Instance.GetItemNumByTemplateID(cost[i][0])
                    < cost[i][1])
                {
                    TipManager.Instance.ShowTip("材料不足");
                    return false;
                }
            }
            return true;
        }

        public void Recast(int craftID,EquipmentData data)
        {
            Craft_template craft_Template = Craft_templateConfig.GetCraft_template(craftID);

            List<List<int>> cost = craft_Template.itemCost;
            for(int i = 0; i < cost.Count; i++)
            {
                ItemSystem.Instance.RemoveItemByuTemplateId(cost[i][0],cost[i][1]);
            }
            ScriptSystem.Instance.SubGold(craft_Template.goldCost);
            ScriptSystem.Instance.SubGold(craft_Template.manaCost);

            data.tempItemLevel = craft_Template.tempItemLevel;
            ItemSystem.Instance.AddItem(data);

            TipManager.Instance.ShowTip("活得新装备");
        }

        public EquipmentData GetEquipmentData(int equipTemplateId)
        {
            Item_instance item = Item_instanceConfig.GetItemInstance(equipTemplateId);
            EquipAttribute equip = (EquipAttribute)ItemSystem.Instance.CreateItem(equipTemplateId,false,
                ItemCreateType.Recoin,(int)item.baseItemLevel);
            EquipmentData data = (EquipmentData)equip.GetItemData();
            return data;
        }

        public void Initialize() { }
        public void Uninitialize() { }
    }
}
