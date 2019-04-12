using Shop.Data;
using System.Collections.Generic;
using Shop.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.View
{
    public class ShopDetial: MonoBehaviour
    {
        private GameObject m_detial;
        private GameObject m_buyBtn;

        private ShopList m_shopList;
        private ShopInfo m_shopInfo;

        private ShopType m_shopType;

        private Text m_refreshTime;

        private bool m_hasInit;

        private ShopItemInfo m_currentSlect;

        private void OnDisable()
        {
            Reset();
            RemoveEvent();
        }

        public void RemoveEvent()
        {
            ControllerCenter.Instance.ShopController.RefreshShop -= UpdateGoods;
            ControllerCenter.Instance.ShopController.RefreshTime -= UpdateRefreshTime;
        }

        public void Reset()
        {
            m_shopType = ShopType.None;
            GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.ShopItem);
            m_currentSlect = null;
        }

        private void InitComponent()
        {
            m_shopList = Utility.RequireComponent<ShopList>(transform.Find("Left/ShopList").gameObject);
            m_shopList.InitComponent();

            m_detial = transform.Find("Right").gameObject;
            m_shopInfo = Utility.RequireComponent<ShopInfo>(m_detial);

            m_refreshTime = transform.Find("RefreshInfo/Text/Day").GetComponent<Text>();

            m_buyBtn = transform.Find("Bottom/BuyBtn").gameObject;
            Utility.AddButtonListener(transform.Find("Bottom/BuyBtn/Btn"),ClickBuyGoods);
        }

        private void UpdateGoods()
        {
            m_detial.SetActive(false);
            InitInfo(m_shopType);
        }

        public void InitInfo(ShopType shopType)
        {
            if(!m_hasInit)
            {
                InitComponent();
                m_hasInit = true;
            }

            m_shopType = shopType;

            List<ShopItemInfo> list = MerchantSystem.Instance.GetShopGoods(shopType);
            m_shopList.InitInfo(list,ClickShopItem);

            ControllerCenter.Instance.ShopController.RefreshShop += UpdateGoods;
            ControllerCenter.Instance.ShopController.RefreshTime += UpdateRefreshTime;

            UpdateRefreshTime(TimeUtil.GetNextWeekNeedDays());

            UpdateShow(false);
        }

        private void ClickShopItem(ShopItemInfo info)
        {
            m_currentSlect = info;
            m_buyBtn.SetActive(info.RemainCount > 0);
            m_shopInfo.InitInfo(info);
            m_detial.SetActive(true);
        }

        private void ClickBuyGoods()
        {
            BuyGoods(m_shopInfo.GetCurrentNum());
        }

        private void BuyGoods(int num)
        {
            bool buySuc = false;

            if(m_shopType == ShopType.NormalShop)
            {
                if(ControllerCenter.Instance.ShopController.CanBuyNormalGoods(m_currentSlect,num))
                {
                    ControllerCenter.Instance.ShopController.BuyNormalGoods(m_currentSlect,num);
                    buySuc = true;
                }
            }
            else
            {
                if(ControllerCenter.Instance.ShopController.CanBuyBlackGoods(m_currentSlect,num))
                {
                    ControllerCenter.Instance.ShopController.BuyBlackGoods(m_currentSlect,num);
                    buySuc = true;
                }
            }

            if(buySuc)
            {
                UpdateInfoWhenBuy(m_currentSlect.OrderNum);
                m_shopInfo.InitInfo(m_currentSlect);

                m_buyBtn.SetActive(m_currentSlect.RemainCount > 0);
                m_currentSlect = null;
            }
        }

        private void UpdateInfoWhenBuy(string orderNum)
        {
            ShopItemInfo info = MerchantSystem.Instance.GetShopItemInfo(orderNum);
            m_shopList.UpdateRemainInfo(orderNum,info.RemainCount);
        }

        private void UpdateRefreshTime(int refreshDay)
        {
            m_refreshTime.text = refreshDay.ToString();
        }

        private void UpdateShow(bool show)
        {
            m_buyBtn.SetActive(show);
            m_detial.SetActive(show);
        }
    }
}