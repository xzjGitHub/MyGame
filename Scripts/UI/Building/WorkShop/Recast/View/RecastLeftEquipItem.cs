using System;
using UnityEngine;
using UnityEngine.UI;

namespace WorkShop.Recast.View
{
    public class RecastLeftEquipItem: MonoBehaviour
    {

        private GameObject m_select;

        private Image m_icon;
        // private Image m_quility;
        private Text m_name;

        private bool m_hasInit;

        private EquipmentData m_data;
        private Action<EquipmentData,int> m_click;
        private int m_craftId;

        private void InitComponent()
        {
            m_select = transform.Find("Select").gameObject;
            m_icon = transform.Find("Item/Icon").GetComponent<Image>();
            // m_quility = transform.Find("Item/Quility").GetComponent<Image>();
            m_name = transform.Find("Name").GetComponent<Text>();

            Utility.AddButtonListener(transform.Find("Btn"),Click);
        }

        public void InitInfo(int craftId,EquipmentData data,Action<EquipmentData,int> click)
        {
            if(!m_hasInit)
            {
                InitComponent();
                m_hasInit = true;
            }
            m_craftId = craftId;
            m_data = data;
            m_click = click;

            m_select.SetActive(false);
            Item_instance item = Item_instanceConfig.GetItemInstance(data.instanceID);
            m_icon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemIcon,
                item.itemIcon.Count > 0 ? item.itemIcon[0] : "");
            //m_quility.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemQuility,data.itemQuality.ToString());
            m_name.text = item.itemName + "+" + data.addLevel;
        }

        private void Click()
        {
            if(m_click != null)
            {
                m_click(m_data,m_craftId);
            }
        }

        public void UpdateSelectShow(bool show)
        {
            m_select.SetActive(show);
        }
    }
}
