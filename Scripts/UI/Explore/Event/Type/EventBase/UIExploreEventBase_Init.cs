﻿using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class UIExploreEventBase
{
    public delegate void CallBack(object param);

    public CallBack OnBlock;
    public CallBack OnAuotoVisit;
    public CallBack OnPullout;
    public CallBack OnExitEvent;
    public CallBack OnStartOpen;
    public CallBack OnVisiting;
    public CallBack OnAddEvent;
    //
    protected CallBack OnOpenUI;
    protected CallBack OnProgressing;
    protected CallBack OnFade;
    protected CallBack OnFadeOk;
    protected CallBack OnOpenShow;
    protected CallBack OnOpenShow1;
    protected CallBack OnOpenPrepare;

    //
    public float xValue = 50;
    public float x1Value = 400;
    public float sizeValue = 36;
    //
    public bool isAutoVisit;

    public Vector2 Size { get { return _size; } }
    public EventAttribute EventAttribute { get { return _eventAttribute; } }
    public int EventIndex { get { return eventIndex; } }
    //
    protected Transform ShowTranS
    {
        get
        {
            if (_showTranS == null)
            {
                _showTranS = transform.Find("Show");
            }

            if (_showTranS == null)
            {
                _showTranS = transform;
            }

            return _showTranS;
        }
    }

    public RectTransform RectTransform { get { return _showRect; } }

    public bool IsBlock { get { return _isBlock; } }
    /// <summary>
    /// 取消阻挡
    /// </summary>
    public void CancelBlock()
    {
        _isBlock = false;
        _eventPosDetection.UpdateBlockState(false);
    }
    /// <summary>
    /// 打开阻挡
    /// </summary>
    public void OpenBlock()
    {
        _eventPosDetection.UpdateBlockState(true);
    }


    public void OpenUI(EventAttribute eventAttribute, Transform charObj = null)
    {
        _charTransform = charObj;
        Initialize(eventAttribute);
        //
        if (eventAttribute.event_template.isBlock == 1)
        {
            _eventPosDetection.UpdateBlockObj(_charTransform);
        }
        //可以自动访问
        if (eventAttribute.EventType == WPEventType.Trap)
        {
            SetAtuoVisit();
        }
    }

    private void Initialize(EventAttribute eventAttribute)
    {
        InfoInit(eventAttribute);
        UIInit(eventAttribute);
        //事件托管
        OnFade = OnCallFade;
        //
        OnOpenShow = OnCallOpenShow;
        //if (isAutoVisit)
        //{
        //    OnCallEventShow();
        //    OnCallEventCanVisit();
        //    //OnCallEventAutoVisit();
        //    return;
        //}
        _eventPosDetection = gameObject.AddComponent<UIExploreEventPosDetection>();
        //_eventPosDetection.OnEnterScreen = OnCallEventShow;
        //_eventPosDetection.OnShow1 = OnCallEventShow1;
        //_eventPosDetection.OnCanVisit = OnCallEventCanVisit;
        _eventPosDetection.OnAutoVisit = OnCallEventAutoVisit;
        _eventPosDetection.OnBlock = OnCallBlock;
        //_eventPosDetection.OnAutoAbandonVisit1 = OnCallEventAutoAbandonVisit1;
        // 
        _eventPosDetection.OnAutoAbandonVisit2 = OnCallEventAutoAbandonVisit2;
        _eventPosDetection._offset1 = xValue;
        _eventPosDetection._offset2 = x1Value;
        //
        PlayEventIdleAnimation();
    }

    /// <summary>
    /// 阻挡
    /// </summary>
    private void OnCallBlock()
    {
        _isBlock = true;
        if (OnBlock != null)
        {
            OnBlock(_eventAttribute);
        }
    }

    /// <summary>
    /// 界面初始化
    /// </summary>
    /// <param name="eventAttribute"></param>
    private void UIInit(EventAttribute eventAttribute)
    {
        _size = transform.GetComponent<RectTransform>().sizeDelta;
        SceneIndex = eventAttribute.SceneIndex;
        ScenePos = eventAttribute.ScenePos;
        _aspd = 5;
        //
        if (_baseCanvas == null)
        {
            _baseCanvas = gameObject.GetComponent<Canvas>();
        }
        if (_baseCanvas == null)
        {
            _baseCanvas = gameObject.AddComponent<Canvas>();
        }
        _canvas.Add(_baseCanvas);
        gameObject.AddComponent<GraphicRaycaster>();
        //
        _selectionPopup = GameModules.popupSystem.GetPopupObj(ModuleName.exploreEventPopup).GetComponent<UIEventSelectionPopup>();
        _exploreItemMove = GameModules.popupSystem.GetPopupObj(ModuleName.itemMovePopup).GetComponent<UIExploreItemMove>();
        //
        Transform obj = ShowTranS.Find("Icon");
        if (obj != null)
        {
            _iconEffect = ShowTranS.Find("Icon/Effect").GetComponent<SkeletonAnimation>();
            if (_iconEffect != null)
            {
                _renderers.Add(GameTools.GetObjRenderer(_iconEffect.gameObject));
                _iconEffect.AnimationState.Event += EventList;
            }
            //
            _eventButton = ShowTranS.Find("Button").GetComponent<Button>();
            _eventButton.onClick.AddListener(OnClickButton);
            UpdateEventButtonEnabled();
            //
            if (_eventType != WPEventType.Combat)
            {
                if (_isShowEventName)
                {
                    obj = ShowTranS.Find("Text");
                    if (obj != null)
                    {
                        _eventNameText = obj.gameObject;
                        _eventNameText.GetComponent<Text>().text = _eventAttribute.event_template.eventName;
                        _eventNameText.AddComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                    }
                }
                obj = ShowTranS.Find("Progress");
                if (obj != null)
                {
                    _eventProgress = obj.gameObject.AddComponent<UIEventProgress>();
                }
                //
                if (_alterParticles.Count > 0)
                {
                    _alterParticles[_alterParticles.Count - 1].Init(_sortingLayerName, _sortingOrder);
                }
                if (_eventAttribute.EventType == WPEventType.Trap)
                {
                    UpdateEventShowState(false);
                }
            }
        }
        //
        UpdateShow(_sortingLayerName, _sortingOrder);
    }

    /// <summary>
    /// 初始化位置
    /// </summary>
    private void InitPos()
    {
        //左边位置
        RectTransform parent = transform.parent.GetComponent<RectTransform>();
        RectTransform rect = transform.GetComponent<RectTransform>();
        _leftPos = rect.position;
        Vector3 _tempPos = rect.anchoredPosition;
        //中间位置
        _middlePos = rect.anchoredPosition;
        _middlePos.x = _middlePos.x - parent.sizeDelta.x / 2 + rect.sizeDelta.x / 2;
        _middlePos.y = rect.sizeDelta.y / 2 + _middlePos.y;
        rect.pivot = Vector2.one / 2;
        rect.anchorMax = Vector2.one / 2;
        rect.anchorMin = Vector2.one / 2;
        rect.anchoredPosition = _middlePos;
        _middlePos = rect.position;
        //右边位置
        _rightPos = rect.anchoredPosition; ;
        rect.pivot = Vector2.right;
        rect.anchorMax = Vector2.right;
        rect.anchorMin = Vector2.right;
        _rightPos.x = _rightPos.x - parent.sizeDelta.x / 2 + rect.sizeDelta.x / 2;
        _rightPos.y = _rightPos.y - rect.sizeDelta.y / 2;
        rect.anchoredPosition = _rightPos;
        _rightPos = rect.position;
        //还原位置
        rect.pivot = Vector2.zero;
        rect.anchorMax = Vector2.zero;
        rect.anchorMin = Vector2.zero;
        rect.anchoredPosition = _tempPos;
        //
        _leftPos = GameTools.WorldToScreenPoint(_leftPos, transform);
        _middlePos = GameTools.WorldToScreenPoint(_middlePos, transform);
        _rightPos = GameTools.WorldToScreenPoint(_rightPos, transform);
    }
    /// <summary>
    /// 初始化信息
    /// </summary>
    private void InfoInit(EventAttribute eventAttribute)
    {
        _event_Info = Event_infoConfig.GetEvent_Info(eventAttribute.event_template.eventType);
        _sortingLayerName = _event_Info.sortingLayer;
        _sortingOrder = _event_Info.sortingOrder;
        switch (eventAttribute.EventPosType)
        {
            case 1:
                _sortingOrder = 20;
                break;
            case 2:
                _sortingOrder = 20;
                break;
            case 3:
                _sortingOrder = 30;
                break;
            case 4:
                _sortingOrder = 10;
                break;
            default:
                _sortingOrder = 20;
                break;
        }
        //
        _eventAttribute = eventAttribute;
        eventIndex = eventAttribute.EventIndex;
        _eventID = eventAttribute.eventId;
        //
        _eventType = (WPEventType)_eventAttribute.event_template.eventType;
        //
        _showRect = ShowTranS.GetComponent<RectTransform>();
        _showRect.anchoredPosition += Vector2.up * 0 /*_event_Info.YValue*/;
    }

    //
    protected int eventIndex;
    //
    protected UIEventSelectionPopup _selectionPopup;
    protected EventAttribute _eventAttribute;
    protected WPVisitEventResult _wpVisitEventResult;
    protected UIEventProgress _eventProgress;
    //
    private Vector3 _leftPos;
    private Vector3 _middlePos;
    private Vector3 _rightPos;
    private bool _isStartDialog;
    private bool _isShowEventName;
    private bool _isBlock;
    private int _eventID;
    private WPEventType _eventType;
    private SkeletonAnimation _iconEffect;
    private List<UIAlterParticleSystemLayer> _alterParticles = new List<UIAlterParticleSystemLayer>();
    private List<Renderer> _renderers = new List<Renderer>();
    private List<Canvas> _canvas = new List<Canvas>();
    private List<int> _selectindexs = new List<int>();
    private List<Transform> _itemObjs = new List<Transform>();
    //
    private const string _bxName1Str = "Idle";
    private const string _bxName2Str = "Open";
    private const string _bxName3Str = "Failed";
    //
    private int _sum;
    private int _sum1;
    //
    private float _aspd = 1f;
    private string _sortingLayerName = "char";
    private int _sortingOrder = 21;
    private Vector2 _size;
    //
    private GameObject _eventNameText;
    private Canvas _baseCanvas;
    private Button _eventButton;
    private RectTransform _showRect;
    private Transform _showTranS;
    //
    private Transform _charTransform;
    //
    private UIExploreItemMove _exploreItemMove;
    private UIExploreEventPosDetection _eventPosDetection;
    private Event_info _event_Info;
    private Combat_config _combatConfig;
    private UIDialogPopup _dialogPopup;
}
