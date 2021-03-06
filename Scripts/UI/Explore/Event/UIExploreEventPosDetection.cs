﻿using UnityEngine;

/// <summary>
/// 探索事件位置检测
/// </summary>
public class UIExploreEventPosDetection : MonoBehaviour
{
    public bool IsEnterScreen { get { return _isEnterScreen; } }

    public delegate void CallBack();

    public CallBack OnBlock;
    public CallBack OnCanVisit;
    public CallBack OnAutoVisit;
    public CallBack OnAutoAbandonVisit1;
    public CallBack OnAutoAbandonVisit2;
    public CallBack OnEnterScreen;
    public CallBack OnShow1;

    private void Start()
    {
        float temp = GameTools.CanvasWidth / GameTools.SceneDefaultWidth;
        float tempOffset = GameTools.CanvasWidth - GameTools.SceneDefaultWidth / 2 * temp;
        //
        _objWidth = transform.GetComponent<RectTransform>().rect.width;
        _show1Width = _offset1 * temp + tempOffset;
        _canVisitWidth = _offset1 * temp + tempOffset;
        _autoVisitWidth = tempOffset - 100 * temp;
        _autoAbandonVisit1 = (_offset2 - _objWidth) * temp;
        _autoAbandonVisit2 = 0;
        //
        // _canVisitWidth = GetPos(_canVisitWidth).x;
        _autoVisitWidth = GetPos(_autoVisitWidth).x;
        _autoAbandonVisit1 = GetPos(_autoAbandonVisit1).x;
        _autoAbandonVisit2 = GetPos(_autoAbandonVisit2).x;
        _screenWidth = GetPos(GameTools.CanvasWidth - _objWidth * 0.5f * temp).x;
    }

    /// <summary>
    /// 更新自动访问
    /// </summary>
    /// <param name="obj"></param>
    public void UpdateAutoVisit(Transform obj)
    {
        _autoVisitRelativelyObj = obj;
    }

    public void UpdateBlockObj(Transform obj)
    {
        _blockObj = obj;
    }

    public void UpdateBlockState(bool isUpdate)
    {
        _isCanUpdateBlock = isUpdate;
    }

    private Vector3 GetPos(float x)
    {
        //if (_camera == null)
        //{
        //    _camera = GameTools.GetCanvas(transform).c;
        //}
        return GameTools.ScreenToWorldPoint(new Vector3(x, 0, 0), transform);
    }
    /// <summary>
    /// 检查进入场景
    /// </summary>
    private void CheckEnterScreen()
    {
        if (_isEnterScreen)
        {
            return;
        }

        if (transform.position.x >= GameTools.CanvasWidth)
        {
            return;
        }
        //    if (OnEnterScreen != null) OnEnterScreen();
        _isEnterScreen = true;
        _isCanUpdateBlock = true;
    }

    private void CheckEventBlock()
    {
        if (!_isEnterScreen)
        {
            return;
        }
        if (_blockObj == null)
        {
            return;
        }

        if (!_isCanUpdateBlock)
        {
            return;
        }
        if (transform.position.x - _blockObj.position.x > 0f)
        {
            return;
        }
        if (OnBlock != null)
        {
            OnBlock();
        }
    }

    /// <summary>
    /// 检查可以访问
    /// </summary>
    private void CheckCanVisit()
    {
        if (_isCan)
        {
            return;
        }

        if (transform.position.x >= _canVisitWidth)
        {
            return;
        }
        //  if (OnCanVisit != null) OnCanVisit();
        _isCan = true;
    }
    /// <summary>
    /// 检查自动访问
    /// </summary>
    private void CheckAutoVisit()
    {
        if (_isAutoVisit)
        {
            return;
        }

        if (_autoVisitRelativelyObj == null)
        {
            return;
        }
        if (transform.position.x - _autoVisitRelativelyObj.position.x > 0f)
        {
            return;
        }
        if (OnAutoVisit != null)
        {
            OnAutoVisit();
        }

        _isAutoVisit = true;
    }
    /// <summary>
    /// 自动放弃访问
    /// </summary>
    private void AutoAbandonVisit1()
    {
        if (_isAbandon1)
        {
            return;
        }

        if (transform.position.x >= _autoAbandonVisit1)
        {
            return;
        }

        _isAbandon1 = true;
        //     if (OnAutoAbandonVisit1 != null) OnAutoAbandonVisit1();
    }
    /// <summary>
    /// 自动放弃访问
    /// </summary>
    private void AutoAbandonVisit2()
    {
        if (_isAbandon2)
        {
            return;
        }

        if (transform.position.x >= _autoAbandonVisit2)
        {
            return;
        }

        _isAbandon2 = true;
        if (OnAutoAbandonVisit2 != null)
        {
            OnAutoAbandonVisit2();
        }
    }


    private void Update()
    {
        CheckEnterScreen();
        //   CheckShow1();
        //CheckCanVisit();
        CheckAutoVisit();
        CheckEventBlock();
        //   AutoAbandonVisit1();
        // AutoAbandonVisit2();
    }

    //
    private readonly Camera _camera;
    //
    private float _show1Width;
    private float _canVisitWidth;
    private float _autoVisitWidth;
    private float _autoAbandonVisit1;
    private float _autoAbandonVisit2;
    private readonly float _nowX;
    private float _objWidth;
    //
    private bool _isCanUpdateBlock;
    private bool _isEnterScreen;
    private readonly bool _isShow1;
    private bool _isAbandon1;
    private bool _isAbandon2;
    private bool _isCan;
    private bool _isAutoVisit;
    private readonly bool _isBlock;
    //
    private float _screenWidth = GameTools.CanvasWidth;
    //
    public float _offset2 = 400f;
    public float _offset1 = 50f;
    private const float _offset = 100f;

    private Transform _autoVisitRelativelyObj;
    private Transform _blockObj;
}
