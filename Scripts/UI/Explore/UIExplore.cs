using GameEventDispose;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// 探索界面
/// </summary>
public class UIExplore : MonoBehaviour/*, IPointerDownHandler, IPointerUpHandler*/
{
    public delegate void CallBack();

    public CallBack OnBack;
    public CallBack OnStart;
    public CallBack OnClickCombat;

    //
    public float sizeValue = 36;
    public float aspdValue;
    public float xValue;
    public float x1Value;
    //

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        GetObj();
        //  maskObj.enabled = false;
        //
        OnClickStartTiming();
        go.SetActive(false);
    }
    /// <summary>
    /// 点击开始
    /// </summary>
    public void OnClickStart()
    {
        exploreEvent.xValue = xValue;
        exploreEvent.x1Value = x1Value;
        exploreEvent.sizeValue = sizeValue;
        //
        mapNameText.text = ExploreSystem.Instance.MapTemplate.mapName;
        ExploreSystem.Instance.PlayExplore(true);
        //
        exploreChar.Init(TeamSystem.Instance.TeamAttribute.combatUnits[0], sizeValue);
        //
        UpdateScriptTimeShow();
        //
        resurrectProp = Combat_configConfig.GetCombat_config().resurrectProp;
        //
        EventDispatcher.Instance.CombatEvent.AddEventListener<CombatEventType, object>(EventId.CombatEvent, OnCombatEvent);
        EventDispatcher.Instance.ExploreEvent.AddEventListener<ExploreEventType, object>(EventId.ExploreEvent, OnExploreEvent);
        EventDispatcher.Instance.ScriptTimeEvent.AddEventListener<ScriptTimeUpdateType, object>(EventId.ScriptTimeEvent, OnScriptTimeUpdateEven);
        //
        CloseMask();
        bigMap.OpenUI(true);
        //
        topTransform.gameObject.SetActive(true);


        //路点开始
        //   EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.WPSelect, (object)ExploreSystem.Instance.NowWaypointId);
    }

    #region 点击
    /// <summary>
    /// 点击了返回
    /// </summary>
    private void OnClickBack()
    {
        OnClickStartTiming();
        if (OnBack != null)
        {
            OnBack();
        }
    }
    /// <summary>
    /// 按下暂停
    /// </summary>
    private void OnClickPause()
    {
        go.SetActive(false);
        startTimingButton.gameObject.SetActive(true);
        pauseTimingButton.gameObject.SetActive(false);
        isMove = false;
        ScriptTimeSystem.Instance.StopTiming();
        gamePause.SetActive(true);
    }
    /// <summary>
    /// 按下暂停
    /// </summary>
    private void OnClickStartTiming()
    {
        startTimingButton.gameObject.SetActive(false);
        pauseTimingButton.gameObject.SetActive(true);
        ScriptTimeSystem.Instance.StartTiming();
        gamePause.SetActive(false);
    }

    /// <summary>
    /// 点击了GO
    /// </summary>
    private void OnClickGo()
    {
        OpenMask();
        go.SetActive(false);
        //是最后可以事件
        if (isLastEvent)
        {
            EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.SceneEnd, (object)null);
            return;
        }
        //
        isCheckEventPos = false;
        //
        LogHelperLSK.Log("重置场景");
        if (IEResetScene != null)
        {
            IEResetScene.Stop();
        }

        IEResetScene = new CoroutineUtil(IResetScene(3, 1));
    }

    /// <summary>
    /// 按下
    /// </summary>
    private void OnClickDown()
    {
        //
        isMove = true;
        //
        if (isLeftMove)
        {
            return;
        }
    }
    /// <summary>
    /// 抬起
    /// </summary>
    private void OnClickUp()
    {
        isMove = false;
        if (isLeftMove)
        {
            return;
        }
        //   EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatUIOperationType.PlayLeftAction, (object)CharModuleAction.Idle);
    }

    private void OnClickMap()
    {
        bigMap.UpdateProgressPoint();
    }
    private void OnClickBag()
    {
        bagPopup.OpenUI();
    }

    /// <summary>
    /// 点击地图探索完成确定
    /// </summary>
    private void OnMapExploreFinishOk()
    {
        OnClickBack();
    }
    #endregion

    #region 移动

    /// <summary>
    /// 开始场景加载
    /// </summary>
    private void StartSceneLoad()
    {
        OpenMask();
        exploreItemMove.DelItem();
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatUIOperationType.PlayLeftAction, (object)CharModuleAction.Idle);
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatUIOperationType.SetLeftPos, (object)Vector3.zero);
        exploreMap.InitMapInfo();
        exploreEvent.OnMoveEnd = OnCallMoveEnd_Event;
        exploreEvent.OnVisiting = OnCallVisiting_Event;
        exploreEvent.OnStartOpen = OnCallStartOpen_Event;

        exploreEvent.Init();
        //场景准备
        LogHelperLSK.LogError("场景准备");
        if (IESceneReady != null)
        {
            IESceneReady.Stop();
        }

        IESceneReady = new CoroutineUtil(ISceneReady(2, 1));
        //EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.SceneStartMove, (object)aspd);
    }
    private CoroutineUtil IESceneReady;
    private CoroutineUtil IEResetScene;
    private CoroutineUtil IEVisitMove;
    /// <summary>
    /// 场景准备
    /// </summary>
    /// <param name="readyTime"></param>
    /// <param name="sceneMimTime"></param>
    /// <returns></returns>
    private IEnumerator ISceneReady(float readyTime, float sceneMimTime)
    {
        bigMap.gameObject.SetActive(false);
        OpenMask();
        ExploreSystem.Instance.StartMove();
        //角色移动到屏幕中点
        float time = 0;
        float movex;
        float charMove = exploreChar.StartCharMove(sceneMimTime, 1, out movex);
        charMove = 300;
        readyTime -= sceneMimTime;
        if (exploreChar.CharTrans.localPosition != exploreChar.StartPos)
        {
            float temp = Math.Abs(movex) / Math.Abs(exploreChar.StartPos.x - exploreChar.InitPos1.x);
            sceneMimTime *= temp;
            charMove = movex / sceneMimTime;
        }
        if (movex > 5)
        {
            exploreChar.PlayAction(CharModuleAction.Run);
            sceneMimTime = movex / charMove;
            while (time <= sceneMimTime)
            {
                time += Time.deltaTime;
                exploreChar.UpdateMove(charMove);
                yield return null;
            }
        }
        doorEffect.SetActive(false);
        //角色移动到屏幕起点
        time = 0;
        charMove = exploreChar.StartCharMove(readyTime, 2, out movex);
        charMove = 300;
        float eventMove = exploreEvent.StartSceneMove(readyTime, movex);
        eventMove = 300;
        exploreChar.PlayAction(CharModuleAction.Idle);
        readyTime = movex / eventMove;
        while (time <= readyTime)
        {
            time += Time.deltaTime;
            exploreChar.UpdateMove(charMove);
            exploreEvent.UpdateMove(eventMove);
            exploreMap.UpdateMove(eventMove);
            exploreItemMove.UpdateItemPos(exploreEvent.RectTransform.anchoredPosition);
            yield return null;
        }
        CloseMask();
        ExploreSystem.Instance.MoveEnd();
        isCheckEventPos = true;
        //没有事件
        if (ExploreSystem.Instance.NowEvents.Count == 0)
        {
            isLastEvent = true;
        }
    }
    /// <summary>
    /// 重置场景
    /// </summary>
    /// <param name="resetTime"></param>
    /// <param name="maxPosTime"></param>
    /// <returns></returns>
    private IEnumerator IResetScene(float resetTime, float maxPosTime)
    {
        bigMap.gameObject.SetActive(false);
        OpenMask();
        ExploreSystem.Instance.StartMove();
        float sp = 300;
        //角色移动到屏幕5/8
        float time = 0;
        float movex;
        float charMove = exploreChar.StartCharMove(maxPosTime, 3, out movex);
        charMove = sp;
        maxPosTime = movex / charMove;
        exploreChar.PlayAction(CharModuleAction.Run);
        while (time <= maxPosTime)
        {
            time += Time.deltaTime;
            exploreChar.UpdateMove(charMove);
            yield return null;
        }
        //角色移动到屏幕起点
        time = 0;
        resetTime -= maxPosTime;
        charMove = exploreChar.StartCharMove(resetTime, 2, out movex);
        charMove = sp;
        resetTime = movex / charMove;
        float eventMove = exploreEvent.StartSceneMove(resetTime, movex);
        eventMove = sp;
        exploreChar.PlayAction(CharModuleAction.Idle);
        while (time <= resetTime)
        {
            time += Time.deltaTime;
            exploreChar.UpdateMove(charMove);
            exploreEvent.UpdateMove(eventMove);
            exploreMap.UpdateMove(eventMove);
            exploreItemMove.UpdateItemPos(exploreEvent.RectTransform.anchoredPosition);
            yield return null;
        }
        CloseMask();
        ExploreSystem.Instance.MoveEnd();
        isCheckEventPos = true;
    }
    /// <summary>
    /// 访问移动
    /// </summary>
    private IEnumerator IVisitMove(Action action, bool isTrapEvent, Vector3 charMovePos, float eventMoveX, float eventWidth)
    {
        bigMap.gameObject.SetActive(false);
        OpenMask();
        ExploreSystem.Instance.StartMove();
        charMovePos += Vector3.down * 120;
        //定速度 定事件 300
        float moveSp = 300;
        float time = 0;
        float moveDistance;
        float eventMoveTime = exploreEvent.StartSceneMove(eventMoveX);
        float charTime = exploreChar.StartCharMove(charMovePos, moveSp, eventWidth, out moveDistance);
        bool isChar = charTime > eventMoveTime;
        exploreChar.PlayAction(CharModuleAction.Run);
        while (time <= (isChar ? charTime : eventMoveTime))
        {
            time += Time.deltaTime;
            if (isChar)
            {
                if (eventMoveTime != 0)
                {
                    exploreEvent.UpdateMove(eventMoveX / charTime);
                    exploreMap.UpdateMove(eventMoveX / charTime);
                    exploreItemMove.UpdateItemPos(exploreEvent.RectTransform.anchoredPosition);
                }
                exploreChar.UpdateMove(moveSp);
            }
            else
            {
                exploreEvent.UpdateMove(moveSp);
                exploreMap.UpdateMove(moveSp);
                exploreItemMove.UpdateItemPos(exploreEvent.RectTransform.anchoredPosition);
                if (charTime != 0)
                {
                    exploreChar.UpdateMove(moveDistance / eventMoveTime);
                }
            }
            //更新角色的层级
            exploreChar.UpdateCharLayer();
            yield return null;
        }
        //
        exploreChar.PlayAction(!isTrapEvent ? CharModuleAction.Idle : CharModuleAction.Idle);
        if (action != null)
        {
            action();
        }

        ExploreSystem.Instance.MoveEnd();
    }

    #endregion



    private CoroutineUtil IEEventVisitFail;
    private CanvasGroup visitFailCanvas;
    private void EventVisitFail(string intro)
    {
        if (IEEventVisitFail != null)
        {
            IEEventVisitFail.Stop();
        }

        if (visitFailCanvas != null)
        {
            DestroyImmediate(visitFailCanvas.gameObject);
        }

        IEEventVisitFail = new CoroutineUtil(IEventVisitFail(intro));
    }

    private IEnumerator IEventVisitFail(string intro)
    {
        visitFailCanvas = ResourceLoadUtil.InstantiateRes(visitFailObj, visitFailTrans).GetComponent<CanvasGroup>();
        visitFailCanvas.GetComponent<Text>().text = intro;
        visitFailCanvas.gameObject.SetActive(true);
        while (visitFailCanvas.alpha > 0.3f)
        {
            visitFailCanvas.alpha -= Time.deltaTime * 0.4f;
            yield return null;
        }
        DestroyImmediate(visitFailCanvas.gameObject);
    }

    //探索事件
    private void OnExploreEvent(ExploreEventType arg1, object arg2)
    {
        switch (arg1)
        {
            case ExploreEventType.WPStartReady:
                EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.SceneStartReady, (object)false);
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
                exploreChar.gameObject.SetActive(true);
                go.SetActive(false);
                break;
            case ExploreEventType.EventQuit:
                exploreChar.gameObject.SetActive(true);
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
    /// <summary>
    /// 路点访问完成
    /// </summary>
    private void WPVisitOk()
    {
        //现在是路点
        if (ExploreSystem.Instance.NowWPAttribute.wp_template.WPCategory == 2)
        {
            //最后一个
            if (ExploreSystem.Instance.NowWPAttribute.nextWP[0] == -1)
            {
                EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.ExploreFinish, (object)null);
                return;
            }
            CloseMask();
            //是否是岔路
            bigMap.OpenUI(true);
            //    return;
            //   bigMap.UpdateProgressPoint();
            return;
        }
        //选择房间
        if (!ExploreSystem.Instance.SelectRoom())
        {
            return;
        }

        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.SceneStartReady, (object)false);
    }

    private void OnCallMoveEnd_Event()
    {
        OnClickUp();
        // EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.WPEnd, (object)null);
    }

    private void OnCallStartOpen_Event(object param)
    {
        if ((param as UIExploreEventBase).EventAttribute.EventType == WPEventType.Trap)
        {
            exploreChar.PlayAction(CharModuleAction.Hurt, true);
        }
    }
    private void OnCallVisiting_Event()
    {
        exploreChar.PlayAction(CharModuleAction.Idle);
    }


    #region 战斗状态
    /// <summary>
    /// 开始战斗
    /// </summary>
    private void OnStartCombat(object _param)
    {
        topTransform.gameObject.SetActive(false);
        go.SetActive(false);
        exploreEvent.gameObject.SetActive(false);
        eventIntroPopup.SetActive(false);
        exploreItemMove.gameObject.SetActive(false);
        //    EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatStage.CombatPrepare, (object)null);
        if (OnClickCombat != null)
        {
            OnClickCombat();
        }

        //  combatManager.StartCombat(_param as CombatSystem);
    }
    /// <summary>
    /// 准备战斗
    /// </summary>
    private void OnReadyCombat(object _param)
    {
        OnClickUp();
        go.SetActive(false);
        exploreChar.gameObject.SetActive(false);
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatUIOperationType.PlayLeftAction, (object)CharModuleAction.Idle);
    }
    /// <summary>
    /// 准备战斗
    /// </summary>
    private void OnReadyCombatOK(object _param)
    {
        combatSystem = _param as CombatSystem;
        OnStartCombat(_param);
    }

    /// <summary>
    /// 战斗结束
    /// </summary>
    private void OnCombatEnd(object param)
    {
        topTransform.gameObject.SetActive(true);
        exploreEvent.gameObject.SetActive(true);
        exploreChar.gameObject.SetActive(true);
        if ((bool)param)
        {
            TeamSystem.Instance.CombatRevivableChar(resurrectProp);
        }
        //
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, (bool)param ? CombatEventType.CombatWin : CombatEventType.CombatFail, (object)null);
        exploreItemMove.gameObject.SetActive(true);
    }

    #endregion

    /// <summary>
    /// 更新威胁描述
    /// </summary>
    private void UpdateThreatIntroStr(int eventID)
    {
        Event_template template = Event_templateConfig.GetEventTemplate(eventID);
        if (template == null)
        {
            return;
        }
        return;
        if (template.baseThreat > 0)
        {
            eventIntroPopup.UpdateShow("你的行动引起了所有敌人的警觉！");
        }

        foreach (List<int> item in template.factionThreat)
        {
            if (item[0] > 0)
            {
                string str = "你的行动引起了{0}的警觉！";
                string str1 = string.Empty;
                switch (item[1])
                {
                    case 1:
                        str1 = "狗头人Waaagh！";
                        break;
                    case 2:
                        str1 = "伪军";
                        break;
                    case 3:
                        str1 = "古神教徒";
                        break;
                    case 4:
                        str1 = "觅血狼人";
                        break;
                    case 5:
                        str1 = "亡灵尸潮";
                        break;
                    case 6:
                        str1 = "蜥蜴人部族";
                        break;
                    case 7:
                        str1 = "蛇人王朝";
                        break;
                    case 8:
                        str1 = "古神化身";
                        break;
                }
                if (str1 != string.Empty)
                {
                    eventIntroPopup.UpdateShow(string.Format(str, str1));
                }
            }
        }
    }
    /// <summary>
    /// 更新剧本时间显示
    /// </summary>
    private void UpdateScriptTimeShow()
    {
        playTimeInfo = TimeUtil.GetPlayTimeInfo();
        timeText.text = string.Format(m_timeFormat2, playTimeInfo.years, playTimeInfo.months, playTimeInfo.days);
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

    private void OnCallClickItem(object param)
    {
        exploreItemMove.OnClickItemMove(param);
    }
    private void OnCallClickHealingGlob(object param)
    {
        exploreItemMove.OnClicHealingGlobMove(param);
    }
    private void OnCallBackCombatReady(object param)
    {
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatUIOperationType.UpdateRightMove, param);
    }
    private void OnCallBackSummonReady(object param)
    {
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatUIOperationType.SetRightPos, param);
    }
    private void OnCallExitEvent()
    {
        CloseMask();
        exploreChar.PlayAction(CharModuleAction.Idle);
    }
    private void OnCallBackVisitReady(object param, Action action)
    {
        eventBase = param as UIExploreEventBase;
        if (ExploreSystem.Instance.NowEventIndex == eventBase.EventAttribute.EventIndex)
        {
            return;
        }
        //是否有陷阱
        EventAttribute eventAttribute = ExploreSystem.Instance.PreviousHaveTrap(eventBase.EventAttribute.EventIndex);
        float temp1 = Screen.width / 1280f;
        Vector3 temp = GameTools.WorldToScreenPoint(eventBase.transform) - new Vector3(Screen.width, Screen.height) / 2;
        bool isHaveTrap = false;
        Vector3 trapPos = sizeVector3;
        if (eventAttribute != null)
        {
            if (exploreEvent.Events[eventAttribute.EventIndex] != null)
            {
                eventBase = exploreEvent.Events[eventAttribute.EventIndex].GetComponent<UIExploreTrapEvent>();
                if (eventBase != null)
                {
                    trapPos = eventBase.transform.position;
                    trapPos = GameTools.WorldToScreenPoint(eventBase.transform) - new Vector3(Screen.width, Screen.height) / 2;
                    isHaveTrap = temp.x > trapPos.x && exploreChar.CharTrans.localPosition.x < trapPos.x;
                    trapPos = trapPos / temp1;
                }
            }
        }
        temp = temp / temp1;
        if (isHaveTrap)
        {
            temp.x = trapPos.x;
        }

        float eventMoveX = 0;
        Vector3 charMove = temp;
        //判断事件所在屏幕的位置
        if (temp.x > 0) //在右边边 移动到中间位置 
        {
            eventMoveX = temp.x;
            charMove = new Vector3(0, temp.y);
        }
        float eventWidth = eventBase.transform.GetComponent<RectTransform>().sizeDelta.x;
        ExploreSystem.Instance.SetNowClickEvent(eventBase.EventAttribute.EventIndex);
        if (isHaveTrap)
        {
            (param as UIExploreEventBase).ResetButton();
        }

        if (IEVisitMove != null)
        {
            IEVisitMove.Stop();
        }

        IEVisitMove = new CoroutineUtil(IVisitMove(isHaveTrap ? VisitTrap : action, isHaveTrap, charMove, eventMoveX, eventWidth));

        //exploreChar.VisitEventMove(charMove, isHaveTrap, isHaveTrap ? VisitTrap : action, exploreEvent.StartVisitMove(eventMoveX));
    }

    /// <summary>
    /// 访问陷阱
    /// </summary>
    private void VisitTrap()
    {
        CloseMask();
        if (eventBase == null)
        {
            return;
        }

        eventBase.ImmediateVisit();
    }
    /// <summary>
    /// 检查事件位置
    /// </summary>
    private void CheckEventPos()
    {
        if (!isCheckEventPos)
        {
            return;
        }
        //检查当前屏幕中没有事件存在了
        foreach (KeyValuePair<int, UIExploreEventBase> item in exploreEvent.EventBases)
        {
            if (item.Value == null)
            {
                continue;
            }

            if (item.Value.EventAttribute.EventType == WPEventType.Trap || item.Value.EventAttribute.EventPosType != 1)
            {
                continue;
            }

            if (!item.Value.IsEnterScreen || ExploreSystem.Instance.GetEventAttribute(item.Key).isCall)
            {
                continue;
            }

            if (item.Value.transform.position.x < exploreChar.CharTrans.position.x)
            {
                continue;
            }

            isCheckEventPos = false;
            go.SetActive(false);
            return;
        }
        go.SetActive(true);
        isCheckEventPos = false;
        LogHelperLSK.Log("没有存在事件");
    }

    private void OnCallEventInitOK()
    {
        eventWidth = exploreEvent.RectTransform.sizeDelta.x;
        // smallMap.OpenUI(eventWidth, exploreEvent.EventLists);
    }
    /// <summary>
    /// 打开遮罩
    /// </summary>
    private void OpenMask()
    {
        maskObj.SetActive(true);
    }
    /// <summary>
    /// 关闭遮罩
    /// </summary>
    private void CloseMask()
    {
        maskObj.SetActive(false);
    }

    private void GetObj()
    {
        visitFailTrans = transform.Find("VisitFail");
        visitFailObj = visitFailTrans.Find("Text").gameObject;
        maskObj = transform.Find("Mask").gameObject;
        OpenMask();
        //
        exploreChar = transform.Find("ExploreChar").gameObject.AddComponent<UIExploreChar>();
        //
        eventIntroPopup = GameModules.popupSystem.GetPopupObj(ModuleName.eventIntroPopup).GetComponent<UIEventIntroPopup>();
        wayponitStart = GameModules.popupSystem.GetPopupObj(ModuleName.exploreWayponitStart).GetComponent<UIWayponitStart>();
        mapExploreFinish = GameModules.popupSystem.GetPopupObj(ModuleName.mapExploreFinish).GetComponent<UIMapExploreFinish>();
        combatEventPopup = GameModules.popupSystem.GetPopupObj(ModuleName.combatEventPopup).GetComponent<UICombatEventPopup>();
        bagPopup = GameModules.popupSystem.GetPopupObj(ModuleName.bagPopup).GetComponent<UIExploreBagPopup>();
        //
        mapExploreFinish.OnConfirm = OnMapExploreFinishOk;
        //
        exploreItemMove = GameModules.popupSystem.GetPopupObj(ModuleName.itemMovePopup).GetComponent<UIExploreItemMove>();
        /* transform.Find("ExploreUI/ItemMove").GetComponent<UIExploreItemMove>();*/
        exploreMap = transform.Find("Map").gameObject.AddComponent<UIExploreMap>();
        doorEffect = transform.Find("DoorEffect").gameObject;
        doorEffect.AddComponent<UIAlterParticleSystemLayer>().Init();
        gamePause = transform.Find("GamePause").gameObject;
        exploreEvent = transform.Find("EventList").gameObject.AddComponent<UIExploreEvent>();
        exploreEvent.OnInitOK = OnCallEventInitOK;
        exploreEvent.OnCallItem = OnCallClickItem;
        exploreEvent.OnCallHealingGlob = OnCallClickHealingGlob;
        exploreEvent.OnCallCombatReady = OnCallBackCombatReady;
        exploreEvent.OnCallSummonReady = OnCallBackSummonReady;
        exploreEvent.OnVisitReady = OnCallBackVisitReady;
        exploreEvent.OnExitEvent = OnCallExitEvent;
        //
        go = transform.Find("Go").gameObject;
        go.AddComponent<Button>().onClick.AddListener(OnClickGo);
        //
        topTransform = transform.Find("Top");
        //
        smallMap = topTransform.Find("MinMap").gameObject.AddComponent<UIExploreSmallMap>();
        bigMap = topTransform.Find("BigMap").gameObject.AddComponent<UIExploreBigMap>();
        bagButton = topTransform.Find("Bag").GetComponent<Button>();
        mapButton = topTransform.Find("Map").GetComponent<Button>();
        bagButton.onClick.AddListener(OnClickBag);
        mapButton.onClick.AddListener(OnClickMap);
        //
        timeText = topTransform.Find("Time/Time").GetComponent<Text>();
        goldText = topTransform.Find("Gold/Num").GetComponent<Text>();
        startTimingButton = topTransform.Find("Time/Start").GetComponent<Button>();
        pauseTimingButton = topTransform.Find("Time/Pause").GetComponent<Button>();
        startTimingButton.onClick.AddListener(OnClickStartTiming);
        pauseTimingButton.onClick.AddListener(OnClickPause);
        //
        backButton = topTransform.Find("Back").GetComponent<Button>();
        backButton.onClick.AddListener(OnClickBack);
        //
        mapNameText = topTransform.Find("Name/Name").GetComponent<Text>();
        //startButton = transform.Find("StartMask/Start").GetComponent<Button>();
        //startButton.onClick.AddListener(OnClickStart);
        //
    }

    private void LateUpdate()
    {
        CheckEventPos();
        if (!isMove)
        {
            return;
        }

        moveX = aspdValue * (1 / Time.fixedDeltaTime) * Time.deltaTime;
        //   exploreEvent.UpdateMove(moveX);
        exploreMap.UpdateMove(moveX);
        //  smallMap.UpdatePos(moveX);
        //if (!isMove) return;
        //if (evenRectTransform == null) evenRectTransform = exploreEvent.transform.GetComponent<RectTransform>();
        //// exploreItemMove.UpdateItemPos(evenRectTransform.anchoredPosition);
    }
    private void Update()
    {
        //CheckEventPos();
        //if (!isMove) return;
        //moveX = aspdValue * (1 / Time.fixedDeltaTime) * Time.deltaTime;
        ////   exploreEvent.UpdateMove(moveX);
        //exploreMap.UpdateMove(moveX);
        //smallMap.UpdatePos(moveX);
    }

    // 当 MonoBehaviour 将被销毁时调用此函数
    private void OnDestroy()
    {
        EventDispatcher.Instance.CombatEvent.RemoveEventListener<CombatEventType, object>(EventId.CombatEvent, OnCombatEvent);
        EventDispatcher.Instance.ExploreEvent.RemoveEventListener<ExploreEventType, object>(EventId.ExploreEvent, OnExploreEvent);
        EventDispatcher.Instance.ScriptTimeEvent.RemoveEventListener<ScriptTimeUpdateType, object>(EventId.ScriptTimeEvent, OnScriptTimeUpdateEven);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClickDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnClickUp();
    }

    //
    private bool isMove;
    private float moveX;
    private float eventWidth;
    //
    private readonly Button combatEndMaskButton;
    private Button backButton;
    private Button bagButton;
    private Button mapButton;
    private Transform topTransform;
    private GameObject go;
    private GameObject doorEffect;
    private GameObject gamePause;
    private Text timeText;
    private Text mapNameText;
    private Text goldText;
    private GameObject maskObj;
    private Transform visitFailTrans;
    private GameObject visitFailObj;
    //
    private readonly Button startButton;
    private Button startTimingButton;
    private Button pauseTimingButton;
    //
    private readonly RectTransform evenRectTransform;
    //
    private UIExploreItemMove exploreItemMove;
    private UIExploreChar exploreChar;
    private UIExploreMap exploreMap;
    private UIExploreEvent exploreEvent;
    private UIWayponitStart wayponitStart;
    private UIMapExploreFinish mapExploreFinish;
    private UICombatEventPopup combatEventPopup;
    private UIEventIntroPopup eventIntroPopup;
    private UIExploreSmallMap smallMap;
    private UIExploreBigMap bigMap;
    private UIExploreBagPopup bagPopup;
    //
    private CombatSystem combatSystem;
    //
    private readonly bool isFirst;
    private readonly bool isLeftMove;
    private readonly bool isFirstExplore;
    //
    private float resurrectProp;
    //
    private UIExploreEventBase eventBase;
    private readonly UIExploreEventBase eventBase1;
    private Vector3 eventPos;
    private bool isCheckEventPos;
    private bool isLastEvent;
    //
    private readonly float aspd = 5;
    private Vector3 leftEndVector3 = new Vector3(1800, 0, 0);
    private Vector3 sizeVector3 = new Vector3(1280, 720, 0);
    private readonly string m_timeFormat2 = "{0}年{1}月{2}日";
    private TimeUtil.PlayTimeInfo playTimeInfo;
}