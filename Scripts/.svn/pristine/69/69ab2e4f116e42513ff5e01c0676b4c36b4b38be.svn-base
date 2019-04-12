using System.Collections.Generic;
using College.Research.Data;


/// <summary>
/// 附魔逻辑
/// </summary>
public class EnchanteController
{


    private bool Enchante()
    {

        int buiLevel = 0;

        Enchant_config enchant_Config = Enchant_configConfig.GetEnchant_Config();

        if(ScriptSystem.Instance.Gold < enchant_Config.goldCost[buiLevel])
        {
            //金币不够
            TipManager.Instance.ShowTip("金币不够!");
            return false;
        }
        if(ScriptSystem.Instance.Mana < enchant_Config.manaCost[buiLevel])
        {
            //魔力不够
            TipManager.Instance.ShowTip("魔力不够!");
            return false;
        }
        return true;
    }

}
