﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TogInfo
{
    [Tooltip("一般状态")]
    [SerializeField]
    private GameObject[] normalState;
    public GameObject[] Normal
    {
        get { return normalState; }
    }

    //[Tooltip("一般状态")]
    //[SerializeField]
    //private CanvasGroup[] normalStateCanvas;
    //public CanvasGroup[] NormalCanvas
    //{
    //    get { return normalStateCanvas; }
    //}

    [Tooltip("选中时显示的物体组合")]
    [SerializeField]
    private GameObject[] selectState;
    public GameObject[] Select
    {
        get { return selectState; }
    }

    //[Tooltip("选中时显示的物体组合")]
    //[SerializeField]
    //private CanvasGroup[] selectStateCanvas;
    //public CanvasGroup[] SelectCanvas
    //{
    //    get { return selectStateCanvas; }
    //}

    [Tooltip("接收点击事件按钮")]
    [SerializeField]
    private Button button;
    public Button Btn
    {
        get { return button; }
    }
}

public class TogGroup: MonoBehaviour
{
    [SerializeField]
    private List<TogInfo> list;

    /// <summary>
    /// 回调
    /// </summary>
    [HideInInspector]
    public Action<int> ClickAction;

    private int m_currentSlectIndex = -1;

    public void Reset()
    {
        for(int i = 0; i < list.Count; i++)
        {
            for(int j = 0; j < list[i].Normal.Length; j++)
            {
                list[i].Normal[j].SetActive(true);
                //  list[i].NormalCanvas[j].alpha = 1;
            }
            for(int j = 0; j < list[i].Select.Length; j++)
            {
                list[i].Select[j].SetActive(false);
                // list[i].NormalCanvas[j].alpha = 0;
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
                list[i].Normal[j].SetActive(true);
                // list[i].NormalCanvas[j].gameObject.SetActive(true);
                //  list[i].NormalCanvas[j].alpha = 1;
            }
            for(int j = 0; j < list[i].Select.Length; j++)
            {
                list[i].Select[j].SetActive(false);
                // list[i].SelectCanvas[j].gameObject.SetActive(true);
                //list[i].SelectCanvas[j].alpha = 0;
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
            list[m_currentSlectIndex].Normal[j].SetActive(!show);
            //list[m_currentSlectIndex].NormalCanvas[j].alpha = show ? 0 : 1;
        }
        for(int j = 0; j < list[m_currentSlectIndex].Select.Length; j++)
        {
            list[m_currentSlectIndex].Select[j].SetActive(show);
            //list[m_currentSlectIndex].SelectCanvas[j].alpha = show ? 1 : 0;
            // if(m_needSetPos)
            //    list[m_currentSlectIndex].SelectCanvas[j].gameObject.transform.SetAsLastSibling();
        }
    }
}

