using GameEventDispose;
using UnityEngine;

public partial class UIExplore : MonoBehaviour
{

    #region 战斗状态
    /// <summary>
    /// 准备战斗
    /// </summary>
    private void OnReadyCombat(object _param)
    {
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatUIOperationType.PlayLeftAction, (object)CharModuleAction.Idle);
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatEventType.ReadyCombatOk, _param);
    }
    /// <summary>
    /// 准备战斗
    /// </summary>
    private void OnReadyCombatOK(object _param)
    {
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatEventType.StartCombat, _param);
    }
    /// <summary>
    /// 开始战斗
    /// </summary>
    private void OnStartCombat(object _param)
    {
        CloseExploreShow();
        if (OnClickCombat != null)
        {
            OnClickCombat();
        }
    }
    /// <summary>
    /// 战斗结束
    /// </summary>
    private void OnCombatEnd(object param)
    {
        OpenExploreShow();
    }
    #endregion

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
                _dialogPopup.OnEnd = OnCloseDialogFinishBounty;
                _dialogPopup.OpenUI((arg2 as BountyAttribute).BountyTemplate.triggerDialog);

                OpenMask();
                break;
            case BountyEventType.MainUpdate:
                break;
        }
    }

    private void OnCloseDialogFinishBounty(object param)
    {
        CloseMask();
    }

    //探索事件
    private void OnExploreEvent(ExploreEventType arg1, object arg2)
    {
        switch (arg1)
        {
            case ExploreEventType.WPStartReady:
                OpenMask();
                _wayponitStart.PlayComlete = OnCallWayponitStartPlayComlete;
                _wayponitStart.PlayShow();
                StartSceneLoad();
                break;
            case ExploreEventType.SceneStartReady:
                //场景准备
                _isClickButton = true;
                LogHelper_MC.LogError("场景准备");
                if (_IESceneReady != null)
                {
                    _IESceneReady.Stop();
                }
                _IESceneReady = new CoroutineUtil(ISceneReady(2, 1));
                // _doorEffect.gameObject.SetActive(true);
                break;
            case ExploreEventType.MoveSceneMim:
                UpdateDoorShow(false);
                break;
            case ExploreEventType.SceneMoveEnd:
                _isClickButton = false;
                CloseMask();
                //   _isCheckEventPos = true;
                break;
            case ExploreEventType.WPEnd:
                WPVisitOk();
                break;
            case ExploreEventType.NeedSelectRoom:
                CloseMask();
                _bigMapPopup.OpenUI(false);
                break;
            case ExploreEventType.ExploreFinish:
                _mapExploreFinish.Show();
                break;
            case ExploreEventType.WPStart:
                break;
            case ExploreEventType.VisitEvent:
                _isClickButton = true;
                if ((arg2 as EventAttribute).EventType == WPEventType.Trap)
                {
                    CloseMask();
                    break;
                }
                OpenMask();
                break;
            case ExploreEventType.VisitEventEnd:
                _isClickButton = false;
                CloseMask();
                _isCheckEventPos = true;
                break;
            case ExploreEventType.EventShow:
                _exploreChar.UpdateShow();
                UpdateGoButtonShow(false);
                break;
            case ExploreEventType.EventQuit:
                _isClickButton = false;
                CloseMask();
                _exploreChar.UpdateShow();
                break;
            case ExploreEventType.ResetSceneEnd:
                _isCheckEventPos = false;
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