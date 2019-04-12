using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIEventIntroPopup : MonoBehaviour
{
    /// <summary>
    /// 更新显示
    /// </summary>
    public void UpdateShow(string msg, bool _new = true, bool isAllUpdate = false)
    {
        GetObj();
        //
        // if (_new) scrollRect.verticalNormalizedPosition = 1;
        if (isAllUpdate)
        {
            valueText.text = "";
            intros.Clear();
        }
        if (intros.Count == MaxNum)
        {
            valueText.text = valueText.text.Remove(0, intros[0].Length);
            intros.RemoveAt(0);
        }
        if (intros.Count != 0) msg = "\n" + msg;
        intros.Add(msg);
        valueText.text += msg;
        if (valueRectTransform == null) valueRectTransform = valueText.GetComponent<RectTransform>();
        valueRectTransform.anchoredPosition = Vector2.zero;
        gameObject.SetActive(true);
    }

    public void SetActive(bool isBool = true)
    {
        gameObject.SetActive(isBool);
    }

    private void GetObj()
    {
        if (isFirst) return;
        scrollRect = transform.Find("Scroll View").GetComponent<ScrollRect>();
        valueText = scrollRect.transform.Find("Viewport/Text").GetComponent<Text>();
        isFirst = true;
    }
    //
    private List<string> intros = new List<string>();
    //
    private ScrollRect scrollRect;
    private Text valueText;
    private RectTransform valueRectTransform;
    //
    private bool isFirst;
    private const int MaxNum = 30;
}
