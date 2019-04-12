using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class itemCommonAttribute
{

    private static itemCommonAttribute instance;
    public static itemCommonAttribute Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new itemCommonAttribute();
            }
            return instance;
        }
    }

    public int GetPrice(int itemID,int num)
    {
        item_instance=Item_instanceConfig.GetItemInstance(itemID);
        return (int)finalSellPrice*num;
    }
}
