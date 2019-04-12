using System.Collections.Generic;
using ProtoBuf;


/// <summary>
/// 远征系统存档
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class ExpeditionSystemData
{
    /// <summary>
    /// 已解锁的列表
    /// </summary>
    public List<int> unlockExpeditions = new List<int>();
    /// <summary>
    /// 已经占领的列表
    /// </summary>
    public List<int> occupyExpeditions = new List<int>();
    /// <summary>
    /// 远征周期奖励
    /// </summary>
    public List<ExpeditionReward> expeditionCycleRewards = new List<ExpeditionReward>();
    /// <summary>
    /// 是否远征中
    /// </summary>
    public bool isExpeditioning;
    /// <summary>
    /// 正在远征的存档
    /// </summary>
    public ExpeditionData expeditionData;
}



/// <summary>
/// 远征存档
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class ExpeditionData
{
    /// <summary>
    /// 远征开始时间
    /// </summary>
    public float expeditionStartTime;
    /// <summary>
    /// 远征时间
    /// </summary>
    public float expeditionTime;
    /// <summary>
    /// 围攻开始时间
    /// </summary>
    public float siegeStartTime;
    /// <summary>
    /// 围攻时间
    /// </summary>
    public float siegeTime;
    /// <summary>
    /// 选择Npc队伍
    /// </summary>
    public int selectNpcTeamId;
    /// <summary>
    /// 角色组
    /// </summary>
    public List<int> charGroups = new List<int>();
    //守备怪物队伍
    public List<GuardMobTeam> guardMobTeams = new List<GuardMobTeam>();
    //战斗单元组信息
    public Dictionary<int, CombatUnit> combatUnitGroups = new Dictionary<int, CombatUnit>();//存档
}

