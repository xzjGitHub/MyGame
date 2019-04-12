using UnityEngine;

namespace College.Research.View
{
    public class EnchantEffList: MonoBehaviour
    {
        //private GameObject m_prefab;
        // private Transform m_parent;

        public void InitComponent()
        {
           // m_prefab = transform.Find("Parent/Scroll/Grid/Item").gameObject;
            //m_prefab.SetActive(false);
            //  m_parent = transform.Find("Parent/Scroll/Grid");
            Utility.AddButtonListener(transform.Find("Mask"),() => gameObject.SetActive(false));
            Utility.AddButtonListener(transform.Find("Back/Btn"),() => gameObject.SetActive(false));
        }

        public void InitInfo(int rareId)
        {
            Free();
            // MR_template rare = MR_templateConfig.GetTemplate(rareId);
            //for(int i = 0; i < rare.enchantEffect.Count; i++)
            //{
            //    for(int j = 0; j < rare.enchantEffect[i].Count; j++)
            //    {
            //        GameObject obj = GameObjectPool.Instance.GetObject(
            //            StringDefine.ObjectPooItemKey.EnchantEffItem,m_prefab);
            //        Utility.SetParent(obj,m_parent);
            //        EnchantEffItem item = Utility.RequireComponent<EnchantEffItem>(obj);
            //        item.UpdateInfo(rareId,rare.enchantEffect[i][j]);
            //    }
            //}
        }

        public void Free()
        {
            GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.EnchantEffItem);
        }
    }
}
