using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class UIExploreEventBase
{

    /// <summary>
    ///  更新物品位置
    /// </summary>
    protected void UpdateItemPos()
    {
        if (itemLaunchPosition == null || !itemLaunchPosition.IsUpdateItemPos) return;
        for (int i = 0; i < selectindexs.Count; i++)
        {
            if (itemOk.Contains(i)) continue;
            if (items[i] == null) continue;
            items[i].gameObject.SetActive(true);
            items[i].position = itemLaunchPosition.PosList[i];
            if (itemLaunchPosition.IsOk[i])
            {
                var temp = items[i].Find("Effect");
                if (temp == null) continue;
                temp.gameObject.SetActive(true);
                itemOk.Add(i);
            }
        }
    }

    private List<int> itemOk = new List<int>();

    /// <summary>
    ///  更新物品位置
    /// </summary>
    protected void UpdateItemPosEnd()
    {
        //for (int i = 0; i < selectindexs.Count; i++)
        //{
        //    if (items[i] == null) continue;
        //    var temp = items[i].Find("Effect");
        //    if (temp == null) continue;
        //    temp.gameObject.SetActive(true);
        //}
        Destroy(gameObject);
    }

    /// <summary>
    /// 加载物品奖励
    /// </summary>
    protected void LoadItemReward(WPVisitEventResult _result)
    {
        //先看是否有物品 得到物品名字列表

        if (_result != null && _result.itemRewards != null && (_result.itemRewards.Count > 0 || _result.healingGlobSum > 0))
        {
            items = exploreItemMove.LoadItemReward(_result, OnClickItem, OnClickHealingGlob);
            foreach (Transform item in items)
            {
                if (item == null) continue;
                item.gameObject.SetActive(false);
            }
            selectindexs = RandomBuilder.RandomList(items.Count, new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
        }
    }

    /// <summary>
    /// 点击物品
    /// </summary>
    private void OnClickItem(int _index)
    {
        ExploreSystem.Instance.AddItem(wpVisitEventResult.itemRewards[_index]);
        if (OnCallBackItem != null)
        {
            OnCallBackItem(items[_index].position);

            DestroyImmediate(items[_index].gameObject);

            // ItemFactory.Instance.Release(items[_index].gameObject);
        }
    }
    /// <summary>
    /// 点击生命球
    /// </summary>
    private void OnClickHealingGlob(int _index)
    {
        if (combatConfig == null)
        {
            combatConfig = Combat_configConfig.GetCombat_config();
        }
        TeamSystem.Instance.UseGlobHealing(combatConfig.globHealing);
        //
        if (OnCallBackHealingGlob != null) OnCallBackHealingGlob(items[_index].position);
        DestroyImmediate(items[_index].gameObject);
    }

    private void Update()
    {
        UpdateItemPos();
        if (itemLaunchPosition == null) return;
        if (itemLaunchPosition.IsPlayEnd) UpdateItemPosEnd();
    }


}
