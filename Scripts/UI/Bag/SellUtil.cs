
public class SellUtil
{
    public static int GetItemSingalSellPrice(ItemAttribute attr)
    {
        EquipAttribute equip = attr as EquipAttribute;
        Item_instance item = Item_instanceConfig.GetItemInstance(attr.instanceID);
        if(equip != null)
        {
            equip.item_instance = item;
            return (int)equip.finalSellPrice;
        }
        return (int)item.baseSellPrice;
    }


    public static bool CanSell(ItemAttribute attr)
    {
        Item_instance item = Item_instanceConfig.GetItemInstance(attr.instanceID);
        return item.notTradable==0;
    }

}

