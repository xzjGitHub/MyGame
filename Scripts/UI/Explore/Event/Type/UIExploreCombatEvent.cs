using System.Collections.Generic;
using GameEventDispose;
using Spine.Unity;
using UnityEngine;

public class UIExploreCombatEvent : UIExploreEventBase
{
    public delegate void CallBack(object pos);

    public CallBack OnCallBackCombatReady;
    public CallBack OnCallBackCombatEnd;

    private void Awake()
    {
        OnOpenUI = OpenUI;
    }



    /// <summary>
    /// 打开UI
    /// </summary>
    private void OpenUI(object param)
    {
        //更新大小
        charList = ShowTranS.Find("Char");
        //
        OnFade = OnCallFad;
        OnOpenShow = OnCallOpenShow;
        OnProgressAchieve = OnCallProgressAchieve;
        //
        base.BaseInit(param as EventAttribute);
        //
        combatItemRewards = GameModules.popupSystem.GetPopupObj(ModuleName.combatItemRewards).GetComponent<UICombatItemRewards>();
        //
        if (eventAttribute.MobCombats != null)
        {
            int _index = -1;
            foreach (CombatUnit item in eventAttribute.MobCombats)
            {
                _index++;
                if (_index > 0)
                {
                    break;
                }

                Transform parent = charList.GetChild(_index);
                CharRPack charShow = CharRPackConfig.GeCharShowTemplate(item.charAttribute.templateID);
                parent.localScale = Vector3.one * sizeValue;
                LoadSkeletonRes(charShow.charRP, parent, 21);
                parent.gameObject.SetActive(true);
            }
        }
        //  FadeFadeIn(iconSkeleton, false);
        charList.gameObject.SetActive(true);
        //   ShowTranS.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    private void OnCallProgressAchieve(object param)
    {
        if (param != null && (param as WPVisitEventResult).isVisitEnd)
        {
            VisitSucceed();
            return;
        }

        CreateCombat();
        if (OnCallBackCombatReady != null)
        {
            OnCallBackCombatReady(transform.position);
        }

        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatEventType.ReadyCombat, (object)null);
        StartCombat();
    }

    /// <summary>
    /// 加载资源
    /// </summary>
    private SkeletonAnimation LoadSkeletonRes(string _RP_Name, Transform _transform, int _sortingOrder)
    {
        SkeletonAnimation _obj = ResourceLoadUtil.LoadCharModel(_RP_Name, _transform).GetComponent<SkeletonAnimation>();
        if (_obj != null)
        {
            SkeletonTool.SetSkeletonLayer(_obj, _sortingOrder);
        }
        if (_obj != null)
        {
            SkeletonTool.PlayCharAnimation(_obj, CharModuleAction.Idle, null);
        }
        //  _obj.transform.localPosition = Vector3.left * 0.61f;
        return _obj;
    }


    private void OnCallFad(object param)
    {
        FadeFadeIn(iconSkeleton);
        charList.gameObject.SetActive(false);
    }
    private void OnCallOpenShow(object param)
    {
        Scale();
        FadeFadeIn(iconSkeleton, false, false);
        charList.gameObject.SetActive(true);
        OnFadeOk = OnCallFadeOk;
    }
    private void OnCallFadeOk(object param)
    {
        //  iconSkeleton.gameObject.AddComponent<cakeslice.Outline>();
    }


    /// <summary>
    /// 开始创建战斗
    /// </summary>
    private void CreateCombat(bool isNew = true)
    {
        combatSystem = new CombatSystem(eventAttribute.MobTeamId);
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
        List<CombatUnit> _combatUnits1 = eventAttribute.MobCombats;
        CombatTeamInfo enemyInfo = new CombatTeamInfo(1, TeamType.Enemy, _combatUnits1);
        //
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatStage.CreateCombat, (object)new CombatTeamInfo[] { playerInfo, enemyInfo });
        //   EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, PlayCombatStage.InitRight, isNew, (object)enemyInfo);
    }
    /// <summary>
    /// 开始战斗
    /// </summary>
    private void StartCombat()
    {
        wpVisitEventResult = ExploreSystem.Instance.VisitEvent(eventAttribute.waypointId, eventIndex, WPEventVisitType.Advanced);
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatEventType.ReadyCombatOk, (object)combatSystem);
        //
        EventDispatcher.Instance.CombatEvent.AddEventListener<CombatEventType, object>(EventId.CombatEvent, OnCombatEvent);
        // exploreEventPopup.OpenUI(eventAttribute);
        gameObject.SetActive(false);
    }

    private void OnCombatEvent(CombatEventType arg1, object arg2)
    {
        switch (arg1)
        {
            case CombatEventType.CombatWin:
                VisitSucceed();
                break;
            case CombatEventType.CombatFail:
                VisitFail();
                break;
        }
    }


    private void VisitSucceed()
    {
        //先看是否有物品 得到物品名字列表
        LoadItemReward(wpVisitEventResult);
        charList.gameObject.SetActive(false);
        //    iconSkeleton.gameObject.SetActive(false);
        Text.gameObject.SetActive(false);
        if (OnCallBackCombatEnd != null)
        {
            OnCallBackCombatEnd(null);
        }
        //  combatItemRewards.OpenUI(wpVisitEventResult);
        gameObject.SetActive(true);
        //发射物品
        itemLaunchPosition.PlayLaunchItem();
        base.VisitEventEnd();
    }

    private void VisitFail()
    {
        CreateCombat(false);
        if (OnCallBackCombatEnd != null)
        {
            OnCallBackCombatEnd(null);
        }

        gameObject.SetActive(true);
        //
        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.EventShow, (object)null);
        //
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatEventType.ReadyCombat, (object)null);
        StartCombat();
    }

    // 当 MonoBehaviour 将被销毁时调用此函数
    private void OnDestroy()
    {
        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.EventQuit, (object)null);
        EventDispatcher.Instance.CombatEvent.RemoveEventListener<CombatEventType, object>(EventId.CombatEvent, OnCombatEvent);
    }




    //
    private readonly CharRPack _charRPack;
    private Transform charList;
    //
    private UICombatItemRewards combatItemRewards;
    //
    private CombatSystem combatSystem;
}
