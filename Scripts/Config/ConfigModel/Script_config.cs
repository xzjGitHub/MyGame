using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Script_configConfig配置表
/// </summary>
public partial class Script_configConfig : IReader
{
    public List<Script_config> _Script_config = new List<Script_config>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Script_config.Add(new Script_config(array[i]));
        }
    }
}



/// <summary>
/// Script_config配置表
/// </summary>
public partial class Script_config : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int timeUnit;
    /// <summary>
    /// 
    /// </summary>
    public float rewardDeviation;
    /// <summary>
    /// 
    /// </summary>
    public int shopResetCost;
    /// <summary>
    /// 最受欢迎的阵营的随机任务几率
    /// </summary>
    public int maxfactionChance;
    /// <summary>
    /// 最低几率
    /// </summary>
    public int minFactionChance;



    public Script_config() { }
    public Script_config(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        timeUnit = int.Parse(array[0]);
        rewardDeviation = float.Parse(array[1]);
        shopResetCost = int.Parse(array[2]);
        maxfactionChance = int.Parse(array[3]);
        minFactionChance = int.Parse(array[4]);
    }
}
