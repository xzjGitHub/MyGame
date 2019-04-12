using System;
using UnityEngine;

public class EquipTiPanel2: UIPanelBehaviour
{
    private EquipDetailInfo m_leftInfo;
    private EquipDetailInfo m_rightInfo;

    private Action<EquipAttribute> m_equipAction;
    private Action<EquipAttribute> m_unloadAction;

    private EquipAttribute m_leftAttr;
    private EquipAttribute m_rightAttr;

    private bool m_hasInit;

    protected override void OnHide()
    {
      //  m_leftInfo.Free();
      //  m_rightInfo.Free();
        GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.EquipTipAttrItem);
    }

    private void InitComponent()
    {
        if (!m_hasInit)
        {
            GameObject left = ResourceLoadUtil.LoadPrefab(StringDefine.PrefabNameDefine.EquipDetialInfo2);
            Utility.SetParent(left,transform.Find("Left/Parent"));
            m_leftInfo = Utility.RequireComponent<EquipDetailInfo>(left);

            GameObject right = ResourceLoadUtil.LoadPrefab(StringDefine.PrefabNameDefine.EquipDetialInfo2); ;
            Utility.SetParent(right,transform.Find("Right/Parent"));
            m_rightInfo = Utility.RequireComponent<EquipDetailInfo>(right);

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
        m_leftInfo.Free();
        m_rightInfo.Free();
        m_leftInfo.InitInfo(leftAttr);
        m_rightInfo.InitInfo(rightAttr);
    }

    private void ClickEquip()
    {
        if (m_equipAction != null)
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
        UIPanelManager.Instance.Hide<EquipTiPanel2>();
    }
}
