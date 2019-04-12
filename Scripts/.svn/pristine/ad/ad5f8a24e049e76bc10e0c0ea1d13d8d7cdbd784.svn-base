using System;
using UnityEngine;
using UnityEngine.UI;

namespace College.Enchant.View
{
    public class EnchantInfo: MonoBehaviour
    {
        private GameObject m_info;
        private GameObject m_select;
        private GameObject m_empty;

        private Image m_icon;
        //private Image m_quility;
        private Text m_itemName;
        private Text m_num;

        private Action<GameObject> m_clickAction;

        public void InitComponent(Action<GameObject> click)
        {
            m_clickAction = click;
            m_info = transform.Find("Info").gameObject;
            m_select = transform.Find("Select").gameObject;
            m_select.SetActive(false);
            m_empty = transform.Find("Empty").gameObject;

            m_icon = transform.Find("Info/Equip/Icon").GetComponent<Image>();
           // m_quility = transform.Find("Info/Equip/Quility").GetComponent<Image>();
            m_itemName = transform.Find("Info/Name").GetComponent<Text>();
            m_num = transform.Find("Info/Equip/Num").GetComponent<Text>();

            Utility.AddButtonListener(transform.Find("Btn/Btn"),Click);
        }

        public void UpdateInfo(EquipAttribute attr)
        {
            if(attr != null)
            {
                Item_instance item = Item_instanceConfig.GetItemInstance(attr.instanceID);
                m_icon.sprite = ResourceLoadUtil.LoadItemIcon(attr);
              //  m_quility.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemQuility,attr.GetItemData().itemQuality.ToString());
                m_itemName.text = item.itemName;
                m_num.gameObject.SetActive(attr.AddLevel > 0);
                if(attr.AddLevel > 0)
                    m_num.text = "+" + attr.AddLevel;
            }
            m_info.SetActive(attr != null);
            m_empty.SetActive(attr == null);
        }

        private void Click()
        {
            if(m_clickAction != null)
            {
                m_clickAction(m_select);
            }
        }
    }
}
