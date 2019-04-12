using ProtoBuf;
using System.Collections.Generic;


/// <summary>
/// 关卡存档
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class LevelData
{
    /// <summary>
    /// 地图id
    /// </summary>
    public int mapId;
    /// <summary>
    /// 已完成的路点列表
    /// </summary>
    public List<int> doneWPIds;
    /// <summary>
    /// 正在访问的路点ID
    /// </summary>
    public int nowCallWPId;
    /// <summary>
    /// 现在事件的索引
    /// </summary>
    public int nowEventIndex;
    /// <summary>
    /// 路点存档列表
    /// </summary>
    public List<WPData> WpDatas;
}
