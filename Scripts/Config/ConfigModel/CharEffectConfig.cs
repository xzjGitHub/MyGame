using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// CharEffectConfigConfig配置表
/// </summary>
public partial class CharEffectConfigConfig : IReader
{
    public List<CharEffectConfig> _CharEffectConfig = new List<CharEffectConfig>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _CharEffectConfig.Add(new CharEffectConfig(array[i]));
        }
    }
}



/// <summary>
/// CharEffectConfig配置表
/// </summary>
public partial class CharEffectConfig : IReader
{
    /// <summary>
    /// 角色ID
    /// </summary>
    public int CharID;
    /// <summary>
    /// 待机特效包
    /// </summary>
    public int effectSet1;
    /// <summary>
    /// 攻击特效包
    /// </summary>
    public int effectSet2;
    /// <summary>
    /// 受击特效包
    /// </summary>
    public int effectSet3;
    /// <summary>
    /// 大受击特效包
    /// </summary>
    public int effectSet4;
    /// <summary>
    /// 死亡特效包
    /// </summary>
    public int effectSet5;
    /// <summary>
    /// 胜利特效包
    /// </summary>
    public int effectSet6;
    /// <summary>
    /// 奥义特效包
    /// </summary>
    public int effectSet7;
    /// <summary>
    /// 跑步特效包
    /// </summary>
    public int effectSet8;
    /// <summary>
    /// 冲锋特效包
    /// </summary>
    public int effectSet9;
    /// <summary>
    /// 召唤特效包
    /// </summary>
    public int effectSet10;
    /// <summary>
    /// 制造特效包
    /// </summary>
    public int effectSet11;
    /// <summary>
    /// 站立阶段特效
    /// </summary>
    public int phaseEffect1;
    /// <summary>
    /// 冲刺阶段特效
    /// </summary>
    public int phaseEffect2;
    /// <summary>
    /// 攻击阶段特效
    /// </summary>
    public int phaseEffect3;
    /// <summary>
    /// 受击阶段特效
    /// </summary>
    public int phaseEffect4;
    /// <summary>
    /// 死亡阶段特效
    /// </summary>
    public int phaseEffect5;
    /// <summary>
    /// 胜利阶段特效
    /// </summary>
    public int phaseEffect6;
    /// <summary>
    /// 走路阶段特效
    /// </summary>
    public int phaseEffect7;



    public CharEffectConfig() { }
    public CharEffectConfig(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        CharID = int.Parse(array[0]);
        effectSet1 = int.Parse(array[1]);
        effectSet2 = int.Parse(array[2]);
        effectSet3 = int.Parse(array[3]);
        effectSet4 = int.Parse(array[4]);
        effectSet5 = int.Parse(array[5]);
        effectSet6 = int.Parse(array[6]);
        effectSet7 = int.Parse(array[7]);
        effectSet8 = int.Parse(array[8]);
        effectSet9 = int.Parse(array[9]);
        effectSet10 = int.Parse(array[10]);
        effectSet11 = int.Parse(array[11]);
        phaseEffect1 = int.Parse(array[12]);
        phaseEffect2 = int.Parse(array[13]);
        phaseEffect3 = int.Parse(array[14]);
        phaseEffect4 = int.Parse(array[15]);
        phaseEffect5 = int.Parse(array[16]);
        phaseEffect6 = int.Parse(array[17]);
        phaseEffect7 = int.Parse(array[18]);
    }
}
