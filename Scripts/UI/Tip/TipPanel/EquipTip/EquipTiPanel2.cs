﻿using System;
using UnityEngine;

public class EquipTiPanel2: UIPanelBehaviour
{
    private Transform m_leftParent;
    private Transform m_rightParent;

    private EquipDetailInfo m_leftInfo;
    private EquipDetailInfo m_rightInfo;

    private Action<EquipAttribute> m_equipAction;
    private Action<EquipAttribute> m_unloadAction;

    private EquipAttribute m_leftAttr;
    private EquipAttribute m_rightAttr;

    private GameObject m_leftEquipDetialObj;
    private GameObject m_rightEquipDetialObj;

    private bool m_hasInit;

    protected override void OnReactive()
    {
        base.OnReactive();
    }

    protected override void OnHide()
    {
        if(m_leftEquipDetialObj != null)
        {
            PrefabPool.Instance.Free(StringDefine.PrefabNameDefine.EquipDetialInfo,m_leftEquipDetialObj);
            m_leftEquipDetialObj = null;
        }
        if(m_rightEquipDetialObj != null)
        {
            PrefabPool.Instance.Free(StringDefine.PrefabNameDefine.EquipDetialInfo,m_rightEquipDetialObj);
            m_rightEquipDetialObj = null;
        }
        PrefabPool.Instance.Free(StringDefine.ObjectPooItemKey.EquipTipAttrItem);
    }

    private void InitComponent()
    {
        if(!m_hasInit)
        {
            m_leftParent = transform.Find("Left/Parent");
            m_rightParent = transform.Find("Right/Parent");

            Utility.AddButtonListener(transform.Find("Mask"),Close);
            Utility.AddButtonListener(transform.Find("Right/Equip/Btn"),ClickEquip);
            Utility.AddButtonListener(transform.Find("Left/Unload/Btn"),ClickUnload);

            m_hasInit = true;
        }
    }

    public void UpdateInfo(EquipAttribute leftAttr,EquipAttribute rightAttr,
        Action<EquipAttribute> equip,Action<EquipAttribute> unload)
    {
        m_leftAttr = leftAttr;
        m_rightAttr = rightAttr;
        m_equipAction = equip;
        m_unloadAction = unload;

        InitComponent();

        if(m_leftEquipDetialObj == null)
        {
            m_leftEquipDetialObj = PrefabPool.Instance.GetObjSync(StringDefine.PrefabNameDefine.EquipDetialInfo,
                Res.AssetType.Prefab);
            Utility.SetParent(m_leftEquipDetialObj,m_leftParent);
            m_leftInfo = Utility.RequireComponent<EquipDetailInfo>(m_leftEquipDetialObj);
        }

        if(m_rightEquipDetialObj == null)
        {
            m_rightEquipDetialObj = PrefabPool.Instance.GetObjSync(StringDefine.PrefabNameDefine.EquipDetialInfo,
              Res.AssetType.Prefab);
            Utility.SetParent(m_rightEquipDetialObj,m_rightParent);
            m_rightInfo = Utility.RequireComponent<EquipDetailInfo>(m_rightEquipDetialObj);
        }

        m_leftInfo.Free();
        m_rightInfo.Free();

        m_leftInfo.InitInfo(leftAttr);
        m_rightInfo.InitInfo(rightAttr);
    }

    private void ClickEquip()
    {
        if(m_equipAction != null)
        {
            m_equipAction(m_rightAttr);
            m_equipAction = null;
        }
        Close();
    }

    private void ClickUnload()
    {
        if(m_unloadAction != null)
        {
            m_unloadAction(m_leftAttr);
            m_unloadAction = null;
        }
        Close();
    }

    private void Close()
    {
        TipEventCenter.Instance.EmitCloseTipEvent();
        UIPanelManager.Instance.Hide<EquipTiPanel2>(false);
    }
}
