using ProtoBuf;
using System.Collections.Generic;



/// <summary>
/// 召唤祭坛数据
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class AltarData
{
    /// <summary>
    /// 召唤池
    /// </summary>
    public List<int> altarChars = new List<int>();
    /// <summary>
    /// 几率列表
    /// </summary>
    public List<int> ChanceList = new List<int>();
    /// <summary>
    /// 本周可召唤列表
    /// </summary>
    public Dictionary<string,CharData> NowCharDcit = new Dictionary<string,CharData>();
    /// <summary>
    /// 下周周可召唤列表
    /// </summary>
    public Dictionary<string,CharData> NextCharDcit = new Dictionary<string,CharData>();
    /// <summary>
    /// 道具召唤角色列表
    /// </summary>
    public Dictionary<int,CharData> ItemCharDcit = new Dictionary<int,CharData>();
    /// <summary>
    /// 本周已经召唤了的角色
    /// </summary>
    public List<string> HaveCallChar=new List<string>();
}

