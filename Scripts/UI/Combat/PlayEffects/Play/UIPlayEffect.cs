using System;
using System.Collections;
using Spine;
using UnityEngine;

/// <summary>
/// 播放特效
/// </summary>
public class UIPlayEffect : MonoBehaviour
{
    public Action<UnityEngine.Object> OnPlayEnd;

    public delegate void Callback1(int charIndex, int stateIndex);

    public int param1;
    public int param2;

    public Callback1 OnPlayEnd1;

    public int waitFPS = 3;
    public float rotateX = 450; // 最大X轴旋转角度  
    public float speed = 100; // 速度  
    public float height = 100; //最大高度   
    public string effectName;

    public bool IsPlayEnd
    {
        get { return isPlayEnd; }
    }


    public bool IsAutoDestory
    {
        get
        {
            return isAutoDestory;
        }
    }

    public EffectInfo Info
    {
        get
        {
            return _info;
        }
    }


    /// <summary>
    /// 播放特效
    /// </summary>
    public void PlayEffect(EffectInfo info, float _TrackTime, bool isAutoDestory = true)
    {
        this.isAutoDestory = isAutoDestory;
        trackTime = _TrackTime;
        StartPlayEffect(info);
    }

    /// <summary>
    /// 播放特效和结果
    /// </summary>
    public void PlayCommonEffect(EffectInfo info, CommonEffectConfig commonEffect, UICharActionOperation actionOperation, float _TrackTime = 1, bool isAutoDestory = true)
    {
        this.isAutoDestory = isAutoDestory;
        this.actionOperation = actionOperation;
        this.commonEffect = commonEffect;
        isOrigin = commonEffect.origin != string.Empty || commonEffect.origin.Length > 2;
        trackTime = _TrackTime;
        StartPlayEffect(info);
    }

    /// <summary>
    /// 销毁资源
    /// </summary>
    public void DestroyRes()
    {
        DestroyImmediate(this);
    }

    /// <summary>
    /// 开始播放效果
    /// </summary>
    /// <param name="info"></param>
    private void StartPlayEffect(EffectInfo info)
    {
        if (info == null || info.SkeletonAn == null)
        {
            PlayEnd(null);
            return;
        }
        _info = info;
        //
        info.SkeletonAn.gameObject.SetActive(false);
        PalySkeleton(info);
        //是否需要移动
        if (!info.isMove)
        {
            return;
        }

        switch (info.moveType)
        {
            case EffectMoveType.Teleportation:
                info.SkeletonAn.transform.localPosition = info.endLocalPos;
                PlayEnd(null);
                break;
            case EffectMoveType.Line:
                new CoroutineUtil(UpdateLineMove(info));
                break;
            case EffectMoveType.Parabola:
                new CoroutineUtil(UpdateParabolaMove(info));
                break;
        }
    }

    /// <summary>
    /// 更新直线移动
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateLineMove(EffectInfo info)
    {
        waitFPS = info.waitFPS;
        float time = 0;
        Transform obj = info.SkeletonAn.transform;
        Vector3 endPos = info.endLocalPos/* / 36f*/;
        obj.eulerAngles = new Vector3(obj.eulerAngles.x, obj.eulerAngles.y, GetZ(obj, endPos, obj.parent.parent.parent.name == LeftTransfromName));
        moveSpeed = info.moveSpeed;
        //  info.endPos += Vector3.right * 150f;
        //
        float _temp = 1f;
        //根据时间控制速度
        info.SkeletonAn.timeScale = _temp;
        //float _time = info.SkeletonAn.skeleton.Data.FindAnimation(info.effectName).Duration;
        //aspd = Vector3.Distance(_obj.SkeletonAn.transform.localPosition, endPos) / _time / _temp;
        //定速移动
        while (Vector3.SqrMagnitude(info.SkeletonAn.transform.localPosition - endPos) > 1f)
        {
            time += Time.deltaTime;
            //根据时间控制速度
            info.SkeletonAn.transform.localPosition = Vector3.MoveTowards(info.SkeletonAn.transform.localPosition, endPos, moveSpeed * Time.deltaTime);
            //_obj.SkeletonAn.transform.localPosition = Vector3.MoveTowards(_obj.SkeletonAn.transform.localPosition, endPos, moveSpeed * Time.deltaTime);
            //定速移动
            //   _obj.SkeletonAn.transform.localPosition = Vector3.Lerp(_obj.SkeletonAn.transform.localPosition, endPos, time * moveSpeed);
            //    _obj.SkeletonAn.transform.eulerAngles = new Vector3(0, 0, GetZ(_obj.SkeletonAn.transform, endPos));
            yield return null;
        }
        //   yield return null;
        //  yield return null;
        //
        //     info.SkeletonAn.transform.position = endPos;
        new CoroutineUtil(PlayEnd());
    }

    /// <summary>
    /// 获得旋转方向Z轴
    /// </summary>
    private float GetZ(Transform _from, Vector3 endPos, bool isLeft = true)
    {
        _from.rotation = Quaternion.FromToRotation(Vector3.forward, endPos - _from.localPosition);
        return isLeft ? -_from.eulerAngles.z : _from.eulerAngles.z;
    }

    /// <summary>
    /// 更新抛物线移动
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateParabolaMove(EffectInfo info)
    {
        _startPos = Vector3.zero;
        _stopPos = Vector3.zero;
        // 计算起始位置到目标位置的角度
        _angleToStop = GetAngleToStop(_startPos, _stopPos);
        _curDistance = 0;
        // 计算总距离  
        Vector3 v = _stopPos - _startPos;
        _totalDistance = Mathf.Sqrt(v.x * v.x + v.z * v.z);
        // 设置单前X，Y轴的旋转角度  
        Vector3 rotation = transform.eulerAngles;
        if (rotateX > 0)
        {
            rotation.x = -rotateX;
        }
        rotation.y = _angleToStop;

        //    transform.eulerAngles = rotation;
        _curRotation = rotation;
        //
        bool _isFiring = true;
        while (_isFiring)
        {
            _isFiring = UpdateParabolaPos();
            yield return null;
        }
        //
        info.SkeletonAn.transform.localPosition = info.endLocalPos;
        PlayEnd(null);
    }

    /// <summary>
    /// 播放骨骼
    /// </summary>
    /// <param name="info"></param>
    private void PalySkeleton(EffectInfo info)
    {
        try
        {
            info.SkeletonAn.AnimationState.Event += SkeletonAnimationEventList;
            info.SkeletonAn.gameObject.SetActive(true);
            info.SkeletonAn.transform.position = info.startWorldPos;
            if (!info.isMove)
            {
                info.SkeletonAn.transform.localPosition = info.endLocalPos;
                info.SkeletonAn.state.Complete += PlayEnd;
            }
            SkeletonTool.SetSkeletonLayer(_info);
            SkeletonTool.PlayAnimation(info.SkeletonAn, info.effectName, info.isLoop, trackTime);
            effectName = info.effectName;
        }
        catch (Exception)
        {
            return;
        }

    }


    /// <summary>
    /// 骨骼动画事件列表
    /// </summary>
    private void SkeletonAnimationEventList(TrackEntry trackEntry, Spine.Event e)
    {
        switch (e.Data.Name)
        {
            case "end":
                isEnd = true;
                if (OnPlayEnd1 != null)
                {
                    OnPlayEnd1(param1, param2);
                }
                if (OnPlayEnd != null)
                {
                    OnPlayEnd(this);
                }
                break;
        }
    }

    /// <summary>
    /// 播放结束
    /// </summary>
    /// <param name="trackentry"></param>
    private void PlayEnd(TrackEntry trackentry)
    {
        new CoroutineUtil(PlayEnd());
        return;

        //立即销毁资源
        if (_info != null && _info.SkeletonAn != null)
        {
            _info.SkeletonAn.state.Complete -= PlayEnd;
            if (isAutoDestory)
            {
                Destroy(_info.SkeletonAn.gameObject);
            }
        }
        isPlayEnd = true;
        if (OnPlayEnd1 != null)
        {
            OnPlayEnd1(param1, param2);
        }

        if (OnPlayEnd != null)
        {
            OnPlayEnd(this);
        }
    }
    /// <summary>
    /// 播放结束
    /// </summary>
    private IEnumerator PlayEnd()
    {
        if (!isEnd)
        {
            if (OnPlayEnd1 != null)
            {
                OnPlayEnd1(param1, param2);
            }
            for (int i = 0; i < waitFPS; i++)
            {
                yield return null;
            }
            if (OnPlayEnd != null)
            {
                OnPlayEnd(this);
            }
        }
        isPlayEnd = true;
        //立即销毁资源
        if (_info != null && _info.SkeletonAn != null)
        {
            _info.SkeletonAn.state.Complete -= PlayEnd;
            if (isAutoDestory)
            {
                Destroy(_info.SkeletonAn.gameObject);
            }
        }

    }



    private void Update()
    {
        if (!isOrigin)
        {
            return;
        }

        if (_info == null || _info.SkeletonAn == null)
        {
            return;
        }

        posVector3 = _startPos;
        switch (commonEffect.origin)
        {
            case SkeletonTool.CharCenterName:
                posVector3 = actionOperation.CharCenterPos;
                break;
            case SkeletonTool.HitBoneName:
                posVector3 = actionOperation.HitPos;
                break;
            case SkeletonTool.WeaponName:
                posVector3 = actionOperation.WeaponPos;
                break;
            default:
                posVector3 = actionOperation.RootPos;
                break;
        }
        _info.SkeletonAn.transform.localPosition = _info.PosAmend(posVector3);

    }

    /// <summary>
    /// 判断是否到达
    /// </summary>
    private bool IsArrived()
    {
        return _curDistance >= _totalDistance;
    }
    /// <summary>
    /// 更新位置
    /// </summary>
    /// <returns></returns>
    private bool UpdateParabolaPos()
    {
        // 更新当前位置  
        transform.localPosition = GetNextPos();
        // 如果到达了目标位置就停止
        return !IsArrived();
    }
    /// <summary>
    /// 计算起始位置到目标位置的角度  
    /// </summary>
    private float GetAngleToStop(Vector3 startPos, Vector3 stopPos)
    {
        stopPos.x -= startPos.x;
        stopPos.z -= startPos.z;

        float deltaAngle = 0;
        if (stopPos.x == 0 && stopPos.z == 0)
        {
            return 0;
        }
        else if (stopPos.x > 0 && stopPos.z > 0)
        {
            deltaAngle = 0;
        }
        else if (stopPos.x > 0 && stopPos.z == 0)
        {
            return 90;
        }
        else if (stopPos.x > 0 && stopPos.z < 0)
        {
            deltaAngle = 180;
        }
        else if (stopPos.x == 0 && stopPos.z < 0)
        {
            return 180;
        }
        else if (stopPos.x < 0 && stopPos.z < 0)
        {
            deltaAngle = -180;
        }
        else if (stopPos.x < 0 && stopPos.z == 0)
        {
            return -90;
        }
        else if (stopPos.x < 0 && stopPos.z > 0)
        {
            deltaAngle = 0;
        }

        float angle = Mathf.Atan(stopPos.x / stopPos.z) * Mathf.Rad2Deg + deltaAngle;
        return angle;
    }
    /// <summary>
    /// 获得下一个位置
    /// </summary>
    private Vector3 GetNextPos()
    {
        // 计算x.z轴移动向量，然后再把它们乘移动距离，这样就能移动到下一个位置  
        float delta = speed * Time.deltaTime;
        float deltaX = Mathf.Sin(_angleToStop * Mathf.Deg2Rad);
        float deltaZ = Mathf.Cos(_angleToStop * Mathf.Deg2Rad);
        //
        Vector3 _curPos = Vector3.zero;
        _curPos.x += deltaX * delta;
        _curPos.z += deltaZ * delta;
        // 增加当前距离，用来判断是否到达到终点了  
        _curDistance += delta;
        float deltaDistance;
        _curPos.y = GetParabolaXY(_startPos, _stopPos, height, _curDistance, out deltaDistance);
        //// 旋转X轴
        //if (rotateX > 0)
        //{
        //    _curRotation.x = -rotateX * (1 + -2 * deltaDistance);
        //    // transform.eulerAngles = _curRotation;
        //}
        return _curPos;
    }
    /// <summary>
    /// 得到抛物线XY
    /// </summary>
    private float GetParabolaXY(Vector3 _startPos, Vector3 _endPos, float maxY, float _curDistance, out float deltaDistance)
    {
        float vX, vZ, a, b, apex, deltaHeight, y;
        vX = (_endPos - _startPos).x;
        vZ = (_endPos - _startPos).z;
        //
        a = -1;
        b = Mathf.Sqrt(vX * vX + vZ * vZ);
        apex = b / 2;
        deltaHeight = 1 / ((-apex) * (apex - b) / maxY);
        deltaDistance = _curDistance / b;
        y = 0/*x * (_endPos.y - _startPos.y) + _startPos.y*/;
        y += deltaHeight * (a * (_curDistance * _curDistance) + b * _curDistance);
        return y;
    }

    private Vector3 _startPos, _stopPos;    // 起始位置，目标位置  
    private float _angleToStop;     // 从起始点到目标点的角度
    private float _totalDistance, _curDistance;  // 总距离， 当前距离  
    private Vector3 _curRotation; // 当前的旋转角度  
    private Vector3 posVector3;

    private float moveSpeed = 60f;
    //
    private CommonEffectConfig commonEffect;
    private UICharActionOperation actionOperation;
    private EffectInfo _info;
    private const string LeftTransfromName = "Left";
    //
    private bool isOrigin;
    private bool isPlayEnd;
    private float trackTime;
    private readonly bool isMove;
    private bool isEnd;
    //
    public object param;
    private bool isAutoDestory = true;


}
