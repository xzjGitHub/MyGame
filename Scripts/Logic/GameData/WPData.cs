using ProtoBuf;
using System.Collections.Generic;


/// <summary>
/// 路点存档
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class WPData
{
    /// <summary>
    /// 路点ID
    /// </summary>
    public int WPId;
    /// <summary>
    /// 是否访问
    /// </summary>
    public bool isCall;
    /// <summary>
    /// 现在访问的事件索引
    /// </summary>
    public int nowCallEventIndex;
    /// <summary>
    /// 完成访问随机事件索引
    /// </summary>
    public  List<int> doneEventIndexs = new List<int>();
}
