using UnityEngine;
using UnityEngine.EventSystems;


public class DragParent: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    protected GameObject m_dragObj;
    private RectTransform m_DraggingPlane;

    protected bool canDrag = true;

    public void OnBeginDrag(PointerEventData eventData)
    {
        var canvas = Utility.FindInParents<Canvas>(gameObject);
        m_dragObj = new GameObject("icon");
        m_dragObj.AddComponent<IgnoreRaycast>();
        m_dragObj.transform.SetParent(canvas.transform,false);
        m_dragObj.transform.SetAsLastSibling();
        m_DraggingPlane = transform as RectTransform;

        BengDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!canDrag)
            return;

        SetDraggedPosition(eventData);
        Drag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDrag(eventData);
    }


    private void SetDraggedPosition(PointerEventData data)
    {
        if(data.pointerEnter != null &&
            data.pointerEnter.transform as RectTransform != null
            && m_DraggingPlane != null)
            m_DraggingPlane = data.pointerEnter.transform as RectTransform;

        if(m_dragObj == null)
            return;
        var rt = m_dragObj.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if(RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane,data.position,data.pressEventCamera,out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = m_DraggingPlane.rotation;
        }
    }



    protected virtual void BengDrag(PointerEventData eventData)
    {

    }

    protected virtual void Drag(PointerEventData eventData)
    {

    }

    protected virtual void EndDrag(PointerEventData eventData)
    {

    }
}
