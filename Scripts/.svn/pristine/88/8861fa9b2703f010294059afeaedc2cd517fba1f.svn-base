using System.Collections.Generic;
using UnityEngine;

public class CharAttrTipPanel: UIPanelBehaviour
{
    private Transform m_target;
    private Canvas m_canvas;

    protected override void OnShow(List<object> parmers = null)
    {
        m_target = transform.Find("Parent");
        GameObject obj = transform.parent.gameObject;
        m_canvas = transform.parent.GetComponent<Canvas>();
        Utility.AddButtonListener(transform.Find("Mask"),ClickMask);
    }

    private void ClickMask()
    {
        Debug.Log("click");
        UIPanelManager.Instance.Hide<CharAttrTipPanel>();
    }

    public void UpdatePos(GameObject currentClickObj)
    {
        RectTransform clickRect = currentClickObj.GetComponent<RectTransform>();
        int x = (int)(clickRect.sizeDelta.x / 2);
        int y = (int)(clickRect.sizeDelta.y / 2);
        RectTransform currentRect = m_target.GetComponent<RectTransform>();
        int y1 = (int)(currentRect.sizeDelta.y / 2);
        int dis = 82;
        int yAdd = -16;
        int xAdd = -8;
        TipPanelPosUtil.UpdatePanelPos(m_canvas,m_target,currentClickObj,-(x + xAdd),-(y + y1 + dis + yAdd));
    }
}
