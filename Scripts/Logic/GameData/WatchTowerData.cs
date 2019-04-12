using ProtoBuf;
using System.Collections.Generic;


/// <summary>
/// 瞭望塔数据
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class WatchTowerData
{
    /// <summary>
    /// 等级
    /// </summary>
    public int level;
}

