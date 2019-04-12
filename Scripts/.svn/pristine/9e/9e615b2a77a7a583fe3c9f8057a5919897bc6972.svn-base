
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class DoubleClick:MonoBehaviour,IPointerClickHandler
{
    public UnityAction DoubleClickAction;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickCount == 2)
        {
            if (DoubleClickAction != null)
            {
                DoubleClickAction();
            }
        }
    }
}

