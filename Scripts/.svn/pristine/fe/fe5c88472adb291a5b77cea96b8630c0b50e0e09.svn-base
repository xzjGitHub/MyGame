using GameEventDispose;
using Spine;
using System;
using UnityEngine;

public class UIExploreChar : CharSkeletonOperation
{
    public Action OnClickButtonMovEnd;

    public Vector2 CharScreenPos { get { return GameTools.WorldToScreenPoint(_charObj); } }

    public bool IsInitStartPos { get { return _isInitStartPos; } }

    public bool IsInitEndPos { get { return _isInitEndPos; } }

    public Transform CharTrans { get { return charTrans; } }

    public void UpdateMoveDirection(bool isLeft = true)
    {
        charTrans.localScale = isLeft ? _leftScale : _rightScale;
    }



    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(CombatUnit combatUnit, float sizeValue = 36)
    {
        charTrans = transform.Find("Char");
        _charObj = charTrans;
        _charRPack = CharRPackConfig.GeCharShowTemplate(combatUnit.charAttribute.char_template.templateID);
        //
        // charTrans.localScale = Vector3.one * sizeValue;
        charTrans.localScale = Vector3.one * 36;
        //
        base.InitAdd(combatUnit);
        base.Init(LoadSkeletonRes(_charRPack.charRP, charTrans, 21));
        InitPos();
        //
        charTrans.position = _initPos;
        //
        EventDispatcher.Instance.CombatEvent.AddEventListener<CombatUIOperationType, object>(EventId.CombatEvent, OnCombatUIEvent);
        EventDispatcher.Instance.ExploreEvent.AddEventListener<ExploreEventType, object>(EventId.ExploreEvent, OnExploreEvent);
    }
    /// <summary>
    /// 更新显示
    /// </summary>
    /// <param name="isShow"></param>
    public void UpdateShow(bool isShow = true)
    {
        gameObject.SetActive(isShow);
    }
    /// <summary>
    /// 播放动作
    /// </summary>
    public void PlayAction(CharModuleAction charModuleAction, bool isLeft = false)
    {
        PlayAction(charModuleAction);
    }
    /// <summary>
    /// 初始化起点
    /// </summary>
    public Vector3 InitOriginPos(float speed)
    {
        PlayAction(CharModuleAction.Run);
        UpdateMoveDirection(false);
        _moveSpeed = speed;
        _isInitStartPos = true;
        _isInitEndPos = false;
        _isClickButtonMove = false;
        _isAtuoIdle = true;
        _movePos = _originPos;
        return _movePos;
    }

    /// <summary>
    /// 初始化终点
    /// </summary>
    public Vector3 InitEndPos(float speed)
    {
        PlayAction(CharModuleAction.Run);
        UpdateMoveDirection(false);
        _moveSpeed = speed;
        _isInitStartPos = false;
        _isInitEndPos = true;
        _isClickButtonMove = false;
        _isAtuoIdle = true;
        _movePos = _endPos;
        return _movePos;
    }

    /// <summary>
    /// 取消自动移动
    /// </summary>
    public void CancelAutoMove()
    {
        _isInitStartPos = false;
        _isInitEndPos = false;
        _isClickButtonMove = false;
        _isAutoMove = false;
        PlayAction(CharModuleAction.Idle);
    }

    /// <summary>
    /// 自动移动
    /// </summary>
    public void AutoMove(float moveSpeed, bool isLeft = true)
    {
        _isAutoMove = true;
        _leftMove = isLeft;
        _moveSpeed = moveSpeed;
        PlayAction(CharModuleAction.Run);
        UpdateMoveDirection(!isLeft);
    }

    /// <summary>
    /// 点击按钮更新移动
    /// </summary>
    /// <param name="moveSpeed"></param>
    /// <param name="isLeft"></param>
    public void ClickButtonUpdateMove(float moveSpeed, bool isEventMove, bool isLeft)
    {
        _moveSpeed = moveSpeed;
        UpdateMoveDirection(isLeft);
        PlayAction(CharModuleAction.Run);
        _isInitStartPos = false;
        _isInitEndPos = false;
        _isClickButtonMove = true;
        _clickLeft = isLeft;
        _isAtuoIdle = !isEventMove;
        if (!isEventMove)
        {
            _movePos = isLeft ? _originPos : _terminusPos;
            return;
        }
        _movePos = _goldPos;
    }
    /// <summary>
    /// 点击按钮事件移动结束
    /// </summary>
    public void ClickButtonEventMoveEnd(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
        _isClickButtonMove = true;
        PlayAction(CharModuleAction.Run);
        _movePos = _clickLeft ? _originPos : _terminusPos;
        _isAtuoIdle = true;
    }

    /// <summary>
    /// 初始化位置
    /// </summary>
    private void InitPos()
    {
        _leftPos = GameTools.LocalToWorldPoint(_leftPos, _charObj.parent);
        _rightPos = GameTools.LocalToWorldPoint(_rightPos, _charObj.parent);
        //初始位置
        _initPos = new Vector3(_initPosX, 0);
        _initPos = GameTools.ScreenToWorldPoint(_initPos, _charObj);
        _initPos.y = _charObj.position.y;
        _initPos.z = _charObj.position.z;
        //起点
        _originPos = new Vector3(_originPosX, 0);
        _originPos = GameTools.ScreenToWorldPoint(_originPos, _charObj);
        _originPos.y = _charObj.position.y;
        _originPos.z = _charObj.position.z;
        //黄金点
        _goldPos = new Vector3(_goldPosX, 0);
        _goldPos = GameTools.ScreenToWorldPoint(_goldPos, _charObj);
        _goldPos.y = _charObj.position.y;
        _goldPos.z = _charObj.position.z;
        //结束点
        _endPos = new Vector3(_endPosX, 0);
        _endPos = GameTools.ScreenToWorldPoint(_endPos, _charObj);
        _endPos.y = _charObj.position.y;
        _endPos.z = _charObj.position.z;
        //终点
        _terminusPos = new Vector3(_terminusX, 0);
        _terminusPos = GameTools.ScreenToWorldPoint(_terminusPos, _charObj);
        _terminusPos.y = _charObj.position.y;
        _terminusPos.z = _charObj.position.z;
    }
    /// <summary>
    /// 更新自动移动
    /// </summary>
    private void UpdateAutoMove()
    {
        if (!_isAutoMove)
        {
            return;
        }
        charTrans.position = Vector3.MoveTowards(charTrans.position, !_leftMove ? _leftPos : _rightPos, _moveSpeed * Time.deltaTime / 100f);
        //   charTrans.localPosition =  Vector3.MoveTowards(charTrans.localPosition, !_leftMove?_leftPos:_rightPos, _moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 播放动作
    /// </summary>
    private void PlayAction(CharModuleAction charModuleAction)
    {
        Spine.AnimationState.TrackEntryDelegate trackentry = null;

        base.PlayAction(charModuleAction, trackentry);
    }

    /// <summary>
    /// 动作播放结束
    /// </summary>
    /// <param name="trackentry"></param>
    private void ActionPlayEnd(TrackEntry trackentry)
    {
        skeletonAnimation.state.Complete -= ActionPlayEnd;
        base.PlayAction(CharModuleAction.Idle, null);
    }

    /// <summary>
    /// 检查是否移动结束
    /// </summary>
    /// <returns></returns>
    private bool IsCheekMoveEnd()
    {
        return Vector3.SqrMagnitude(_movePos - charTrans.position) <= 0.000001f ? false : true;
    }

    /// <summary>
    /// 更新移动
    /// </summary>
    private void UpdateMove(float speed)
    {
        charTrans.position = Vector3.MoveTowards(charTrans.position, _movePos, speed * Time.deltaTime / 100f);
    }

    /// <summary>
    /// 战斗阶段事件
    /// </summary>
    private void OnCombatUIEvent(CombatUIOperationType arg1, object arg2)
    {
        switch (arg1)
        {
            case CombatUIOperationType.PlayLeftAction:
                PlayAction((CharModuleAction)arg2, null);
                //发送队伍动作
                break;
            case CombatUIOperationType.SetLeftPos:
                //设置位置
                transform.localPosition = (Vector3)arg2;
                Fade();
                break;
            case CombatUIOperationType.SetRightPos:
                //设置召唤位置
                break;
            case CombatUIOperationType.UpdateRightMove:
                //更新Npc移动
                break;
        }
    }

    /// <summary>
    /// 探索事件
    /// </summary>
    private void OnExploreEvent(ExploreEventType _type, object param)
    {
        switch (_type)
        {
            case ExploreEventType.ExploreFinish:
                break;
            case ExploreEventType.OneselfMove:
                PlayAction(CharModuleAction.Run, null);
                break;
            case ExploreEventType.OneselfMoveFinish:
                break;
            case ExploreEventType.SceneStartMove:
                break;
            case ExploreEventType.SceneMoveEnd:
                break;
            case ExploreEventType.VisiteEventMoveEnd:
                break;
        }
    }

    private void Update()
    {
        UpdateAutoMove();
        if (_isInitStartPos || _isInitEndPos || _isClickButtonMove)
        {
            if (!IsCheekMoveEnd())
            {
                if (_isAtuoIdle)
                {
                    PlayAction(CharModuleAction.Idle);
                }
                if (_isInitStartPos)
                {
                    UpdateMoveDirection(false);
                }
                if (_isInitEndPos)
                {
                    _charObj.position = _initPos;
                    UpdateMoveDirection(false);
                }

                if (_isClickButtonMove)
                {
                    if (OnClickButtonMovEnd != null)
                    {
                        OnClickButtonMovEnd();
                    }
                    _charObj.position = _movePos;
                }
                _isInitStartPos = false;
                _isInitEndPos = false;
                _isClickButtonMove = false;
                return;
            }
            UpdateMove(_moveSpeed);
        }
    }
    private void OnDestroy()
    {
        EventDispatcher.Instance.CombatEvent.RemoveEventListener<CombatUIOperationType, object>(EventId.CombatEvent, OnCombatUIEvent);
        EventDispatcher.Instance.ExploreEvent.RemoveEventListener<ExploreEventType, object>(EventId.ExploreEvent, OnExploreEvent);
    }
    //
    private Transform _charObj;
    private Transform charTrans;
    private CharRPack _charRPack;
    //
    private bool _isAtuoIdle;
    private bool _isInitStartPos;
    private bool _isInitEndPos;
    private bool _leftMove;
    private bool _isAutoMove;
    private bool _isClickButtonMove;
    private bool _clickLeft;
    //
    private float _moveSpeed = 150f;
    private readonly float _initPosX = -100f;
    private readonly float _originPosX = 200f;
    private readonly float _goldPosX = 500f;
    private readonly float _terminusX = Screen.width - 100f;
    private readonly float _endPosX = Screen.width + 200f;
    private Vector3 _goldPos;
    private Vector3 _initPos;
    private Vector3 _originPos;
    private Vector3 _terminusPos;
    private Vector3 _endPos;
    private Vector3 _movePos;
    private Vector3 _leftScale = new Vector3(-1, 1, 1) * 36f;
    private Vector3 _rightScale = Vector3.one * 36f;
    private Vector3 _leftPos = new Vector2(-GameTools.CanvasWidth * 2, -180);
    private Vector3 _rightPos = new Vector2(GameTools.CanvasWidth * 2, -180);
}
