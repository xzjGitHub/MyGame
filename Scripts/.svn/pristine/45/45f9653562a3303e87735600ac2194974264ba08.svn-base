using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public partial class RareMaterialAttribute: Singleton<RareMaterialAttribute>
{
    private RareMaterialAttribute() { }

    public int GetDailExp(float enchantExpReward,Game_config gameConfig,int MRChar,
        HR_config hR_Config)
    {
        this.enchantExpReward = enchantExpReward;
        this.game_config = gameConfig;
        this.MRChar = MRChar;
        this.hr_config = hR_Config;
        return (int)dailyResearchExp;
    }

}
