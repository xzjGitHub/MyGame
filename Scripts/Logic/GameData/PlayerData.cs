using ProtoBuf;
using System.Collections.Generic;


/// <summary>
/// 玩家数据
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class PlayerData
{
    /// <summary>
    /// 代币
    /// </summary>
    public int token;
    /// <summary>
    /// 剧本id
    /// </summary>
    public int lastScriptID;
    /// <summary>
    /// 选择的索引
    /// </summary>
    public int selectIndex;

    public Dictionary<int,long> ScriptsPlayTimes = new Dictionary<int,long>();
}

public class SeriptsPlayTimes
{
    public int ScriptId;
    public long Times;
}
