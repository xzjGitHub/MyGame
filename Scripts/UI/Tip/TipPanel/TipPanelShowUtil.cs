using System.Collections.Generic;
using Char.View;
using UnityEngine;
using UITip;

public class TipPanelShowUtil
{
    public static CharAttrTipPanel ShowCharAttrTipPanel()
    {
        CavasType cavasType = CavasType.Three;
        if(UIPanelManager.Instance.GetUiPanelBehaviour<CharPanel>() != null)
        {
            cavasType = CavasType.SpecialUI;
        }
        return UIPanelManager.Instance.Show<CharAttrTipPanel>(cavasType);
    }

    public static SkillTipPanel ShowSkillTip()
    {
        CavasType cavasType = CavasType.Three;
        if (UIPanelManager.Instance.GetUiPanelBehaviour<CharPanel>() != null)
        {
            cavasType = CavasType.SpecialUI;
        }
        return UIPanelManager.Instance.Show<SkillTipPanel>(cavasType);
    }

    public static void ShowSimpleTip(ItemData itemData)
    {
        EquipmentData data = itemData as EquipmentData;
        if(data != null)
        {
            UIPanelManager.Instance.Show<SimpleEquipTip>(CavasType.PopUI,new List<object> { new EquipAttribute(data) });
        }
        else
        {
            UIPanelManager.Instance.Show<SimpleItemTip>(CavasType.PopUI,new List<object> { itemData });
        }
    }

    public static Color GetColorByQuility(int qui)
    {
        Color color = Color.white;
        if(qui == 1)
        {
            color = new Color(255 / 255f,255 / 255f,255 / 255f);
        }
        if(qui == 2)
        {
            color = new Color(70 / 255f,190 / 255f,90 / 255f);
        }
        if(qui == 3)
        {
            color = new Color(60 / 255f,90 / 255f,200 / 255f);
        }
        if(qui == 4)
        {
            color = new Color(180 / 255f,40 / 255f,150 / 255f);
        }
        if(qui == 5)
        {
            color = new Color(20 / 255f,110 / 255f,30 / 255f);
        }
        return color;
    }
}
