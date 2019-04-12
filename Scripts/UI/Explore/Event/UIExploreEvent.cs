using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameEventDispose;
using UnityEngine;

/// <summary>
/// 探索事件
/// </summary>
public class UIExploreEvent : MonoBehaviour
{

    public delegate void CallBack();
    public delegate void CallBackBool(object param, Action action);
    public delegate void CallBack2(object param);

    public CallBack OnExitEvent;
    public CallBack OnInitOK;
    public CallBack OnMoveEnd;
    public CallBack OnVisiting;
    public CallBack2 OnStartOpen;
    public CallBack2 OnCallItem;
    public CallBack2 OnCallHealingGlob;
    public CallBack2 OnCallCombatReady;
    public CallBack2 OnCallSummonReady;
    public CallBackBool OnVisitReady;
    //
    public float xValue;
    public float x1Value;
    public float sizeValue;
    //

    public bool IsEnd { get { return isEnd; } }

    public RectTransform LastEventRectTransform { get { return lastEventRectTransform; } }

    public Vector3 EndVector3 { get { return endVector3; } }

    public List<RectTransform> EventLists { get { return eventLists; } }

    public RectTransform RectTransform { get { return rectTransform; } }

    public Dictionary<int, RectTransform> Events { get { return events; } }

    public Dictionary<int, UIExploreEventBase> EventBases { get { return eventBases; } }

    public void Init()
    {
        EventDispatcher.Instance.ExploreEvent.AddEventListener<ExploreEventType, object>(EventId.ExploreEvent, OnExploreEvent);
        isEnd = false;
        rectTransform = transform.GetComponent<RectTransform>();
        //初始位置
        rectTransform.anchoredPosition = Vector2.up * hight + Vector2.right * 550f;
        ResourceLoadUtil.DeleteChildObj(transform, new List<string> { "ItemMove" });
        eventLists.Clear();
        events.Clear();
        eventBases.Clear();
        rectTransform.sizeDelta = new Vector2(1, 1);
        lastEventShowX = 0;
        CreateEventList(ExploreSystem.Instance.NowWPAttribute);
    }

    /// <summary>
    /// 创建事件列表
    /// </summary>
    private void CreateEventList(WPAttribute _wpAttribute)
    {
        List<EventAttribute> eventtAttributes = ExploreSystem.Instance.NowEvents;
        //没有事件
        if (eventtAttributes == null || eventtAttributes.Count == 0)
        {
            VisitEventEnd();
            return;
        }
        UIExploreEventBase exploreEventBase;
        //创建事件
        foreach (EventAttribute item in eventtAttributes)
        {
            exploreEventBase = LoadEventRes(item, item.EventPos, false);
            if (exploreEventBase == null)
            {
                continue;
            }
            GameObject _obj = exploreEventBase.gameObject;
            _obj.name = item.eventId.ToString();
            eventLists.Add(_obj.GetComponent<RectTransform>());
            events.Add(item.EventIndex, _obj.GetComponent<RectTransform>());
            eventBases.Add(item.EventIndex, exploreEventBase);
        }
        //
        lastEventRectTransform = eventLists.Count == 0 ? null : eventLists.Last().GetComponent<RectTransform>();
        //
        if (OnInitOK != null)
        {
            OnInitOK();
        }
    }
    /// <summary>
    /// 访问事件结束
    /// </summary>
    private void VisitEventEnd()
    {
        isEnd = true;
        if (OnMoveEnd != null)
        {
            OnMoveEnd();
        }

        OnMoveEnd = null;
    }

    /// <summary>
    /// 开始移动
    /// </summary>
    private void StartMove()
    {
        if (!isMove)
        {
            return;
        }

        rectTransform.anchoredPosition = Vector3.MoveTowards(rectTransform.anchoredPosition, Vector2.up * hight, aspd * Time.deltaTime);
        if (Vector3.SqrMagnitude(rectTransform.anchoredPosition - Vector2.up * hight) >= 100)
        {
            return;
        }

        isMove = false;
        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.SceneMoveEnd, (object)aspd);
        //  EventDispatcher.Instance.ExploreEvent.RemoveEventListener<ExploreEventType, object>(EventId.ExploreEvent, OnExploreEvent);
    }

    private float aspd;

    private bool isMove;
    private void Update()
    {
        StartMove();
    }


    /// <summary>
    /// 探索事件
    /// </summary>
    private void OnExploreEvent(ExploreEventType _type, object param)
    {
        switch (_type)
        {
            case ExploreEventType.OneselfMove:
                break;
            case ExploreEventType.MoveSceneMim:
                isMove = true;
                aspd = 300;
                float temp = Vector3.Distance(rectTransform.anchoredPosition, Vector2.up * hight);
                aspd = temp / (float)param;
                EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.MoveSceneMimStart, (object)aspd);
                break;
            case ExploreEventType.VisiteEventMove:    //访问移动
                                                      //  endVector3 = rectTransform.anchoredPosition;
                                                      //  endVector3.x -= (float)param;
                                                      //   aspd = 300;
                                                      //   isVisitMove = true;
                break;
        }
    }

    /// <summary>
    /// 重置场景
    /// </summary>
    public void ResetScene(float movex)
    {
        StartVisitMove(movex);
    }

    /// <summary>
    /// 开始移动
    /// </summary>
    /// <param name="moveX">平移距离</param>
    public float StartSceneMove(float moveX)
    {
        endVector3 = rectTransform.anchoredPosition;
        endVector3.x -= moveX;
        aspd = 300;
        return Vector3.Distance(rectTransform.anchoredPosition, endVector3) / aspd;
    }

    /// <summary>
    /// 开始移动
    /// </summary>
    /// <param name="moveTime">移动时间</param>
    /// <param name="moveX">平移距离</param>
    public float StartSceneMove(float moveTime, float moveX)
    {
        endVector3 = rectTransform.anchoredPosition;
        endVector3.x -= moveX;
        //  aspd = Vector3.Distance(rectTransform.anchoredPosition, endVector3) / moveTime;
        //    IEStartSceneMove = new CoroutineUtil(IStartSceneMove(moveTime, moveX));
        return Vector3.Distance(rectTransform.anchoredPosition, endVector3) / moveTime;
    }

    public void UpdateMove(float aspd1)
    {
        rectTransform.anchoredPosition = Vector3.MoveTowards(rectTransform.anchoredPosition, endVector3, aspd1 * Time.deltaTime);
    }

    private readonly CoroutineUtil IEStartSceneMove;
    private IEnumerator IStartSceneMove(float moveTime, float moveX)
    {
        yield return null;
        float time = 0;
        while (time < moveTime)
        {
            rectTransform.anchoredPosition = Vector3.MoveTowards(rectTransform.anchoredPosition, endVector3, aspd * Time.deltaTime);
            moveTime += Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// 开始访问移动
    /// </summary>
    public float StartVisitMove(float movex)
    {
        isVisitMove = false;
        endVector3 = rectTransform.anchoredPosition;
        endVector3.x -= movex;
        if (Vector3.SqrMagnitude(rectTransform.anchoredPosition - endVector3) <= 100)
        {
            EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.VisiteEventMoveEnd, (object)aspd);
            return 0;
        }
        aspd = 300;
        isVisitMove = true;
        if (IEVisitMove != null)
        {
            IEVisitMove.Stop();
        }

        IEVisitMove = new CoroutineUtil(VisitMove(endVector3));
        return Vector3.Distance(rectTransform.anchoredPosition, endVector3) / aspd;
    }

    private CoroutineUtil IEVisitMove;
    private IEnumerator VisitMove(Vector3 position)
    {
        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.VisiteEventMove, (object)aspd);
        while (Vector3.SqrMagnitude(rectTransform.anchoredPosition - endVector3) >= 100)
        {
            isMove = false;
            aspd = 300;
            rectTransform.anchoredPosition = Vector3.MoveTowards(rectTransform.anchoredPosition, position, aspd * Time.deltaTime);
            yield return null;
        }
        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.VisiteEventMoveEnd, (object)aspd);
    }

    /// <summary>
    /// 加载事件资源
    /// </summary>
    private UIExploreEventBase LoadEventRes(EventAttribute _eventAttribute, int posIndex, bool isAutoVisit)
    {
        if (_eventAttribute == null)
        {
            return null;
        }

        return LoadEventRes(_eventAttribute, GetEventVector(_eventAttribute.SceneIndex, _eventAttribute.ScenePos, _eventAttribute.EventPosType), isAutoVisit);
        // return LoadEventRes(_eventAttribute, GetEventVector(posIndex), isAutoVisit);
    }
    /// <summary>
    /// 加载事件资源
    /// </summary>
    private UIExploreEventBase LoadEventRes(EventAttribute _eventAttribute, Vector3 pos, bool isAutoVisit)
    {
        GameObject _obj = null;
        UIExploreEventBase eventBase = null;
        try
        {

            switch ((WPEventType)_eventAttribute.event_template.eventType)
            {
                case WPEventType.Boss:
                case WPEventType.Combat:
                    _obj = LoadEventRes(pos, "combatEvent", transform);
                    eventBase = LoadCombatEvent(_obj, _eventAttribute);
                    InitEventBase(eventBase, _eventAttribute, isAutoVisit);
                    break;
                case WPEventType.Summon:
                case WPEventType.Remains:
                case WPEventType.Herb:
                case WPEventType.Choice:
                case WPEventType.Treasure:
                case WPEventType.Grass:
                case WPEventType.Trap:
                case WPEventType.Abnormal:
                    eventBase = LoadNormalEvent(pos, _eventAttribute);
                    if (eventBase != null)
                    {
                        // _obj = eventBase.gameObject;
                        InitEventBase(eventBase, _eventAttribute, isAutoVisit);
                    }
                    else
                    {
                        DestroyImmediate(eventBase.gameObject);
                    }
                    break;
                case WPEventType.NangPao:
                    break;
                default:
                    LogHelperLSK.LogError(_eventAttribute.eventId + "未用类型" + _eventAttribute.event_template.eventType);
                    return null;
            }
        }
        catch (Exception e)
        {
            LogHelperLSK.LogError(_eventAttribute.eventId + "  " + e.Message);
            return null;
        }
        float temp;
        switch (_eventAttribute.EventPosType)
        {
            case 1:
                temp = 0.9f;
                break;
            case 2:
                temp = 0.9f;
                break;
            case 3:
                temp = 0.8f;
                break;
            case 4:
                temp = 1f;
                break;
            default:
                temp = 0.9f;
                break;
        }
        temp = 1;
        eventBase.transform.localScale = Vector3.one * temp;

        return eventBase;
    }

    /// <summary>
    /// 获得事件坐标
    /// </summary>
    private Vector3 GetEventVector(int sceneIndex, float pos, int posType)
    {
        float temp = ScreenDefultWideth * (sceneIndex + pos);
        switch (posType)
        {
            case 3: //  上                                           
                return Vector3.right * temp + Vector3.up * 125f;
            case 4: //  下                                           
                return Vector3.right * temp + Vector3.up * 0f;
            default:  // 中间                                         
                return Vector3.right * temp + Vector3.up * 75;
        }
    }

    /// <summary>
    /// 战斗事件
    /// </summary>
    private UIExploreEventBase LoadCombatEvent(GameObject _obj, EventAttribute _eventAttribute)
    {
        UIExploreCombatEvent combatEvent = _obj.AddComponent<UIExploreCombatEvent>();
        combatEvent.OnCallBackCombatReady = OnCallBackCombatReady;
        combatEvent.OnCallBackCombatEnd = OnCallBackCombatEnd;
        //
        return combatEvent;
    }
    /// <summary>
    /// 普通事件
    /// </summary>
    private UIExploreEventBase LoadNormalEvent(Vector3 _pos, EventAttribute _eventAttribute)
    {
        //跳出一下事件
        switch ((WPEventType)_eventAttribute.event_template.eventType)
        {
            case WPEventType.Combat:
                return null;
        }
        string eventRP = _eventAttribute.event_template.eventRP;
        GameObject _obj = LoadEventRes(_pos, eventRP, transform);
        switch ((WPEventType)_eventAttribute.event_template.eventType)
        {
            case WPEventType.Summon:
                return _obj.AddComponent<UIExploreSummonEvent>();
            case WPEventType.Remains:
                return _obj.AddComponent<UIExploreBaseEvent>();
            case WPEventType.Herb:
                return _obj.AddComponent<UIExploreBaseEvent>();
            case WPEventType.Choice:
                return _obj.AddComponent<UIExploreChoiceEvent>();
            case WPEventType.Treasure:
                return _obj.AddComponent<UIExploreTreasureEvent>();
            case WPEventType.Grass:
                return _obj.AddComponent<UIExploreGrassEvent>();
            case WPEventType.Trap:
                return _obj.AddComponent<UIExploreTrapEvent>();
            case WPEventType.Combat:
                break;
            case WPEventType.Boss:
                break;
            case WPEventType.NangPao:
                break;
            case WPEventType.Abnormal:
                return _obj.AddComponent<UIExploreGrassEvent>();
        }
        return null;
    }
    /// <summary>
    /// 初始化事件基类
    /// </summary>
    private void InitEventBase(UIExploreEventBase eventBase, EventAttribute eventAttribute, bool isAutoVisit)
    {
        eventBase.OnStartOpen = OnCallStartOpen;
        eventBase.OnVisiting = OnCallVisiting;
        eventBase.OnCallBackItem = OnCallClickItem;
        eventBase.OnCallBackHealingGlob = OnCallClickHealingGlob;
        eventBase.OnAddEvent = OnCallAddEvent;
        eventBase.OnVisitReady = OnCallVisitReady;
        eventBase.OnExitEvent = OnCallExitEvent;
        //
        eventBase.xValue = xValue;
        eventBase.x1Value = x1Value;
        eventBase.sizeValue = sizeValue;
        eventBase.isAutoVisit = isAutoVisit;
        //
        eventBase.OpenUI(eventAttribute);
    }

    /// <summary>
    /// 加载事件资源
    /// </summary>
    private GameObject LoadEventRes(Vector3 _pos, string rp, Transform parent)
    {
        GameObject _obj = ResourceLoadUtil.LoadEventRes<GameObject>(rp);
        _obj = ResourceLoadUtil.ObjSetParent(ResourceLoadUtil.InstantiateRes(_obj), parent);
        _obj.transform.GetComponent<RectTransform>().anchoredPosition = _pos + Vector3.left * _obj.GetComponent<RectTransform>().sizeDelta.x / 2f;
        _obj.transform.localScale = Vector3.one;
        return _obj;
    }

    #region 回调事件
    /// <summary>
    /// 开始访问
    /// </summary>
    private void OnCallStartOpen(object param)
    {
        if (OnStartOpen != null)
        {
            OnStartOpen(param);
        }
    }


    /// <summary>
    /// 访问中
    /// </summary>
    private void OnCallVisiting(object param)
    {
        if (OnVisiting != null)
        {
            OnVisiting();
        }
    }
    /// <summary>
    /// 点击物品
    /// </summary>
    private void OnCallClickItem(object param)
    {
        if (OnCallItem != null)
        {
            OnCallItem(param);
        }
    }
    /// <summary>
    /// 点击生命球
    /// </summary>
    private void OnCallClickHealingGlob(object param)
    {
        if (OnCallHealingGlob != null)
        {
            OnCallHealingGlob(param);
        }
    }
    private void OnCallAddEvent(object param)
    {
    }
    /// <summary>
    /// 访问事件准备回调
    /// </summary>
    private void OnCallVisitReady(object param, Action action)
    {
        if (OnVisitReady != null)
        {
            OnVisitReady(param, action);
        }
    }
    /// <summary>
    /// 离开事件
    /// </summary>
    private void OnCallExitEvent(object param)
    {
        if (OnExitEvent != null)
        {
            OnExitEvent();
        }
    }


    /// <summary>
    /// 战斗准备
    /// </summary>
    private void OnCallBackCombatReady(object param)
    {
        isReadyCombat = true;
    }
    /// <summary>
    /// 战斗结束
    /// </summary>
    private void OnCallBackCombatEnd(object param)
    {
        isReadyCombat = false;
    }
    /// <summary>
    /// 召唤准备
    /// </summary>
    private void OnCallBackSummonReady(object param)
    {
        if (OnCallSummonReady != null)
        {
            OnCallSummonReady(null);
        }
    }
    #endregion

    private void OnDestroy()
    {
        EventDispatcher.Instance.ExploreEvent.RemoveEventListener<ExploreEventType, object>(EventId.ExploreEvent, OnExploreEvent);
    }

    private readonly float eventSpacing = ScreenDefultWideth / 2f;
    private const float ScreenDefultWideth = 1280f;
    //
    private RectTransform rectTransform;
    //
    private List<RectTransform> eventLists = new List<RectTransform>();
    private Dictionary<int, RectTransform> events = new Dictionary<int, RectTransform>();
    private Dictionary<int, UIExploreEventBase> eventBases = new Dictionary<int, UIExploreEventBase>();
    private RectTransform lastEventRectTransform;
    private float lastEventShowX;
    private Vector2 endVector3;
    private Vector3 tempVector3;
    private bool isEnd;
    private readonly float hight = 250;
    private bool isReadyCombat;
    private bool isVisitMove;
}
