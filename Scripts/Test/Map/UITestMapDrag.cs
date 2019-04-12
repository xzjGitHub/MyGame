using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UITestMapDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private int type;
    public void SetMap()
    {
        type = 1;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (type != 1) return;

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
       
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localPosition = Vector3.right * transform.localPosition.x + Vector3.up * transform.localPosition.y;
    }
}
