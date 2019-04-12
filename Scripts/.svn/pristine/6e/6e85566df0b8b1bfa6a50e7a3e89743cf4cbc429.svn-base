using System;
using System.Collections.Generic;
using UnityEngine;

namespace WorkShop.EquipResearch.View
{
    public class EquipResearchList : MonoBehaviour
    {
        private Transform m_parent;
        private GameObject m_prefab;

        private Dictionary<string, EquipResearchItem> m_dict = new Dictionary<string, EquipResearchItem>();

        public void InitComponent()
        {
            m_parent = transform.Find("Grid");
            m_prefab = transform.Find("Grid/Item").gameObject;
            m_prefab.SetActive(false);
        }

        public void AddResearch(EquipResearchInfo info,Action<string> clickAction)
        {
            GameObject temp = GameObjectPool.Instance.GetObject(StringDefine.ObjectPooItemKey.EquipResearchItem,
                m_prefab, info.WorkId);
            Utility.SetParent(temp, m_parent);

            EquipResearchItem researchItem = Utility.RequireComponent<EquipResearchItem>(temp);
            researchItem.InitInfo(info,clickAction);

            m_dict[info.WorkId] = researchItem;
        }

        public void RemoveResearch(string id)
        {
            GameObjectPool.Instance.FreeGameObjectByName(StringDefine.ObjectPooItemKey.EquipResearchItem, id);
            m_dict.Remove(id);
        }

        public void UpdateSelectShow(string id, bool show)
        {
            if (m_dict.ContainsKey(id))
                m_dict[id].UpdateSelectShow(show);
        }

        public void UpdateSlider(string workId,float allTime,int haveUseTime,float exp)
        {
            if(m_dict.ContainsKey(workId))
                m_dict[workId].UpdateSlider(allTime,haveUseTime,exp);
        }

        public void UpdateWhenResEnd(string id)
        {
            if(m_dict.ContainsKey(id))
                m_dict[id].UpdateEndShow(true);
        }
    }

}