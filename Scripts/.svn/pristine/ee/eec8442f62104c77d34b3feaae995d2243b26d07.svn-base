using System;
using System.Collections.Generic;
using ProtoBuf;



/// <summary>
/// 工坊数据
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class WorkshopData
{
    /// <summary>
    /// 等级
    /// </summary>
    public int level;
    /// <summary>
    /// 正在研究的序列
    /// </summary>
    public List<EquipResearchInfo> ResearchList = new List<EquipResearchInfo>();
    //[类型 当前等级]
    public Dictionary<int,ERTypeInfo> ERInfoDict = new Dictionary<int,ERTypeInfo>();
    //类型 研究提升的最小制造等级
    public Dictionary<int, int> MinLevel = new Dictionary<int, int>();
}

