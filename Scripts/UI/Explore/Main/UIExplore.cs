using GameEventDispose;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// 探索界面
/// </summary>
public partial class UIExplore : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void CallBack();
    public delegate void CallBack1(object isFront);

    public CallBack1 OnBack;
    public CallBack OnStart;
    public CallBack OnClickCombat;

    //
    public float sizeValue = 36;
    public float aspdValue;
    public float xValue;
    public float x1Value;
    //


    /// <summary>
    /// 点击开始
    /// </summary>
    public void OnClickStart()
    {
        _exploreEvent.xValue = xValue;
        _exploreEvent.x1Value = x1Value;
        _exploreEvent.sizeValue = sizeValue;
        //
        _mapNameText.text = ExploreSystem.Instance.MapTemplate.mapName;
        _goldText.text = ScriptSystem.Instance.Gold.ToString();
        _manaText.text = ScriptSystem.Instance.Mana.ToString();
        ExploreSystem.Instance.PlayExplore();
        //
        _exploreChar.Init(TeamSystem.Instance.TeamAttribute.combatUnits[0], sizeValue);
        //
        UpdateScriptTimeShow();
        //
        AddEventListener();
        //
        CloseMask();
        _bigMapPopup.OpenUI();
        //
        _topTransform.gameObject.SetActive(true);
        _maskBgObj.SetActive(false);
    }


    private void EventVisitFail(string intro)
    {
        if (_IEEventVisitFail != null)
        {
            _IEEventVisitFail.Stop();
        }

        if (_visitFailCanvas != null)
        {
            DestroyImmediate(_visitFailCanvas.gameObject);
        }

        _IEEventVisitFail = new CoroutineUtil(IEventVisitFail(intro));
    }

    private IEnumerator IEventVisitFail(string intro)
    {
        _visitFailCanvas = ResourceLoadUtil.InstantiateRes(_visitFailObj, _visitFailTrans).GetComponent<CanvasGroup>();
        _visitFailCanvas.GetComponent<Text>().text = intro;
        _visitFailCanvas.gameObject.SetActive(true);
        while (_visitFailCanvas.alpha > 0.3f)
        {
            _visitFailCanvas.alpha -= Time.deltaTime * 0.4f;
            yield return null;
        }
        DestroyImmediate(_visitFailCanvas.gameObject);
    }


    /// <summary>
    /// 路点访问完成
    /// </summary>
    private void WPVisitOk()
    {
        //TODO 路点访问结束没有做
        return;
    }

    private void OnCallMoveEnd_Event()
    {
        //todo 事件移动结束未做
    }

    /// <summary>
    /// 更新威胁描述
    /// </summary>
    private void UpdateThreatIntroStr(int eventID)
    {

    }
    /// <summary>
    /// 更新剧本时间显示
    /// </summary>
    private void UpdateScriptTimeShow()
    {
        playTimeInfo = TimeUtil.GetPlayTimeInfo();
        _timeText.text = string.Format(m_timeFormat2, playTimeInfo.years, playTimeInfo.months, playTimeInfo.days);
    }

    /// <summary>
    /// 路点开始播放完成
    /// </summary>
    private void OnCallWayponitStartPlayComlete()
    {
        _dialogPopup.OnEnd = OnCallDialogColse;
        _dialogPopup.OpenUI(ExploreSystem.Instance.NowWPAttribute.wp_template.WPDialog);
        OpenMask();
    }


    /// <summary>
    /// 回调前置探索对话关闭
    /// </summary>
    private void OnCallDialogColse(object param)
    {
        CloseMask();
        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.SceneStartReady, (object)false);
        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.WPStart, (object)null);
    }

    private void OnCallExitEvent()
    {
        CloseMask();
        _exploreChar.PlayAction(CharModuleAction.Idle);
    }
    private void OnCallBackVisitReady(object param, Action action)
    {
        _eventBase = param as UIExploreEventBase;
        if (ExploreSystem.Instance.NowEventIndex == _eventBase.EventIndex)
        {
            return;
        }
        StopAllAutoMove(true);
        if (_IEVisitMove != null)
        {
            _IEVisitMove.Stop();
        }
        _IEVisitMove = new CoroutineUtil(OnIVisitMove(_eventBase, action));
    }

    /// <summary>
    /// 自动访问
    /// </summary>
    private void OnCallAutoVisit(object param)
    {
        _isClickButton = false;
        if (_IEVisitMove != null)
        {
            _IEVisitMove.Stop();
        }
        StopAllAutoMove(true);
        if (param == null)
        {
            return;
        }
        (param as UIExploreEventBase).ImmediateVisit();
    }
    /// <summary>
    /// 阻挡
    /// </summary>
    /// <param name="param"></param>
    private void OnCallBlock(object param)
    {
        StopAllAutoMove(true);
    }

    /// <summary>
    /// 检查事件位置
    /// </summary>
    private void CheckEventPos()
    {
        if (!_isCheckEventPos)
        {
            return;
        }
        //todo 检查关键事件是否完成
        _isCheckEventPos = false;
        UpdateGoButtonShow(ExploreSystem.Instance.IsKeyAllCall());
    }

    private void OnCallEventInitOK()
    {

    }


    private void LateUpdate()
    {
        CheckEventPos();
    }

    // 当 MonoBehaviour 将被销毁时调用此函数
    private void OnDestroy()
    {
        RemoveEventListener();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isClickButton)
        {
            return;
        }
        if (!_isLeft)
        {
            OnPointerUpRight();
        }
        else
        {
            OnPointerUpLeft();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isClickButton)
        {
            return;
        }
        _isLeft = eventData.position.x < _exploreChar.CharScreenPos.x;
        _isClickLeftButton = _isLeft;
        if (!_isLeft)
        {
            _exploreEvent.ClickRight();
            if (IsHaveBlockEvent())
            {
                if (_IEUpdateBanMoveHintShow != null)
                {
                    _IEUpdateBanMoveHintShow.Stop();
                }
                _IEUpdateBanMoveHintShow = new CoroutineUtil(IEUpdateBanMoveHintShow(false));
                return;
            }
            OnPointerDownRight();
        }
        else
        {
            _exploreEvent.ClickLeft();
            OnPointerDownLeft();
        }
    }


    private CoroutineUtil _IEUpdateBanMoveHintShow;

    private IEnumerator IEUpdateBanMoveHintShow(bool isLeft)
    {
        _leftBanMove.SetActive(isLeft);
        _rightBanMove.SetActive(!isLeft);
        int sum = 0;
        while (sum < 60)
        {
            sum++;
            yield return null;
        }
        _leftBanMove.SetActive(false);
        _rightBanMove.SetActive(false);
    }



    /// <summary>
    /// 是否有阻挡事件
    /// </summary>
    /// <returns></returns>
    private bool IsHaveBlockEvent()
    {
        return _exploreEvent.IsHaveBlockEvent();
    }

    //

    private bool _isLeft;
    private bool _isClickButton;
    //
    private CoroutineUtil _IEEventVisitFail;
    private CanvasGroup _visitFailCanvas;
    //
    private bool _isFront;
    //
    private UIExploreEventBase _eventBase;
    private bool _isCheckEventPos;
    //
    private readonly string m_timeFormat2 = "{0}年{1}月{2}日";
    private TimeUtil.PlayTimeInfo playTimeInfo;
}