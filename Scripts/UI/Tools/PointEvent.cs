using System;
using UnityEngine.EventSystems;
using UnityEngine;

public class PointEvent:MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [HideInInspector]
    public Action<System.Object,GameObject> PointDownCallBack;
    [HideInInspector]
    public Action<System.Object> PointUpCallBack;
    [HideInInspector]
    public System.Object Obj;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (PointDownCallBack != null)
        {
            PointDownCallBack(Obj,gameObject);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(PointUpCallBack != null)
        {
            PointUpCallBack(Obj);
        }
    }
}
