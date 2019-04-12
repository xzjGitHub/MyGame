using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Shop.View
{
    public class ShopUIEquipInfo: MonoBehaviour
    {

        private EquipDetailInfo m_equipDetialInfo;

        public void InitComponent()
        {
            GameObject prefab = ResourceLoadUtil.LoadPrefab(StringDefine.PrefabNameDefine.EquipDetialInfo);
            Utility.SetParent(prefab,transform);
            m_equipDetialInfo = Utility.RequireComponent<EquipDetailInfo>(prefab);
        }

        public void UpdateInfo(EquipAttribute attr)
        {
            m_equipDetialInfo.Free();
            m_equipDetialInfo.InitInfo(attr);
        }

        public void Free()
        {
            if (m_equipDetialInfo != null)
            {
                m_equipDetialInfo.Free();
            }
        }
    }
}
