
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void DragCallBack(PointerEventData eventData);

public class DragUtil: MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public DragCallBack OnBeginDragCallBack;
    public DragCallBack OnDragCallBack;
    public DragCallBack OnEndDragCallBack;


    public void OnBeginDrag(PointerEventData eventData)
    {
        if(OnBeginDragCallBack != null)
        {
            OnBeginDragCallBack(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(OnDragCallBack != null)
        {
            OnDragCallBack(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(OnEndDragCallBack != null)
        {
            OnEndDragCallBack(eventData);
        }
    }
}

