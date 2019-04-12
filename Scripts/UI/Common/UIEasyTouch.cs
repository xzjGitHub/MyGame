using HedgehogTeam.EasyTouch;
using System.Collections.Generic;
using UnityEngine;

public class UIEasyTouch : MonoBehaviour
{

    private void Start()
    {
        if (!isFirst)
        {
            InitEasyTouch();
        }
    }
    public void CheckPos()
    {
        if (!isFirst)
        {
            InitEasyTouch();
        }
        //
        if (transform.localScale.x < quickPinch.minScale)
        {
            return;
        }
        //
        if (_camera == null)
        {
            _camera = GameTools.GetCamera(transform);
        }
        //超左
        nowXY = GameTools.WorldToScreenPoint(points[0].position, _camera).x;
        if (nowXY > 0)
        {
            transform.localPosition += Vector3.left * nowXY;
        }
        //超右
        nowXY = Screen.width - GameTools.WorldToScreenPoint(points[1].position, _camera).x;
        if (nowXY > 0)
        {
            transform.localPosition += Vector3.right * nowXY;
        }
        //超上
        nowXY = Screen.height - GameTools.WorldToScreenPoint(points[0].position, _camera).y;
        if (nowXY > 0)
        {
            transform.localPosition += Vector3.up * nowXY;
        }
        //超下
        nowXY = GameTools.WorldToScreenPoint(points[2].position, _camera).y;
        if (nowXY > 0)
        {
            transform.localPosition += Vector3.down * nowXY;
        }
    }

    #region EasyTouch
    private void InitEasyTouch()
    {
        isFirst = true;
        points.AddRange(CreatePoints());
        //
        quickPinch = transform.GetComponent<QuickPinch>();
        localDrag = transform.GetComponent<QuickLocalDrag>();
        //
        if (quickPinch == null)
        {
            return;
        }

        if (localDrag == null)
        {
            return;
        }
        //
        quickPinch.onPinchActioning = (OnPinchActioning);
        quickPinch.OnPinchStart.AddListener(OnPinchStart);
        quickPinch.OnPinching.AddListener(OnPinching);
        quickPinch.OnPinchEnd.AddListener(OnPinchEnd);
        //
        if (transform.localScale.x <= quickPinch.minScale)
        {
            transform.localScale = Vector3.one * quickPinch.minScale;
        }

        if (transform.localScale.x >= quickPinch.maxScale)
        {
            transform.localScale = Vector3.one * quickPinch.maxScale;
        }
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
        if (transform.localScale.x <= quickPinch.minScale)
        {
            return false;
        }

        if (transform.localScale.x >= quickPinch.maxScale)
        {
            return false;
        }
        //屏幕坐标转换成本地坐标
        nowPos = arg0.position - screenSize / 2;
        transform.localPosition = (startPos * transform.localScale.x + nowPos);
        nowPos = transform.localPosition;
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
        CheckPos();
    }


    /// <summary>
    /// 创建点
    /// </summary>
    /// <returns></returns>
    private List<Transform> CreatePoints()
    {
        RectTransform rectTransform = transform.GetComponent<RectTransform>();
        //新建辅助点
        Transform Point = CreateTrans("Points", transform, Anchor.Middle, rectTransform.rect.width, rectTransform.rect.height);
        //
        Transform Point1 = CreateTrans("1", transform, Anchor.UpperLeft);
        Transform Point2 = CreateTrans("2", transform, Anchor.RightTop);
        Transform Point3 = CreateTrans("3", transform, Anchor.LowerRight);
        Transform Point4 = CreateTrans("4", transform, Anchor.LeftLower);

        Point1.SetParent(Point);
        Point2.SetParent(Point);
        Point3.SetParent(Point);
        Point4.SetParent(Point);
        //
        return new List<Transform> { Point1, Point2, Point3, Point4 };
    }

    private Transform CreateTrans(string name, Transform parent, Anchor anchor, float width = 0, float height = 0)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(parent);
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        RectTransform rectTransform = obj.transform.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            rectTransform = obj.AddComponent<RectTransform>();
        }

        rectTransform.sizeDelta = new Vector2(width, height);
        Vector2 pos = Vector2.zero;
        switch (anchor)
        {
            case Anchor.UpperLeft:
                pos = new Vector2(0, 1);
                break;
            case Anchor.LeftLower:
                pos = new Vector2(0, 0);
                break;
            case Anchor.RightTop:
                pos = new Vector2(1, 1);
                break;
            case Anchor.LowerRight:
                pos = new Vector2(1, 0);
                break;
            case Anchor.Middle:
                pos = new Vector2(0.5f, 0.5f);
                break;
        }
        rectTransform.anchorMin = pos;
        rectTransform.anchorMax = pos;
        rectTransform.pivot = pos;
        rectTransform.anchoredPosition = Vector3.zero;
        return obj.transform;
    }

    #endregion

    private Camera _camera;
    private bool isFirst;
    //
    private float nowScaleX;
    private float nowXY;
    //
    private Vector3 nowPos;
    private Vector2 screenSize = new Vector2(1280, 720);
    private Vector3 startPos;
    //
    public List<Transform> points = new List<Transform>();
    public QuickPinch quickPinch;
    public QuickLocalDrag localDrag;


    private enum Anchor
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// 左上
        /// </summary>
        UpperLeft = 1,
        /// <summary>
        /// 左下
        /// </summary>
        LeftLower = 2,
        /// <summary>
        /// 右上
        /// </summary>
        RightTop = 3,
        /// <summary>
        /// 右下
        /// </summary>
        LowerRight = 4,
        /// <summary>
        /// 中间
        /// </summary>
        Middle = 5,
    }
}
