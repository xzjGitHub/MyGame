using System.Collections.Generic;
using UnityEngine;

namespace UITip
{
    public class SimpleItemTip: UIPanelBehaviour
    {

        private ItemTipInfo m_detialInfo;

        private void InitComponent()
        {
            GameObject prefab = ResourceLoadUtil.LoadPrefab(StringDefine.PrefabNameDefine.ItemDetialInfo);
            Utility.SetParent(prefab,transform);
            m_detialInfo = Utility.RequireComponent<ItemTipInfo>(prefab);

            Utility.AddButtonListener(transform.Find("Image"),() => UIPanelManager.Instance.Hide<SimpleItemTip>());
        }

        protected override void OnShow(List<object> parmers = null)
        {
            if(m_detialInfo == null)
            {
                InitComponent();
            }

            ItemData data = (ItemData)parmers[0];
            if(data != null)
                m_detialInfo.UpdateInfo(data);
            else
                m_detialInfo.UpdateInfo(1);
        }
    }
}
