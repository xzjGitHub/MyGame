using UnityEngine;
using UnityEngine.UI;

namespace College.Enchant.View
{
    public class SelectLeftInfo: MonoBehaviour
    {
        private GameObject m_equip;
        private GameObject m_item;

        private EquipDetailInfo m_equipInfo;
        private MatEnchantDetialInfo m_itemInfo;

        public void InitComponent()
        {
            m_equip = transform.Find("Equip").gameObject;
            m_item = transform.Find("Item").gameObject;

            m_equip.SetActive(false);
            m_item.SetActive(false);

            GameObject prefab = ResourceLoadUtil.LoadPrefab(StringDefine.PrefabNameDefine.EquipDetialInfo2);
            Utility.SetParent(prefab,m_equip.transform);
            m_equipInfo = Utility.RequireComponent<EquipDetailInfo>(prefab);

           // m_equipInfo = Utility.RequireComponent<EquipDetailInfo>(transform.Find("Equip/EquipDetialInfo").gameObject);
            m_itemInfo = Utility.RequireComponent<MatEnchantDetialInfo>(transform.Find("Item/EnchantMatInfo").gameObject);
        }

        public void UpdateInfo(ItemAttribute attr)
        {
            gameObject.SetActive(attr != null);
            if(attr != null)
            {
                EquipAttribute equipAttr = attr as EquipAttribute;
                m_equip.SetActive(equipAttr != null);
                m_item.SetActive(equipAttr == null);

                if(equipAttr != null)
                {
                    m_equipInfo.Free();
                    m_equipInfo.InitInfo(equipAttr);
                }
                else {
                    m_itemInfo.UpdateInfo(attr.instanceID);
                }
            }
        }

        public void Free()
        {
            if(m_equipInfo != null)
                m_equipInfo.Free();
        }
    }
}
