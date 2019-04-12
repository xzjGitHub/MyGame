using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameEventDispose;

/// <summary>
/// 小地图
/// </summary>
public class UIExploreSmallMap : MonoBehaviour
{
    public void OpenUI(float eventWidth, List<RectTransform> list = null)
    {
        GetObj();
        //
        moveObj.anchoredPosition = Vector2.zero;
        ResourceLoadUtil.DeleteChildObj(pointTrans);
        eventRes.Clear();
        //
     //   UpdateShow(eventWidth, list);
        //
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 更新位置
    /// </summary>
    public void UpdatePos(float x)
    {
        moveObj.anchoredPosition += Vector2.right * x * moveRatio;
    }

    private void UpdateShow(float eventWidth, List<RectTransform> list)
    {
        moveRatio = pointTrans.GetComponent<RectTransform>().sizeDelta.x / eventWidth;
        //
        var eventAttributes = ExploreSystem.Instance.NowEvents;

        for (int i = 0; i < eventAttributes.Count; i++)
        {
            GameObject _obj = LoadRes(eventAttributes[i].event_template.WPIcon);
            if (_obj == null) continue;
            eventRes.Add(eventAttributes[i].EventIndex, _obj);
            if (list[i] == null) continue;
            _obj.GetComponent<RectTransform>().anchoredPosition = Vector2.right * (list[i].anchoredPosition.x) * moveRatio;
        }
    }


    private GameObject LoadRes(int type)
    {
        switch (type)
        {
            case 1:
                return ResourceLoadUtil.InstantiateRes(bossRes, pointTrans);
            case 2:
                return ResourceLoadUtil.InstantiateRes(boxRes, pointTrans);
            case 3:
                return ResourceLoadUtil.InstantiateRes(eyeRes, pointTrans);
            default:
                return null;
        }
    }

    private void GetObj()
    {
        if (isFirst) return;
        var temp = transform.Find("Temp");
        bossRes = temp.Find("Boss").gameObject;
        eyeRes = temp.Find("Eye").gameObject;
        boxRes = temp.Find("Box").gameObject;
        //
        moveObj = transform.Find("Move/Move").GetComponent<RectTransform>();
        pointTrans = transform.Find("Point");
        //
        isFirst = true;
        //
        EventDispatcher.Instance.ExploreEvent.AddEventListener<ExploreEventType, object>(EventId.ExploreEvent, OnExploreEvent);
    }
    private void DeleRes(int index)
    {
        if (eventRes == null) return;
        if (!eventRes.ContainsKey(index)) return;
        if (eventRes[index] == null) return;
        DestroyImmediate(eventRes[index]);
        eventRes[index] = null;
    }


    //探索事件
    private void OnExploreEvent(ExploreEventType arg1, object arg2)
    {
        switch (arg1)
        {
            case ExploreEventType.VisitEventEnd:
                DeleRes((arg2 as EventAttribute).eventId);
                break;
        }
    }


    private void OnDestroy()
    {
        EventDispatcher.Instance.ExploreEvent.RemoveEventListener<ExploreEventType, object>(EventId.ExploreEvent, OnExploreEvent);
    }


    //
    private Dictionary<int, GameObject> eventRes = new Dictionary<int, GameObject>();
    //
    private bool isFirst;
    private float moveRatio;
    //
    private GameObject bossRes;
    private GameObject eyeRes;
    private GameObject boxRes;
    private RectTransform moveObj;
    private Transform pointTrans;
}
