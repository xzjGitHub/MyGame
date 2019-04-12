using System.Collections.Generic;
using College.Research.Controller;
using UnityEngine;


namespace College.Research.View
{
    public class EnchantViewInfo:MonoBehaviour
    {
        private List<EnchantViewItem> m_list = new List<EnchantViewItem>();

        public void InitComponent()
        {
            Utility.AddButtonListener(transform.Find("Mask"),() => gameObject.SetActive(false));
        }

        public void UpdateInfo()
        {
            if(m_list.Count == 0)
            {
                InitList();
            }
            for(int i = 0; i < m_list.Count; i++)
            {
                m_list[i].UpdateInfo(i + 1);
            }
        }

        private void InitList()
        {
            GameObject prefab = transform.Find("Parent/Grid/Item").gameObject;
            prefab.SetActive(false);
            Transform parent = transform.Find("Parent/Grid");
            for(int i = 0; i < 4; i++)
            {
                GameObject obj = GameObject.Instantiate(prefab);
                Utility.SetParent(obj,parent);
                EnchantViewItem view = Utility.RequireComponent<EnchantViewItem>(obj);
                view.InitComponent(EnchanteResearchController.GetName(i + 1));
                m_list.Add(view);
            }
        }
    }
}
