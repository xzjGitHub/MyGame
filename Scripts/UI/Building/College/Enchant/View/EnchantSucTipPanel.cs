﻿using System.Collections.Generic;
using UnityEngine;

public class EnchantSucTipPanel: UIPanelBehaviour
{
    private GameObject m_equipDetialObj;
    private EquipDetailInfo m_info;
    protected override void OnShow(List<object> parmers = null)
    {
        m_equipDetialObj = PrefabPool.Instance.GetObjSync(StringDefine.PrefabNameDefine.EquipDetialInfo,
                Res.AssetType.Prefab);
        Utility.SetParent(m_equipDetialObj,transform.Find("Parent"));
        m_info = Utility.RequireComponent<EquipDetailInfo>(m_equipDetialObj);
        EquipAttribute attr = (EquipAttribute)parmers[0];
        m_info.InitInfo(attr);

        Utility.AddButtonListener(transform.Find("Mask"),Close);
    }

    private void Close()
    {
        m_info.Free();
        UIPanelManager.Instance.Hide<EnchantSucTipPanel>();
    }

    protected override void OnHide()
    {
        m_info.Free();
        if(m_equipDetialObj != null)
        {
            PrefabPool.Instance.Free(StringDefine.PrefabNameDefine.EquipDetialInfo,m_equipDetialObj);
            m_equipDetialObj = null;
        }
    }

}

