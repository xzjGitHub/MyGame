using ProtoBuf;
using System.Collections.Generic;

[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class BountyData
{
    /// <summary>
    /// 悬赏ID
    /// </summary>
    public int bountyID;
    /// <summary>
    /// 创建时间
    /// </summary>
    public float createTime;

    public int factionType;

    public BountyState bountyState;
    /// <summary>
    /// 事件完成内容
    /// </summary>
    public Dictionary<int,int> eventFinishContents=new Dictionary<int, int>();   
    /// <summary>
    /// 物品完成内容
    /// </summary>
    public Dictionary<int,int> itemFinishContents = new Dictionary<int, int>();
    /// <summary>
    /// 要塞完成内容
    /// </summary>
    public Dictionary<int, int> fortFinishContents = new Dictionary<int, int>();
    /// <summary>
    /// 路点完成内容
    /// </summary>
    public Dictionary<int, int> WPFinishContents = new Dictionary<int, int>();
    /// <summary>
    /// 悬赏奖励
    /// </summary>
    public BountyReward bountyReward;

}

