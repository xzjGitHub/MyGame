using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public partial class PreLoadUti
{

    private List<string> GetPreLoadPanelList()
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

    private List<string> GetPreLoadPrefabList()
    {
        List<string> s = new List<string>();
       // s.Add("CharList");
       // s.Add("EquipList");
       // s.Add("NewItemList");
        return s;
    }

    private List<string> GetPreLoadEffList()
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

