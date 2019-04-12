
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropParent:MonoBehaviour,IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Drop(eventData);
    }

    protected virtual void Drop(PointerEventData eventData)
    {

    }
}
