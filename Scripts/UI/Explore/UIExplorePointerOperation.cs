using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIExplorePointerOperation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public Action PointerDown;
    public Action PointerUp;

    /// <summary>
    /// 更新大小
    /// </summary>
    /// <param name="isLeft"></param>
    public void UpdateSize(bool isLeft)
    {
        RectTransform rect = transform.GetComponent<RectTransform>();
        rect.sizeDelta = transform.parent.GetComponent<RectTransform>().sizeDelta;
        rect.anchoredPosition = (rect.sizeDelta.x / 2 + 50f) * (isLeft ? Vector2.left : Vector2.right);
    }

    /// <summary>
    /// 更新点击显示
    /// </summary>
    /// <param name="isShow"></param>
    public void UpdateClickShow(bool isShow)
    {
        gameObject.SetActive(isShow);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (PointerUp != null)
        {
            PointerUp();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (PointerDown != null)
        {
            PointerDown();
        }
    }

}
