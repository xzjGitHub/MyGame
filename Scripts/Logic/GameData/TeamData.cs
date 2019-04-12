#region Version Info
/* =======================================================================
* 
* 作者：lsk 
* 创建时间：2017/10/19 21:36:58
* 文件名：TeamData
* 版本：V0.0.1
*
* ========================================================================
*/
#endregion

using ProtoBuf;
using System.Collections.Generic;


/// <summary>
/// 队伍存档
/// </summary>
[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
public class TeamData
{
    /// <summary>
    /// 队伍编号
    /// </summary>
    public int teamId;
    /// <summary>
    /// 队伍冷却
    /// </summary>
    public float finalCharCooldown;
    /// <summary>
    /// 角色列表
    /// </summary>
    public List<int>charIds=new List<int>();
    /// <summary>
    /// 队伍位置
    /// </summary>
    public TeamLocation teamLocation;
}
