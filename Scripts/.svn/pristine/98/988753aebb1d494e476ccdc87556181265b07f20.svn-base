using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UICombatEndPopup : MonoBehaviour, IPointerDownHandler
{
    public delegate void CallBack(object param);

    public CallBack OnClose;
    //
    public object param;
    //
    private bool isFirst;


    public void Show()
    {
        Init();
        gameObject.SetActive(true);
    }

    private void Init()
    {
        if (isFirst) return;
        //

        //
        isFirst = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        if (OnClose != null) { OnClose(param); }
    }
}
