﻿using System.Collections.Generic;

public partial class SelectionAttribute
{
    public Combat_config combat_config;
    public Event_config event_config;
    public float Difficulty { get { return difficulty; } }
    public long SelectionID { get { return event_selection.selectionID; } }
    public int MobLevel { get { return WPLevel + event_selection.addMobLevel; } }
    public SelectionType SelectionType { get { return selectionType; } }

    public int phase;
    public float finalRewardBonus;
    public EventAttribute eventAttribute;
    public int ExpRward { get { return (int)finalCharExpReward; } }

    public float rndRate
    {
        get { return RandomBuilder.RandomNum(1 - combat_config.DMGDev, 1 + combat_config.DMGDev); }
    }


    /// <summary>
    /// 是否能使用
    /// </summary>
    /// <returns></returns>
    public bool IsCanUse()
    {
        //金币消耗，魔力消耗，代币消耗
        List<int> cost = event_selection.currencyCost;
        if (cost != null)
        {
            for (int i = 0; i < cost.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        if (ScriptSystem.Instance.Gold < cost[i])
                        {
                            return false;
                        }
                        break;
                    case 1:
                        if (ScriptSystem.Instance.Mana < cost[i])
                        {
                            return false;
                        }
                        break;
                    case 2:
                        if (PlayerSystem.Instance.Token < cost[i])
                        {
                            return false;
                        }
                        break;
                }
            }
        }
        //物品消耗
        if (event_selection.itemCost != null)
        {
            foreach (List<int> item in event_selection.itemCost)
            {
                if (item.Count < 2)
                {
                    continue;
                }
                if (ItemSystem.Instance.GetItemNumByTemplateID(item[0]) < item[1])
                {
                    return false;
                }
            }
        }
        return true;
    }
    /// <summary>
    /// 选项消耗
    /// </summary>
    public void SelectionCost()
    {
        //金币消耗，魔力消耗，代币消耗
        List<int> cost = event_selection.currencyCost;
        if (cost != null)
        {
            for (int i = 0; i < cost.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        ScriptSystem.Instance.SubGold(cost[i]);
                        break;
                    case 1:
                        ScriptSystem.Instance.SubMana(cost[i]);
                        break;
                    case 2:
                        PlayerSystem.Instance.SubToken(cost[i]);
                        break;
                }
            }
        }
        //物品消耗
        if (event_selection.itemCost != null)
        {
            foreach (List<int> item in event_selection.itemCost)
            {
                if (item.Count < 2)
                {
                    continue;
                }
                ItemSystem.Instance.UseItem_Instanceid(item[0], item[1]);
            }
        }
    }

    /// <summary>
    /// 获得奖励
    /// </summary>
    /// <returns></returns>
    public RwardInfo GetRwardInfo()
    {
        return new RwardInfo()
        {
            gold = (int)finalGoldReward,
            exp = (int)finalCharExpReward,
            items = GetEventItemRewards(),
        };
    }

    /// <summary>
    /// 得到怪物队伍信息
    /// </summary>
    /// <returns></returns>
    public CombatTeamInfo GetMobTeamInfo(out int mobTeamID)
    {
        return new CombatTeamInfo(1, TeamType.Enemy, GetbTeamAttribute(out mobTeamID)); ;
    }


    /// <summary>
    /// 获得怪物队伍属性
    /// </summary>
    /// <returns></returns>
    public MobTeamAttribute GetbTeamAttribute(out int mobTeamID)
    {
        mobTeamID = RandomBuilder.RandomValues(event_selection.mobTeamList, 1)[0];
        return new MobTeamAttribute(mobTeamID, MobLevel, event_selection.addTeamInitiative);
    }


    /// <summary>
    /// 获得下一层选项
    /// </summary>
    /// <param name="selections"></param>
    /// <returns></returns>
    public List<SelectionAttribute> GetNextLayerSelections()
    {
        List<SelectionAttribute> selections = new List<SelectionAttribute>();
        int index = RandomBuilder.RandomIndex_Chances(event_selection.chanceSet);
        if (index != -1)
        {
            foreach (int selectionID in event_selection.resultSet[index])
            {
                selections.Add(new SelectionAttribute(selectionID, phase + 1, eventAttribute));
            }
        }

        return selections;
    }

    /// <summary>
    /// 创建选项
    /// </summary>
    /// <param name="id"></param>
    /// <param name="phase"></param>
    /// <param name="eventAttribute"></param>
    public SelectionAttribute(int id, int phase, EventAttribute eventAttribute)
    {
        this.phase = phase;
        event_selection = Event_selectionConfig.GetSelection(id);
        if (event_selection == null)
        {
            LogHelper_MC.LogError("selection没有找到=" + id);
        }
        this.eventAttribute = eventAttribute;
        event_config = Event_configConfig.GetConfig();
        char_config = Char_configConfig.GetConfig();
        combat_config = Combat_configConfig.GetCombat_config();
        wp_template = eventAttribute.wp_template;
        event_template = Event_templateConfig.GetEventTemplate(eventAttribute.eventId);
        teamAttribute = eventAttribute.teamAttribute;
        selectionType = (SelectionType)event_selection.selectionType;
        WPLevel = eventAttribute.WPLevel;
        difficulty = GetDifficulty();
    }

    /// <summary>
    /// 获得结果
    /// </summary>
    /// <returns></returns>
    public int GetResult()
    {
        return RandomBuilder.RandomIndex_Chances(new List<int> { finalJackpotChance, finalSuccessChance, finalFailureChance, finalEpicFailureChance });
    }

    /// <summary>
    /// 得到物品奖励
    /// </summary>
    private List<ItemAttribute> GetEventItemRewards()
    {
        List<int> _rewardSet = event_selection.itemRewardSet;
        if (_rewardSet == null || _rewardSet.Count == 0)
        {
            return null;
        }
        return ItemSystem.Instance.Itemrewards_Attribute(new ItemRewardInfo(GetRewardLevels(_rewardSet.Count), event_selection.itemRewardSet, 1/*eventLevel + event_template.addItemLevel*/));
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

    private float finalRewardLevel
    {
        get
        {
            return event_selection.baseRewardLevel * (1 + finalRewardBonus) *
                   RandomBuilder.RandomNum(1 - event_config.selectionDeviation, 1 + event_config.selectionDeviation);
        }
    }

    /// <summary>
    /// 是否能使用
    /// </summary>
    /// <param name="selectionID"></param>
    /// <param name="units"></param>
    /// <returns></returns>
    private bool IsUsable(int selectionID, List<CombatUnit> units)
    {


        return true;
    }

    private int GetDifficulty()
    {
        if (event_selection.chanceSet.Count < 3)
        {
            return 0;
        }
        if (event_selection.chanceSet.Count < 4)
        {
            return event_selection.chanceSet[2];
        }
        return event_selection.chanceSet[2] + event_selection.chanceSet[3];
    }
    //
    private SelectionType selectionType;
    private int WPLevel;
    private int _finalTeamInitiative;
    private float difficulty;
}

/// <summary>
/// 选项类型
/// </summary>
public enum SelectionType
{
    /// <summary>
    /// 默认
    /// </summary>
    Default = 0,
    /// <summary>
    /// 基本
    /// </summary>
    Base = 1,
    /// <summary>
    /// 书页
    /// </summary>
    Page = 2,
    /// <summary>
    /// 战斗
    /// </summary>
    Combat = 3,
    /// <summary>
    /// 无视
    /// </summary>
    Ignore = 4,
    /// <summary>
    /// 返回
    /// </summary>
    Back = 5,
    /// <summary>
    /// 复活
    /// </summary>
    Resurrection = 6,
    /// <summary>
    /// 撤退
    /// </summary>
    Pullout = 7,
}

/// <summary>
/// 奖励信息
/// </summary>
public class RwardInfo
{
    public int gold;
    public int token;
    public int exp;
    public List<ItemAttribute> items;
}