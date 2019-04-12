using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;



/// <summary>
/// RareMaterialAttribute
/// </summary>
public partial class RareMaterialAttribute
{
    /// <summary>
    /// 素材研究表
    /// </summary>
    public MR_template rarematerial_template;
    /// <summary>
    /// 游戏配置
    /// </summary>
    public Game_config game_config;
    /// <summary>
    /// 人员管理配置
    /// </summary>
    public HR_config hr_config;
    /// <summary>
    /// 参与素材研究的角色数
    /// </summary>
    public int MRChar ;
    /// <summary>
    /// 素材基础研究等级
    /// </summary>
    public float enchantExpReward ;
    /// <summary>
    /// 素材最终研究等级
    /// </summary>
    public float dailyResearchExp 
    {
        get
        {
            return (float)(enchantExpReward / game_config.productionCycle) * RandomBuilder.RandomNum((1 + game_config.researchDeviation),(1 - game_config.researchDeviation)) * (1 + MRChar * hr_config.researchBonus);
        }
    }

}
