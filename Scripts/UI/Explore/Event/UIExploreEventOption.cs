﻿using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 探索事件选项
/// </summary>
public class UIExploreEventOption : MonoBehaviour
{
    /// <summary>
    /// 访问
    /// </summary>
    public Action<WPEventOptionType, int> OnVisit;

    /// <summary>
    /// 新建
    /// </summary>
    /// <param name="eventAttribute"></param>
    public void NewCreate(EventAttribute eventAttribute, WPEventOptionType type, int optionValue)
    {
        Initialization();
        this.eventAttribute = eventAttribute;
        optionType = type;
        this.optionValue = optionValue;
        //
        eventName.text = eventAttribute.event_template.eventName;
       // eventIntro.text = eventAttribute.event_template.eventInfo1;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Initialization()
    {
        Transform intro = transform.Find("Intro");
        icon = intro.Find("Icon").GetComponent<Image>();
        eventName = intro.Find("Name").GetComponent<Text>();
        eventIntro = intro.Find("Text").GetComponent<Text>();
        button = transform.Find("Button").GetComponent<Button>();
        //
        button.onClick.AddListener(OnClickButton);
    }

    /// <summary>
    /// 点击按钮
    /// </summary>
    private void OnClickButton()
    {
        if (OnVisit != null)
        {
            OnVisit(optionType, optionValue);
        }
    }

    //
    private EventAttribute eventAttribute;
    private WPEventOptionType optionType;
    private int optionValue;
    //
    private Image icon;
    private Text eventName;
    private Text eventIntro;
    private Button button;
}

/// <summary>
/// 路点事件选项类型
/// </summary>
public enum WPEventOptionType
{
    Fixed = 1,
    Random = 2,
}
