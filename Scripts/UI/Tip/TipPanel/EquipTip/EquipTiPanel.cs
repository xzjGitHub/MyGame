using System;
using UnityEngine;

public class EquipTiPanel: UIPanelBehaviour
{
    private GameObject m_equipBtn;
    private GameObject m_unloadBtn;

    private EquipDetailInfo m_equipDetailInfo;

    private Action<EquipAttribute> m_equipAction;
    private Action<EquipAttribute> m_unloadAction;

    private EquipAttribute m_attr;

    private bool m_hasInit = false;

    protected override void OnHide()
    {
        GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.EquipTipAttrItem);
    }

    private void InitComponent()
    {
        if (!m_hasInit)
        {
            m_equipBtn = transform.Find("Parent/Equip").gameObject;
            m_unloadBtn = transform.Find("Parent/Unload").gameObject;

            GameObject detailPrefab = ResourceLoadUtil.LoadPrefab(StringDefine.PrefabNameDefine.EquipDetialInfo);
            Utility.SetParent(detailPrefab,transform.Find("Parent/InfoParent"));
            m_equipDetailInfo = Utility.RequireComponent<EquipDetailInfo>(detailPrefab);

            Utility.AddButtonListener(transform.Find("Parent/Equip/Btn"),ClickEquip);
            Utility.AddButtonListener(transform.Find("Parent/Unload/Btn"),ClickUnload);
            Utility.AddButtonListener(transform.Find("Mask"),ClickClose);

            m_hasInit = true;
        }
    }

    public void UpdateInfo(EquipAttribute attr,Action<EquipAttribute> equip,Action<EquipAttribute> unload)
    {
        m_attr = attr;
        m_equipAction = equip;
        m_unloadAction = unload;

        InitComponent();
        m_equipDetailInfo.Free();
        m_equipDetailInfo.InitInfo(attr);

        m_equipBtn.SetActive(equip!=null);
        m_unloadBtn.SetActive(unload!=null);
    }

    private void ClickEquip()
    {
        if(m_equipAction != null)
        {
            m_equipAction(m_attr);
            m_equipAction = null;
        }
        ClickClose();
    }

    private void ClickUnload()
    {
        if(m_unloadAction != null)
        {
            m_unloadAction(m_attr);
            m_unloadAction = null;
        }
        ClickClose();
    }

    private void ClickClose()
    {
        TipEventCenter.Instance.EmitCloseTipEvent();
        UIPanelManager.Instance.Hide<EquipTiPanel>(false);
    }
}
