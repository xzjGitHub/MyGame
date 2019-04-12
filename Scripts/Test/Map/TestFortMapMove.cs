using HedgehogTeam.EasyTouch;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TestFortMapMove : MonoBehaviour
{
    public List<Transform> points;
    public QuickPinch quickPinch;
    public QuickLocalDrag localDrag;
    public Transform a;
    public Transform b;

    public void Start()
    {
        quickPinch.onPinchActioning = (OnPinchActioning);
        quickPinch.OnPinchStart.AddListener(OnPinchStart);
        quickPinch.OnPinching.AddListener(OnPinching);
        quickPinch.OnPinchEnd.AddListener(OnPinchEnd);
        //
        startPos = transform.localPosition;
    }

    private void OnPinchEnd(Gesture arg0)
    {
        localDrag.enabled = true;
    }
    /// <summary>
    /// 捏行动中
    /// </summary>
    private bool OnPinchActioning(Gesture arg0)
    {
        if (transform.localScale.x <= quickPinch.minScale) return false;
        if (transform.localScale.x >= quickPinch.maxScale) return false;
        //
        a.position = arg0.GetTouchToWorldPoint(arg0.startPosition, a.position.z);
        a.localPosition = new Vector3(a.localPosition.x, a.localPosition.y, 0);
        b.position = arg0.GetTouchToWorldPoint(b.position.z);
        b.localPosition = new Vector3(b.localPosition.x, b.localPosition.y, 0);
        //屏幕坐标转换成本地坐标
        nowPos = arg0.position - screenSize / 2;
        transform.localPosition = (startPos * transform.localScale.x + nowPos);
        nowPos = transform.localPosition;
        //
        return true;
    }
    /// <summary>
    /// 两指开始按下
    /// </summary>
    private void OnPinchStart(Gesture arg0)
    {
        nowScaleX = transform.localScale.x;
        nowPos = arg0.position - screenSize / 2;
        startPos = (transform.localPosition - nowPos) / nowScaleX;
    }
    /// <summary>
    /// 两指按下
    /// </summary>
    private void OnPinching(Gesture arg0)
    {
        localDrag.enabled = false;
        if (transform.localScale.x < quickPinch.minScale) return;
        //超左
        nowXY = Camera.main.WorldToScreenPoint(points[0].position).x;
        if (nowXY > 0) transform.localPosition += Vector3.left * nowXY;
        //超右
        nowXY = 1280 - Camera.main.WorldToScreenPoint(points[1].position).x;
        if (nowXY > 0) transform.localPosition += Vector3.right * nowXY;
        //超上
        nowXY = 720 - Camera.main.WorldToScreenPoint(points[0].position).y;
        if (nowXY > 0) transform.localPosition += Vector3.up * nowXY;
        //超下
        nowXY = Camera.main.WorldToScreenPoint(points[2].position).y;
        if (nowXY > 0) transform.localPosition += Vector3.down * nowXY;
    }

    private float nowScaleX;
    private float nowXY;
    //
    private Vector3 nowPos;
    private Vector2 screenSize = new Vector2(1280, 720);
    private Vector3 startPos;
}
