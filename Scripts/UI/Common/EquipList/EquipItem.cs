using System;
using UnityEngine;
using UnityEngine.UI;

namespace Comomon.EquipList
{
    public class EquipItem: BaseItem
    {
        private GameObject m_research;
        private GameObject m_fomo;
        private GameObject m_suoding;
        private GameObject m_charInfo;

        private Image m_charHeadIcon;

        protected override void InitSelfComponent()
        {
            m_research = transform.Find("Research").gameObject;
            m_fomo = transform.Find("Enchant").gameObject;
            m_suoding = transform.Find("Lock").gameObject;

            m_charInfo = transform.Find("Char").gameObject;
            m_charHeadIcon = transform.Find("Char/HeadIcon").GetComponent<Image>();
        }

        public void InitInfo(ItemAttribute attr, Action<ItemAttribute,GameObject> clickCallBack)
        {
            m_attr = attr;

            m_clickCallBackObj = clickCallBack;

            InitComponent();

            InitShow(attr);

            UpdateCharInfo();
        }

        public void InitInfo(ItemAttribute attr,Action<ItemAttribute> clickCallBack)
        {
            m_attr = attr;
            m_clickCallBack = clickCallBack;

            InitComponent();

            InitShow(attr);

            UpdateCharInfo();
        }

        private void InitShow(ItemAttribute attr)
        {
            m_icon.sprite = ResourceLoadUtil.LoadItemIcon(attr); //LoadItemQuiltySprite
           // m_quility.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemQuility,
             //   attr.GetItemData().itemQuality.ToString());

            EquipAttribute equi = (EquipAttribute)attr;
            UpdateStatusShow(equi.EquipState);

            m_num.gameObject.SetActive(equi.AddLevel > 1);
            if(equi.AddLevel > 1)
                m_num.text = "+" + equi.AddLevel;
        }

        public void UpdateStatusShow(EquipState status)
        {
            m_fomo.SetActive(status == EquipState.Enchanting);
            m_research.SetActive(status == EquipState.Researching);
            m_suoding.SetActive(status == EquipState.Lock);
        }

        public void UpdateCharInfo()
        {
            EquipAttribute attr = (EquipAttribute)m_attr;
            m_charInfo.SetActive(attr.charID != 0);
            if(attr.charID != 0)
            {
                CharAttribute charAttr = CharSystem.Instance.GetAttribute(attr.charID);
                m_charHeadIcon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.CharHeadIcon,charAttr.char_template.HeadIcon);
            }
        }

        public void ResetCharInfo()
        {
            m_charInfo.SetActive(false);
        }
    }
}
