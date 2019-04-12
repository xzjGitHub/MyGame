using DG.Tweening;
using GameEventDispose;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIExploreMap : MonoBehaviour
{
    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        if (isFirst) return;
        //
        GetObj();
        EventDispatcher.Instance.ExploreEvent.AddEventListener<ExploreEventType, object>(EventId.ExploreEvent, OnExploreEvent);
        isFirst = true;
    }

    public void InitMapInfo()
    {
        Init();
        //
        mapId = ExploreSystem.Instance.NowMapId;
        WPId = ExploreSystem.Instance.NowWaypointId;
        //
        wpAttribute = ExploreSystem.Instance.NowWPAttribute;
        //
    }


    //探索事件
    private void OnExploreEvent(ExploreEventType arg1, object arg2)
    {
        switch (arg1)
        {
            case ExploreEventType.SceneMoveEnd:
                isMove = false;
                break;
            case ExploreEventType.MoveSceneMimStart:
                isMove = true;
                aspd = (float)arg2;
                break;
            case ExploreEventType.VisiteEventMove:
                isMove = true;
                aspd = (float)arg2;
                break;
            case ExploreEventType.VisiteEventMoveEnd:
                isMove = false;
                break;
        }
    }


    private void LateUpdate()
    {
      //  if (!isMove) return;
     //   UpdateMove(aspd);
    }


    /// <summary>
    /// 获得组建
    /// </summary>
    private void GetObj()
    {
        UIExploreMapMove _tempExploreMapMove;
        foreach (Transform item in transform.Find("List"))
        {
            _tempExploreMapMove = item.gameObject.AddComponent<UIExploreMapMove>();
            _tempExploreMapMove.aspdRate = mapMoveAspds[item.GetSiblingIndex()];
            mapMoves.Add(_tempExploreMapMove);
        }
    }


    public void UpdateMove(float aspd)
    {
        foreach (var item in mapMoves)
        {
            item.UpdateSartMove(aspd);
        }
    }


    // 当 MonoBehaviour 将被销毁时调用此函数
    private void OnDestroy()
    {
        EventDispatcher.Instance.ExploreEvent.RemoveEventListener<ExploreEventType, object>(EventId.ExploreEvent, OnExploreEvent);
    }

    //
    private List<UIExploreMapMove> mapMoves = new List<UIExploreMapMove>();
    //
    private RectTransform bgRectTransform;
    private Transform eventListTransform;
    //
    private Vector3 startVector3;
    private Vector3 endVector3;
    private float time;
    private float distance;
    private float startTime;
    private float moveTimeSum;
    //
    private bool isFirst;
    private int mapId;
    private int WPId;
    private bool isMove;
    private float aspd;
    //
    private WPAttribute wpAttribute;
    //
    private Transform lastEventTransform;
    private float lastEventShowPos;
    private float[] mapMoveAspds = { 0.6f, 0.75f, 0.75f, 1f, 1f, 1.2f };
}
