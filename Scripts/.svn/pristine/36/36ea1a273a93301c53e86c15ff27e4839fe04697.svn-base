using System;
using UnityEngine;
using UnityEngine.UI;

namespace College.Enchant.View
{
    public class EnchantMatInfo: MonoBehaviour
    {
        private GameObject m_info;
        private GameObject m_select;
        private GameObject m_empty;

        private Image m_icon;
        // private Image m_quility;
        private Text m_itemName;

        private Action<GameObject> m_clickAction;

        public void InitComponent(Action<GameObject> click)
        {
            m_clickAction = click;
            m_info = transform.Find("Info").gameObject;
            m_select = transform.Find("Select").gameObject;
            m_select.SetActive(false);
            m_empty = transform.Find("Empty").gameObject;

            m_icon = transform.Find("Info/Item/Icon").GetComponent<Image>();
            //  m_quility = transform.Find("Info/Item/Quility").GetComponent<Image>();
            m_itemName = transform.Find("Info/Name").GetComponent<Text>();
            transform.Find("Info/Item/Num").gameObject.SetActive(false);


            Utility.AddButtonListener(transform.Find("Btn/Btn"),Click);
        }

        public void UpdateInfo(ItemAttribute attr)
        {
            if(attr != null)
            {
                Item_instance item = Item_instanceConfig.GetItemInstance(attr.instanceID);
                m_icon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemIcon,item.itemIcon.Count > 0 ? item.itemIcon[0] : "");
                //  m_quility.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemQuility,attr.GetItemData().itemQuality.ToString());
                m_itemName.text = item.itemName;
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
