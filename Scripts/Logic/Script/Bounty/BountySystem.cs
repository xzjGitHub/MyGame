﻿using Core.Data;
using GameEventDispose;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 任务系统
/// </summary>
public class BountySystem : ScriptBase
{
    public static BountySystem Instance { get { return instance; } }
    /// <summary>
    /// 上次刷新时间
    /// </summary>
    public float LastRefreshTime { get { return lastRefreshTime; } }

    public List<int> MainFinisheds { get { return mainFinisheds; } }

    public BountyAttribute RandomBounty { get { return randomBounty; } }

    public List<BountyAttribute> UsableRandomBounty { get { return usableRandomBounty; } }

    public List<BountyAttribute> MainBountyAttributes { get { return mainBountyAttributes; } }
    public int MaxRenownValue
    {
        get
        {
            if (maxRenownValue <= 0)
            {
                maxRenownValue = Bounty_configConfig.GetConfig().rewardRenown;
            }

            return maxRenownValue;
        }
    }

    public bool IsAcceptedRandomBounty { get { return isAcceptedRandomBounty; } }

    public int SelectRandomBountyIndex { get { return selectRandomBountyIndex; } }

    public List<BountyAttribute> RemainsBountys { get { return remainsBountys; } }

    /// <summary>
    /// 是否能用——任务物品筛选
    /// </summary>
    /// <param name="bountyID"></param>
    /// <returns></returns>
    public bool IsItemrewardBountyReq(int bountyID)
    {
        if (mainBountyAttributes.Find(a => a.BountyId == bountyID && a.BountyState == BountyState.Accepted) != null)
        {
            return true;
        }

        return randomBounty != null && randomBounty.BountyState == BountyState.Accepted;
    }

    //
    public BountySystem()
    {
        instance = this;
    }

    public override void Init()
    {
        if (bountySystemData == null)
        {
            bountySystemData = new BountySystemData();
            lastRefreshTime = ScriptTimeSystem.Instance.Second;
            //
            FirstScriptBounty();
        }
        else
        {
            lastRefreshTime = bountySystemData.lastRefreshTime;
            foreach (BountyData item in bountySystemData.mainBountys)
            {
                mainBountyAttributes.Add(new BountyAttribute(item));
            }
            foreach (BountyData item in bountySystemData.usableRandomBounty)
            {
                usableRandomBounty.Add(new BountyAttribute(item));
            }
            foreach (BountyData item in bountySystemData.remainsBountys)
            {
                remainsBountys.Add(new BountyAttribute(item));
            }
            //
            mainFinisheds = bountySystemData.mainFinisheds;
            randomFinisheds = bountySystemData.randomFinisheds;
            remainsFinisheds = bountySystemData.remainsFinisheds;
            randomBounty = bountySystemData.randomBounty == null ? null : new BountyAttribute(bountySystemData.randomBounty);
            favorValue = bountySystemData.favorValue;
            renownValue = bountySystemData.renownValue;
            isAcceptedRandomBounty = bountySystemData.isAcceptedRandomBounty;
            selectRandomBountyIndex = bountySystemData.selectRandomBountyIndex;
        }
        //
        EventDispatcher.Instance.ScriptTimeEvent.AddEventListener<ScriptTimeUpdateType, object>(EventId.ScriptTimeEvent, OnScriptTimeUpdateEvent);
    }

    public override void SaveData(string parentPath)
    {
        this.parentPath = parentPath;
        bountySystemData = new BountySystemData
        {
            lastRefreshTime = lastRefreshTime,
        };
        foreach (BountyAttribute item in mainBountyAttributes)
        {
            bountySystemData.mainBountys.Add(item.GetBountyData());
        }
        foreach (BountyAttribute item in usableRandomBounty)
        {
            bountySystemData.usableRandomBounty.Add(item.GetBountyData());
        }
        foreach (BountyAttribute item in remainsBountys)
        {
            bountySystemData.remainsBountys.Add(item.GetBountyData());
        }
        //
        bountySystemData.mainFinisheds = mainFinisheds;
        bountySystemData.randomFinisheds = randomFinisheds;
        bountySystemData.remainsFinisheds = remainsFinisheds;
        bountySystemData.isAcceptedRandomBounty = isAcceptedRandomBounty;
        bountySystemData.selectRandomBountyIndex = selectRandomBountyIndex;
        bountySystemData.randomBounty = randomBounty != null ? randomBounty.GetBountyData() : null;
        bountySystemData.favorValue = favorValue;
        bountySystemData.renownValue = renownValue;
        GameDataManager.SaveData(parentPath, BountyFilePath, bountySystemData);
    }

    public override void ReadData(string parentPath)
    {
        this.parentPath = parentPath;
        //剧本存档
        bountySystemData = GameDataManager.ReadData<BountySystemData>(parentPath + BountyFilePath) as BountySystemData;
    }

    /// <summary>
    /// 初始剧本悬赏_只运行一次
    /// </summary>
    public void FirstScriptBounty()
    {
        //初始人情值
        ResetFavorValue();
        ResetRenownValue();
        //刷新主线悬赏
        mainBountyAttributes.Clear();
        RefreshMainBounty();
    }

    /// <summary>
    /// 选中主线悬赏
    /// </summary>
    /// <param name="bountyID"></param>
    public void SelectMainBounty(int bountyID)
    {
        foreach (BountyAttribute item in mainBountyAttributes)
        {
            if (bountyID != item.BountyId)
            {
                continue;
            }

            item.SetBountyState(BountyState.Accepted);
            EventDispatcher.Instance.BountyEvent.DispatchEvent(EventId.BountyEvent, BountyEventType.AcceptBounty, (object)item);
            break;
        }

    }

    /// <summary>
    /// 选中随机悬赏
    /// </summary>
    /// <param name="index"></param>
    public void SelectRandomBounty(int index)
    {
        randomBounty = usableRandomBounty[index];
        randomBounty.SetBountyState(BountyState.Accepted);
        isAcceptedRandomBounty = true;
        selectRandomBountyIndex = index;
        //   usableRandomBounty.Clear();
        EventDispatcher.Instance.BountyEvent.DispatchEvent(EventId.BountyEvent, BountyEventType.AcceptBounty, (object)null);
    }
    /// <summary>
    /// 领取声望奖励
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public RenownBoxReward ReceiveRenownAward(int type)
    {
        renownValue[type] -= MaxRenownValue;
        ResetFavorValue();
        return GetRenownBoxReward(type); ;
    }
    /// <summary>
    /// 完成任务
    /// </summary>
    /// <param name="bountyID"></param>
    public bool FinishedBounty(int bountyID)
    {
        BountyAttribute bountyAttribute = mainBountyAttributes.Find(a => a.BountyId == bountyID);
        if (bountyAttribute != null)
        {
            AddRenownValue(bountyAttribute.BountyReward);
            LogHelper_MC.Log("未添加其他奖励");
            EventDispatcher.Instance.BountyEvent.DispatchEvent(EventId.BountyEvent, BountyEventType.FinishBounty, (object)bountyAttribute);
            return true;
        }
        if (randomBounty == null)
        {
            return false;
        }

        AddRenownValue(randomBounty.BountyReward);
        //增加人情值
        favorValue[(int)randomBounty.FactionType]++;
        if (favorValue[(int)randomBounty.FactionType] >= Bounty_configConfig.GetConfig().maxFavor)
        {
            ResetFavorValue(1);
        }
        LogHelper_MC.Log("未添加其他奖励");
        EventDispatcher.Instance.BountyEvent.DispatchEvent(EventId.BountyEvent, BountyEventType.FinishBounty, (object)randomBounty);
        return true;
    }
    /// <summary>
    /// 更新悬赏信息
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="id">id</param>
    public void UpdateBountyConditionInfo(BountyConditionType type, int id)
    {
        List<int> nextBountys = new List<int>();
        //主线
        foreach (BountyAttribute item in mainBountyAttributes)
        {
            if (item.BountyState != BountyState.Accepted)
            {
                continue;
            }

            item.UpdateBountyFinishInfo(type, id, ref nextBountys);
            //添加后续任务
            foreach (int next in nextBountys)
            {
                mainBountyAttributes.Add(new BountyAttribute(next));
            }
        }
        nextBountys.Clear();
        //随机
        randomBounty.UpdateBountyFinishInfo(type, id, ref nextBountys);
        EventDispatcher.Instance.BountyEvent.DispatchEvent(EventId.BountyEvent, BountyEventType.TargetUpdate, (object)null);
    }

    public string GetRenownValueStr(int type)
    {
        return string.Format(IsRenownFull(type) ? introSumStr1 : introSumStr2, renownValue[type],
         MaxRenownValue * (renownValue[type] > MaxRenownValue ? 2 : 1));
    }

    public bool IsRenownFull(int type)
    {
        return renownValue[type] >= MaxRenownValue;
    }

    public float GetRenownRatio(int type)
    {
        if (renownValue[type] > MaxRenownValue)
        {
            return 1;
        }

        return renownValue[type] / (float)MaxRenownValue;
    }

    /// <summary>
    /// 添加残骸任务
    /// </summary>
    /// <param name="instanceID"></param>
    public void AddRemainsBounty(int instanceID)
    {
        Item_startbounty item_startBounty = Item_startbountyConfig.GetItem_startbounty(instanceID);
        if (item_startBounty == null)
        {
            return;
        }

        if (Bounty_templateConfig.GetBounty_template(item_startBounty.startBounty) == null)
        {
            return;
        }

        int bountyID = item_startBounty.startBounty;
        if (remainsBountys.Find(a => a.BountyId == bountyID && a.BountyState == BountyState.Acceptable) != null)
        {
            return;
        }

        if (remainsBountys.Find(a => a.BountyId == bountyID && a.BountyState == BountyState.Accepted) != null)
        {
            return;
        }

        if (remainsFinisheds.FindAll(a => a == bountyID).Count == 0)
        {
            remainsBountys.Add(new BountyAttribute(bountyID));
            return;
        }
        if (item_startBounty.isRepeatable != 1)
        {
            return;
        }

        remainsBountys.Add(new BountyAttribute(bountyID));
    }

    /// <summary>
    /// 获得任务属性
    /// </summary>
    /// <param name="bountyID"></param>
    /// <returns></returns>
    public BountyAttribute GetBountyAttribute(int bountyID)
    {
        if (randomBounty!=null&& randomBounty.BountyId==bountyID)
        {
            return randomBounty;
        }

        return mainBountyAttributes.Find(a => a.BountyId == bountyID);
    }
    /// <summary>
    /// 获得主线关联的事件ID
    /// </summary>
    /// <returns></returns>
    public List<int> GetMainRelatedEventID()
    {
        foreach (var itme in mainBountyAttributes)
        {
            if (itme.BountyState == BountyState.Accepted)
            {
                return itme.BountyTemplate.relatedEvent;
            }
        }
        return new List<int>();
    }
    /// <summary>
    /// 获得随机关联的事件ID
    /// </summary> 
    /// <returns></returns>
    public List<int> GetRoundomRelatedEventID()
    {
        if (randomBounty != null)
        {
            return randomBounty.BountyTemplate.relatedEvent;
        }
        return new List<int>();
    }
    /// <summary>
    /// 刷新主线悬赏
    /// </summary>
    private void RefreshMainBounty()
    {
        foreach (int item in ScriptSystem.Instance.ScriptTemplate.bountyList)
        {
            if (mainBountyAttributes.Find(a => a.BountyId == item) != null)
            {
                continue;
            }

            if (!GetMainBountyState(item))
            {
                continue;
            }

            mainBountyAttributes.Add(new BountyAttribute(item));
            mainBountyAttributes.Last().SetBountyState(BountyState.Acceptable);
        }
        EventDispatcher.Instance.BountyEvent.DispatchEvent(EventId.BountyEvent, BountyEventType.MainUpdate, (object)null);
    }
    /// <summary>
    /// 刷新随机悬赏
    /// </summary>
    private void RefreshRandomBounty()
    {
        //现在有随机任务任务
        if (randomBounty != null && randomBounty.BountyState != BountyState.Completed)
        {
            return;
        }

        isAcceptedRandomBounty = false;
        selectRandomBountyIndex = -1;
        usableRandomBounty.Clear();
        int intimateValue = favorValue.Values.Sum();

        List<int> chance = favorValue.Select(item => (int)(Math.Max(1, item.Value) / (float)Math.Max(8, intimateValue) * 10000)).ToList();

        // List<int> chance = favorValue.Select(item => (int)(item.Value / (float)intimateValue * 10000)).ToList();

        chance = RandomBuilder.RandomIndexs(chance, 2);
        if (chance == null || chance.Count < 2)
        {
            return;
        }

        List<int> list = FortSystem.Instance.NewZone.ZoneTemplate.bountySetList;
        foreach (int item in chance)
        {
            Bounty_bountySet bountySet = Bounty_bountySetConfig.GetBountyBountySet(list[item]);
            if (bountySet == null)
            {
                LogHelper_MC.Log("bountySet==null" + list[item]);
                continue;
            }
            int id = RandomBuilder.RandomList(1, bountySet.bountyList)[0];
            bountyTemplate = Bounty_templateConfig.GetBounty_template(id);
            if (bountyTemplate == null)
            {
                LogHelper_MC.Log("bountyTemplate==null" + id);
                continue;
            }
            usableRandomBounty.Add(new BountyAttribute(id, item + 1));
            usableRandomBounty.Last().SetBountyState(BountyState.Acceptable);
        }
        EventDispatcher.Instance.BountyEvent.DispatchEvent(EventId.BountyEvent, BountyEventType.RandomUpdate, (object)null);
    }
    /// <summary>
    /// 重置人情值
    /// </summary>
    private void ResetFavorValue(int value = 0)
    {
        favorValue.Clear();
        for (int i = 1; i <= hordeSum; i++)
        {
            favorValue.Add(i, value);
        }
    }
    /// <summary>
    /// 重置声望值
    /// </summary>
    private void ResetRenownValue()
    {
        renownValue.Clear();
        for (int i = 1; i <= hordeSum; i++)
        {
            renownValue.Add(i, 0);
            if (i == hordeSum)
            {
                renownValue[i] = MaxRenownValue + 5;
            }
            else
            {
                renownValue[i] = (int)(MaxRenownValue * 0.7f);
            }
        }
    }
    /// <summary>
    /// 添加声望奖励
    /// </summary>
    /// <param name="bountyReward"></param>
    private void AddRenownValue(BountyReward bountyReward)
    {
        foreach (KeyValuePair<int, int> item in bountyReward.renowRewards)
        {
            if (!renownValue.ContainsKey(item.Key))
            {
                continue;
            }

            renownValue[item.Key] = Math.Min(renownValue[item.Key] + item.Value, MaxRenownValue * 2);
        }

    }
    /// <summary>
    /// 获得主线悬赏状态
    /// </summary>
    /// <param name="bountyId">悬赏ID</param>
    /// <returns>返回是否能用</returns>
    private bool GetMainBountyState(int bountyId)
    {
        bountyTemplate = Bounty_templateConfig.GetBounty_template(bountyId);
        if (bountyTemplate == null)
        {
            return false;
        }
        //检查等级
        if (CoreSystem.Instance.GetLevel() < bountyTemplate.coreLevelReq)
        {
            return false;
        }
        //检查是否全部完成
        if (!bountyTemplate.bountyReq.All(id => mainFinisheds.Contains(id)))
        {
            return false;
        }
        //检查剧本开始时间
        if (!TimeUtil.ReachOnTime(ScriptSystem.Instance.ScriptTemplate.initialDate, bountyTemplate.timeReq))
        {
            return false;
        }
        //争霸 
        return bountyTemplate.fortReq.All(item => FortSystem.Instance.UnlockForts.Contains(item));
    }
    /// <summary>
    /// 剧本时间更新事件
    /// </summary>
    private void OnScriptTimeUpdateEvent(ScriptTimeUpdateType arg1, object arg2)
    {
        if (arg1 == ScriptTimeUpdateType.Day)
        {
            //刷新主线
            RefreshMainBounty();
        }
        if (arg1 == ScriptTimeUpdateType.Week)
        {
            //刷新随机
            RefreshRandomBounty();
        }
    }

    /// <summary>
    /// 获得描述字段
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public string GetIntroStr(int type)
    {
        switch (type)
        {
            case 1:
                return "击败";
            case 2:
                return "回收";
            case 3:
                return "巡逻";
            case 4:
                return "收复";
            default:
                return "击败";
        }
    }
    public string GetFactionStr(int type)
    {
        switch (type)
        {
            case 1:
                return "节点公会";
            case 2:
                return "自治委员会";
            case 3:
                return "盗贼公会";
            case 4:
                return "骑士团";
            case 5:
                return "流亡贵族会";
            case 6:
                return "学院";
            case 7:
                return "亡灵法师";
            case 8:
                return "森林种族";
            default:
                return "节点公会";
        }
    }
    public List<string> GetTargetStr(List<int> list)
    {
        List<string> strs = new List<string> { GetIntroStr(list[0]) };
        switch (list[0])
        {
            case 1:
                Event_template event_Template = Event_templateConfig.GetEventTemplate(list[1]);
                strs.Add(event_Template.eventName);
                break;
            case 2:
                Item_instance item_instance = Item_instanceConfig.GetItemInstance(list[1]);
                strs.Add(item_instance.itemName);
                break;
            case 3:
                WP_template WP_template = WP_templateConfig.GetWpTemplate(list[1]);
                strs.Add(WP_template.WPName);
                break;
            case 4:
                Fort_template fort_template = Fort_templateConfig.GetFort_template(list[1]);
                strs.Add(fort_template.fortName);
                break;
        }
        strs.Add(list[2].ToString());
        return strs;
    }
    public string GetRenowRewardStrs(int type, int sum)
    {
        return "+" + sum + " " + GetFactionStr(type);
    }

    public GameObject GetItem(ItemAttribute itemAttribute, Transform parent, GameObject itemIntroObj = null)
    {
        return GetItem(itemAttribute.GetItemData(), parent, itemIntroObj);
    }

    public GameObject GetItem(ItemData itemData, Transform parent, GameObject itemIntroObj = null)
    {
        try
        {
            GameObject obj = ResourceLoadUtil.InstantiateRes(itemIntroObj, parent);
            Image image = obj.transform.Find("Item").GetComponent<Image>();
            image.sprite = ResourceLoadUtil.LoadItemQuiltySprite(itemData.itemQuality);
            image = obj.transform.Find("Item/KuangBG").GetComponent<Image>();
            image.sprite = ResourceLoadUtil.LoadItemQuiltyFrameSprite(itemData.itemQuality);
            image = obj.transform.Find("Item/Icon").GetComponent<Image>();
            image.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemIcon, itemData.instanceID);
            Text text = obj.transform.Find("Item/Intro/Text").GetComponent<Text>();
            //
            string str = Item_instanceConfig.GetItemInstance(itemData.instanceID).itemName;
            if (itemData is EquipmentData)
            {
                if ((itemData as EquipmentData).addLevel > 0)
                {
                    str += " +" + (itemData as EquipmentData).addLevel;
                }
            }
            text.text = str;
            //
            return obj;
        }
        catch (Exception e)
        {
            LogHelper_MC.LogError(e.Message);
            return null;
        }

    }

    /// <summary>
    /// 获得声望奖励
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private RenownBoxReward GetRenownBoxReward(int type)
    {
        RenownBoxReward info = new RenownBoxReward()
        {
            factionType = type,
            itemAttributes = new List<ItemAttribute>(),
        };
        RR_template rr_template = RR_templateConfig.GetTemplate(FortSystem.Instance.NewZone.ZoneId * 10 + type);
        if (rr_template == null)
        {
            return info;
        }
        //物品奖励
        List<float> levels = new List<float>();
        Building_template bui = Building_templateConfig.GetBuildingTemplate((int)BuildingType.TownHall);
        for (int i = 0; i < rr_template.itemRewardSet.Count; i++)
        {
            levels.Add(BuildingAttribute.Building.finalRewardLevel(bui));
        }
        info.itemAttributes = ItemSystem.Instance.CreateItemreward(new ItemRewardInfo(levels, rr_template.itemRewardSet));
        //角色奖励
        if (RandomBuilder.RandomIndex_Chances(new List<int> { rr_template.charRewardChance }) == -1)
        {
            return info;
        }

        int charID = RandomBuilder.RandomValues(rr_template.charList, 1)[0];
        info.charAttribute = new CharAttribute(new CharCreate(charID));
        return info;
    }
    //
    private static BountySystem instance;
    //
    private bool isAcceptedRandomBounty;
    private int selectRandomBountyIndex = -1;
    private int maxRenownValue;
    private const string introSumStr1 = "<color=#82fafa>{0}/{1}</color>";
    private const string introSumStr2 = "{0}/{1}";
    private const int hordeSum = 8;
    private string parentPath;
    private const string BountyFilePath = "BountySystemData";
    //
    private List<int> mainFinisheds = new List<int>();
    private List<int> randomFinisheds = new List<int>();
    private List<int> remainsFinisheds = new List<int>();
    //
    private List<BountyAttribute> mainBountyAttributes = new List<BountyAttribute>();
    private List<BountyAttribute> usableRandomBounty = new List<BountyAttribute>();
    private List<BountyAttribute> remainsBountys = new List<BountyAttribute>();
    //
    private BountyAttribute randomBounty;
    private Dictionary<int, int> favorValue = new Dictionary<int, int>();
    private Dictionary<int, int> renownValue = new Dictionary<int, int>();
    private float lastRefreshTime;
    //
    private BountySystemData bountySystemData;
    //
    private readonly Bounty_bountySet bountyBountySet;
    private Bounty_template bountyTemplate;
}


public enum FactionType
{
    /// <summary>
    /// 主线
    /// </summary>
    Nul = 0,
    /// <summary>
    /// 节点
    /// </summary>
    JieDian = 1,
    /// <summary>
    /// 自治
    /// </summary>
    ZiZhi = 2,
    /// <summary>
    /// 盗贼
    /// </summary>
    DaoZei = 3,
    /// <summary>
    /// 骑士
    /// </summary>
    QinShi = 4,
    /// <summary>
    /// 流亡
    /// </summary>
    LiuWang = 5,
    /// <summary>
    /// 学院
    /// </summary>
    XueYuan = 6,
    /// <summary>
    /// 亡灵
    /// </summary>
    WangLing = 7,
    /// <summary>
    /// 森林
    /// </summary>
    SenLing = 8,
}

/// <summary>
/// 声望箱子奖励
/// </summary>
public class RenownBoxReward
{
    public int factionType;
    public int goldReward;
    public List<ItemAttribute> itemAttributes;
    public CharAttribute charAttribute;
}
