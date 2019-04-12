using ProtoBuf;
using System.Collections.Generic;

[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class BountySystemData
{
    /// <summary>
    /// 上次刷新时间
    /// </summary>
    public float lastRefreshTime;
    /// <summary>
    /// 主线悬赏
    /// </summary>
    public List<BountyData> mainBountys = new List<BountyData>();
    /// <summary>
    /// 可用的随机任务
    /// </summary>
    public List<BountyData> usableRandomBounty = new List<BountyData>();
    /// <summary>
    /// 残骸任务
    /// </summary>
    public List<BountyData> remainsBountys = new List<BountyData>();
    /// <summary>
    /// 已完成的主线
    /// </summary>
    public List<int> mainFinisheds = new List<int>();
    /// <summary>
    /// 已完成的随机
    /// </summary>
    public List<int> randomFinisheds = new List<int>();
    /// <summary>
    /// 已完成的残骸
    /// </summary>
    public List<int> remainsFinisheds = new List<int>();

    /// <summary>
    /// 已接的随机任务
    /// </summary>
    public BountyData randomBounty;
    /// <summary>
    /// 人情值
    /// </summary>
    public Dictionary<int, int> favorValue = new Dictionary<int, int>();
    /// <summary>
    /// 声望值
    /// </summary>
    public Dictionary<int, int> renownValue = new Dictionary<int, int>();

    public bool isAcceptedRandomBounty;
    public int selectRandomBountyIndex;
}
