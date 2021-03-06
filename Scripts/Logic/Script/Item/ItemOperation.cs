﻿using GameEventDispose;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 物品系统
/// </summary>
public partial class ItemSystem
{
    /// <summary>
    /// 增加物品
    /// </summary>
    /// <param name="itemDatas"></param>
    public List<ItemAttribute> AddItem(List<ItemData> itemDatas)
    {
        if (itemDatas.Count == 0)
        {
            return null;
        }

        return itemDatas.Select(AddItem).ToList();
    }

    /// <summary>
    /// 增加物品
    /// </summary>
    /// <param name="itemDatas"></param>
    public List<ItemAttribute> AddItem(List<ItemAttribute> itemAtts)
    {
        if (itemAtts.Count == 0)
        {
            return null;
        }

        return itemAtts.Select(AddItem).ToList();
    }

    /// <summary>
    /// 增加物品
    /// </summary>
    public ItemAttribute AddItem(ItemData itemData)
    {
        ItemAttribute attribute;
        itemInstance = Item_instanceConfig.GetItemInstance(itemData.instanceID);
        //累加数量
        if (IsItemAccumulate(itemInstance.itemType))
        {
            foreach (KeyValuePair<int, ItemAttribute> item in itemAttributes)
            {
                if (item.Value.instanceID != itemData.instanceID)
                {
                    continue;
                }

                attribute = item.Value;
                item.Value.sum += itemData.sum;
                return attribute;
            }
        }
        //
        itemData.itemID = GetCerateItemId(itemData.itemID);
        //
        attribute = CreateItemAttribute(itemData);
        //
        if (!IsCanAddItem(attribute))
        {
            return attribute;
        }
        //
        itemAttributes.Add(itemData.itemID, attribute);
        CheckRemainsBounty(attribute);
        return attribute;
    }

    /// <summary>
    /// 增加物品
    /// </summary>
    public ItemAttribute AddItem(ItemAttribute itemAtt)
    {
        ItemAttribute attribute;
        itemInstance = Item_instanceConfig.GetItemInstance(itemAtt.instanceID);
        //累加数量
        if (IsItemAccumulate(itemInstance.itemType))
        {
            foreach (KeyValuePair<int, ItemAttribute> item in itemAttributes)
            {
                if (item.Value.instanceID != itemAtt.instanceID)
                {
                    continue;
                }

                attribute = item.Value;
                item.Value.sum += itemAtt.sum;
                return attribute;
            }
        }
        //
        itemAtt.itemID = GetCerateItemId(itemAtt.itemID);
        //
        attribute = itemAtt;
        if (!IsCanAddItem(attribute))
        {
            return attribute;
        }
        itemAttributes.Add(itemAtt.itemID, attribute);
        CheckRemainsBounty(attribute);
        return attribute;
    }

    /// <summary>
    /// 添加物品
    /// </summary>
    /// <param name="itemId">唯一Id</param>
    /// <param name="templateId">配置表Id</param>
    /// <param name="num">数量</param>
    public void AddItem(int itemId, int instanceID, int num, ItemCreateType createType = ItemCreateType.Drop, int itemLevel = 0)
    {
        if (!itemAttributes.ContainsKey(itemId))
        {
            CreateItem(instanceID, num, true, createType, itemLevel);
        }
        else
        {
            itemAttributes[itemId].sum += num;
            CheckRemainsBounty(itemAttributes[itemId]);
        }
    }



    /// <summary>
    /// 创建奖励
    /// </summary>
    public List<ItemAttribute> CreateItemreward(ItemRewardInfo itemRewardInfo, bool isSave = true)
    {
        Dictionary<int, ItemAttribute> _lists = new Dictionary<int, ItemAttribute>();
        //
        for (int i = 0; i < itemRewardInfo.rewardlist.Count; i++)
        {
            Item_rewardlist _itemRewardlist = Item_rewardlistConfig.GetItemRewardlist(itemRewardInfo.rewardlist[i]);
            if (_itemRewardlist == null)
            {
                continue;
            }
            //任务物品的筛选
            if (_itemRewardlist.bountyReq > 0 && !BountySystem.Instance.IsItemrewardBountyReq(_itemRewardlist.bountyReq))
            {
                continue;
            }

            bool isHaveItem = false;
            for (int j = 0; j < _itemRewardlist.itemList.Count; j++)
            {
                float rewardChance = 0;
                try
                {
                    rewardChance = _itemRewardlist.rewardCost[j] == 0 ? 1 : itemRewardInfo.finalRewardLevels[i] / _itemRewardlist.rewardCost[j];
                }
                catch (Exception ex)
                {
                    LogHelper_MC.LogError(ex.ToString());
                }
                int itemId = _itemRewardlist.itemList[j];

                //第一种情况
                if (rewardChance < 1)
                {
                    //随机能得到物品
                    if (RandomBuilder.RandomIndex_Chances(new List<float> { rewardChance, 1 - rewardChance }, 1) == 0)
                    {
                        ItemAttribute itemAttribute = CreateItem(itemId, isSave, ItemCreateType.Drop, itemRewardInfo.itemLevel);
                        while (_lists.ContainsKey(itemAttribute.itemID))
                        {
                            itemAttribute.itemID++;
                        }

                        _lists.Add(itemAttribute.itemID, itemAttribute);
                        isHaveItem = true;
                        break;
                    }
                    continue;
                }
                //第二种情况
                if (rewardChance == 1)
                {
                    ItemAttribute itemAttribute = CreateItem(itemId, isSave, ItemCreateType.Drop, itemRewardInfo.itemLevel);
                    while (_lists.ContainsKey(itemAttribute.itemID))
                    {
                        itemAttribute.itemID++;
                    }

                    _lists.Add(itemAttribute.itemID, itemAttribute);
                    isHaveItem = true;
                    break;
                }
                //第三种情况 rewardChance>1
                //第2次的概率
                float _tempChance = rewardChance - (int)rewardChance;
                bool isAdd = RandomBuilder.RandomIndex_Chances(new List<float> { _tempChance, 1 - _tempChance }, 1) == 0;
                int sum = (int)rewardChance;
                if (isAdd)
                {
                    sum++;
                }

                List<ItemAttribute> _tempItems = CreateItem(itemId, sum, isSave, ItemCreateType.Drop, itemRewardInfo.itemLevel);
                //
                for (int k = 0; k < _tempItems.Count; k++)
                {
                    while (_lists.ContainsKey(_tempItems[k].itemID))
                    {
                        _tempItems[k].itemID++;
                    }
                }
                //
                foreach (ItemAttribute _item in _tempItems)
                {
                    _lists.Add(_item.itemID, _item);
                }
                isHaveItem = true;
                break;
            }
            //已经选出了物品
            if (isHaveItem)
            {
                break;
            }
        }
        //
        return _lists.Select(item => item.Value).ToList();
    }


    /// <summary>
    /// 创建物品_单个物品
    /// </summary>
    /// <param name="instanceId">实例化Id</param>
    /// <param name="_sum">数量</param>
    /// <param name="isSave">是否保存</param>
    /// <returns></returns>
    public ItemAttribute CreateItem(int instanceId, bool isSave = false, ItemCreateType create = ItemCreateType.Drop, int itemLevel = 0, int itemMaxLevel = 0, float minUpgrade = 0, float maxUpgrade = 0, EquimentCreate equimentCreate = null)
    {
        itemInstance = Item_instanceConfig.GetItemInstance(instanceId);
        //
        if (itemInstance == null)
        {
            return null;
        }

        int _id = itemAttributes.Count;

        int _itemid = GetCerateItemId(_id, isSave);
        //开始创建物品
        ItemCreate itemCreate = GetItemCreateInfo(instanceId, _itemid, create, itemLevel, itemMaxLevel, minUpgrade, maxUpgrade, equimentCreate);
        ItemAttribute _attribute = CreateItemAttribute(itemCreate);
        //
        return !isSave ? _attribute : AddItem(_attribute.GetItemData());
        //
    }
    /// <summary>
    /// 创建物品_单个物品
    /// </summary>
    /// <param name="equimentCreate"></param>
    /// <param name="isSave"></param>
    /// <returns></returns>
    public ItemAttribute CreateItem(EquimentCreate equimentCreate = null, bool isSave = false)
    {
        if (equimentCreate == null)
        {
            return null;
        }

        int _id = itemAttributes.Count;
        int _itemid = GetCerateItemId(_id, isSave);
        //开始创建物品
        ItemAttribute equipAttribute = CreateItemAttribute(equimentCreate);
        //
        return !isSave ? equipAttribute : AddItem(equipAttribute);
    }

    ///// <summary>
    ///// 创建物品_单个物品
    ///// </summary>
    ///// <param name="instanceId">实例化Id</param>
    ///// <param name="_sum">数量</param>
    ///// <param name="isSave">是否保存</param>
    ///// <returns></returns>
    //public ItemAttribute CreateItem(int instanceId, bool isSave = false, int itemLevel = 0, int itemMaxLevel = 0, ItemCreateType create = ItemCreateType.Drop, EquimentCreate equimentCreate = null)
    //{
    //    float minUpgrade = 0;
    //    float maxUpgrade = 0;
    //    itemInstance = Item_instanceConfig.GetItemInstance(instanceId);
    //    //
    //    if (itemInstance == null)
    //    {
    //        return null;
    //    }

    //    int _id = itemAttributes.Count;

    //    int _itemid = GetCerateItemId(_id, isSave);
    //    //开始创建物品
    //    ItemCreate itemCreate = GetItemCreateInfo(instanceId, _itemid, create, itemLevel, itemMaxLevel, minUpgrade, maxUpgrade, equimentCreate);
    //    ItemAttribute _attribute = CreateItemAttribute(itemCreate);
    //    //
    //    return !isSave ? _attribute : AddItem(_attribute.GetItemData());
    //    //
    //}
    /// <summary>
    /// 创建物品——物品列表
    /// </summary>
    /// <param name="instanceId">实例化Id</param>
    /// <param name="sum">数量</param>
    /// <param name="isSave">是否保存</param>
    /// <returns></returns>
    public List<ItemAttribute> CreateItem(int instanceId, int sum, bool isSave, ItemCreateType createType = ItemCreateType.Drop, int itemLevel = 1, int itemMaxLevel = 0, float minUpgrade = 0, float maxUpgrade = 0, EquimentCreate equimentCreate = null)
    {
        itemInstance = Item_instanceConfig.GetItemInstance(instanceId);
        //
        if (itemInstance == null)
        {
            LogHelper_MC.LogError("itemInstance is null id is : " + instanceId);
            return null;
        }
        //开始创建物品
        List<ItemAttribute> _list = new List<ItemAttribute>();
        for (int i = 0; i < sum; i++)
        {
            _list.Add(CreateItem(instanceId, false, createType, itemLevel, itemMaxLevel, minUpgrade, maxUpgrade, equimentCreate));
        }
        //累加数量
        if (IsItemAccumulate(itemInstance.itemType))
        {
            _list[0].sum = _list.Count;
            _list = new List<ItemAttribute> { _list[0] };
        }
        //保存物品
        if (isSave)
        {
            AddItem(_list);
        }

        return _list;
    }
    /// <summary>
    /// 移除物品
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="sum"></param>
    public void RemoveItem(int itemId, int sum)
    {
        if (!itemAttributes.ContainsKey(itemId))
        {
            return;
        }

        itemAttributes[itemId].sum -= sum;
        itemAttributes[itemId].sum = Math.Max(0, itemAttributes[itemId].sum);
        if (itemAttributes[itemId].sum == 0)
        {
            itemAttributes.Remove(itemId);
            return;
        }
        CheckRemainsBounty(itemAttributes[itemId]);
    }

    /// <summary>
    /// 得到物品奖励表
    /// </summary>
    public List<ItemData> Itemrewards_ItemDate(ItemRewardInfo itemRewardInfo, bool _isSave = true)
    {
        List<ItemAttribute> _attributes = CreateItemreward(itemRewardInfo, _isSave);
        if (_attributes == null || _attributes.Count == 0)
        {
            return null;
        }

        return _attributes.Select(item => item.GetItemData()).ToList();
    }

    /// <summary>
    /// 得到物品奖励表
    /// </summary>
    public List<ItemAttribute> Itemrewards_Attribute(ItemRewardInfo itemRewardInfo, bool _isSave = true)
    {
        List<ItemAttribute> _attributes = CreateItemreward(itemRewardInfo, _isSave);
        if (_attributes == null || _attributes.Count == 0)
        {
            return null;
        }

        return _attributes;
    }

    /// <summary>
    /// 获得物品创建信息
    /// </summary>
    private ItemCreate GetItemCreateInfo(int instanceId, int itemId, ItemCreateType create = ItemCreateType.Drop, int itemLevel = 0, int itemMaxLevel = 0, float minUpgrade = 0, float maxUpgrade = 0, EquimentCreate equimentCreate = null)
    {
        ItemCreate itemCreate;
        itemInstance = Item_instanceConfig.GetItemInstance(instanceId);
        //
        if (itemInstance == null)
        {
            return null;
        }

        if (GameTools.IsEquip(itemInstance.itemType))
        {
            if (equimentCreate != null)
            {
                equimentCreate.itemID = itemId;
                equimentCreate.itemType = itemInstance.itemType;
            }
            itemCreate = equimentCreate == null
                ? new EquimentCreate(instanceId, itemId, create, itemLevel, itemMaxLevel, minUpgrade, maxUpgrade)
                : equimentCreate;
        }
        else
        {
            itemCreate = new ItemCreate(instanceId, itemId, create, itemLevel, itemMaxLevel, minUpgrade, maxUpgrade);
        }
        return itemCreate;
    }

    /// <summary>
    /// 检查残骸任务
    /// </summary>
    /// <param name="instanceID"></param>
    private static void CheckRemainsBounty(ItemAttribute attribute)
    {
        if (attribute.itemType != (int)ItemType.LingHai)
        {
            return;
        }

        BountySystem.Instance.AddRemainsBounty(attribute.instanceID);
    }

    /// <summary>
    /// 检查物品类型
    /// </summary>
    /// <param name="itemAttribute"></param>
    /// <returns></returns>
    private static bool IsCanAddItem(ItemAttribute itemAttribute)
    {
        if (itemAttribute.ItemType == ItemType.MoJing)
        {
            EventDispatcher.Instance.SystemEvent.DispatchEvent(EventId.SystemEvent, GameSystemEventType.MoJing, itemAttribute.instanceID);
            return false;
        }
        return true;
    }


    /// <summary>
    /// 物品数量是否能累加
    /// </summary>
    /// <param name="itemType"></param>
    /// <returns></returns>
    public bool IsItemAccumulate(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.ZhuCai:
            case ItemType.XiSu:
            case ItemType.LingHai:
            case ItemType.ShengWu:
            case ItemType.Task:
                return true;
            default:
                return false;
        }
    }
    /// <summary>
    /// 物品数量是否能累加
    /// </summary>
    /// <param name="itemType"></param>
    /// <returns></returns>
    public bool IsItemAccumulate(int itemType)
    {
        return IsItemAccumulate((ItemType)itemType);
    }
}

/// <summary>
/// 物品奖励
/// </summary>
public class ItemRewardInfo
{
    public List<int> rewardlist;
    /// <summary>
    /// 最终奖励等级
    /// </summary>
    public List<float> finalRewardLevels;
    /// <summary>
    /// 物品等级
    /// </summary>
    public int itemLevel;

    public ItemRewardInfo(List<float> finalRewardLevels, List<int> rewardlist, int itemLevel = 1)
    {
        this.finalRewardLevels = finalRewardLevels;
        this.rewardlist = rewardlist;
        this.itemLevel = itemLevel;
    }
}