using ProtoBuf;
using System.Collections.Generic;

/// <summary>
/// 召唤祭坛数据
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class EnchantRndData
{
    public int finalItemLevel;
    public float upgradeAll;
    public int templatID;
    public int instanceID;
}

