﻿using UnityEngine;

namespace Shop.View
{
    public class ShopUIEquipInfo: MonoBehaviour
    {
        private GameObject m_equipDetialObj;
        private EquipDetailInfo m_equipDetialInfo;

        public void InitComponent()
        {
            m_equipDetialObj = PrefabPool.Instance.GetObjSync(StringDefine.PrefabNameDefine.EquipDetialInfo,
                 Res.AssetType.Prefab);
            Utility.SetParent(m_equipDetialObj,transform);
            m_equipDetialInfo = Utility.RequireComponent<EquipDetailInfo>(m_equipDetialObj);
        }

        public void UpdateInfo(EquipAttribute attr)
        {
            m_equipDetialInfo.Free();
            m_equipDetialInfo.InitInfo(attr);
        }

        public void Free()
        {
            if(m_equipDetialInfo != null)
            {
                m_equipDetialInfo.Free();
            }
            if(m_equipDetialObj != null)
            {
                PrefabPool.Instance.Free(StringDefine.PrefabNameDefine.EquipDetialInfo,m_equipDetialObj);
                m_equipDetialObj = null;
            }
        }
    }
}
