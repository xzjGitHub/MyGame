using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// State_templateConfig配置表
/// </summary>
public partial class State_templateConfig : IReader
{
    public List<State_template> _State_template = new List<State_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _State_template.Add(new State_template(array[i]));
        }
    }
}



/// <summary>
/// State_template配置表
/// </summary>
public partial class State_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int stateID;
    /// <summary>
    /// 
    /// </summary>
    public string stateName;
    /// <summary>
    /// 
    /// </summary>
    public int loadType;
    /// <summary>
    /// 加载频率（每N回合内必定能且只能加载1次）
    /// </summary>
    public int loadFrequency;
    /// <summary>
    /// 
    /// </summary>
    public int loadChance;
    /// <summary>
    /// 
    /// </summary>
    public int stateType;
    /// <summary>
    /// 技能能否共存，1=共存，2=替换
    /// </summary>
    public int stackType;
    /// <summary>
    /// 
    /// </summary>
    public int duration;
    /// <summary>
    /// 
    /// </summary>
    public int skillEffect;
    /// <summary>
    /// 公式枚举
    /// </summary>
    public int bvFormula;
    /// <summary>
    /// 状态系数
    /// </summary>
    public List<float> stateCoeSet;
    /// <summary>
    /// 护盾伤害修正
    /// </summary>
    public float stateShieldDB;
    /// <summary>
    /// 护甲伤害修正
    /// </summary>
    public float stateArmorDB;
    /// <summary>
    /// 生命伤害修正
    /// </summary>
    public float stateHPDB;
    /// <summary>
    /// 命中显示效果
    /// </summary>
    public int resultType;
    /// <summary>
    /// 状态持续特效
    /// </summary>
    public int stateEffect;
    /// <summary>
    /// 1=命中时增加标签
    /// </summary>
    public int HealingTag;
    /// <summary>
    /// 生命触发状态
    /// </summary>
    public int HPPropReq;
    /// <summary>
    /// 前置/后置触发
    /// </summary>
    public int triggerType;
    /// <summary>
    /// 触发的技能ID
    /// </summary>
    public int triggerSkill;
    /// <summary>
    /// 技能能否被驱散
    /// </summary>
    public int canBeDispelled ;



    public State_template() { }
    public State_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        stateID = int.Parse(array[0]);
        stateName = array[1];
        loadType = int.Parse(array[2]);
        loadFrequency = int.Parse(array[3]);
        loadChance = int.Parse(array[4]);
        stateType = int.Parse(array[5]);
        stackType = int.Parse(array[6]);
        duration = int.Parse(array[7]);
        skillEffect = int.Parse(array[8]);
        bvFormula = int.Parse(array[9]);
        //列表stateCoeSet取值
        array[10] = array[10].Replace("[", "").Replace("]", "").Replace(" ","");
        stateCoeSet = new List<float>();
        foreach (var _str in array[10].Split(','))
        {
            try { stateCoeSet.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        stateShieldDB = float.Parse(array[11]);
        stateArmorDB = float.Parse(array[12]);
        stateHPDB = float.Parse(array[13]);
        resultType = int.Parse(array[14]);
        stateEffect = int.Parse(array[15]);
        HealingTag = int.Parse(array[16]);
        HPPropReq = int.Parse(array[17]);
        triggerType = int.Parse(array[18]);
        triggerSkill = int.Parse(array[19]);
        canBeDispelled  = int.Parse(array[20]);
    }
}
