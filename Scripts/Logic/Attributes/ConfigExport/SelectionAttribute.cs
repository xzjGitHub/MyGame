using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;



/// <summary>
/// SelectionAttribute
/// </summary>
public partial class SelectionAttribute
{
    /// <summary>
    /// 游戏设置
    /// </summary>
    public Game_config game_config;
    /// <summary>
    /// 角色设置
    /// </summary>
    public Char_config char_config;
    /// <summary>
    /// 路点模板
    /// </summary>
    public WP_template wp_template;
    /// <summary>
    /// 事件模板
    /// </summary>
    public Event_template event_template;
    /// <summary>
    /// 选项模板
    /// </summary>
    public Event_selection event_selection;
    /// <summary>
    /// 队伍属性
    /// </summary>
    public TeamAttribute teamAttribute;
    /// <summary>
    /// 事件等级
    /// </summary>
    public int selectionLevel
    {
        get
        {
            return (int)(wp_template.WPLevel);
        }
    }
    /// <summary>
    /// 伤害成长
    /// </summary>
    public float finalGrowthRate
    {
        get
        {
            return (float)((Math.Pow((11 / 10),(selectionLevel / 3)) + char_config.lvTEHP * selectionLevel / char_config.TankEHP));
        }
    }
    /// <summary>
    /// 最终大奖率
    /// </summary>
    public int finalJackpotChance
    {
        get
        {
            return (int)(event_selection.chanceSet[0]);
        }
    }
    /// <summary>
    /// 最终成功率
    /// </summary>
    public int finalSuccessChance
    {
        get
        {
            return (int)(event_selection.chanceSet[1]);
        }
    }
    /// <summary>
    /// 最终失败率
    /// </summary>
    public int finalFailureChance
    {
        get
        {
            return (int)(event_selection.chanceSet[2]);
        }
    }
    /// <summary>
    /// 史诗失败率
    /// </summary>
    public int finalEpicFailureChance
    {
        get
        {
            return (int)(event_selection.chanceSet[3]);
        }
    }
    /// <summary>
    /// 最终经验奖励
    /// </summary>
    public float finalCharExpReward
    {
        get
        {
            return (float)(event_selection.baseCharExpReward * finalGrowthRate);
        }
    }
    /// <summary>
    /// 最终金币奖励
    /// </summary>
    public float finalGoldReward
    {
        get
        {
            return (float)(event_selection.baseGoldReward * finalGrowthRate * rndRate);
        }
    }

}
