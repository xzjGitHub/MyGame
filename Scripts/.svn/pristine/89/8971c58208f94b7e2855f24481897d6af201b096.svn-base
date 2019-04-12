using System;
using System.Collections.Generic;
using System.Linq;


public partial class WPAttribute
{
    private int wpValue1 = -1;
    private int wpValue2 = -1;
    private int wpValue3 = -1;
    private int wpValue4 = -1;
    private int wpValue5 = -1;
    private int wpValue6 = -1;
    //
    private int sceneCount;
    /// <summary>
    /// 路点ID
    /// </summary>
    public int waypointId;

    /// <summary>
    /// 完成访问事件索引
    /// </summary>
    public List<int> doneEventIndexs = new List<int>();

    /// <summary>
    /// 上一个路点
    /// </summary>
    public int previousWP;

    /// <summary>
    /// 下一个路点
    /// </summary>
    public List<int> nextWP;

    public int baseWPLevel;

    /// <summary>
    /// 事件列表   key=屏幕索引，value=事件属性列表
    /// </summary>
    public Dictionary<int, List<EventAttribute>> WPEvents = new Dictionary<int, List<EventAttribute>>();

    /// <summary>
    /// 事件列表
    /// </summary>
    public List<EventAttribute> events = new List<EventAttribute>();


    /// <summary>
    /// 是否访问
    /// </summary>
    public bool isCall;

    /// <summary>
    /// 额外事件列表
    /// </summary>
    public Dictionary<int, List<EventAttribute>> extraEvents = new Dictionary<int, List<EventAttribute>>();


    /// <summary>
    /// 新建
    /// </summary>
    public WPAttribute(int waypointId, TeamAttribute teamAttribute, List<int> unusableEvents, int previousWP,
        List<int> nextWP, int baseWPLevel, List<int> testValues = null)
    {
        this.waypointId = waypointId;
        this.previousWP = previousWP;
        this.nextWP = nextWP;
        this.baseWPLevel = baseWPLevel;
        //
        if (testValues != null)
        {
            wpValue1 = testValues[0];
            wpValue2 = testValues[1];
            wpValue3 = testValues[2];
            wpValue4 = testValues[3];
            wpValue5 = testValues[4];
            wpValue6 = testValues[5];
        }
        //
        wp_template = WP_templateConfig.GetWpTemplate(waypointId);
        if (wp_template == null)
        {
            return;
        }
        sceneCount = wp_template.WPLength;
        //
        int eventIndex = 0;
        //添加道中事件
        AddMainWPCentreEvent(wp_template.WPEvent, ref eventIndex);
        switch (wp_template.WPCategory)
        {
            case 1:
                //添加随机道中事件
                AddRandomWPCentreEvent(wp_template.eventCount, wp_template.eventSet, unusableEvents, ref eventIndex);
                //添加草丛事件
                AddBushEvent(wp_template.maxBush, wp_template.bushSet, wp_template.bushChance, unusableEvents, ref eventIndex);
                //添加陷阱事件
                AddTrapEvent(wp_template.maxTrap, wp_template.trapSet, wp_template.trapChance, unusableEvents, ref eventIndex);
                break;
            case 2: //房间只有一种事件
                break;
        }

        #region 以前添加事件备份
        ////生成每屏事件
        //for (int i = 0; i < wp_template.WPEventSet.Count; i++)
        //{
        //    //草丛事件
        //    List<int> bushEvents = GetWPSetupEvents(wp_template.bushEventSet, unusableEvents);
        //    //道中事件
        //    List<int> mainEvents = wp_template.mainEventSet.Count <= i
        //        ? new List<int>()
        //        : GetWPSetupEvents(wp_template.mainEventSet[i], unusableEvents);
        //    //最后道中事件
        //    List<int> lastEvents = GetWPSetupEvents(wp_template.WPEventSet, unusableEvents);
        //    //陷阱事件
        //    //每个屏幕添加陷阱事件
        //    int trapEvent = -1;
        //    int _index = RandomBuilder.RandomIndex_Chances(wp_template.trapChance);
        //    if (wp_template.trapEvent.Count > _index && _index != -1)
        //    {
        //        trapEvent = wp_template.trapEvent[_index];
        //    }
        //    //创建事件
        //    if (!WPEvents.ContainsKey(i))
        //    {
        //        WPEvents.Add(i, new List<EventAttribute>());
        //    }
        //    //草丛事件
        //    List<int> temp = i == 0 ? new List<int> { 5, 7, 9, 11, 13 } : new List<int> { 1, 3, 7, 9, 11, 13 };
        //    List<float> posIndexs = temp.Select(item => item / 16f).ToList();
        //    posIndexs = RandomBuilder.RandomList(bushEvents.Count, posIndexs);
        //    CreateEventAttribute(bushEvents, posIndexs, 1, i, ref eventIndex);
        //    //道中事件
        //    temp = new List<int>();
        //    if (mainEvents.Count == 1)
        //    {
        //        temp = i == 0 ? new List<int> { 3, 4, 5 } : new List<int> { 1, 2, 4, 5 };
        //        temp = RandomBuilder.RandomList(1, temp);
        //    }
        //    else
        //    {
        //        temp = new List<int>();
        //        int index = RandomBuilder.RandomList(1, new List<int> { 1, 2 })[0];
        //        switch (index)
        //        {
        //            case 1:
        //                temp.Add(RandomBuilder.RandomList(1, new List<int> { 1, 2 })[0]);
        //                temp.Add(RandomBuilder.RandomList(1, new List<int> { 4, 5 })[0]);
        //                break;
        //            case 2:
        //                temp.Add(RandomBuilder.RandomList(1, new List<int> { 4, 5 })[0]);
        //                temp.Add(RandomBuilder.RandomList(1, new List<int> { 1, 2 })[0]);
        //                break;
        //        }
        //    }
        //    posIndexs = temp.Select(item => item / 8f).ToList();
        //    CreateEventAttribute(mainEvents, posIndexs, 0, i, ref eventIndex);
        //    //陷阱事件
        //    if (trapEvent != -1)
        //    {
        //        List<int> tempTarp = new List<int> { 3, 4, 5, 6 };
        //        //先移除占用的位置
        //        foreach (int item in temp)
        //        {
        //            tempTarp.Remove(item);
        //        }
        //        temp = RandomBuilder.RandomList(1, tempTarp);
        //        posIndexs = temp.Select(item => item / 8f).ToList();
        //        if (posIndexs != null && posIndexs.Count > 0)
        //        {
        //            CreateEventAttribute(trapEvent, posIndexs[0], 0, i, ref eventIndex);
        //        }
        //    }
        //    //屏幕终点事件
        //    try
        //    {
        //        CreateEventAttribute(lastEvents[0], 7 / 8f, 0, i, ref eventIndex);
        //    }
        //    catch (Exception)
        //    {
        //        LogHelperLSK.LogError(lastEvents);
        //    }

        //}
        #endregion
        //
        UpdateEvents();
    }



    /// <summary>
    /// 添加主道中事件
    /// </summary>
    /// <param name="events">事件列表</param>
    /// <param name="eventIndex">事件索引</param>
    private void AddMainWPCentreEvent(List<List<int>> events, ref int eventIndex)
    {
        List<int> eventIDs;
        int eventID;
        int sceneIndex;
        List<float> tempPos;
        for (int i = 0; i < events.Count; i++)
        {
            sceneIndex = i;
            eventIDs = events[i];
            tempPos = GetWPCentreEventPoss(sceneIndex);
            for (int j = 0; j < eventIDs.Count; j++)
            {
                eventID = eventIDs[j];
                if (eventID == 0 || tempPos.Count - 1 < j)
                {
                    continue;
                }
                CreateEventAttribute(eventID, tempPos[j], 1, sceneIndex, ref eventIndex);
            }
        }
    }
    /// <summary>
    /// 添加随机道中事件
    /// </summary>
    /// <param name="eventCount">事件总数</param>
    /// <param name="eventSet">事件Set</param>
    /// <param name="eventIndex">事件索引</param>
    private void AddRandomWPCentreEvent(List<int> eventCount, int eventSet, List<int> unusableEvents, ref int eventIndex)
    {
        //得到随机道中事件的个数
        int randomSum = 0;
        if (eventCount == null)
        {
            return;
        }
        // randomSum = eventCount.Count == 1 ? eventCount[0] : RandomBuilder.RandomNum(eventCount[0], eventCount[1]);
        //测试
        if (wpValue1 == -1)
        {
            wpValue1 = eventCount.Count > 0 ? eventCount[0] : 0;
        }
        if (wpValue2 == -1)
        {
            wpValue2 = eventCount.Count > 1 ? eventCount[1] : 0;
        }
        randomSum = RandomBuilder.RandomNum(wpValue1, wpValue2);
        if (wpValue2 == 0)
        {
            randomSum = wpValue1;
        }
        //
        if (randomSum <= 0)
        {
            return;
        }
        //道中事件能出现的位置
        Dictionary<int, List<float>> WPCentreEventPos = new Dictionary<int, List<float>>();
        for (int i = 0; i < sceneCount; i++)
        {
            WPCentreEventPos.Add(i, GetWPCentreEventPoss(i));
        }
        //取现在有的道中事件位置
        Dictionary<int, List<float>> havePos = new Dictionary<int, List<float>>();
        foreach (KeyValuePair<int, List<EventAttribute>> item in WPEvents)
        {
            if (!havePos.ContainsKey(item.Key))
            {
                havePos.Add(item.Key, new List<float>());
            }
            foreach (EventAttribute eventInfo in item.Value)
            {
                if (eventInfo.EventPosType == 1)
                {
                    havePos[item.Key].Add(eventInfo.ScenePos);
                }
            }
        }
        //道中事件剩余的位置
        Dictionary<int, List<float>> WPCentreEventSurplusPos = new Dictionary<int, List<float>>();
        foreach (KeyValuePair<int, List<float>> item in WPCentreEventPos)
        {
            foreach (float pos in item.Value)
            {
                if (havePos.ContainsKey(item.Key))
                {
                    if (havePos[item.Key].Contains(pos))
                    {
                        continue;
                    }
                }
                if (!WPCentreEventSurplusPos.ContainsKey(item.Key))
                {
                    WPCentreEventSurplusPos.Add(item.Key, new List<float>());
                }
                WPCentreEventSurplusPos[item.Key].Add(pos);
            }
        }
        //前一个屏幕的索引
        int previousSceneIndex = -1;
        float eventPos;
        int eventID;
        int sceneIndex;

        for (int i = 0; i < randomSum; i++)
        {
            WPCentreEventSurplusPos = GetWPCentreEventInfo(WPCentreEventSurplusPos, ref previousSceneIndex,
                out sceneIndex, out eventPos);
            if (sceneIndex == -1 || eventPos == -1)
            {
                continue;
            }
            //选个事件
            eventID = GetWPRandSetupEvents(eventSet, unusableEvents);
            CreateEventAttribute(eventID, eventPos, 1, sceneIndex, ref eventIndex);
        }
    }
    /// <summary>
    /// 添加草丛事件
    /// </summary>
    /// <param name="eventMaxSum"></param>
    /// <param name="eventSet"></param>
    private void AddBushEvent(int eventMaxSum, int eventSet, int chance, List<int> unusableEvents, ref int eventIndex)
    {
        int count = 0;
        //count = RandomBuilder.SelectRandomCount(eventMaxSum, chance);
        //测试
        if (wpValue3 == -1)
        {
            wpValue3 = eventMaxSum;
        }
        if (wpValue4 == -1)
        {
            wpValue4 = chance;
        }
        count = RandomBuilder.SelectRandomCount(wpValue3, wpValue4);
        if (count <= 0)
        {
            return;
        }
        //草丛事件能出现的位置
        Dictionary<int, List<float>> bushEventPos = new Dictionary<int, List<float>>();
        for (int i = 0; i < sceneCount; i++)
        {
            bushEventPos.Add(i, GetBushEventPoss(i));
        }

        //前一个屏幕的索引
        int previousSceneIndex = -1;
        float eventPos;
        int eventID;
        int sceneIndex;
        int sum = 0;
        List<int> typs = new List<int> { 3, 4 };
        for (int i = 0; i < count; i++)
        {
            bushEventPos = GetWPCentreEventInfo(bushEventPos, ref previousSceneIndex, out sceneIndex, out eventPos);
            if (sceneIndex == -1 || eventPos == -1)
            {
                continue;
            }
            //选个事件
            eventID = GetWPRandSetupEvents(eventSet, unusableEvents);

            int type = RandomBuilder.RandomList(1, typs)[0];
            CreateEventAttribute(eventID, eventPos, type, sceneIndex, ref eventIndex);
            sum++;
        }
    }
    /// <summary>
    /// 添加陷进事件
    /// </summary>
    /// <param name="eventMaxSum"></param>
    /// <param name="eventSet"></param>
    private void AddTrapEvent(int eventMaxSum, int eventSet, int chance, List<int> unusableEvents, ref int eventIndex)
    {
        // int count = RandomBuilder.SelectRandomCount(eventMaxSum, chance);
        //测试
        int count = 0;
        if (wpValue5 == -1)
        {
            wpValue5 = eventMaxSum;
        }
        if (wpValue6 == -1)
        {
            wpValue6 = chance;
        }
        count = RandomBuilder.SelectRandomCount(wpValue5, wpValue6);
        if (count <= 0)
        {
            return;
        }
        //陷阱事件能出现的位置
        Dictionary<int, List<float>> trapEventPos = new Dictionary<int, List<float>>();
        for (int i = 0; i < sceneCount; i++)
        {
            trapEventPos.Add(i, GetTrapEventPoss());
        }
        //取现在有的道中事件位置
        Dictionary<int, List<float>> havePos = new Dictionary<int, List<float>>();
        foreach (KeyValuePair<int, List<EventAttribute>> item in WPEvents)
        {
            if (!havePos.ContainsKey(item.Key))
            {
                havePos.Add(item.Key, new List<float>());
            }
            foreach (EventAttribute eventInfo in item.Value)
            {
                if (eventInfo.EventPosType == 1 || eventInfo.EventPosType == 2)
                {
                    havePos[item.Key].Add(eventInfo.ScenePos);
                }
            }
        }
        //道中事件剩余的位置
        Dictionary<int, List<float>> trapEventSurplusPos = new Dictionary<int, List<float>>();
        foreach (KeyValuePair<int, List<float>> item in trapEventPos)
        {
            foreach (float pos in item.Value)
            {
                if (havePos.ContainsKey(item.Key))
                {
                    if (havePos[item.Key].Contains(pos))
                    {
                        continue;
                    }
                }
                if (!trapEventSurplusPos.ContainsKey(item.Key))
                {
                    trapEventSurplusPos.Add(item.Key, new List<float>());
                }
                trapEventSurplusPos[item.Key].Add(pos);
            }
        }

        //前一个屏幕的索引
        int previousSceneIndex = -1;
        float eventPos;
        int eventID;
        int sceneIndex;

        for (int i = 0; i < count; i++)
        {
            trapEventSurplusPos = GetWPCentreEventInfo(trapEventSurplusPos, ref previousSceneIndex,
                out sceneIndex, out eventPos);
            if (sceneIndex == -1 || eventPos == -1)
            {
                continue;
            }
            //选个事件
            eventID = GetWPRandSetupEvents(eventSet, unusableEvents);
            CreateEventAttribute(eventID, eventPos, 2, sceneIndex, ref eventIndex);
        }
    }

    private Dictionary<int, List<float>> GetWPCentreEventInfo(Dictionary<int, List<float>> WPCentreEventSurplusPos, ref int previousSceneIndex, out int sceneIndex, out float eventPos)
    {
        sceneIndex = -1;
        eventPos = -1;
        //没有可以用的位置
        if (WPCentreEventSurplusPos.Keys.Count == 0)
        {
            return WPCentreEventSurplusPos;
        }
        //只有一个屏幕可以用
        if (WPCentreEventSurplusPos.Keys.Count == 1)
        {
            sceneIndex = WPCentreEventSurplusPos.First().Key;
            previousSceneIndex = sceneIndex;
            eventPos = RandomBuilder.RandomList(1, WPCentreEventSurplusPos[sceneIndex])[0];
            WPCentreEventSurplusPos[sceneIndex].Remove(eventPos);
            //是否有可用的位置移除该屏幕
            if (WPCentreEventSurplusPos[sceneIndex].Count == 0)
            {
                WPCentreEventSurplusPos.Remove(sceneIndex);
            }
            return WPCentreEventSurplusPos;
        }
        List<int> sceneIndexs = new List<int>();
        sceneIndexs.AddRange(WPCentreEventSurplusPos.Keys);
        sceneIndexs.Remove(previousSceneIndex);
        previousSceneIndex = RandomBuilder.RandomList(1, sceneIndexs)[0];
        sceneIndex = previousSceneIndex;
        //是否有可用的位置
        if (WPCentreEventSurplusPos[previousSceneIndex].Count == 0)
        {
            WPCentreEventSurplusPos.Remove(previousSceneIndex);
            //重新选屏幕
            sceneIndex = WPCentreEventSurplusPos.First().Key;
            eventPos = RandomBuilder.RandomList(1, WPCentreEventSurplusPos[sceneIndex])[0];
            WPCentreEventSurplusPos[sceneIndex].Remove(eventPos);
            //是否有可用的位置移除该屏幕
            if (WPCentreEventSurplusPos[sceneIndex].Count == 0)
            {
                WPCentreEventSurplusPos.Remove(sceneIndex);
            }

            return WPCentreEventSurplusPos;
        }
        eventPos = RandomBuilder.RandomList(1, WPCentreEventSurplusPos[sceneIndex])[0];
        WPCentreEventSurplusPos[sceneIndex].Remove(eventPos);
        //是否有可用的位置移除该屏幕
        if (WPCentreEventSurplusPos[sceneIndex].Count == 0)
        {
            WPCentreEventSurplusPos.Remove(sceneIndex);
        }
        return WPCentreEventSurplusPos;
    }



    /// <summary>
    /// 获得道中事件位置
    /// </summary>
    /// <param name="sceneIndex"></param>
    /// <returns></returns>
    private List<float> GetWPCentreEventPoss(int sceneIndex)
    {
        List<float> tempPos = new List<float>();
        //
        List<int> temp = sceneIndex == 0 ? new List<int> { 3, 4, 5 } : new List<int> { 1, 2, };
        tempPos.Add(RandomBuilder.RandomList(1, temp)[0]);
        //
        if (sceneIndex != 0)
        {
            temp = new List<int> { 4, 5 };
            tempPos.Add(RandomBuilder.RandomList(1, temp)[0]);
        }
        //
        tempPos.Add(7);
        //取两位小数
        tempPos = tempPos.Select(item => (int)(item / 8f * 10000) / 10000f).ToList();
        return tempPos;
    }
    /// <summary>
    /// 获得草丛事件位置
    /// </summary>
    /// <param name="sceneIndex"></param>
    /// <returns></returns>
    private List<float> GetBushEventPoss(int sceneIndex)
    {
        //草丛事件
        List<float> posIndexs = sceneIndex == 0 ? new List<float> { 5, 7, 9, 11, 13 } : new List<float> { 1, 3, 7, 9, 11, 13 };
        posIndexs = posIndexs.Select(item => (int)(item / 16f * 10000) / 10000f).ToList();
        return posIndexs;
    }

    /// <summary>
    /// 获得陷阱事件位置
    /// </summary>
    /// <param name="sceneIndex"></param>
    /// <returns></returns>
    private List<float> GetTrapEventPoss()
    {
        return new List<float> { 3, 4, 5, 6, }.Select(item => (int)(item / 8f * 10000) / 10000f).ToList();
    }

    /// <summary>
    /// 创建事件属性
    /// </summary>
    private void CreateEventAttribute(int eventID, float pos, int eventPosType, int sceneIndex, ref int eventIndex)
    {
        if (!WPEvents.ContainsKey(sceneIndex))
        {
            WPEvents.Add(sceneIndex, new List<EventAttribute>());
        }

        if (eventPosType == 3)
        {
            eventPosType = RandomBuilder.RandomList(1, new List<int> { 3, 4 })[0];
        }

        WPEvents[sceneIndex].Add(new EventAttribute(eventID, waypointId, sceneIndex, pos, eventPosType,
            teamAttribute, baseWPLevel, false));
        eventIndex++;
    }

    /// <summary>
    /// 获得路点事件
    /// </summary>
    private List<int> GetWPSetupEvents(List<int> setups, List<int> unusableEvents)
    {
        List<int> list = new List<int>();
        for (int i = 0; i < setups.Count; i++)
        {
            list.AddRange(GetWPSetupEvents(setups[i], unusableEvents));
        }
        return list;
    }

    /// <summary>
    /// 获得路点事件
    /// </summary>
    private List<int> GetWPSetupEvents(int setup, List<int> unusableEvents)
    {
        List<int> list = new List<int>();
        WP_setup wP_Setup = WP_setupConfig.GetWPSetup(setup);
        if (wP_Setup == null)
        {
            return list;
        }

        int temp = RandomBuilder.RandomIndex_Chances(wP_Setup.selectChance);
        if (temp == -1 || temp > wP_Setup.selectChance.Count - 1)
        {
            return list;
        }

        int eventsetID = wP_Setup.EventSet[temp];
        Event_rndset event_Rndset = Event_rndsetConfig.GetEvent_rndset(eventsetID);
        if (event_Rndset == null)
        {
            return list;
        }

        for (int j = 0; j < event_Rndset.eventList.Count; j++)
        {
            List<int> eventInfo = event_Rndset.eventList[j];
            try
            {
                int selectIndex = RandomBuilder.RandomIndex_Chances(event_Rndset.selectChance[j]);
                if (selectIndex == -1 || unusableEvents.Contains(eventInfo[selectIndex]) || eventInfo[selectIndex] == 0)
                {
                    continue;
                }
                //只选一个事件
                list.Add(eventInfo[selectIndex]);
            }
            catch (Exception)
            {
                LogHelperLSK.LogError(j + "   " + event_Rndset.eventSet);
            }

        }

        return list;
    }

    /// <summary>
    /// 获得路点事件
    /// </summary>
    private int GetWPRandSetupEvents(int eventsetID, List<int> unusableEvents)
    {
        Event_rndset event_Rndset = Event_rndsetConfig.GetEvent_rndset(eventsetID);
        if (event_Rndset == null)
        {
            return 0;
        }
        for (int i = 0; i < event_Rndset.eventList.Count; i++)
        {
            List<int> eventInfo = event_Rndset.eventList[i];
            try
            {
                int selectIndex = RandomBuilder.RandomIndex_Chances(event_Rndset.selectChance[i]);
                if (selectIndex == -1 || unusableEvents.Contains(eventInfo[selectIndex]) || eventInfo[selectIndex] == 0)
                {
                    continue;
                }
                //只选一个事件
                return eventInfo[selectIndex];
            }
            catch (Exception)
            {
                LogHelperLSK.LogError(i + "  选择出错 " + event_Rndset.eventSet);
            }
        }

        return 0;
    }

    /// <summary>
    /// 更新事件
    /// </summary>
    private void UpdateEvents()
    {
        events.Clear();
        foreach (KeyValuePair<int, List<EventAttribute>> item in WPEvents)
        {
            if (item.Value.Count == 0)
            {
                continue;
            }

            events.AddRange(item.Value);
        }

        List<EventAttribute> toList = events.OrderBy(a => a.SceneIndex).ThenBy(b => b.ScenePos).ToList();
        for (int i = 0; i < toList.Count; i++)
        {
            toList[i].SetEventIndex(i);
        }

        events = toList;
    }
}

