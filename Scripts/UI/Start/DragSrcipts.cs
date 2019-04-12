using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum DragDir
{
    None,
    Left,
    Right,
    Top,
    Bottoom
}

public class DragSrcipts: MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    private ScrollRect m_rect;

    //[拖拽结束的坐标，是否是向左]
    public Action<float,float,DragDir> DragEndCallBack;

    private float m_beginDragPos;
    private DragDir m_dragDir=DragDir.None;

    private void Awake()
    {
        m_rect = transform.GetComponent<ScrollRect>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_beginDragPos = m_rect.horizontalNormalizedPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_rect.horizontalNormalizedPosition > m_beginDragPos)
        {
            m_dragDir = DragDir.Right;
        }
        else
        {
            m_dragDir = DragDir.Left;
        }

        if (DragEndCallBack!=null)
        {
            DragEndCallBack(m_rect.horizontalNormalizedPosition,m_beginDragPos,m_dragDir);
        }
        m_dragDir = DragDir.None;
    }

}

