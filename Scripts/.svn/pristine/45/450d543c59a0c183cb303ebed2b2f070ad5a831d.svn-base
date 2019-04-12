using System.Collections.Generic;
using UnityEngine;

public class EnchantSucTipPanel: UIPanelBehaviour
{
    private EquipDetailInfo m_info;
    protected override void OnShow(List<object> parmers = null)
    {
        GameObject prefab = ResourceLoadUtil.LoadPrefab(StringDefine.PrefabNameDefine.EquipDetialInfo2);
        Utility.SetParent(prefab,transform.Find("Parent"));
        m_info = Utility.RequireComponent<EquipDetailInfo>(prefab);
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
    }

}

