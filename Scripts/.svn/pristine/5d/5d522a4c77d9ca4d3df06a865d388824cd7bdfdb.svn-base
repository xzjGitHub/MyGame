using GameEventDispose;
using UnityEngine;

public partial class UIExplore : MonoBehaviour
{

    /// <summary>
    /// 悬赏事件
    /// </summary>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    private void OnBountyEvent(BountyEventType arg1, object arg2)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        switch (arg1)
        {
            case BountyEventType.TargetUpdate:
                break;
            case BountyEventType.AcceptBounty:
                break;
            case BountyEventType.RandomUpdate:
                break;
            case BountyEventType.FinishBounty:
                if (arg2 == null)
                {
                    return;
                }
                dialogPopup.callAcceptBounty = OnCallAcceptBounty;
                dialogPopup.OpenUI((arg2 as BountyAttribute).BountyTemplate.triggerDialog);
                break;
            case BountyEventType.MainUpdate:
                break;
        }
    }
    //探索事件
    private void OnExploreEvent(ExploreEventType arg1, object arg2)
    {
        switch (arg1)
        {
            case ExploreEventType.WPStartReady:
                wayponitStart.PlayComlete = OnCallWayponitStartPlayComlete;
                wayponitStart.PlayShow();
                break;
            case ExploreEventType.SceneStartReady:
                isLastEvent = false;
                StartSceneLoad();
                doorEffect.gameObject.SetActive(true);
                break;
            case ExploreEventType.MoveSceneMim:
                doorEffect.gameObject.SetActive(false);
                break;
            case ExploreEventType.SceneMoveEnd:
                CloseMask();
                //   isCheckEventPos = true;
                break;
            case ExploreEventType.WPEnd:
                WPVisitOk();
                break;
            case ExploreEventType.ExploreFinish:
                mapExploreFinish.Show();
                break;
            case ExploreEventType.WPStart:
                break;
            case ExploreEventType.VisitEvent:
                if ((arg2 as EventAttribute).EventType == WPEventType.Trap)
                {
                    CloseMask();
                    break;
                }
                OpenMask();
                break;
            case ExploreEventType.VisitEventEnd:
                CloseMask();
                EventAttribute eventAtt = arg2 as EventAttribute;
                UpdateThreatIntroStr(eventAtt.eventId);
                //是否是场景中最后一个事件
                if (ExploreSystem.Instance.IsSceneEnd(eventAtt.EventIndex))
                {
                    isLastEvent = true;
                    go.SetActive(true);
                    return;
                }
                isCheckEventPos = true;
                break;
            case ExploreEventType.EventShow:
                exploreChar.OpenShow();
                go.SetActive(false);
                break;
            case ExploreEventType.EventQuit:
                exploreChar.OpenShow();
                break;
            case ExploreEventType.ResetSceneEnd:
                isCheckEventPos = false;
                CloseMask();
                break;
            case ExploreEventType.CombatEnd:
                OpenMask();
                break;
            case ExploreEventType.EventVisitFail:
                EventVisitFail(arg2 as string);
                break;
        }
    }

    //战斗事件
    private void OnCombatEvent(CombatEventType arg1, object arg2)
    {
        switch (arg1)
        {
            case CombatEventType.StartCombat:
                OnStartCombat(arg2);
                break;
            case CombatEventType.ReadyCombat:
                OnReadyCombat(arg2);
                break;
            case CombatEventType.ReadyCombatOk:
                OnReadyCombatOK(arg2);
                break;
            case CombatEventType.CombatResult:
                OnCombatEnd(arg2);
                break;
        }
    }

    /// <summary>
    /// 剧本时间更新事件
    /// </summary>
    private void OnScriptTimeUpdateEven(ScriptTimeUpdateType arg1, object arg2)
    {
        if (arg1 != ScriptTimeUpdateType.Day)
        {
            return;
        }

        UpdateScriptTimeShow();
    }
    /// <summary>
    /// 添加事件
    /// </summary>
    private void AddEventListener()
    {
        EventDispatcher.Instance.BountyEvent.AddEventListener<BountyEventType, object>(EventId.BountyEvent, OnBountyEvent);
        EventDispatcher.Instance.CombatEvent.AddEventListener<CombatEventType, object>(EventId.CombatEvent, OnCombatEvent);
        EventDispatcher.Instance.ExploreEvent.AddEventListener<ExploreEventType, object>(EventId.ExploreEvent, OnExploreEvent);
        EventDispatcher.Instance.ScriptTimeEvent.AddEventListener<ScriptTimeUpdateType, object>(EventId.ScriptTimeEvent, OnScriptTimeUpdateEven);
    }
    /// <summary>
    /// 移除事件
    /// </summary>
    private void RemoveEventListener()
    {
        EventDispatcher.Instance.BountyEvent.RemoveEventListener<BountyEventType, object>(EventId.BountyEvent, OnBountyEvent);
        EventDispatcher.Instance.CombatEvent.RemoveEventListener<CombatEventType, object>(EventId.CombatEvent, OnCombatEvent);
        EventDispatcher.Instance.ExploreEvent.RemoveEventListener<ExploreEventType, object>(EventId.ExploreEvent, OnExploreEvent);
        EventDispatcher.Instance.ScriptTimeEvent.RemoveEventListener<ScriptTimeUpdateType, object>(EventId.ScriptTimeEvent, OnScriptTimeUpdateEven);
    }
}
