
using System.Collections.Generic;
using Core.Data;
using Shop.Data;

namespace Shop.Controller
{
    public partial class ShopController
    {
        private void InitGold()
        {
            ShopItemInfo shopItemInfo = new ShopItemInfo();
            shopItemInfo.AllCount = 1;
            shopItemInfo.RemainCount = 1;
            shopItemInfo.OrderNum = Utility.GenerateOnlyId();
            shopItemInfo.IsGold = true;
            shopItemInfo.ShopType = ShopType.BlackShop;

            Shop_config shop_Config = Shop_configConfig.GetShop_Config();
            shopItemInfo.Price = shop_Config.baseManaToGold;

            MerchantSystem.Instance.AddShopGoods(shopItemInfo);
        }

        private void UpdateBlackMarketGoods()
        {
            InitGold();

            int zoneId = FortSystem.Instance.NewZone.ZoneId;
            Blackmarket_template blackmarket = Blackmarket_templateConfig.GetBlackmarket_Template(zoneId);
            if(blackmarket == null)
                return;

            List<int> itemRewordSet = blackmarket.itemRewardSet;
            List<float> levelList = new List<float>();
            Building_template bui = Building_templateConfig.GetBuildingTemplate((int)BuildingType.Shop);

            for(int i = 0; i < blackmarket.baseRewardLevel.Count; i++)
            {
                levelList.Add(BuildingAttribute.Building.finalRewardLevel(bui,blackmarket,i));
            }
            ItemRewardInfo itemRewardInfo = new ItemRewardInfo(levelList,itemRewordSet);

            List<ItemData> list = ItemSystem.Instance.Itemrewards_ItemDate(itemRewardInfo,false);
            if(list == null)
                return;

            for(int i = 0; i < list.Count; i++)
            {
                ItemAttribute attr = list[i] is EquipmentData ?
                    new EquipAttribute(list[i]) : new ItemAttribute(list[i]);
                ShopItemInfo shopItemInfo = new ShopItemInfo();
                shopItemInfo.AllCount = attr.sum;
                shopItemInfo.RemainCount = attr.sum;
                shopItemInfo.itemData = attr.GetItemData();
                shopItemInfo.OrderNum = Utility.GenerateOnlyId();
                shopItemInfo.IsGold = false;
                shopItemInfo.ShopType = ShopType.BlackShop;

                Item_instance item = Item_instanceConfig.GetItemInstance(attr.instanceID);
                EquipAttribute equip = attr as EquipAttribute;
                if(equip != null)
                {
                    equip.item_instance = item;
                    shopItemInfo.Price = (int)equip.finalPurchasePrice;
                }
                else
                {
                    shopItemInfo.Price = (int)item.basePurchasePrice;
                }

                MerchantSystem.Instance.AddShopGoods(shopItemInfo);
            }
        }

        public bool CanBuyBlackGoods(ShopItemInfo shopItemInfo ,int num)
        {;
            if(shopItemInfo == null)
            {
                LogHelperLSK.Log("获取商品信息出错，订单号： " + shopItemInfo.OrderNum);
                return false;
            }

            if(shopItemInfo.IsGold)
            {
                if(shopItemInfo.HasBuyGold)
                {
                    TipManager.Instance.ShowTip("一周只能购买一次");
                    return false;
                }

                Shop_config shop_Config = Shop_configConfig.GetShop_Config();
                if(ScriptSystem.Instance.Mana < shop_Config.baseManaToGold)
                {
                    TipManager.Instance.ShowTip("魔力不足");
                    return false;
                }
            }

            if(shopItemInfo.RemainCount == 0)
            {
                return false;
            }

            if(!shopItemInfo.IsGold)
            {
                int itemInstanceId = shopItemInfo.itemData.instanceID;
                Item_instance item_Instance = Item_instanceConfig.GetItemInstance(itemInstanceId);
                if(ScriptSystem.Instance.Gold < item_Instance.baseSellPrice * num)
                {
                    TipManager.Instance.ShowTip("金币不够");
                    return false;
                }
            }
            else
            {
                Shop_config shop_Config = Shop_configConfig.GetShop_Config();
                if(ScriptSystem.Instance.Mana < shop_Config.baseManaToGold)
                {
                    TipManager.Instance.ShowTip("魔力不够");
                    return false;
                }
            }
            return true;
        }

        public int GetGold()
        {
            int getGold = BuildingAttribute.Building.GetFinalGoldSales(GetCore(),GetShop_Config());
            return getGold;
        }

        public void BuyBlackGoods(ShopItemInfo shopItemInfo,int num)
        {
            if(shopItemInfo.IsGold)
            {
                Shop_config shop_Config = Shop_configConfig.GetShop_Config();
                ScriptSystem.Instance.SubMana(shop_Config.baseManaToGold);
                int getGold = GetGold();
                ScriptSystem.Instance.AddGold(getGold);
                shopItemInfo.HasBuyGold = true;
            }
            else
            {
                ScriptSystem.Instance.SubGold(shopItemInfo.Price * num);
                MerchantSystem.Instance.BuyShopGoods(shopItemInfo.OrderNum,num);

                ItemAttribute attr = shopItemInfo.itemData is EquipmentData ?
                    new EquipAttribute(shopItemInfo.itemData)
                    : new ItemAttribute(shopItemInfo.itemData);

                attr.sum = num;
                ItemSystem.Instance.AddItem(attr);
            }
        }

        private Core_lvup GetCore()
        {
            int level = CoreSystem.Instance.GetLevel();
            Core_lvup cl = Core_lvupConfig.GetCore_lvup(level);
            return cl;
        }

        private Shop_config GetShop_Config()
        {
            return Shop_configConfig.GetShop_Config();
        }
    }
}
