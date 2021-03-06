﻿
//--------------------------------------------------------------
//Creator： XZJ
//Data：    2019/5/30 9:20:50
//Note:     
//--------------------------------------------------------------
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class DiaNpc: MonoBehaviour
{
    private GameObject m_tag;
    private Image icon;
    private Text m_text;

    public List<string> m_allInfo = new List<string>();
    private int m_index = 0;

    private Action m_clickAction;

    public void InitComponent(Action action)
    {
        m_clickAction = action;

        m_tag = transform.Find("Tag").gameObject;
        m_tag.SetActive(false);

        icon = transform.Find("Icon").GetComponent<Image>();
        m_text = transform.Find("Info").GetComponent<Text>();

        Utility.AddButtonListener(transform.Find("Bg"),ClickBtn);
    }

    public void InitInfo(List<string> list)
    {
        m_index = 0;
        m_allInfo.Clear();
        m_allInfo.AddRange(list);
    }

    public void UpdateTextInfo(string iconName)
    {
        icon.gameObject.SetActive(!string.IsNullOrEmpty(iconName));
        if(!string.IsNullOrEmpty(iconName))
        {
            icon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.CharDiaIcon,iconName);
            icon.SetNativeSize();
        }
        m_text.text = m_allInfo[m_index];
        m_index++;
    }

    public void UpdateTagShow(bool showTag)
    {
        m_tag.SetActive(!showTag);
    }

    public bool CheckAllShowEnd()
    {
        return m_index > m_allInfo.Count - 1;
    }

    private void ClickBtn()
    {
        if(m_clickAction != null)
            m_clickAction();
    }
}

