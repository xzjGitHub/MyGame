using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// ResultEffectConfigConfig配置表
/// </summary>
public partial class ResultEffectConfigConfig : IReader
{
    public List<ResultEffectConfig> _ResultEffectConfig = new List<ResultEffectConfig>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _ResultEffectConfig.Add(new ResultEffectConfig(array[i]));
        }
    }
}



/// <summary>
/// ResultEffectConfig配置表
/// </summary>
public partial class ResultEffectConfig : IReader
{
    /// <summary>
    /// 编号
    /// </summary>
    public int resultTypeID;
    /// <summary>
    /// 状态名
    /// </summary>
    public int stateID;
    /// <summary>
    /// 
    /// </summary>
    public string stateName;
    /// <summary>
    /// 命中动作
    /// </summary>
    public string action_onHit;
    /// <summary>
    /// 吸收特例
    /// </summary>
    public string action_onAbsorb;
    /// <summary>
    /// 命中时，以目标为主体的特效
    /// </summary>
    public int effect_onHit;
    /// <summary>
    /// 命中并且吸收时，以目标为主体的特效
    /// </summary>
    public int effect_onAbsorb;
    /// <summary>
    /// 类型
    /// </summary>
    public int textType;
    /// <summary>
    /// 格挡动作
    /// </summary>
    public string action_onblock;
    /// <summary>
    /// 暴击动作
    /// </summary>
    public string action_onCritical;
    /// <summary>
    /// 闪避动作
    /// </summary>
    public string action_onDodge;
    /// <summary>
    /// 格挡爆点
    /// </summary>
    public int effect_onblock;
    /// <summary>
    /// 暴击爆点
    /// </summary>
    public int effect_onCritical;
    /// <summary>
    /// 闪避爆点
    /// </summary>
    public int effect_onDodge;



    public ResultEffectConfig() { }
    public ResultEffectConfig(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        resultTypeID = int.Parse(array[0]);
        stateID = int.Parse(array[1]);
        stateName = array[2];
        action_onHit = array[3];
        action_onAbsorb = array[4];
        effect_onHit = int.Parse(array[5]);
        effect_onAbsorb = int.Parse(array[6]);
        textType = int.Parse(array[7]);
        action_onblock = array[8];
        action_onCritical = array[9];
        action_onDodge = array[10];
        effect_onblock = int.Parse(array[11]);
        effect_onCritical = int.Parse(array[12]);
        effect_onDodge = int.Parse(array[13]);
    }
}
