
//--------------------------------------------------------------
//Creator： XZJ
//Data：    2019/5/30 16:30:34
//Note:     
//--------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class CanvasGroupTog: MonoBehaviour
{
    [SerializeField]
    private List<CanvasGroupTogInfo> list;

    /// <summary>
    /// 回调
    /// </summary>
    [HideInInspector]
    public Action<int> ClickAction;

    private int m_currentSlectIndex = -1;

    public void Reset()
    {
        if(list == null)
            return;
        for(int i = 0; i < list.Count; i++)
        {
            for(int j = 0; j < list[i].Normal.Length; j++)
            {
                list[i].Normal[j].alpha = 1;
            }
            for(int j = 0; j < list[i].Select.Length; j++)
            {
                list[i].Normal[j].alpha = 0;
            }
        }
        m_currentSlectIndex = -1;
    }

    private void InitComponent()
    {
        for(int i = 0; i < list.Count; i++)
        {
            for(int j = 0; j < list[i].Normal.Length; j++)
            {
                list[i].Normal[j].alpha = 1;
            }
            for(int j = 0; j < list[i].Select.Length; j++)
            {
                list[i].Select[j].alpha = 0;
            }
            int index = i;
            Utility.AddButtonListener(list[i].Btn,() => ClickTog(index));
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="unityAction"></param>
    /// <param name="defaultClickIndex">默认点击的索引</param>
    public void Init(Action<int> unityAction,int defaultClickIndex = 0)
    {
        InitComponent();
        ClickAction = unityAction;
        if(defaultClickIndex != -1)
            ClickTog(defaultClickIndex);
    }

    public void ClickTog(int index)
    {
        if(m_currentSlectIndex != -1)
        {
            UpdateShowInfo(false);
        }

        m_currentSlectIndex = index;

        UpdateShowInfo(true);

        if(ClickAction != null)
        {
            ClickAction(index);
        }
    }

    private void UpdateShowInfo(bool show)
    {
        for(int j = 0; j < list[m_currentSlectIndex].Normal.Length; j++)
        {
            list[m_currentSlectIndex].Normal[j].alpha = show ? 0 : 1;
        }
        for(int j = 0; j < list[m_currentSlectIndex].Select.Length; j++)
        {
            list[m_currentSlectIndex].Select[j].alpha = show ? 1 : 0;
        }
    }
}

