using System;
using Shop.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.View
{
    public class ShopItem: MonoBehaviour
    {
        private Image m_icon;
        private Text m_name;
        private Text m_num;
        private Text m_price;

        private GameObject m_select;

        private bool m_hasInit;
        private Action<ShopItemInfo,GameObject> m_clickAction;

        private ShopItemInfo m_shopItemInfo;

        private void InitComponent()
        {
            if(m_hasInit)
            {
                return;
            }

            m_icon = transform.Find("Item/Icon").GetComponent<Image>();
            m_name = transform.Find("Name").GetComponent<Text>();
            m_num = transform.Find("Num/Num").GetComponent<Text>();
            m_price = transform.Find("Price/Num").GetComponent<Text>();

            m_select = transform.Find("Select").gameObject;
            m_select.SetActive(false);

            Utility.AddButtonListener(transform.Find("Btn"),Click);
            Utility.AddButtonListener(transform.Find("Item/Btn"),ClickItem);

            m_hasInit = true;
        }

        public void InitInfo(ShopItemInfo info,Action<ShopItemInfo,GameObject> clickAction)
        {
            InitComponent();
            m_shopItemInfo = info;
            m_clickAction = clickAction;
            m_select.SetActive(false);

            Item_instance item = null;
            if (info.IsGold)
            {
                item = Item_instanceConfig.GetItemInstance(1);
            }
            else
            {

                item = Item_instanceConfig.GetItemInstance(info.itemData.instanceID);
            }

            m_name.text = item.itemName;
            m_icon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemIcon,item.itemIcon.Count > 0 ? item.itemIcon[0] : "");
            m_icon.SetNativeSize();
            m_price.text = info.Price.ToString();
            UpdateRemainNum(info.RemainCount);
        }

        private void Click()
        {
            if(m_clickAction != null)
            {
                m_clickAction(m_shopItemInfo,m_select);
            }
        }


        public void UpdateRemainNum(int remianNum)
        {
            if (remianNum <= 0)
            {
                m_num.text ="已卖光";
            }
            else
            {
                m_num.text =remianNum.ToString();
            }

            m_select.SetActive(false);
        }

        private void ClickItem()
        {
            TipPanelShowUtil.ShowSimpleTip(m_shopItemInfo.itemData);
        }
    }
}