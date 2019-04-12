using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICombatItemRewards : MonoBehaviour
{
    public delegate void CallBack(object pos);

    public CallBack OnCallBackItem;
    public CallBack OnCallBackHealingGlob;
    //
    private const string str1 = "effect_EventEffect05";
    //
    private GameObject explodeGameObject;
    private SkeletonAnimation explodeSkeletonAnimation;
    //
    private bool isFirst;
    private UIItemLaunchPosition itemLaunchPosition;
    //
    private Combat_config combatConfig;
    //
    private List<int> selectindexs = new List<int>();
    private List<Transform> items = new List<Transform>();
    //
    private UIExploreItemMove exploreItemMove;


    /// <summary>
    /// 打开UI
    /// </summary>
    public void OpenUI(WPVisitEventResult _wpVisitEventResult)
    {
        Init();
        //
        explodeGameObject.SetActive(false);
        itemLaunchPosition.Reset();
        //
        LoadRewards(_wpVisitEventResult);
        //
        gameObject.SetActive(true);
        //
        explodeGameObject.SetActive(true);
        explodeSkeletonAnimation.AnimationState.SetAnimation(0, str1, false);
        itemLaunchPosition.PlayLaunchItem();
    }

    /// <summary>
    ///初始化
    /// </summary>
    private void Init()
    {
        if (isFirst) return;
        //
        //  itemMoveTransform = GameObject.Find("Root/Explore/ExploreUI/ItemMove").transform;
        //
        explodeGameObject = transform.Find("effect_EventEffect05").gameObject;
        explodeSkeletonAnimation = explodeGameObject.transform.Find("effect_EventEffect05").GetComponent<SkeletonAnimation>();
        //
        itemLaunchPosition = transform.Find("ItemLaunch").GetComponent<UIItemLaunchPosition>();
        exploreItemMove = GameModules.popupSystem.GetPopupObj(ModuleName.itemMovePopup).GetComponent<UIExploreItemMove>();
        //
        isFirst = true;
    }


    /// <summary>
    /// 访问成功
    /// </summary>
    private void LoadRewards(WPVisitEventResult _resul)
    {
        //先看是否有物品 得到物品名字列表
        if (_resul.itemRewards.Count > 0 || _resul.healingGlobSum > 0)
        {
            items = exploreItemMove.LoadItemReward(_resul, OnClickItem, OnClickHealingGlob);
            selectindexs = RandomBuilder.RandomList(items.Count, new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
        }
    }

    private void LateUpdate()
    {
        UpdateItemPos();
        if (itemLaunchPosition.IsPlayEnd)
        {
            gameObject.SetActive(false);
            itemLaunchPosition.Reset();
        }
    }


    /// <summary>
    /// 点击物品
    /// </summary>
    private void OnClickItem(int _index)
    {
        if (OnCallBackItem != null)
        {
            OnCallBackItem(items[_index].position);
            //ItemFactory.Instance.Release(items[_index].gameObject);
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

    /// <summary>
    /// 更新物品位置
    /// </summary>
    private void UpdateItemPos()
    {
        if (itemLaunchPosition == null || !itemLaunchPosition.IsUpdateItemPos) return;

        for (int i = 0; i < selectindexs.Count; i++)
        {
            if (items[i] == null) continue;
            items[i].gameObject.SetActive(true);
            items[i].position = itemLaunchPosition.PosList[i];

        }
    }

}
