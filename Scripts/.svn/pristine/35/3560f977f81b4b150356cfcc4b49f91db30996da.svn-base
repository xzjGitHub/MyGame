using GameEventDispose;


/// <summary>
/// 剧本时间系统
/// </summary>
public class ScriptTimeSystem : ScriptBase
{
    private static ScriptTimeSystem instance;
    private float second;
    private bool isStartTiming;
    //
    private float lastSecond;
    private float lastDay;
    private float lastMonth;
    private float lastYear;
    private float lastWeek;
    //是否是新建
    private bool isNewCreate;
    //
    private const float aspd = 1;
    //
    public static ScriptTimeSystem Instance { get { return instance; } }

    public float Second
    {
        get { return second; }
    }

    public bool IsStartTiming
    {
        get { return isStartTiming; }
    }
    private string parentPath;
    //
    private const string timePath = "ScriptTimeData";
    //
    private ScriptTimeData scriptTimeData;

    public ScriptTimeSystem(bool _isNewCreate, float _initTime)
    {
        isNewCreate = _isNewCreate;
        second = _initTime;
        instance = this;
    }
    public override void SaveData(string parentPath)
    {
        this.parentPath = parentPath;
        scriptTimeData.second = second;
        scriptTimeData.isStartTiming = isStartTiming;
        scriptTimeData.lastSecond = lastSecond;
        scriptTimeData.lastDay = lastDay;
        scriptTimeData.lastMonth = lastMonth;
        scriptTimeData.lastYear = lastYear;
        scriptTimeData.lastWeek = lastWeek;
        //
        GameDataManager.SaveData(parentPath, timePath, scriptTimeData);
    }

    public override void ReadData(string parentPath)
    {
        this.parentPath = parentPath;
        scriptTimeData = GameDataManager.ReadData<ScriptTimeData>(parentPath + timePath) as ScriptTimeData;
    }

    public override void Init()
    {
        isStartTiming = false;
        //存档恢复
        if (scriptTimeData == null)
        {
            scriptTimeData = new ScriptTimeData { isStartTiming = true };
        }
        //新建
        if (isNewCreate)
        {
            lastSecond = second;
            lastDay = TimeUtil.GetPlayDays();
            lastMonth = TimeUtil.GetPlayMonths();
            lastYear = TimeUtil.GetPlayYears();
            lastWeek = TimeUtil.GetPlayWeeks();
            return;
        }
        //存档
        second = scriptTimeData.second;
        //    isStartTiming = scriptTimeData.isStartTiming;
        lastSecond = scriptTimeData.lastSecond;
        lastDay = scriptTimeData.lastDay;
        lastMonth = scriptTimeData.lastMonth;
        lastYear = scriptTimeData.lastYear;
        lastWeek = scriptTimeData.lastWeek;
    }

    /// <summary>
    /// 开始计时
    /// </summary>
    public void Timing(float _time)
    {
        if (!isStartTiming) return;
        second += _time * aspd;
        //天
        if ((int)lastSecond != (int)second)
        {
            lastSecond = second;
            EventDispatcher.Instance.ScriptTimeEvent.DispatchEvent(EventId.ScriptTimeEvent, ScriptTimeUpdateType.Second, (object)(int)second);
        }
        //天
        if (lastDay != TimeUtil.GetPlayDays())
        {
            lastDay = TimeUtil.GetPlayDays();
            EventDispatcher.Instance.ScriptTimeEvent.DispatchEvent(EventId.ScriptTimeEvent, ScriptTimeUpdateType.Day, (object)lastDay);
        }
        //周
        if (lastWeek != TimeUtil.GetPlayWeeks())
        {
            lastWeek = TimeUtil.GetPlayWeeks();
            EventDispatcher.Instance.ScriptTimeEvent.DispatchEvent(EventId.ScriptTimeEvent, ScriptTimeUpdateType.Week, (object)lastDay);
        }
        //月
        if (lastMonth != TimeUtil.GetPlayMonths())
        {
            lastMonth = TimeUtil.GetPlayMonths();
            EventDispatcher.Instance.ScriptTimeEvent.DispatchEvent(EventId.ScriptTimeEvent, ScriptTimeUpdateType.Month, (object)lastMonth);
        }
        //年
        if (lastYear != TimeUtil.GetPlayYears())
        {
            lastYear = TimeUtil.GetPlayYears();
            EventDispatcher.Instance.ScriptTimeEvent.DispatchEvent(EventId.ScriptTimeEvent, ScriptTimeUpdateType.Year, (object)lastYear);
        }
    }

    /// <summary>
    /// 恢复计时——只能在初始化数据时调用
    /// </summary>
    public void RecoverTiming()
    {
        if (scriptTimeData.isStartTiming == false) return;
        isStartTiming = true;
    }
    /// <summary>
    /// 开始计时
    /// </summary>
    public void StartTiming()
    {
        if (isStartTiming) return;
        isStartTiming = true;
    }
    /// <summary>
    /// 停止计时
    /// </summary>
    public void StopTiming()
    {
        if (!isStartTiming) return;
        isStartTiming = false;
    }
}


