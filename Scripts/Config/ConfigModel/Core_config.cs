using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Core_configConfig配置表
/// </summary>
public partial class Core_configConfig : IReader
{
    public List<Core_config> _Core_config = new List<Core_config>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Core_config.Add(new Core_config(array[i]));
        }
    }
}



/// <summary>
/// Core_config配置表
/// </summary>
public partial class Core_config : IReader
{
    /// <summary>
    /// 初始核心等级
    /// </summary>
    public int baseCorePower;
    /// <summary>
    /// 核心研究随机范围
    /// </summary>
    public float deviation;
    /// <summary>
    /// 最大核心等级
    /// </summary>
    public int maxCoreLevel;



    public Core_config() { }
    public Core_config(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        baseCorePower = int.Parse(array[0]);
        deviation = float.Parse(array[1]);
        maxCoreLevel = int.Parse(array[2]);
    }
}
