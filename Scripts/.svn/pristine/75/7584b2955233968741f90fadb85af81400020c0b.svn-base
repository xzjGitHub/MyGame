using System;
using GameEventDispose;

/// <summary>
/// 争霸系统
/// </summary>
public class HegemonySystem : ScriptBase
{

    private static HegemonySystem instance;

    public static HegemonySystem Instance { get { return instance; } }

    public CycleInvasionSystem CycleInvasionSystem { get { return cycleInvasionSystem; } }

    public ExpeditionSystem ExpeditionSystem { get { return expeditionSystem; } }

    //上次入侵月份
    private float lastCycleInvasionMonth = -1;
    //父路径
    private string parentPath;
    //
    private const string HegemonySystemPath = "HegemonySystemData";
    //是否在入侵
    private bool isCycleInvasion;
    //
    private HegemonySystemData hegemonySystemData;
    //
    private CycleInvasionSystem cycleInvasionSystem;
    private ExpeditionSystem expeditionSystem;


    public HegemonySystem()
    {
        instance = this;
        expeditionSystem = new ExpeditionSystem();
    }

    public override void SaveData(string parentPath)
    {
        this.parentPath = parentPath;
        hegemonySystemData.lastCycleInvasionMonth = lastCycleInvasionMonth;
        hegemonySystemData.isCycleInvasion = isCycleInvasion;
        //
        if (cycleInvasionSystem != null)
        {
        }
        //
        GameDataManager.SaveData(parentPath, HegemonySystemPath, hegemonySystemData);
        expeditionSystem.SaveData(parentPath);
    }

    public override void ReadData(string parentPath)
    {
        this.parentPath = parentPath;
        hegemonySystemData = GameDataManager.ReadData<HegemonySystemData>(parentPath + HegemonySystemPath) as HegemonySystemData;
        expeditionSystem.ReadData(parentPath);
    }

    public override void Init()
    {

    }


}

/// <summary>
/// 入侵类型
/// </summary>
public enum HegemonyType
{
    /// <summary>
    /// 周期性
    /// </summary>
    Cycle = 1,
    /// <summary>
    /// 反击
    /// </summary>
    Retaliation = 2,
    /// <summary>
    /// 远征
    /// </summary>
    Expedition = 3,
}