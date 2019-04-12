using ProtoBuf;
using System.Collections.Generic;


/// <summary>
/// 魔力井数据
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class ManaWellData
{
    /// <summary>
    /// 等级
    /// </summary>
    public int level;
    /// <summary>
    /// 当前魔力
    /// </summary>
    public int currentMana;
    /// <summary>
    /// 魔力井有角色的时间
    /// </summary>
    public float m_haveCharTime;
}

