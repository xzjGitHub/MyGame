using GameEventDispose;
using System;
using System.Collections;
using UnityEngine;

public partial class UIExplore : MonoBehaviour
{

    /// <summary>
    /// 抬起左边
    /// </summary>
    private void OnPointerUpLeft()
    {
        StopAllAutoMove();
    }
    /// <summary>
    /// 抬起右边
    /// </summary>
    private void OnPointerUpRight()
    {
        StopAllAutoMove();
    }

    /// <summary>
    /// 按下左边
    /// </summary>
    private void OnPointerDownLeft()
    {
        if (_IEPointerDownLeft != null)
        {
            _IEPointerDownLeft.Stop();
        }
        _IEPointerDownLeft = new CoroutineUtil(IEPointerDownLeft());
    }
    /// <summary>
    /// 按下右边
    /// </summary>
    private void OnPointerDownRight()
    {
        if (_IEPointerDownRight != null)
        {
            _IEPointerDownRight.Stop();
        }
        _IEPointerDownRight = new CoroutineUtil(IEPointerDownRight());
    }
    private IEnumerator IEPointerDownLeft()
    {
        StopAllAutoMove(true);
        yield return null;
        _isCharRun = _exploreEvent.AutoMove(_moveSpeed, false);
        _exploreChar.ClickButtonUpdateMove(_moveSpeed * (_isCharRun ? 0.4f : 1), _isCharRun, true);
        if (_isCharRun)
        {
            _exploreMap.MoveMap(_moveSpeed, true);
        }
    }
    private IEnumerator IEPointerDownRight()
    {
        StopAllAutoMove(true);
        yield return null;
        _isCharRun = _exploreEvent.AutoMove(_moveSpeed);
        _exploreChar.ClickButtonUpdateMove(_moveSpeed * (_isCharRun ? 0.4f : 1), _isCharRun, false);
        if (_isCharRun)
        {
            _exploreMap.MoveMap(_moveSpeed, false);
        }
    }
    private void StopAllAutoMove(bool isVisitMove = false)
    {
        if (isVisitMove)
        {
            if (_IEVisitMove != null)
            {
                _IEVisitMove.Stop();
            }
        }
        _exploreEvent.CancelAutoMove();
        _exploreChar.CancelAutoMove();
        _exploreMap.StopMoveMap();
    }
    /// <summary>
    /// 回调事件自动移动结束
    /// </summary>
    private void OnCallEventAutoMoveEnd()
    {
        _exploreChar.ClickButtonEventMoveEnd(_moveSpeed);
        _exploreMap.StopMoveMap();
    }

    /// <summary>
    /// 回调事件点击按钮移动结束
    /// </summary>
    private void OnCallClickButtonMoveEnd()
    {
        _exploreEvent.UpdateMoveSpeed(_moveSpeed);
        _exploreMap.UpdateMoveSpeed(_moveSpeed);
        if (_IEUpdateBanMoveHintShow != null)
        {
            _IEUpdateBanMoveHintShow.Stop();
        }
        _IEUpdateBanMoveHintShow = new CoroutineUtil(IEUpdateBanMoveHintShow(_isClickLeftButton));
    }


    /// <summary>
    /// 开始场景加载
    /// </summary>
    private void StartSceneLoad()
    {
        OpenMask();
        _exploreChar.UpdateShow();
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatUIOperationType.PlayLeftAction, (object)CharModuleAction.Idle);
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatUIOperationType.SetLeftPos, (object)Vector3.zero);
        _exploreEvent.OnClickPullout = OnCallClickPullout;
        _exploreEvent.OnMoveEnd = OnCallMoveEnd_Event;
        //
        Vector2 size = _exploreEvent.Init(_exploreChar.CharTrans);
        _exploreMap.InitMapInfo(size);
        _exploreEvent.SetInitPos();
    }
    /// <summary>
    /// 场景准备
    /// </summary>
    /// <param name="readyTime"></param>
    /// <param name="sceneMimTime"></param>
    /// <returns></returns>
    private IEnumerator ISceneReady(float readyTime, float sceneMimTime)
    {
        //_exploreEvent.UpdateEventShow(false);
        _exploreChar.InitOriginPos(_moveSpeed);
        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.SceneStartMove, (object)null);
        while (_exploreChar.IsInitStartPos)
        {
            yield return null;
        }
        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.SceneMoveEnd, (object)null);
        if (_isFirstHint)
        {
            new CoroutineUtil(UpdateMoveHintShow());
        }
        yield break;
        _bigMapPopup.UpdateShow(false);
    }


    private IEnumerator UpdateMoveHintShow()
    {
        _moveHintObj.SetActive(true);
        int sum = 0;
        while (sum<120)
        {
            sum++;
            yield return null;
        }
        _isFirstHint = false;
        _moveHintObj.SetActive(false);
    }

    /// <summary>
    /// 重置场景
    /// </summary>
    /// <param name="resetTime"></param>
    /// <param name="maxPosTime"></param>
    /// <returns></returns>
    private IEnumerator IResetScene(float resetTime, float maxPosTime)
    {
        _bigMapPopup.UpdateShow(false);
        _exploreChar.InitEndPos(_moveSpeed);
        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.SceneStartMove, (object)null);
        // if (ExploreSystem.Instance.IsForkRoad)
        // {
        UpdateDoorShow(true);
        //  }
        while (_exploreChar.IsInitEndPos)
        {
            yield return null;
        }
        //   _exploreEvent.SetInitPos();
        UpdateDoorShow(false);
        // _exploreEvent.UpdateEventShow(false);

        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.SceneMoveEnd, (object)null);
        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.SceneEnd, (object)null);
        yield break;
    }

    /// <summary>
    /// 回调点击事件
    /// </summary>
    /// <param name="eventTrans">事件</param>
    /// <param name="action">执行的操作</param>
    private IEnumerator OnIVisitMove(UIExploreEventBase eventBase, Action action)
    {
        Vector2 charPos = _exploreChar.CharScreenPos;
        int posType;
        Vector2 nowEventScreenPos = eventBase.GetRecentlyPos(charPos, out posType);
        if (posType != 0 && Mathf.Abs(nowEventScreenPos.x - charPos.x) / GameTools.WidthRatio > 50f/* _eventBase.Size.x*/)
        {
            bool isLeft = nowEventScreenPos.x < charPos.x;
            float distance = GameTools.GetVector3Distance(nowEventScreenPos, charPos);
            float mapUsable = _exploreEvent.GetEventUsableMoveLength(isLeft);
            float allMoveLength = mapUsable >= distance / 2 ? distance / 2 : mapUsable;
            float charMoveLength = distance - allMoveLength * 2;
            //同时移动
            float moveTime = allMoveLength * 2 / _moveSpeed;
            moveTime = moveTime / GameTools.WidthRatio;
            float tempTime = 0;
            while (tempTime < moveTime)
            {
                _exploreMap.MoveMap(_moveSpeed / 2, isLeft);
                _exploreEvent.AutoMove(_moveSpeed / 2, !isLeft);
                _exploreChar.AutoMove(_moveSpeed / 2, !isLeft);
                tempTime += Time.deltaTime;
                yield return null;
            }
            StopAllAutoMove();
            //角色移动
            moveTime = charMoveLength / _moveSpeed;
            moveTime = moveTime / GameTools.WidthRatio;
            tempTime = 0;
            while (tempTime < moveTime)
            {
                _exploreChar.AutoMove(_moveSpeed, !isLeft);
                tempTime += Time.deltaTime;
                yield return null;
            }
            StopAllAutoMove();
        }
        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.SceneMoveEnd, (object)null);
        if (action != null)
        {
            action();
        }
    }


    /// <summary>
    /// 回调撤退
    /// </summary>
    private void OnCallClickPullout(object param)
    {
        OnClickBack();
    }
    //
    private CoroutineUtil _IESceneReady;
    private CoroutineUtil _IEResetScene;
    private CoroutineUtil _IEVisitMove;
    private CoroutineUtil _IEPointerDownLeft;
    private CoroutineUtil _IEPointerDownRight;
    //
    private bool _isHaveBlock;
    private bool _isCharRun;
    private int _nowSceneIndex;
    private float _moveSpeed = 360f;
    //
    private bool _isClickLeftButton;
}