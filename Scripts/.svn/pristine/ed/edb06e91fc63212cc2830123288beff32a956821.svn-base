
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Core.View
{
    public class PowerList : MonoBehaviour
    {
        private Transform m_parent;
        private GameObject m_prefab;

        private Dictionary<int, GameObject> m_dict = new Dictionary<int, GameObject>();

        public void InitComponent()
        {
            m_parent = transform.Find("Grid/");
            m_prefab = transform.Find("Grid/Item").gameObject;
            m_prefab.SetActive(false);

        }


        public void AddItem(ItemAttribute itemAttribute)
        {
            GameObject temp = GameObjectPool.Instance.GetObject(StringDefine.ObjectPooItemKey.CorePowerItem,
                m_prefab, itemAttribute.itemID.ToString());
            Utility.SetParent(temp, m_parent);

            GameObject item = temp.transform.Find("Item").gameObject;
            ItemUtil.SetItemInfo(itemAttribute, item);

            Shard_template shard_Template = Shard_templateConfig.GetShard_Template(itemAttribute.instanceID);
            Text addPower = temp.transform.Find("AddPower").GetComponent<Text>();
            addPower.text = "核心功率提高+" + shard_Template.tempCorePower;

            m_dict[itemAttribute.itemID] = temp;
        }

        public void RemoveItem(int itemId)
        {
            GameObjectPool.Instance.FreeGameObjectByName(StringDefine.ObjectPooItemKey.CorePowerItem, itemId.ToString());
            m_dict.Remove(itemId);
        }

        public void Clear()
        {
            GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.CorePowerItem);
            m_dict.Clear();
        }
    }
}
