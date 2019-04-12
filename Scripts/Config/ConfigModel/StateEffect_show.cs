using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// StateEffect_showConfig配置表
/// </summary>
public partial class StateEffect_showConfig : IReader
{
    public List<StateEffect_show> _StateEffect_show = new List<StateEffect_show>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _StateEffect_show.Add(new StateEffect_show(array[i]));
        }
    }
}



/// <summary>
/// StateEffect_show配置表
/// </summary>
public partial class StateEffect_show : IReader
{
    /// <summary>
    /// 编号
    /// </summary>
    public int ID;
    /// <summary>
    /// 资源包名
    /// </summary>
    public string RP_Name;
    /// <summary>
    /// 动作名称
    /// </summary>
    public string CharActionName;
    /// <summary>
    /// 特效名称
    /// </summary>
    public string EffectName;
    /// <summary>
    /// 是否循环
    /// </summary>
    public int Loop;
    /// <summary>
    /// 是否跟随
    /// </summary>
    public int Follow;
    /// <summary>
    /// 类型
    /// </summary>
    public int Type;



    public StateEffect_show() { }
    public StateEffect_show(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        ID = int.Parse(array[0]);
        RP_Name = array[1];
        CharActionName = array[2];
        EffectName = array[3];
        Loop = int.Parse(array[4]);
        Follow = int.Parse(array[5]);
        Type = int.Parse(array[6]);
    }
}
