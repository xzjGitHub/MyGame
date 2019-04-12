using System.Collections.Generic;
using ProtoBuf;

/// <summary>
/// 探索地图
/// </summary>
public class ExploreMap
{
    /// <summary>
    /// 完成度
    /// </summary>
    public int FinishProgress { get { return (int)(FinishWPs.Count / (float)map_template.WPCount) * (int)RandomBuilder.RandomMaxFactor; } }

    /// <summary>
    /// 是否探索中
    /// </summary>
    public bool IsExploring { get { return isExploring; } }

    public int ZoneId { get { return zoneID; } }

    public int PosIndex { get { return posIndex; } }

    public Map_template MapTemplate { get { return map_template; } }

    public bool IsTaskAdd { get { return isTaskAdd; } }

    /// <summary>
    /// 地图id
    /// </summary>
    public int MapId { get { return mapID; } }

    public List<int> UnusableEvents { get { return zone.CDEvents; } }

    public List<int> FinishWPs { get { return finishWPs; } }

    public int CreateTime { get { return createTime; } }

    public int FinishRate { get { return finishRate; } }

    public int NowIndex
    {
        get
        {
            return nowIndex;
        }
    }

    public int PreviousWpId
    {
        get
        {
            return previousWpId;
        }
    }

    public bool IsUnLockFort
    {
        get
        {
            return isUnLockFort;
        }
    }


    /// <summary>
    /// 新建探索地图
    /// </summary>
    public ExploreMap(int mapID, int time, int index, Zone zone)
    {
        posIndex = index;
        zoneID = zone.ZoneId;
        this.mapID = mapID;
        createTime = time;
        map_template = Map_templateConfig.GetMap_templat(mapID);
        previousWpId = 0;
        nowIndex = 0;
        this.zone = zone;
    }
    /// <summary>
    /// 新建探索地图
    /// </summary>
    public ExploreMap(ExplorMapData data, Zone zone)
    {
        zoneID = data.zoneID;
        mapID = data.mapID;
        createTime = data.createTime;
        posIndex = data.posIndex;
        finishRate = data.finishRate;
        isExploring = data.isExploring;
        finishWPs = data.finishWPs;
        nowIndex = data.nowIndex;
        previousWpId = data.previousWpId;
        isUnLockFort = data.isUnLockFort;
        //
        this.zone = zone;
        //
        map_template = Map_templateConfig.GetMap_templat(mapID);
    }

    /// <summary>
    /// 是否销毁
    /// </summary>
    public bool IsDestroy(float time)
    {
        return ((int)time - createTime) % map_template.duration == 0;
    }

    /// <summary>
    /// 是否ShowTime
    /// </summary>
    public bool IsShowTime(float time)
    {
        return (int)time - (createTime + map_template.duration) < 4;
    }

    public int TimeLeft(float time)
    {
        return createTime + map_template.duration - (int)time;
    }

    public void SetPosIndex(int index)
    {
        posIndex = index;
    }

    public void SetExploreState(bool isExploring)
    {
        this.isExploring = isExploring;
    }

    /// <summary>
    /// 获得探索地图数据
    /// </summary>
    /// <returns></returns>
    public ExplorMapData GetExplorMapData()
    {
        return new ExplorMapData(this);
    }
    /// <summary>
    /// 添加Boss事件
    /// </summary>
    public void AddBossEventVisitEnd(int eventID)
    {
        zone.AddComDisEvent(eventID);
    }

    /// <summary>
    /// 获得下一个路点列表
    /// </summary>
    /// <returns></returns>
    private List<int> GetNextWPIDs(int wpIndex)
    {
        List<int> list = new List<int>();

        return list;
    }

    /// <summary>
    /// 得到可用的路点列表
    /// </summary>
    private List<int> GetUsableWpList(List<int> list)
    {
        WP_template wP_Template;
        List<int> _list = new List<int>();
        for (int i = 0; i < list.Count; i++)
        {
            wP_Template = WP_templateConfig.GetWpTemplate(list[i]);
            if (wP_Template == null)
            {
                continue;
            }

            _list.Add(list[i]);
        }
        return _list;
    }
    //
    private int posIndex = -1;
    //区域ID
    private readonly int zoneID;
    /// <summary>
    /// 地图id
    /// </summary>
    private readonly int mapID;
    /// <summary>
    /// 创建时间
    /// </summary>
    private readonly int createTime;
    /// <summary>
    /// 完成率
    /// </summary>
    private readonly int finishRate;
    /// <summary>
    /// 是否探索中
    /// </summary>
    private bool isExploring;

    private bool isUnLockFort;
    private readonly bool isTaskAdd;
    //
    private readonly List<int> finishWPs = new List<int>();
    private readonly List<int> unusableEvents;
    private readonly int nowIndex;
    private readonly int previousWpId;
    //
    private readonly ExplorMapData mapData;
    private Map_template map_template;
    private Zone zone;
}
/// <summary>
/// 探索地图数据
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class ExplorMapData
{
    public int posIndex = -1;
    //区域ID
    public int zoneID;
    /// <summary>
    /// 地图id
    /// </summary>
    public int mapID;
    /// <summary>
    /// 创建时间
    /// </summary>
    public int createTime;
    /// <summary>
    /// 完成率
    /// </summary>
    public int finishRate;
    /// <summary>
    /// 是否探索中
    /// </summary>
    public bool isExploring;
    public int nowIndex;
    public int previousWpId;

    public bool isUnLockFort;
    //
    public List<int> finishWPs = new List<int>();

    public ExplorMapData() { }

    public ExplorMapData(ExploreMap map)
    {
        posIndex = map.PosIndex;
        zoneID = map.ZoneId;
        mapID = map.MapId;
        createTime = map.CreateTime;
        finishRate = map.FinishRate;
        isExploring = map.IsExploring;
        nowIndex = map.NowIndex;
        previousWpId = map.PreviousWpId;
        finishWPs = map.FinishWPs;
        isUnLockFort = map.IsUnLockFort;
    }

}