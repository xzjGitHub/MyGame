﻿using Shop.Data;
using UnityEngine;

namespace Shop.View
{
    public class ShopInfo: MonoBehaviour
    {
        private GameObject m_equip;
        private GameObject m_item;

        private ShopUIEquipInfo m_equipInfo;
        private ShopUIItemInfo m_itemInfo;

        private bool m_hasInit;
        private bool m_isGold;

        public void Free()
        {
            if(m_equipInfo != null)
            {
                m_equipInfo.Free();
            }

            if(m_itemInfo != null)
                m_itemInfo.Free();
        }

        private void InitComponent()
        {
            m_equip = transform.Find("Equip").gameObject;
            m_item = transform.Find("Item").gameObject;

            m_equipInfo = Utility.RequireComponent<ShopUIEquipInfo>(m_equip);
            m_equipInfo.InitComponent();

            m_itemInfo = Utility.RequireComponent<ShopUIItemInfo>(m_item);
            m_itemInfo.InitComponent();
        }


        public void InitInfo(ShopItemInfo info)
        {
            if(!m_hasInit)
            {
                InitComponent();
                m_hasInit = true;
            }

            m_isGold = info.IsGold;
            if(!info.IsGold)
            {
                EquipmentData data = info.itemData as EquipmentData;
                if(data != null)
                {
                    m_equipInfo.UpdateInfo(new EquipAttribute(data));
                }
                else
                {
                    m_itemInfo.UpdateInfo(info);
                }
                m_equip.SetActive(data != null);
                m_item.SetActive(data == null);
            }
            else
            {
                m_itemInfo.UpdateInfo(info);
                m_equip.SetActive(false);
                m_item.SetActive(true);
            }
        }


        public int GetCurrentNum()
        {
            int num = 0;
            if(m_isGold)
            {
                num = 1;
            }
            else
            {
                num = m_equip.activeSelf ? 1 : m_itemInfo.GetCurrentNum();
            }
            return num;
        }

    }
}
