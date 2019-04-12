using ProtoBuf;
using System.Collections.Generic;



/// <summary>
/// 要塞系统数据
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class FortSystemData
{
    /// <summary>
    /// 现在的区域列表
    /// </summary>
    public List<ZoneData> zorts = new List<ZoneData>();
    /// <summary>
    /// 发现了的要塞列表
    /// </summary>
    public List<int> findForts = new List<int>();
    /// <summary>
    /// 解锁了的要塞列表
    /// </summary>
    public List<int> unlockForts = new List<int>();
}

