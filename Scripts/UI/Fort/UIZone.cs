using Core.View;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIZone : UIPanelBehaviour
{
    protected override void OnReactive()
    {
        GetObj();
        //
        zoneID = FortSystem.Instance.NewZone.ZoneId;
        zoneTemplate = Zone_templateConfig.GetZoneTemplate(zoneID);
        //
        UpdateShow();
    }

    protected override void OnHide()
    {

    }

    protected override void OnShow(List<object> parmers = null)
    {
        OnReactive();
    }


    ///// <summary>
    ///// 打开显示
    ///// </summary>
    ///// <param name="zoneID"></param>
    //public void OpenUI(int zoneID)
    //{
    //    this.zoneID = zoneID;

    //    GetObj();
    //    //
    //    zoneTemplate = Zone_templateConfig.GetZoneTemplate(zoneID);
    //    //
    //    UpdateShow();
    //}


    private void UpdateShow()
    {
        ResourceLoadUtil.DeleteChildObj(mapList);
        foreach (int item in zoneTemplate.mapList)
        {
            LoadMapRes(item);
        }
    }
    /// <summary>
    /// 点击返回
    /// </summary>
    private void OnClickBack()
    {
        SceneManager.Instance.BackToUIScene();
        //UIPanelManager.Instance.Show<NewMainPanel>(CavasType.Three);
        //UIPanelManager.Instance.Show<CorePanel>();
        UIPanelManager.Instance.Hide<UIZone>();
    }
    /// <summary>
    /// 点击区域地图
    /// </summary>
    /// <param name="mapID"></param>
    private void OnClickZoneMap(int mapID)
    {
        //更新地图显示
        this.mapID = mapID;

        // 
        mapPopup.OnClickMap = OnCallClickMap;
        mapPopup.OnClickFort = OnCallClickFort;
        mapPopup.OpenUI(mapID);
    }

    /// <summary>
    /// 点击简介
    /// </summary>
    private void ClickIntro()
    {
        zoneText.text = zoneTemplate.zoneDescription;
        zoneIntro.SetActive(true);
    }
    /// <summary>
    /// 点击遮罩
    /// </summary>
    private void ClickMask()
    {
        zoneIntro.SetActive(false);
    }

    private void OnCallClickMap(int mapID)
    {
        //去探索
        //新建探索系统了
        UIPanelManager.Instance.Hide<UIZone>();
        //
        FortSystem.Instance.PrepareExplore(mapID);
        SceneManager.Instance.LoadScene(SceneType.Fight);
    }


    private void OnCallClickFort(int mapID)
    {
        //去远征
    }

    /// <summary>
    /// 加载地图资源
    /// </summary>
    /// <param name="mapID"></param>
    private void LoadMapRes(int mapID)
    {
        Map_template mapInfo = Map_templateConfig.GetMap_templat(mapID);
        if (mapInfo == null)
        {
            return;
        }
        GameObject obj = ResourceLoadUtil.InstantiateRes(mapTemplate, mapList, null,
            new Vector3(mapInfo.position[0], mapInfo.position[1]));
        obj.transform.Find("Name").GetComponent<Text>().text = mapInfo.mapName;
        obj.transform.Find("Level").GetComponent<Text>().text = mapInfo.mapLevel;
        int temp = mapID;
        obj.GetComponent<Button>().onClick.AddListener(delegate { OnClickZoneMap(temp); });
    }

    /// <summary>
    /// 获得组建
    /// </summary>
    private void GetObj()
    {
        if (isFirst)
        {
            return;
        }
        //
        transform.Find("Title").gameObject.AddComponent<Title>().Init(OnClickBack);
        //
        Transform zone = transform.Find("Zone");
        mapList = zone.Find("MapList");
        popoupObj = zone.Find("Popup");
        zoneIntro = popoupObj.transform.Find("ZoneIntro").gameObject;
        zoneText = zoneIntro.transform.Find("Text").GetComponent<Text>();
        mapPopup = popoupObj.transform.Find("MapPop").gameObject.AddComponent<UIZoneMapPopup>();
        mapTemplate = zone.Find("Temp/Map").gameObject;
        //
        maskButton = zone.Find("Mask").GetComponent<Button>();
        maskButton.onClick.AddListener(ClickMask);
        if (introButton != null)
        {
            introButton.onClick.AddListener(ClickIntro);
        }
        //
        isFirst = true;
    }
    //
    private int zoneID;
    private readonly int fortID;
    private int mapID;
    //
    private Button maskButton;
    private Button introButton;
    private Transform mapList;
    private GameObject zoneIntro;
    private Transform popoupObj;
    private GameObject mapTemplate;
    private Text zoneText;
    private UIZoneMapPopup mapPopup;
    private Zone_template zoneTemplate;
    //
    private bool isFirst;

}
