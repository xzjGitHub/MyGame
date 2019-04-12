using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum PointerStatus
{
    PointerDown,
    PointerUp,
    LongPress
}

public delegate void LongPressCallBack(PointerStatus type);


public class LongPress: MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float LongPressTime = 1f;

    private float m_timer = 0;

    private bool m_isPress = false;
    private bool m_isDispatch = false;

    public LongPressCallBack OnLongPressCallback;

    public void OnPointerDown(PointerEventData eventData)
    {
        m_isPress = true;
        m_isDispatch = false;
        m_timer = 0;

        if(OnLongPressCallback != null)
        {
            OnLongPressCallback(PointerStatus.PointerDown);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_isPress = false;

        if(OnLongPressCallback != null)
        {
            OnLongPressCallback(PointerStatus.PointerUp);
        }
    }
    private void Update()
    {
        if(m_isPress && !m_isDispatch)
        {
            m_timer += Time.deltaTime;
            if(m_timer > LongPressTime)
            {
                //派发长按事件
                m_isDispatch = true;
                if(OnLongPressCallback != null)
                {
                    OnLongPressCallback(PointerStatus.LongPress);
                }
            }
        }
    }
}

