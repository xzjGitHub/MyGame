﻿using System.Collections.Generic;

public partial class PreLoadUti
{

    private static List<string> GetPreLoadPanelList()
    {
        List<string> s = new List<string>();
        s.Add("CorePanel");
       // s.Add("ShopPanel");
        //s.Add("NewWorkShopPanel");
        //s.Add("CollegePanel");
       // s.Add("BarrackPanel");
       // s.Add("HallPanel");
       // s.Add("NewMainPanel");
       // s.Add("BagPanel");
        //s.Add("CharPanel");
        return s;
    }

    private static List<string> GetPreLoadPrefabList()
    {
        List<string> s = new List<string>();
       // s.Add("CharList");
       // s.Add("EquipList");
       // s.Add("NewItemList");
        return s;
    }

    private static List<string> GetPreLoadEffList()
    {
        List<string> s = new List<string>()
        {
            //StringDefine.UIEffectNameDefine.BarrackEff,
            //StringDefine.UIEffectNameDefine.CollegeEff,
            //StringDefine.UIEffectNameDefine.CoreEff,
            //StringDefine.UIEffectNameDefine.HallEff,
            //StringDefine.UIEffectNameDefine.ShopEff,
            //StringDefine.UIEffectNameDefine.WorkShopEff
        };
        return s;
    }
}

