using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Summon_costConfig配置表
/// </summary>
public partial class Summon_costConfig : IReader
{
    public List<Summon_cost> _Summon_cost = new List<Summon_cost>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Summon_cost.Add(new Summon_cost(array[i]));
        }
    }
}



/// <summary>
/// Summon_cost配置表
/// </summary>
public partial class Summon_cost : IReader
{
    /// <summary>
    /// 召唤角色ID
    /// </summary>
    public int charID;
    /// <summary>
    /// 金币消耗
    /// </summary>
    public int goldCost;
    /// <summary>
    /// 魔力消耗
    /// </summary>
    public int manaCost;



    public Summon_cost() { }
    public Summon_cost(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        charID = int.Parse(array[0]);
        goldCost = int.Parse(array[1]);
        manaCost = int.Parse(array[2]);
    }
}
