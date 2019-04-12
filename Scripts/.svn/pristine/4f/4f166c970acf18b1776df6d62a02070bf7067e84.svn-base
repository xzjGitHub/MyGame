using UnityEngine;

/// <summary>
/// 角色模型动作
/// </summary>
public enum CharModuleAction
{
    /// <summary>
    /// 默认为空
    /// </summary>
    Default = 0,
    /// <summary>
    /// 战斗待机
    /// </summary>
    Idle = 1,
    /// <summary>
    /// 受击
    /// </summary>
    Hurt = 2,
    /// <summary>
    /// 大受击
    /// </summary>
    Hurt_jitui = 3,
    /// <summary>
    /// 死亡
    /// </summary>
    Die = 4,
    /// <summary>
    /// 胜利
    /// </summary>
    Celebrate = 5,
    /// <summary>
    /// 跑步
    /// </summary>
    Run = 6,
    /// <summary>
    /// 冲锋
    /// </summary>
    Atk_chongci = 7,
    /// <summary>
    /// 召唤
    /// </summary>
    Zhaohuan = 8,
    /// <summary>
    /// 制造
    /// </summary>
    Zhizao = 9,
    /// <summary>
    ///小动作 
    /// </summary>
    Idle_1 = 10,
    /// <summary>
    /// 返回
    /// </summary>
    Fanhui = 11,
    /// <summary>
    /// 准备
    /// </summary>
    Atk_zhunbei = 12,
    /// <summary>
    /// Buff
    /// </summary>
    Buff=13,
    /// <summary>
    /// 攻击
    /// </summary>
    Atk_gongji = 14,
    /// <summary>
    /// 大招
    /// </summary>
    Dazhao=15,
    /// <summary>
    /// 小招1
    /// </summary>
    XiaoZhao1 = 16,
    /// <summary>
    /// 小招2
    /// </summary>
    XiaoZhao2 = 17,
    /// <summary>
    /// 小招3
    /// </summary>
    XiaoZhao3 = 18,
    /// <summary>
    /// 小招4
    /// </summary>
    XiaoZhao4= 19,
}

public class HeroModuleInfo
{
    private static Vector3 pos1 = new Vector3(-183, -185, 0);
    private static Vector3 pos2 = new Vector3(-283, -122, 0);
    private static Vector3 pos3 = new Vector3(-383, -185, 0);
    private static Vector3 pos4 = new Vector3(-483, -122, 0);
    private static Vector3 pos5 = new Vector3(-583, -185, 0);


    public static Vector3 Pos1
    {
        get { return pos1; }
    }

    public static Vector3 Pos2
    {
        get { return pos2; }
    }

    public static Vector3 Pos3
    {
        get { return pos3; }
    }

    public static Vector3 Pos4
    {
        get { return pos4; }
    }

    public static Vector3 Pos5
    {
        get { return pos5; }
    }



    /// <summary>
    /// 获得角色模型的位置
    /// </summary>
    /// <param name="_index"></param>
    /// <returns></returns>
    public static Vector3 CharModuleUIPos(int _index)
    {
        switch (_index)
        {
            case 0:
                return pos1;
            case 1:
                return pos2;
            case 2:
                return pos3;
            case 3:
                return pos4;
            case 4:
                return pos5;
            default:
                return pos1;
        }
    }

}

