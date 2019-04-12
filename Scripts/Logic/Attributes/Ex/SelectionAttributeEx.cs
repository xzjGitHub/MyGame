using System.Collections.Generic;

public partial class SelectionAttribute
{
    public int SelectionID
    {
        get { return event_selection.selectionID; }
    }


    public int phase;
    public float finalRewardBonus;
    public EventAttribute eventAttribute;

    public SelectionAttribute(int id, int phase, EventAttribute eventAttribute)
    {
        this.phase = phase;
        event_selection = Event_selectionConfig.GetSelection(id);
        if (event_selection == null)
        {
            LogHelperLSK.LogError("selection没有找到=" + id);
        }
        this.eventAttribute = eventAttribute;
        currentPhase = eventAttribute.NowPhase;
        event_config = Event_configConfig.GetConfig();
        event_template = Event_templateConfig.GetEventTemplate(eventAttribute.eventId);
        teamAttribute = eventAttribute.teamAttribute;
    }

    public void SetMobTeamID()
    {
        eventAttribute.SetMobTeamID(RandomBuilder.RandomList(1, event_selection.mobTeamList)[0]);
    }

    public int SelectionType
    {
        get { return event_selection.selectionType; }
    }


    /// <summary>
    /// 获得结果
    /// </summary>
    /// <returns></returns>
    public int GetResult()
    {
        return RandomBuilder.RandomIndex_Chances(new List<int> { finalSuccessChance, finalFailureChance, finalTrapChance, finalAmbushChance });
    }

    /// <summary>
    /// 得到物品奖励
    /// </summary>
    public List<ItemData> GetEventItemRewards()
    {
        List<int> _rewardSet = event_selection.itemRewardSet;
        if (_rewardSet == null || _rewardSet.Count == 0)
        {
            return null;
        }
        return ItemSystem.Instance.Itemrewards_ItemDate(new ItemRewardInfo(GetRewardLevels(_rewardSet.Count), event_selection.itemRewardSet, 1/*eventLevel + event_template.addItemLevel*/));
    }

    /// <summary>
    /// 获得奖励等级
    /// </summary>
    /// <returns></returns>
    private List<float> GetRewardLevels(int sum)
    {
        List<float> list = new List<float>();
        for (int i = 0; i < sum; i++)
        {
            list.Add(finalRewardLevel * event_selection.itemRewardSetup[i]);
        }

        return list;
    }

}