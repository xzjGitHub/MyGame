using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Explore_configConfig配置表
/// </summary>
public partial class Explore_configConfig : IReader
{
    public List<Explore_config> _Explore_config = new List<Explore_config>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Explore_config.Add(new Explore_config(array[i]));
        }
    }
}



/// <summary>
/// Explore_config配置表
/// </summary>
public partial class Explore_config : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int baseBranchChance;



    public Explore_config() { }
    public Explore_config(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        baseBranchChance = int.Parse(array[0]);
    }
}
