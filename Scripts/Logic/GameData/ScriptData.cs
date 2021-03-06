﻿using System;
using System.Collections.Generic;
using ProtoBuf;


/// <summary>
/// 剧本数据
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class ScriptData
{
    /// <summary>
    /// 剧本id
    /// </summary>
    public int scriptID;
    /// <summary>
    /// 金币
    /// </summary>
    public float gold;
    /// <summary>
    /// 魔力
    /// </summary>
    public float mana;
    /// <summary>
    /// 已经玩过的地图类型列表
    /// </summary>
    public List<int> m_havePlayMapTypeList = new List<int>();
    /// <summary>
    /// 剧本时间
    /// </summary>
    public float scriptTime;
    /// <summary>
    /// 目录索引
    /// </summary>
    public int directoryIndex;
    /// <summary>
    /// 剧本阶段
    /// </summary>
    public  ScriptPhase scriptPhase;

    public int buildingIndex;

    public List<int> haveInRoomList = new List<int>();
    public List<int> flags = new List<int>();
}

