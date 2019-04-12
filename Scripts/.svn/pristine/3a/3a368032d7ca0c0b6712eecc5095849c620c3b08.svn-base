using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;



/// <summary>
/// MobTeamAttribute
/// </summary>
public partial class MobTeamAttribute
{
    /// <summary>
    /// 怪物队伍属性
    /// </summary>
    public Mob_mobteam mob_mobteam;
    /// <summary>
    /// 入侵属性
    /// </summary>
    public Invasion_template invasion_template;
    /// <summary>
    /// 经验奖励
    /// </summary>
    public float finalCharExpReward
    {
        get
        {
            return (float)(mob_mobteam.baseCharExpReward);
        }
    }
    /// <summary>
    /// 物品奖励
    /// </summary>
    public float finalRewardLevel
    {
        get
        {
            return (float)(invasion_template.baseRewardLevel[0]);
        }
    }

}
