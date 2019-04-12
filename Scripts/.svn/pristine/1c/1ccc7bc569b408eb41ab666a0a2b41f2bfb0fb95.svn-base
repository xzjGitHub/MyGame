using System;
using UnityEngine;
using UnityEngine.UI;

namespace WorkShop.EquipMake.View
{
    public class ZcInfo:MonoBehaviour
    {
        private GameObject m_info;
        private GameObject m_select;
        private GameObject m_empty;

        private Image m_icon;
        private Text m_itemName;
        private Text m_itemLevel;
        private Text m_num;

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
            m_itemLevel = transform.Find("Info/ItemLevel/Level").GetComponent<Text>();
            m_num= transform.Find("Info/Item/Num").GetComponent<Text>();

            Utility.AddButtonListener(transform.Find("Btn/Btn"),Click);
        }

        public void UpdateInfo(ItemAttribute attr,int forgeId)
        {
            if(attr != null)
            {
                Item_instance item = Item_instanceConfig.GetItemInstance(attr.instanceID);
                m_icon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemIcon,item.itemIcon.Count > 0 ? item.itemIcon[0] : "");
              //  m_quility.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemQuility,attr.GetItemData().itemQuality.ToString());
                m_itemName.text = item.itemName;
                m_itemLevel.text= attr.GetItemData().resultItemLevel.ToString();

                Forge_template forge = ControllerCenter.Instance.EquipMakeController.GetForge_Template(forgeId);
                ItemUtil.SetTextInfo(forge.materialCost,attr.sum,m_num);
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
