using ProtoBuf;
using System.Collections.Generic;

[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
/// <summary>
/// 兵营数据
/// </summary>
public class BarrackData
{
    /// <summary>
    /// 角色Id 当前状态
    /// </summary>
    public List<WorkCharInfo> m_charUse = new List<WorkCharInfo>();

    /// <summary>
    /// 正在训练的角色
    /// </summary>
    public List<TraniCharInfo> TrainChars = new List<TraniCharInfo>();

    /// <summary>
    /// 已经复活的了Id
    /// </summary>
    public List<int> HaveFuHuoId = new List<int>();
}


[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class WorkCharInfo
{
    public int CharId;
    public CharStatus Status;
    public bool CanWork;

    public WorkCharInfo(int id,CharStatus useCharType,bool canWork)
    {
        CharId = id;
        Status = useCharType;
        CanWork = canWork;
    }
}


public class TraniCharInfo
{
    /// <summary>
    /// 角色Id
    /// </summary>
    public int CharId;
    /// <summary>
    /// 是否扣除魔力成功
    /// </summary>
    public bool SubManaSuc;
    /// <summary>
    /// 是否已经主动暂停
    /// </summary>
    public bool HasPause;
    /// <summary>
    /// 已经用的时间 每天清零
    /// </summary>
    public int HaveUseTime;

}
