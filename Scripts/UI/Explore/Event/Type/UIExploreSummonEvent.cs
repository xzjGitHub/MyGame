using GameEventDispose;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 召唤事件
/// </summary>
public class UIExploreSummonEvent : UIExploreEventBase
{
    public CallBack OnCallBackSummonReady;


    private void Awake()
    {
        OnOpenUI = OpenUI;
    }

    /// <summary>
    /// 打开UI
    /// </summary>
    private void OpenUI(object param)
    {
        Init(param as EventAttribute);
        //
        iconSkeleton.AnimationState.SetAnimation(0, bxName1Str, true);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init(EventAttribute _eventAttribute)
    {
        if (isFirst)
        {
            return;
        }
        //
        base.BaseInit(_eventAttribute);
        //添加脚本
        //
        //  button.onClick.AddListener(OnClickButton);
        //
        OnProgressAchieve = OnEventProgressAchieve;
        OnFade = OnCallFad;

        //
        isFirst = true;
    }

    private void OnCallFad(object param)
    {
        base.FadeFadeIn(iconSkeleton);
    }
    /// <summary>
    /// 创建战斗
    /// </summary>
    private void CreateCombat(EventAttribute _eventAttribute)
    {
        combatSystem = new CombatSystem(_eventAttribute.MobTeamId);
        //创建玩家
        List<CombatUnit> _combatUnits = new List<CombatUnit>();
        int _index = 0;
        foreach (CombatUnit item in TeamSystem.Instance.TeamAttribute.combatUnits)
        {
            _combatUnits.Add(new CombatUnit(item));
            _index++;
        }
        CombatTeamInfo playerInfo = new CombatTeamInfo(0, TeamType.Player, _combatUnits);
        //创建Npc
        CombatTeamInfo enemyInfo = new CombatTeamInfo(1, TeamType.Enemy, _eventAttribute.MobCombats);
        //
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatStage.CreateCombat, (object)new CombatTeamInfo[] { playerInfo, enemyInfo });
        //
        StartCombat();
    }
    /// <summary>
    /// 开始战斗
    /// </summary>
    private void StartCombat()
    {
        if (OnCallBackSummonReady != null)
        {
            OnCallBackSummonReady(null);
        }
        //
        wpVisitEventResult = ExploreSystem.Instance.VisitEvent(eventAttribute.waypointId, eventIndex, WPEventVisitType.Advanced);
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatEventType.ReadyCombatOk, (object)combatSystem);
        //
        EventDispatcher.Instance.CombatEvent.AddEventListener<CombatEventType, object>(EventId.CombatEvent, OnCombatEvent);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isFirst)
        {
            return;
        }
    }


    /// <summary>
    /// 战斗事件
    /// </summary>
    private void OnCombatEvent(CombatEventType arg1, object arg2)
    {
        switch (arg1)
        {
            case CombatEventType.CombatWin:
                gameObject.SetActive(true);
                iconSkeleton.gameObject.SetActive(true);
                //先看是否有物品 得到物品名字列表
                LoadItemReward(ExploreSystem.Instance.VisitEvent(eventAttribute.waypointId, eventIndex, WPEventVisitType.Advanced));
                //打开宝箱
                particleSystemAlpha.StartUpdate();
                iconSkeleton.AnimationState.SetAnimation(0, bxName2Str, false);
                //combatItemRewards.OnCallBackHealingGlob = OnClickHealingGlob;
                //combatItemRewards.OnCallBackItem = OnClickItem;
                //combatItemRewards.OpenUI(wpVisitEventResult);
                //Destroy(gameObject);
                break;
            case CombatEventType.CombatFail:
                iconSkeleton.AnimationState.SetAnimation(0, bxName3Str, false);
                Destroy(gameObject);
                break;
        }
    }

    private void OnClickItem(object pos)
    {
        if (OnCallBackItem != null)
        {
            OnCallBackItem(pos);
        }
    }

    private void OnClickHealingGlob(object pos)
    {
        if (OnCallBackHealingGlob != null)
        {
            OnCallBackHealingGlob(pos);
        }
    }

    /// <summary>
    /// 事件进度完成
    /// </summary>
    private void OnEventProgressAchieve(object param)
    {
        particleSystemAlpha.StartUpdate();
        iconSkeleton.gameObject.SetActive(false);
        //
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatEventType.ReadyCombat, (object)null);
        //打开怪物
        CreateCombat(eventAttribute);

    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.CombatEvent.RemoveEventListener<CombatEventType, object>(EventId.CombatEvent, OnCombatEvent);
    }


    //
    private bool isFirst;
    //
    private readonly bool isShow;
    private UICombatItemRewards combatItemRewards;
    //
    private CombatSystem combatSystem;
}
