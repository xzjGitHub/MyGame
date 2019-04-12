using UnityEngine;
using UnityEngine.UI;

namespace WorkShop.EquipMake.View
{
    public class FcItem: MonoBehaviour
    {
        private GameObject m_item;
        private GameObject m_empty;
        private GameObject m_suo;

        private Image m_icon;
      //  private Image m_quility;
        public int ItemId { get; private set; }

        public void InitCompoment(bool jiesuo)
        {
            m_item = transform.Find("Item").gameObject;
            m_empty = transform.Find("Empty").gameObject;
            m_suo = transform.Find("Lock").gameObject;

            m_icon = transform.Find("Item/Icon").GetComponent<Image>();
          //  m_quility = transform.Find("Item/Quility").GetComponent<Image>();

            m_item.SetActive(jiesuo);
            m_empty.SetActive(jiesuo);
            m_suo.SetActive(!jiesuo);
        }

        public void UpdateSuoInfo(bool jiesuo)
        {
            m_item.SetActive(jiesuo);
            m_empty.SetActive(jiesuo);
            m_suo.SetActive(!jiesuo);
        }

        public void UpdateInfo(ItemData itemData)
        {
            m_item.SetActive(itemData != null);
            m_empty.SetActive(itemData == null);
            if (itemData != null)
            {
                ItemId = itemData.itemID;

                Item_instance item = Item_instanceConfig.GetItemInstance(itemData.instanceID);
                m_icon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemIcon,
                    item.itemIcon.Count > 0 ? item.itemIcon[0] : "");
                //m_quility.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemQuility, itemData.itemQuality.ToString());
            }
            else
            {
                ItemId = -1;
            }
        }

        public bool CanUse()
        {
            return !m_suo.activeSelf && m_empty.activeSelf;
        }

        public bool JieSuo()
        {
            return !m_suo.activeSelf;
        }

    }
}
