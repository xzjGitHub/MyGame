using ProtoBuf;
using System.Collections.Generic;


/// <summary>
/// 宴会厅数据
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class MessHallData
{
    /// <summary>
    /// 等级
    /// </summary>
    public int level;
}

