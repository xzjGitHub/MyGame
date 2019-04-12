using UnityEngine;
using UnityEngine.UI;

namespace WorkShop.Recast.View
{
    public class RecastCostInfo: MonoBehaviour
    {
        private GameObject m_prefab;
        private Transform m_parent;
        private Transform m_detialParent;

        private Text m_gold;
        private Text m_mana;

        private EquipDetailInfo m_detialInfo;

        public void InitComponent()
        {
            m_prefab = transform.Find("Left/Grid/Prefab").gameObject;
            m_prefab.SetActive(false);
            m_parent = transform.Find("Left/Grid");
            m_detialParent = transform.Find("Right/Parent");
            m_gold = transform.Find("Cost/GoldCost/Num").GetComponent<Text>();
            m_mana = transform.Find("Cost/ManaCost/Num").GetComponent<Text>();

            Utility.AddButtonListener(transform.Find("Back/Btn"),()=>gameObject.SetActive(false));
        }

        public void UpdateInfo(int id,EquipmentData data)
        {
            Free();
            Craft_template craft = Craft_templateConfig.GetCraft_template(id);
            for (int i = 0; i < craft.itemCost.Count; i++)
            {
                GameObject obj = GameObjectPool.Instance.GetObject(
                    StringDefine.ObjectPooItemKey.EquipRecastCost,m_prefab);
                Utility.SetParent(obj, m_parent);

                Item_instance item = Item_instanceConfig.GetItemInstance(data.instanceID);
                obj.transform.Find("Item/Icon").GetComponent<Image>().sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemIcon,
                    item.itemIcon.Count > 0 ? item.itemIcon[0] : "");
               // obj.transform.Find("Item/Quility").GetComponent<Image>().sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemQuility,
               // data.itemQuality.ToString());

                int haveNum = ItemSystem.Instance.GetItemNumByTemplateID(craft.itemCost[i][0]);
                Text m_num = obj.transform.Find("Item/Num").GetComponent<Text>();
                ItemUtil.SetTextInfo(craft.itemCost[i][1],haveNum,m_num);
            }

            if (m_detialInfo == null)
            {
                GameObject prefab = ResourceLoadUtil.LoadPrefab(StringDefine.PrefabNameDefine.EquipDetialInfo);
                Utility.SetParent(prefab,m_detialParent);
                m_detialInfo = Utility.RequireComponent<EquipDetailInfo>(prefab);
            }
            m_detialInfo.Free();
            m_detialInfo.InitInfo(new EquipAttribute(data));

            m_mana.text = craft.manaCost.ToString();
            m_gold.text = craft.goldCost.ToString();
        }

        public void Free()
        {
            GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.EquipRecastCost);
        }
    }

}
