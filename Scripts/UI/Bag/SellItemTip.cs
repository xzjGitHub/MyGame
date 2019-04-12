using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bag
{
    public class SellItemTip: MonoBehaviour
    {
        private GameObject m_sliderMask;

        private Image m_icon;
        // private Image m_quility;
        private Text m_des;
        private Text m_getGold;
        private Text m_num;
        private Slider m_slider;

        private int m_currentNum;
        private int m_maxNum;
        private int m_singPrice;

        private Item_instance m_itemInstance;
        private ItemAttribute m_attr;

        private Action<ItemAttribute,int> m_sellAction;
        private Action m_cancel;

        public void InitComponent()
        {
            m_sliderMask = transform.Find("SelectNum/SliderMask").gameObject;

            m_icon = transform.Find("Info/Item/Icon").GetComponent<Image>();
            //   m_quility = transform.Find("Info/Item/Quility").GetComponent<Image>();
            m_des = transform.Find("Info/Des").GetComponent<Text>();
            m_getGold = transform.Find("SelectNum/Price/Num").GetComponent<Text>();
            m_num = transform.Find("SelectNum/Num/Text").GetComponent<Text>();

            m_slider = transform.Find("SelectNum/Slider").GetComponent<Slider>();
            m_slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });

            Utility.AddButtonListener(transform.Find("SellBtn/Bg"),ClickSell);
            Utility.AddButtonListener(transform.Find("CancelBtn/Bg"),ClickCancel);
        }


        public void UpdateInfo(ItemAttribute attr,int price,
            Action<ItemAttribute,int> sellAction,Action cancel)
        {
            m_attr = attr;
            m_singPrice = price;
            m_sellAction = sellAction;
            m_cancel = cancel;

            m_currentNum = 1;
            m_maxNum = attr.GetItemData().sum;

            m_itemInstance = Item_instanceConfig.GetItemInstance(attr.instanceID);
            m_icon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemIcon,
                m_itemInstance.itemIcon.Count > 0 ? m_itemInstance.itemIcon[0] : "");
            //   m_quility.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemQuility,attr.GetItemData().itemQuality.ToString());
            m_des.text = m_itemInstance.itemName + "+" + attr.GetItemData().resultItemLevel;
            m_sliderMask.SetActive(attr.GetItemData().sum <= 1);

            if(m_maxNum == 1)
            {
                m_slider.value = 1f;
            }
            else
            {
                m_slider.value = 1f / m_maxNum;
            }
        }

        private void OnSliderValueChanged()
        {
            m_currentNum = (int)(m_maxNum * m_slider.value);
            UpdateShow();
        }


        private void UpdateShow()
        {
            m_num.text = m_currentNum.ToString();
            m_getGold.text = (m_currentNum * m_singPrice).ToString();
        }

        private void ClickSell()
        {
            if(m_currentNum == 0)
            {
                TipManager.Instance.ShowTip("数量不能为0");
                return;
            }
            if(m_sellAction != null)
            {
                gameObject.SetActive(false);
                m_sellAction(m_attr,m_currentNum);
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
