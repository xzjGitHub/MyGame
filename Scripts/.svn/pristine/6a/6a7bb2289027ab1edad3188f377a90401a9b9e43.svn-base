using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 悬赏属性
/// </summary>
public class BountyAttribute
{

    /// <summary>
    /// 更新悬赏完成信息
    /// </summary>
    /// <param name="_index">更新类型</param>
    /// <param name="_id">更新ID</param>
    public void UpdateBountyFinishInfo(int _index, int _id, ref List<int> nextBountys)
    {
        //如果没有接受任务 跳过
        if (bountyState != BountyState.Accepted) return;
        switch (_index)
        {
            case 1:
                bountyFinishContent.eventInfo.Find(a => a.id == _id).sum++;
                break;
            case 2:
                bountyFinishContent.itemInfo.Find(a => a.id == _id).sum++;
                break;
            case 3:
                bountyFinishContent.WPInfo.Find(a => a.id == _id).sum++;
                break;
            case 4:
                bountyFinishContent.fortInfo.Find(a => a.id == _id).sum++;
                break;
        }
        if (!CheckIsFinish()) return;
        //完成了 添加后续任务
        bountyState = BountyState.HaveAward;
        nextBountys.AddRange(bountyTemplate.nextBounty);
    }
    /// <summary>
    /// 设置悬赏状态
    /// </summary>
    public void SetBountyState(BountyState _state)
    {
        bountyState = _state;
    }
    /// <summary>
    /// 得到悬赏存档
    /// </summary>
    public BountyData GetBountyData()
    {
        BountyData _data = new BountyData
        {
            bountyID = bountyId,
            createTime = createTime,
            factionType = (int)_factionType,
            bountyReward = bountyReward,
            bountyState = bountyState,
        };
        foreach (var item in bountyFinishContent.eventInfo)
        {
            _data.eventFinishContents.Add(item.id, item.sum);
        }
        foreach (var item in bountyFinishContent.itemInfo)
        {
            _data.itemFinishContents.Add(item.id, item.sum);
        }
        foreach (var item in bountyFinishContent.fortInfo)
        {
            _data.fortFinishContents.Add(item.id, item.sum);
        }
        foreach (var item in bountyFinishContent.WPInfo)
        {
            _data.WPFinishContents.Add(item.id, item.sum);
        }
        return _data;
    }

    /// <summary>
    /// 新建
    /// </summary>
    /// <param name="_id"></param>
    public BountyAttribute(int _id, int faction = 0)
    {
        createTime = ScriptTimeSystem.Instance.Second;
        bountyId = _id;
        bountyTemplate = Bounty_templateConfig.GetBounty_template(_id);
        bountyState = BountyState.Acceptable;
        bountyFinishContent = new BountyFinishContent();
        _factionType = (FactionType)faction;
        bountyReward = GetBountyReward();
    }
    /// <summary>
    /// 根据存档创建
    /// </summary>
    public BountyAttribute(BountyData _bountyData)
    {
        createTime = _bountyData.createTime;
        bountyId = _bountyData.bountyID;
        _factionType = (FactionType)_bountyData.factionType;
        bountyTemplate = Bounty_templateConfig.GetBounty_template(bountyId);
        bountyState = _bountyData.bountyState;
        //
        bountyReward = _bountyData.bountyReward;
        //得到完成项
        if (_bountyData.eventFinishContents.Count > 0)
        {
            foreach (var item in _bountyData.eventFinishContents)
            {
                bountyFinishContent.eventInfo.Add(new BountyFinishInfo { id = item.Key, sum = item.Value });
            }
        }
        if (_bountyData.itemFinishContents.Count > 0)
        {
            foreach (var item in _bountyData.itemFinishContents)
            {
                bountyFinishContent.itemInfo.Add(new BountyFinishInfo { id = item.Key, sum = item.Value });
            }
        }
        if (_bountyData.fortFinishContents.Count > 0)
        {
            foreach (var item in _bountyData.fortFinishContents)
            {
                bountyFinishContent.fortInfo.Add(new BountyFinishInfo { id = item.Key, sum = item.Value });
            }
        }
        if (_bountyData.WPFinishContents.Count > 0)
        {
            foreach (var item in _bountyData.WPFinishContents)
            {
                bountyFinishContent.WPInfo.Add(new BountyFinishInfo { id = item.Key, sum = item.Value });
            }
        }
    }


    /// <summary>
    /// 悬赏Id
    /// </summary>
    public int BountyId { get { return bountyId; } }

    public Bounty_template BountyTemplate { get { return bountyTemplate; } }

    public float CreateTime { get { return createTime; } }

    public BountyState BountyState { get { return bountyState; } }

    public FactionType FactionType { get { return _factionType; } }

    public BountyReward BountyReward { get { return bountyReward; } }

    private BountyReward GetBountyReward()
    {
        var reward = new BountyReward
        {
            gold = bountyTemplate.baseGoldReward,
            token = bountyTemplate.baseTokenReward,
            renowRewards = new Dictionary<int, int>(),
            itemRewards = new List<ItemData>(),
        };
        //声望奖励
        foreach (var item in bountyTemplate.renownReward)
        {
            reward.renowRewards.Add(item[0], item[1]);
        }
        //物品奖励
        foreach (var item in bountyTemplate.itemReward)
        {
            var item_Instance = Item_instanceConfig.GetItemInstance(item[0]);
            var deviation = Bounty_configConfig.GetConfig().deviation;
            deviation = RandomBuilder.RandomNum(1 + deviation, 1 - deviation);
            int itemLevel= (int)item_Instance.baseItemLevel + (int)(bountyTemplate.bountyLevel * deviation);
            reward.itemRewards.Add(
                ItemSystem.Instance.CreateItem(item[0], false, ItemCreateType.Drop, itemLevel, bountyTemplate.bountyLevel).GetItemData());
        }
        //
        return reward;
    }

    /// <summary>
    /// 检查是否完成
    /// </summary>
    private bool CheckIsFinish()
    {
        var info = new List<BountyFinishInfo>();
        foreach (var item in bountyTemplate.bountyTarget)
        {
            switch (item[0])
            {
                case 1:
                    info = bountyFinishContent.eventInfo;
                    break;
                case 2:
                    info = bountyFinishContent.itemInfo;
                    break;
                case 3:
                    info = bountyFinishContent.WPInfo;
                    break;
                case 4:
                    info = bountyFinishContent.fortInfo;
                    break;
            }
            if (info.Where(_info => _info.id == item[1]).Any(_info => _info.sum < item[2])) return false;
        }
        return true;
    }

    //
    private Bounty_template bountyTemplate;
    private int bountyId;
    private BountyState bountyState = BountyState.Default;
    private BountyFinishContent bountyFinishContent = new BountyFinishContent();
    private float createTime;
    private FactionType _factionType;
    private BountyReward bountyReward;
}

/// <summary>
/// 悬赏奖励
/// </summary>
public class BountyReward
{
    /// <summary>
    /// 金币
    /// </summary>
    public int gold;
    /// <summary>
    /// 代币
    /// </summary>
    public int token;
    /// <summary>
    /// 声望奖励
    /// </summary>
    public Dictionary<int, int> renowRewards;
    /// <summary>
    /// 物品奖励
    /// </summary>
    public List<ItemData> itemRewards;
}

/// <summary>
/// 悬赏完成内容
/// </summary>
public class BountyFinishContent
{
    /// <summary>
    /// 事件
    /// </summary>
    public List<BountyFinishInfo> eventInfo = new List<BountyFinishInfo>();
    /// <summary>
    /// 争霸
    /// </summary>
    public List<BountyFinishInfo> fortInfo = new List<BountyFinishInfo>();
    /// <summary>
    /// 物品拾取
    /// </summary>
    public List<BountyFinishInfo> itemInfo = new List<BountyFinishInfo>();
    /// <summary>
    /// 路点完成
    /// </summary>
    public List<BountyFinishInfo> WPInfo = new List<BountyFinishInfo>();
}
/// <summary>
/// 悬赏完成信息
/// </summary>
public class BountyFinishInfo
{
    public int id;
    public int sum;
}
/// <summary>
/// 悬赏状态
/// </summary>
public enum BountyState
{
    Default = 0,
    /// <summary>
    /// 不可接受
    /// </summary>
    Unacceptable = 1,
    /// <summary>
    /// 可接受
    /// </summary>
    Acceptable = 2,
    /// <summary>
    /// 已接受
    /// </summary>
    Accepted = 3,
    /// <summary>
    /// 有奖励
    /// </summary>
    HaveAward = 4,
    /// <summary>
    /// 已完成
    /// </summary>
    Completed = 5,
}
