using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Action_templateConfig配置表
/// </summary>
public partial class Action_templateConfig : IReader
{
    public List<Action_template> _Action_template = new List<Action_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Action_template.Add(new Action_template(array[i]));
        }
    }
}



/// <summary>
/// Action_template配置表
/// </summary>
public partial class Action_template : IReader
{
    /// <summary>
    /// 动作效果ID
    /// </summary>
    public int actionID;
    /// <summary>
    /// isAnmiated = 1的targetSetID
    /// </summary>
    public int mainTargetSet;
    /// <summary>
    /// isAnmiated = 0的targetSetID
    /// </summary>
    public List<int> subTargetSet;
    /// <summary>
    /// 动作名称
    /// </summary>
    public string charActionName;
    /// <summary>
    /// 动作目的地
    /// </summary>
    public int skillType;
    /// <summary>
    /// 动作目的地偏移坐标
    /// </summary>
    public float CSYS_x;
    /// <summary>
    /// 
    /// </summary>
    public float CSYS_y;
    /// <summary>
    /// 在事件上绑定的效果，基本上每个效果对应1个事件，每个EO效果通常包含1个发射和1个命中效果；EOEvent的效果读自ObjectEffect；
    /// </summary>
    public List<int> skillEffect;
    /// <summary>
    /// 
    /// </summary>
    public List<int> hitEffect;
    /// <summary>
    /// EO必须要填，否则状态加载无法工作
    /// </summary>
    public List<int> EOEffect;



    public Action_template() { }
    public Action_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        actionID = int.Parse(array[0]);
        mainTargetSet = int.Parse(array[1]);
        //列表subTargetSet取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        subTargetSet = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { subTargetSet.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        charActionName = array[3];
        skillType = int.Parse(array[4]);
        CSYS_x = float.Parse(array[5]);
        CSYS_y = float.Parse(array[6]);
        //列表skillEffect取值
        array[7] = array[7].Replace("[", "").Replace("]", "").Replace(" ","");
        skillEffect = new List<int>();
        foreach (var _str in array[7].Split(','))
        {
            try { skillEffect.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表hitEffect取值
        array[8] = array[8].Replace("[", "").Replace("]", "").Replace(" ","");
        hitEffect = new List<int>();
        foreach (var _str in array[8].Split(','))
        {
            try { hitEffect.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表EOEffect取值
        array[9] = array[9].Replace("[", "").Replace("]", "").Replace(" ","");
        EOEffect = new List<int>();
        foreach (var _str in array[9].Split(','))
        {
            try { EOEffect.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
