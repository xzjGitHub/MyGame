using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Invasion_configConfig配置表
/// </summary>
public partial class Invasion_configConfig : IReader
{
    public List<Invasion_config> _Invasion_config = new List<Invasion_config>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Invasion_config.Add(new Invasion_config(array[i]));
        }
    }
}



/// <summary>
/// Invasion_config配置表
/// </summary>
public partial class Invasion_config : IReader
{
    /// <summary>
    /// 每月增加威胁值
    /// </summary>
    public int baseThreat;
    /// <summary>
    /// 触发入侵的威胁需求
    /// </summary>
    public int threatReq;
    /// <summary>
    /// 威胁需求随机范围
    /// </summary>
    public float deviation;
    /// <summary>
    /// 集结时间(隐藏)随机范围
    /// </summary>
    public List<int> preTime ;
    /// <summary>
    /// 围攻周期
    /// </summary>
    public int siegeCycle;
    /// <summary>
    /// 核心最大生命值
    /// </summary>
    public int maxCoreHP;



    public Invasion_config() { }
    public Invasion_config(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        baseThreat = int.Parse(array[0]);
        threatReq = int.Parse(array[1]);
        deviation = float.Parse(array[2]);
        //列表preTime 取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        preTime  = new List<int>();
        foreach (var _str in array[3].Split(','))
        {
            try { preTime .Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        siegeCycle = int.Parse(array[4]);
        maxCoreHP = int.Parse(array[5]);
    }
}
