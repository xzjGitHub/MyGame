﻿using System.Collections.Generic;
using UITip;

public class TipPanelShowUtil
{
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
}
