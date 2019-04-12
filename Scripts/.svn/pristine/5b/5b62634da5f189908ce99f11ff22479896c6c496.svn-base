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
    public float tempOARewardBonus
    {
        get
        {
            return event_selection.tempOARewardBonus * RandomBuilder.RandomNum((1 - event_config.selectionDeviation), (1 + event_config.selectionDeviation));
        }
    }
    /// <summary>
    /// 最终角色经验奖励
    /// </summary>
    public float finalCharExpReward
    {
        get
        {
            return event_selection.baseCharExpReward;
        }
    }
    /// <summary>
    /// 最终金币奖励
    /// </summary>
    public float finalGoldReward
    {
        get
        {
            return event_selection.baseGoldReward;
        }
    }
    /// <summary>
    /// 最终代币奖励
    /// </summary>
    public float finalTokenReward
    {
        get
        {
            return event_selection.baseTokenReward;
        }
    }

    /// <summary>
    /// 基本成功率
    /// </summary>
    public float baseSuccessChance
    {
        get
        {
            return 10000;
        }
    }
    /// <summary>
    /// 计算成功率
    /// </summary>
    public float tempSuccessChance;
    /// <summary>
    /// 最终成功率
    /// </summary>
    public int finalSuccessChance
    {
        get
        {
            return (int)(baseSuccessChance);
        }
    }

    /// <summary>
    /// 最终失败率
    /// </summary>
    public int finalFailureChance
    {
        get { return (int)((10000f - finalSuccessChance) / 3f); }
    }

    /// <summary>
    /// 最终陷阱率
    /// </summary>
    public int finalTrapChance
    {
        get { return (int)((10000f - finalSuccessChance) / 3f); }
    }

    /// <summary>
    /// 最终埋伏率
    /// </summary>
    public int finalAmbushChance
    {
        get { return (int)(10000f - finalFailureChance - finalTrapChance); }
    }
    /// <summary>
    /// 访问次数
    /// </summary>
    public int visitCount;
    /// <summary>
    /// 最终生命球奖励
    /// </summary>
    public int finalGlobReward
    {
        get
        {
            return event_selection.baseGlobReward;
        }
    }
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
    public float finalOARewardBonus;
    /// <summary>
    /// 
    /// </summary>
    public float bribeGoldCost
    {
        get
        {
            return 0;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public float bribeManaCost;
    /// <summary>
    /// 
    /// </summary>
    public float combatBonus;
    /// <summary>
    /// 
    /// </summary>
    public float addBonus;

    public float finalRewardLevel
    {
        get { return baseRewardLevel * (1 + finalRewardBonus) * RandomBuilder.RandomNum(1 - event_config.selectionDeviation, 1 + event_config.selectionDeviation); }
    }

    public float baseRewardLevel
    {
        get { return event_selection.baseRewardLevel; }
    }
}
