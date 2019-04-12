using UnityEngine;
using System.Collections;

public class UIExploreMapMove : MonoBehaviour
{
    public float aspdRate = 1;
    //
    private RectTransform rectTransform;
    private Vector2 sceneVector2 = Vector2.left * 10000;
    private float width;

    private float time;


    /// <summary>
    /// 开始移动
    /// </summary>
    /// <param name="endVector3"></param>
    /// <param name="aspd"></param>
    //public void FixedUpdateSartMove(float aspd)
    //{
    //    if (rectTransform == null)
    //    {
    //        rectTransform = transform.GetComponent<RectTransform>();
    //    }
    //    //
    //    if (rectTransform.anchoredPosition.x <= sceneVector2.x)
    //    {
    //        rectTransform.anchoredPosition = Vector2.zero;
    //    }
    //    //
    //    rectTransform.anchoredPosition = Vector3.MoveTowards(rectTransform.anchoredPosition, sceneVector2, aspd * aspdRate);
    //}

    public void UpdateSartMove(float aspd)
    {
        if (rectTransform == null)
        {
            width = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
            rectTransform = transform.GetComponent<RectTransform>();
            distance = Vector3.Distance(rectTransform.anchoredPosition, sceneVector2);
        }
        if (rectTransform.anchoredPosition.x <= -width) rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.anchoredPosition =// Vector3.Lerp(rectTransform.anchoredPosition, sceneVector2, x);
        Vector3.MoveTowards(rectTransform.anchoredPosition, sceneVector2,/* distance /*/ aspd * aspdRate*Time.deltaTime);
    }

    private float distance;
}
