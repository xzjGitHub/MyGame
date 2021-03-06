﻿
using System.Collections.Generic;
using Shop.Data;

namespace Shop.Controller
{
    public partial class ShopController
    {
        private void UpdateNormalGoods()
        {

            List<int> fortList = FortSystem.Instance.UnlockForts;

            Item_instance item = null;

            Shop_config shop_Config = Shop_configConfig.GetShop_Config();
            for(int i = 0; i < shop_Config.sellList.Count; i++)
            {
                ItemAttribute attr = ItemSystem.Instance.CreateItem(shop_Config.sellList[i]);
                ShopItemInfo shopItemInfo = new ShopItemInfo();
                int cout = shop_Config.baseSupply[i] + GetExtraNum(fortList,i);
                shopItemInfo.AllCount = cout;
                shopItemInfo.RemainCount = cout;
                shopItemInfo.itemData = attr.GetItemData();
                shopItemInfo.OrderNum = Utility.GenerateOnlyId();
                shopItemInfo.ShopType = ShopType.NormalShop;
                shopItemInfo.IsGold = false;

                item = Item_instanceConfig.GetItemInstance(shop_Config.sellList[i]);
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

        private int GetExtraNum(List<int> list,int index)
        {
            Fort_template fort_Template = null;
            int sum = 0;
            for(int i = 0; i < list.Count; i++)
            {
                fort_Template = Fort_templateConfig.GetFort_template(list[i]);
                sum += fort_Template.addSupply[index];
            }
            return sum;
        }

        public bool CanBuyNormalGoods(ShopItemInfo shopItemInfo,int num)
        {
            if(shopItemInfo == null)
            {
                LogHelper_MC.Log("获取商品信息出错，订单号： " + shopItemInfo.OrderNum);
                return false;
            }
            if(shopItemInfo.RemainCount == 0)
            {
                return false;
            }
            int itemInstanceId = shopItemInfo.itemData.instanceID;
            Item_instance item_Instance = Item_instanceConfig.GetItemInstance(itemInstanceId);
            if(ScriptSystem.Instance.Gold < item_Instance.baseSellPrice * num)
            {
                TipManager.Instance.ShowTip("金币不够");
                return false;
            }
            return true;
        }

        public void BuyNormalGoods(ShopItemInfo shopItemInfo,int num)
        {
  
            ScriptSystem.Instance.SubGold(shopItemInfo.Price * num);

            ItemAttribute attr = shopItemInfo.itemData is EquipmentData
                ? new EquipAttribute(shopItemInfo.itemData)
                : new ItemAttribute(shopItemInfo.itemData);
            attr.sum = num;
            ItemSystem.Instance.AddItem(attr);
            MerchantSystem.Instance.BuyShopGoods(shopItemInfo.OrderNum,num);
        }
    }
}
