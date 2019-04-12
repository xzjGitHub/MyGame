using System.Collections.Generic;
using ProtoBuf;



/// <summary>
/// 周期入侵系统存档
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class CycleInvasionSystemData
{
    /// <summary>
    /// 上次入侵的天数
    /// </summary>
    public float lastCycleInvasionDay;
    /// <summary>
    /// 是否在入侵
    /// </summary>
    public bool isCycleInvasioning;
    /// <summary>
    /// 周期入侵存档
    /// </summary>
    public CycleInvasionData cycleInvasionData;
}


/// <summary>
/// 周期入侵存档
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class CycleInvasionData
{
    /// <summary>
    /// 前置开始时间
    /// </summary>
    public float preStartTime;
    /// <summary>
    /// 前置时间
    /// </summary>
    public float preTime;
    /// <summary>
    /// 预警开始时间
    /// </summary>
    public float warningStartTime;
    /// <summary>
    /// 预警时间
    /// </summary>
    public float warningTime;
    /// <summary>
    /// 围攻开始时间
    /// </summary>
    public float siegeStartTime;
    /// <summary>
    /// 围攻时间
    /// </summary>
    public float siegeTime;
    /// <summary>
    /// 阶段
    /// </summary>
    public int cycleInvasionPhase;
    /// <summary>
    /// 选择的Npc队伍
    /// </summary>
    public int selectNpcTeamId;
    /// <summary>
    /// 入侵的怪物队伍
    /// </summary>
    public List<InvasionMobTeam> invasionMobTeams = new List<InvasionMobTeam>();
    public Dictionary<int, int> factionThreats = new Dictionary<int, int>();
}
