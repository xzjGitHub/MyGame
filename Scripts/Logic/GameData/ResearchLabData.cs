using ProtoBuf;
using System.Collections.Generic;
using College.Research.Data;


/// <summary>
/// 研究室数据
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class ResearchLabData
{
    /// <summary>
    /// 等级
    /// </summary>
    public int level;
    /// <summary>
    /// 正在研究的信息
    /// </summary>
    public List<ResearchingInfo> researchingInfo = new List<ResearchingInfo>();
    /// <summary>
    /// 类型 等级
    /// </summary>
    public Dictionary<int,EnchantInfo> EnchantInfoDict = new Dictionary<int,EnchantInfo>();
}

