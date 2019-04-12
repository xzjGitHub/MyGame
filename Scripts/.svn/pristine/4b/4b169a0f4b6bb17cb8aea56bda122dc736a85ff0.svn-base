using System.Collections.Generic;
using UnityEngine;

namespace UITip
{
    public class SimpleEquipTip: UIPanelBehaviour
    {
        private EquipDetailInfo m_detialInfo;

        private void InitComponent()
        {
            GameObject prefab = ResourceLoadUtil.LoadPrefab(StringDefine.PrefabNameDefine.EquipDetialInfo);
            Utility.SetParent(prefab,transform);
            m_detialInfo = Utility.RequireComponent<EquipDetailInfo>(prefab);

            Utility.AddButtonListener(transform.Find("Image"), () => UIPanelManager.Instance.Hide<SimpleEquipTip>());
        }

        protected override void OnHide()
        {
            m_detialInfo.Free();
        }

        protected override void OnShow(List<object> parmers = null)
        {
            EquipAttribute attr = (EquipAttribute)parmers[0];
            if (m_detialInfo == null)
            {
                InitComponent();
            }
            m_detialInfo.Free();
            m_detialInfo.InitInfo(attr);
        }

    }
}
