using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProtoBuf;
using Shop.Controller;

namespace Shop.Data
{
    /// <summary>
    /// 商人系统
    /// </summary>
    public class MerchantSystem: Building
    {
        private const string MerchantPath = "Merchant";
        private MerchantData m_merchantData;

        public static MerchantSystem Instance { get; private set; }

        public MerchantSystem()
        {
            Instance = this;
        }

        public override void Init()
        {
            if(m_merchantData == null)
                m_merchantData = new MerchantData();
        }


        /// <summary>
        /// 读取存档信息
        /// </summary>
        /// <param name="parentPath"></param>
        public override void ReadData(string parentPath)
        {
            this.parentPath = parentPath;
            m_merchantData = GameDataManager.ReadData<MerchantData>(parentPath + MerchantPath) as MerchantData;
        }

        public override void SaveData(string parentPath)
        {
            GameDataManager.SaveData(parentPath,MerchantPath,m_merchantData);
        }

        public List<ShopItemInfo> GetShopGoods(ShopType shopType)
        {
            List<ShopItemInfo> list = new List<ShopItemInfo>();
            for(int i = 0; i < m_merchantData.ShopGoods.Count; i++)
            {
                if(m_merchantData.ShopGoods[i].ShopType == shopType)
                {
                    list.Add(m_merchantData.ShopGoods[i]);
                }
            }
            return list;
        }

        public void ClearShopGoods()
        {
            m_merchantData.ShopGoods.Clear();
        }

        public void AddShopGoods(ShopItemInfo info)
        {
            m_merchantData.ShopGoods.Add(info);
        }

        public void BuyShopGoods(string orderNum,int num)
        {
            ShopItemInfo shopItemInfo = GetShopItemInfo(orderNum);
            if(shopItemInfo != null)
                shopItemInfo.RemainCount -= num;
        }

        public ShopItemInfo GetShopItemInfo(string orderNum)
        {
            for(int i = 0; i < m_merchantData.ShopGoods.Count; i++)
            {
                if(m_merchantData.ShopGoods[i].OrderNum == orderNum)
                {
                    return m_merchantData.ShopGoods[i];
                }
            }
            return null;
        }

        public void AddTime(int time)
        {
            m_merchantData.Time+=time;
        }

        public void AddTime()
        {
            m_merchantData.Time++;
        }

        public void ResetTime()
        {
            m_merchantData.Time = 0;
        }

        public int GetTime()
        {
            return m_merchantData.Time;
        }
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public class ShopItemInfo
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNum;

        /// <summary>
        /// 商品数据
        /// </summary>
        public ItemData itemData;

        /// <summary>
        /// 总共得数量
        /// </summary>
        public int AllCount;

        /// <summary>
        /// 剩余数量
        /// </summary>
        public int RemainCount;

        /// <summary>
        /// 是否是金币
        /// </summary>
        public bool IsGold;

        /// <summary>
        /// 是否已经购买金币
        /// </summary>
        public bool HasBuyGold;

        /// <summary>
        /// 单价
        /// </summary>
        public int Price;

        /// <summary>
        /// 所属商店类型
        /// </summary>
        public ShopType ShopType;
    }
}



