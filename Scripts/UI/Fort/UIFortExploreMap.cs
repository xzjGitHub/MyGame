﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 要塞探索地图
/// </summary>
public class UIFortExploreMap : MonoBehaviour
{
    public delegate void CallBack(object param);
    public delegate void CallBack1(object param1, object param2);
    public CallBack1 OnClick;
    public CallBack OnUpdateTime;
    public CallBack OnDestroying;



    public ExploreMap Map { get { return exploreMap; } }

    public void OpenUI(ExploreMap exploreMap)
    {
        this.exploreMap = exploreMap;
        //
        GetObj();
        //
        UpdatePos(exploreMap.PosIndex);
        //  LoadMapShow(exploreMap, parent);
        //更新图标显示
       // starIcon.SetActive(exploreMap.MapTemplate.mapQuality == 2);
        taskIcon.SetActive(exploreMap.IsTaskAdd);
    }

    /// <summary>
    /// 更新地图显示
    /// </summary>
    public void UpdateMapShow(float nowTime)
    {
        lastUpdateTime = nowTime;
        timeIcon.SetActive(exploreMap.IsShowTime(nowTime));
        if (OnUpdateTime != null) OnUpdateTime(nowTime);
    }
    /// <summary>
    /// 取消点击
    /// </summary>
    public void CancelClick()
    {
        isClick = false;
    }

    /// <summary>
    /// 点击了按钮
    /// </summary>
    private void OnClickButton()
    {
        if (OnClick != null) OnClick(this, lastUpdateTime);
    }


    /// <summary>
    /// 更新位置
    /// </summary>
    private void UpdatePos(int index)
    {
        int nowRow = index / RowmaxNum;
        int nowColumn = index % RowmaxNum;

        transform.localPosition = startPos + Vector3.right * rectTransform.rect.width * nowColumn +
                                  Vector3.down * rectTransform.rect.height * nowRow;
    }

    private void GetObj()
    {
        rectTransform = transform.GetComponent<RectTransform>();
        timeIcon = transform.Find("Time").gameObject;
        taskIcon = transform.Find("Task").gameObject;
        starIcon = transform.Find("Star").gameObject;
        button = transform.Find("Button").GetComponent<Button>();
        //
        button.onClick.AddListener(OnClickButton);
    }

    private void OnDestroy()
    {
        if (OnDestroying != null) OnDestroying(this);
    }
    //
    private RectTransform rectTransform;
    private GameObject timeIcon;
    private GameObject taskIcon;
    private GameObject starIcon;
    private Button button;
    //
    private float lastUpdateTime;
    //
    private ExploreMap exploreMap;
    //
    private const int RowmaxNum = 6;
    private Vector3 startPos = new Vector3(-199.5f, 119.5f);
    private bool isClick;

}
