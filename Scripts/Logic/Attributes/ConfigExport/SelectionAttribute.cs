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
    /// 事件模板
    /// </summary>
    public Event_selection event_selection;
    /// <summary>
    /// 事件设置
    /// </summary>
    public Event_config event_config;
    /// <summary>
    /// 事件模板
    /// </summary>
    public Event_template event_template;
    /// <summary>
    /// 队伍属性
    /// </summary>
    public TeamAttribute teamAttribute;
    /// <summary>
    /// 选项全局奖励加值
    /// </summary>
    public float tempOARewardBonus ;
    /// <summary>
    /// 最终角色经验奖励
    /// </summary>
    public float finalCharExpReward;
    /// <summary>
    /// 最终金币奖励
    /// </summary>
    public float finalGoldReward;
    /// <summary>
    /// 最终代币奖励
    /// </summary>
    public float finalTokenReward;
    /// <summary>
    /// 基本奖励等级
    /// </summary>
    public float baseRewardLevel;
    /// <summary>
    /// 最终奖励等级
    /// </summary>
    public float finalRewardLevel;
    /// <summary>
    /// 基本成功率
    /// </summary>
    public float baseSuccessChance;
    /// <summary>
    /// 计算成功率
    /// </summary>
    public float tempSuccessChance;
    /// <summary>
    /// 最终成功率
    /// </summary>
    public int finalSuccessChance;
    /// <summary>
    /// 最终失败率
    /// </summary>
    public int finalFailureChance;
    /// <summary>
    /// 最终陷阱率
    /// </summary>
    public int finalTrapChance;
    /// <summary>
    /// 最终埋伏率
    /// </summary>
    public int finalAmbushChance;
    /// <summary>
    /// 访问次数
    /// </summary>
    public int visitCount;
    /// <summary>
    /// 最终生命球奖励
    /// </summary>
    public int finalGlobReward;
    /// <summary>
    /// 冒险风险
    /// </summary>
    public float ventureRisk;
    /// <summary>
    /// 当前层
    /// </summary>
    public int currentPhase;
    /// <summary>
    /// 最终全局奖励
    /// </summary>
    public float finalOARewardBonus ;
    /// <summary>
    /// 
    /// </summary>
    public float bribeGoldCost;
    /// <summary>
    /// 
    /// </summary>
    public float bribeManaCost;
    /// <summary>
    /// 
    /// </summary>
    public float combatBonus;

}
