﻿using Core.View;
using GameEventDispose;
using GameFsmMachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 要塞
/// </summary>
public class UIFort : UIPanelBehaviour
{
    protected override void OnReactive()
    {
        Init();
        //
        game_Config = Game_configConfig.GetGame_Config();
        //
        worldName.text = string.Format(worldNameStr, game_Config.worldName, game_Config.regionName);
        //
        zones.Clear();
        //
        UpdateTimeShow();
        LoadBaseShow();
        LoadFortShow();
        UpdateRefreshTimeShow(TimeUtil.GetPlayDays());
        //
        UpdatePos(forts.First().Value);
        //
        EventDispatcher.Instance.FortEvent.AddEventListener<FortShowEvent, int, object>(EventId.FortShow, OnFortShowEvent);
        EventDispatcher.Instance.ScriptTimeEvent.AddEventListener<ScriptTimeUpdateType, object>(EventId.ScriptTimeEvent, OnScriptTimeUpdateEvent);
        //
        UIPanelManager.Instance.Hide<NewMainPanel>();
    }

    protected override void OnHide()
    {
        EventDispatcher.Instance.FortEvent.RemoveEventListener<FortShowEvent, int, object>(EventId.FortShow, OnFortShowEvent);
        EventDispatcher.Instance.ScriptTimeEvent.RemoveEventListener<ScriptTimeUpdateType, object>(EventId.ScriptTimeEvent, OnScriptTimeUpdateEvent);

    }

    protected override void OnShow(List<object> parmers = null)
    {
        OnReactive();
    }



    private void UpdatePos(GameObject obj)
    {
        //移动的增量
        fortList.localPosition = obj.transform.localPosition * -1;
        easyTouch.CheckPos();
    }
    private void OnFortShowEvent(FortShowEvent arg1, int arg2, object arg3)
    {
        if (!zones.ContainsKey(arg2))
        {
            return;
        }

        switch (arg1)
        {
            case FortShowEvent.RefreshZone:
                ZoneRefresh zoneRefresh = arg3 as ZoneRefresh;
                //
                UpdateRefreshTimeShow(zoneRefresh.refreshTime);
                //
                UpdateZoneConfineShow(FortSystem.Instance.NowZones.Find(a => a.ZoneId == zoneRefresh.zoneID), zones[zoneRefresh.zoneID]);
                //加载新地图
                foreach (int item in zoneRefresh.addMapIndex)
                {
                    ExploreMap exploreMap = zoneRefresh.zone.ExploreMaps[item];
                    LoadExploreMapRes(exploreMap, zones[zoneRefresh.zoneID]);
                }
                break;
            case FortShowEvent.RefreshMap:
                UIFortExploreMap fortExploreMap;
                ZoneRefreshMap msg = (ZoneRefreshMap)arg3;
                foreach (int item in msg.destroyMapPos)
                {
                    fortExploreMap = exploreMaps[arg2].Find(a => a.Map.PosIndex == item);
                    if (fortExploreMap == null)
                    {
                        continue;
                    }

                    exploreMaps[arg2].Remove(fortExploreMap);
                    DestroyImmediate(fortExploreMap.gameObject);
                }
                foreach (UIFortExploreMap item in exploreMaps[arg2])
                {
                    item.UpdateMapShow(msg.refreshTime);
                }
                break;
        }
    }

    /// <summary>
    /// 剧本时间更新事件
    /// </summary>
    private void OnScriptTimeUpdateEvent(ScriptTimeUpdateType arg1, object arg2)
    {
        if (arg1 != ScriptTimeUpdateType.Day)
        {
            return;
        }

        float time = (float)arg2;
        //
        UpdateTimeShow();
        //
        UpdateRefreshTimeShow(time);
    }

    /// <summary>
    /// 更新日期显示
    /// </summary>
    private void UpdateTimeShow()
    {
        playTimeInfo = TimeUtil.GetPlayTimeInfo();
        nowTime.text = string.Format(m_timeFormat2, playTimeInfo.years, playTimeInfo.months, playTimeInfo.days);
    }
    /// <summary>
    /// 更新刷新事件显示
    /// </summary>
    private void UpdateRefreshTimeShow(float time)
    {
        int _temp = (int)FortSystem.Instance.NowZones.Find(a => a.ZoneState == ZoneState.New).RefreshSurplusTime(time);
        refreashTimeIntro.text = string.Format(timeIntroStr, _temp.ToString("00"));
    }

    /// <summary>
    /// 加载主城显示
    /// </summary>
    private void LoadBaseShow()
    {
        exploreMaps.Clear();
        Zone zone = FortSystem.Instance.NowZones.Find(a => a.FortType == FortType.Base);
        if (zone == null)
        {
            return;
        }

        GameObject obj = LoadFortRes(0.ToString(), fortList, new Vector2(game_Config.capitalSet[0], game_Config.capitalSet[1]));
        forts.Add(zone.FortId, obj);
        //添加区域
        zones.Add(zone.ZoneId, LoadZoneShow(zone, obj.transform));
    }

    /// <summary>
    /// 加载要塞显示
    /// </summary>
    private void LoadFortShow()
    {
        string resName;
        Vector2 anchoredPos;
        bool isUnlock;
        Fort_template fort_Template;
        //加载要塞
        foreach (int item in FortSystem.Instance.FindForts)
        {
            fort_Template = Fort_templateConfig.GetFort_template(item);
            resName = "" + (fort_Template.zoneType * 10 + fort_Template.fortType);
            anchoredPos = new Vector2(fort_Template.coordinate[0], fort_Template.coordinate[1]);
            isUnlock = FortSystem.Instance.UnlockForts.Contains(item);
            //
            GameObject obj = LoadFortRes(resName, fortList, anchoredPos, item, isUnlock);
            //添加区域
            Zone zone = FortSystem.Instance.NowZones.Find(a => a.FortId == item);
            if (zone != null)
            {
                zones.Add(zone.ZoneId, LoadZoneShow(zone, obj.transform));
            }
        }
    }

    /// <summary>
    /// 加载要塞资源
    /// </summary>
    private GameObject LoadFortRes(string name, Transform parent, Vector2 anchoredPos, int fortID = 0, bool isOnClick = false)
    {
        GameObject obj = ResourceLoadUtil.LoadFortRes(name, parent);
        obj.GetComponent<RectTransform>().anchoredPosition = anchoredPos;
        if (isOnClick)
        {
            Button button = obj.transform.Find("Button").GetComponent<Button>();
            button.enabled = true;
            //添加按钮点击监听
        }
        return obj;
    }

    /// <summary>
    /// 加载区域显示
    /// </summary>
    private GameObject LoadZoneShow(Zone zone, Transform parent)
    {
        GameObject obj = ResourceLoadUtil.LoadZoneRes(parent.Find("Zone"));
        UpdateZoneConfineShow(zone, obj);
        //加载探索地图
        foreach (KeyValuePair<int, ExploreMap> item in zone.ExploreMaps)
        {
            LoadExploreMapRes(item.Value, obj);
        }
        //
        return obj;
    }

    /// <summary>
    /// 加载探索地图
    /// </summary>
    private void LoadExploreMapRes(ExploreMap exploreMap, GameObject obj)
    {
        Transform parent = obj.transform.Find("MapList");
        if (!exploreMaps.ContainsKey(exploreMap.ZoneId))
        {
            exploreMaps.Add(exploreMap.ZoneId, new List<UIFortExploreMap>());
        }

        exploreMaps[exploreMap.ZoneId].Add(LoadMapShow(exploreMap, parent));
    }

    /// <summary>
    /// 更新区域光环显示
    /// </summary>
    private void UpdateZoneConfineShow(Zone zone, GameObject obj)
    {
        switch (zone.ZoneState)
        {
            case ZoneState.Old:
                obj.transform.Find("Confine/Confine2").gameObject.SetActive(true);
                break;
            case ZoneState.New:
                obj.transform.Find("Confine/Confine1").gameObject.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// 加载地图显示
    /// </summary>
    private UIFortExploreMap LoadMapShow(ExploreMap exploreMap, Transform parent)
    {
        return null;
        //UIFortExploreMap fortExploreMap = ResourceLoadUtil.LoadFortMapRes(exploreMap.MapTemplate.mapType, parent);
        //fortExploreMap.OnClick = OnCallClick_Map;
        //fortExploreMap.OnUpdateTime = OnCallUpdateTime_Map;
        //fortExploreMap.OnDestroying = OnCallOnDestroying_Map;
        //fortExploreMap.OpenUI(exploreMap);
        //return fortExploreMap;
    }

    /// <summary>
    /// 点击返回
    /// </summary>
    private void OnClickBack()
    {
        UIPanelManager.Instance.Show<NewMainPanel>(CavasType.Three);
        UIPanelManager.Instance.Show<CorePanel>();
        UIPanelManager.Instance.Hide<UIFort>();
    }
    /// <summary>
    /// 点击暂停
    /// </summary>
    private void OnClickPause()
    {

    }
    /// <summary>
    /// 点击开始
    /// </summary>
    private void OnClickStart()
    {

    }

    /// <summary>
    /// 回调点击了地图
    /// </summary>
    private void OnCallClick_Map(object param1, object param2)
    {
        nowOperationMap = param1 as UIFortExploreMap;
        if (nowOperationMap == null)
        {
            return;
        }

        mapPopup.OpenUI(nowOperationMap.Map, (float)param2);
    }
    /// <summary>
    /// 回调更新事件显示
    /// </summary>
    private void OnCallUpdateTime_Map(object param)
    {
        if (nowOperationMap == null)
        {
            mapPopup.gameObject.SetActive(false);
            return;
        }

        mapPopup.UpdateTimeShow((float)param);
    }
    /// <summary>
    /// 回调更新事件显示
    /// </summary>
    private void OnCallOnDestroying_Map(object param)
    {
        if (param == null || nowOperationMap == null)
        {
            return;
        }

        if (nowOperationMap != param as UIFortExploreMap)
        {
            return;
        }

        mapPopup.gameObject.SetActive(false);
        nowOperationMap.CancelClick();
    }

    private void OnCallClickClose_Popup()
    {
        nowOperationMap.CancelClick();
    }

    private void OnCallClickStart_Popup()
    {
        nowOperationMap.Map.SetExploreState(true);
        nowOperationMap.CancelClick();
        //新建探索系统了
        UIPanelManager.Instance.Hide<UIFort>();
        //
        FortSystem.Instance.PrepareExplore(nowOperationMap.Map);
        //SceneManagerUtil.LoadScene(SceneType.Fight);
        EventCenter.EventManager.Instance.TriggerEvent(EventCenter.EventSystemType.FSM,EventCenter.EventTypeNameDefine.UpdateFsm,GameFsmType.Fight);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        if (isFirst)
        {
            return;
        }
        //
        GetObj();
        //
        isFirst = true;
    }
    /// <summary>
    /// 获得组件
    /// </summary>
    private void GetObj()
    {
        fortList = transform.Find("FortList");
        Transform top = transform.Find("Top");
        back = top.Find("Back").GetComponent<Button>();
        pauseTime = top.Find("Time/Pause").GetComponent<Button>();
        startTime = top.Find("Time/Start").GetComponent<Button>();
        nowTime = top.Find("Time/Time").GetComponent<Text>();
        refreashTimeIntro = top.Find("RefreshTime/Text").GetComponent<Text>();
        //
        Transform down = transform.Find("Down");
        worldName = down.Find("Name").GetComponent<Text>();
        //
        mapPopup = transform.Find("MapPopup").gameObject.AddComponent<UIFortExploreMapPopup>();
        mapPopup.OnClose = OnCallClickClose_Popup;
        mapPopup.OnStart = OnCallClickStart_Popup;
        //
        back.onClick.AddListener(OnClickBack);
        pauseTime.onClick.AddListener(OnClickPause);
        startTime.onClick.AddListener(OnClickStart);
        //
        easyTouch = fortList.gameObject.AddComponent<UIEasyTouch>();
    }
    //
    private Transform fortList;
    //
    private Button back;
    private Button pauseTime;
    private Button startTime;
    private Text nowTime;
    private Text refreashTimeIntro;
    private Text worldName;
    //
    private bool isFirst;
    private TimeUtil.PlayTimeInfo playTimeInfo;
    //
    private Dictionary<int, GameObject> zones = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> forts = new Dictionary<int, GameObject>();
    //
    private Dictionary<int, List<UIFortExploreMap>> exploreMaps = new Dictionary<int, List<UIFortExploreMap>>();
    private UIFortExploreMap nowOperationMap;
    //
    private const string timeIntroStr = "将在<color=#ffffff> {0}</color> 天后刷新";
    private const string worldNameStr = "{0} <size=28>{1}</size>";
    private const string m_timeFormat2 = "{0}年{1}月{2}日";
    //
    private Game_config game_Config;
    private UIFortExploreMapPopup mapPopup;
    private UIEasyTouch easyTouch;
}
