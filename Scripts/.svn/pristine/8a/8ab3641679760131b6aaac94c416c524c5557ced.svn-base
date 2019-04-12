using Shop.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.View
{
    public class ShopUIItemInfo: MonoBehaviour
    {
        private GameObject m_sliderMask;

        private Text m_price;
        private Text m_num;

        private Slider m_slider;

        private int m_currentNum;
        private int m_maxNum;
        private int m_singPrice;

        private ItemTipInfo m_itemInfo;

        public void InitComponent()
        {
            m_sliderMask = transform.Find("SliderMask").gameObject;

            m_price = transform.Find("Price/Num").GetComponent<Text>();
            m_num = transform.Find("BuyNum/Num/Text").GetComponent<Text>();

            m_slider = transform.Find("Slider").GetComponent<Slider>();
            m_slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });

            GameObject obj = ResourceLoadUtil.LoadPrefab(StringDefine.PrefabNameDefine.ItemDetialInfo);
            Utility.SetParent(obj,transform.Find("ItemInfoParent"));
            m_itemInfo = Utility.RequireComponent<ItemTipInfo>(obj);

        }

        public void UpdateInfo(ShopItemInfo info)
        {
            if (info.RemainCount <= 0)
            {
                UpdateInfoWhenRemainCountZero();
            }
            else
            {
                UpdateInfoWhenRemainNotZero(info);
            }
        }

        private void UpdateInfoWhenRemainNotZero(ShopItemInfo info)
        {
            if(info.IsGold)
            {
                m_itemInfo.UpdateInfo(1);
            }
            else
            {
                m_itemInfo.UpdateInfo(info.itemData);
            }

            m_price.text = info.Price.ToString();
            m_num.text = 1.ToString();

            m_maxNum = info.RemainCount;
            m_currentNum = 1;
            m_singPrice = info.Price;

            if(m_maxNum == 1)
            {
                m_slider.value = 1f;
            }
            else
            {
                m_slider.value = 1f / m_maxNum;
            }
            m_sliderMask.SetActive(m_maxNum == 1);
        }

        private void UpdateInfoWhenRemainCountZero()
        {
            m_num.text = 0.ToString();
            m_slider.value = 1f;
            m_sliderMask.SetActive(true);
        }

        private void OnSliderValueChanged()
        {
            m_currentNum = (int)(m_maxNum * m_slider.value);
            m_num.text = m_currentNum.ToString();
            m_price.text = (m_currentNum * m_singPrice).ToString();
        }

        public int GetCurrentNum()
        {
            return m_currentNum;
        }
    }
}
