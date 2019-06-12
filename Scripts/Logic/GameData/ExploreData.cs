﻿using ProtoBuf;
using System.Collections.Generic;

[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class ExploreData 
{
    //现在地图ID
    public int nowMapId;
    //现在路点iD
    public int nowWaypointId;
    //现在事件索引
    public int nowEventIndex;
    //已经解锁了的地图列表
    public List<int> unlockMapList;
    /// <summary>
    /// 
    /// </summary>
    public List<int> wpEvent;

}
