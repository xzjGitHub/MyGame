﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bag
{
    public class SellEquipTip: MonoBehaviour
    {
        private Image m_icon;
        // private Image m_quility;
        private Text m_des;
        private Text m_getGold;

        private Item_instance m_itemInstance;
        private ItemAttribute m_attr;

        private Action<ItemAttribute,int> m_sellAction;
        private Action m_cancel;

        public void InitComponent()
        {
            m_icon = transform.Find("Center/Info/Item/Icon").GetComponent<Image>();
            //  m_quility = transform.Find("Center/Info/Item/Quility").GetComponent<Image>();
            m_des = transform.Find("Center/Info/Des").GetComponent<Text>();
            m_getGold = transform.Find("Center/GetGold/Num").GetComponent<Text>();

            Utility.AddButtonListener(transform.Find("SellBtn/Bg"),ClickSell);
            Utility.AddButtonListener(transform.Find("CancelBtn/Bg"),ClickCancel);
        }


        public void UpdateInfo(ItemAttribute attr,int price,
          Action<ItemAttribute,int> sellAction,Action cancel)
        {
            m_attr = attr;
            m_sellAction = sellAction;
            m_cancel = cancel;

            m_itemInstance = Item_instanceConfig.GetItemInstance(attr.instanceID);
            m_icon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemIcon,
                m_itemInstance.itemIcon.Count > 0 ? m_itemInstance.itemIcon[0] : "");
            //m_quility.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemQuility,attr.GetItemData().itemQuality.ToString());
            m_des.text = m_itemInstance.itemName;
            m_getGold.text = price.ToString();
        }


        private void ClickSell()
        {
            if(m_sellAction != null)
            {
                gameObject.SetActive(false);
                m_sellAction(m_attr,1);
            }
        }

        private void ClickCancel()
        {
            if(m_cancel != null)
            {
                gameObject.SetActive(false);
                m_cancel();
            }
        }

    }
}
