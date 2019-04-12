using System;
using UnityEngine;
using UnityEngine.UI;

namespace Char.View
{
    public class EquipPartInfo: MonoBehaviour
    {
        private GameObject m_empty;
        private GameObject m_equip;
        private GameObject m_eff;

        private Image m_icon;

        private Action<EquipPart> m_click;
        private EquipPart m_part;

        public bool CanClickPart;

        public void InitComponent(Action<EquipPart> click,EquipPart part)
        {
            m_click = click;
            m_part = part;
            m_eff = transform.Find("Eff").gameObject;
            m_empty = transform.Find("Empty").gameObject;
            m_equip = transform.Find("Equip").gameObject;
            m_icon = transform.Find("Equip/Icon").GetComponent<Image>();

            Utility.AddButtonListener(transform.Find("Btn"),ClickBtn);
        }


        public void UpdateInfo(EquipmentData data)
        {
            m_empty.SetActive(data == null);
            m_equip.SetActive(data != null);
            m_eff.SetActive(data!=null);
            if(data != null)
            {
                m_icon.sprite = ResourceLoadUtil.LoadItemIcon(data);
                //m_quility.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemQuility,data.itemQuality.ToString());
            }
        }

        private void ClickBtn()
        {
            if(m_click != null && CanClickPart)
            {
                m_click(m_part);
            }
        }
    }
}
