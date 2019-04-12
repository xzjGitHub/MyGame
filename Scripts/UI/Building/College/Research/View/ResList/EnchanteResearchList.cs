using System;
using System.Collections.Generic;
using College.Research.Data;
using UnityEngine;

namespace College.Research.View
{
    public class EnchanteResearchList: MonoBehaviour
    {
        private Transform m_parent;
        private GameObject m_prefab;

        private Dictionary<string,EnchanteResearchItem> m_dict = new Dictionary<string,EnchanteResearchItem>();

        public void InitComponent()
        {
            m_parent = transform.Find("Grid");
            m_prefab = transform.Find("Grid/Item").gameObject;
            m_prefab.SetActive(false);
        }

        public void InitList(List<ResearchingInfo> list,Action<string> clickAction)
        {
            for(int i = 0; i < list.Count; i++)
            {
                AddResearch(list[i],clickAction);
            }
        }

        public void AddResearch(ResearchingInfo researching,Action<string> clickAction)
        {
            GameObject temp = GameObjectPool.Instance.GetObject(StringDefine.ObjectPooItemKey.RareResearchItem,
                m_prefab,researching.Id);
            temp.name = researching.Id;
            Utility.SetParent(temp,m_parent);

            EnchanteResearchItem researchItem = Utility.RequireComponent<EnchanteResearchItem>(temp);
            researchItem.InitInfo(researching,clickAction);

            m_dict[researching.Id] = researchItem;
        }

        public void RemoveResearch(string id)
        {
            if (m_dict.ContainsKey(id))
            {
                GameObjectPool.Instance.FreeGameObjectByName(StringDefine.ObjectPooItemKey.RareResearchItem,id);
                m_dict.Remove(id);
            }
        }


        public void UpdateSelectShow(string id,bool show)
        {
            if(m_dict.ContainsKey(id))
                m_dict[id].UpdateSelectShow(show);
        }

        public void UpdateSlider(string workId,float allTime,int haveUseTime,int exp)
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