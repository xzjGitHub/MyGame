using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// WP_ConfigConfig配置表
/// </summary>
public partial class WP_ConfigConfig : IReader
{
    public List<WP_Config> _WP_Config = new List<WP_Config>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _WP_Config.Add(new WP_Config(array[i]));
        }
    }
}



/// <summary>
/// WP_Config配置表
/// </summary>
public partial class WP_Config : IReader
{
    /// <summary>
    /// 每个路点上的最小事件数
    /// </summary>
    public int minEvent;
    /// <summary>
    /// 每个路点上的最大事件数
    /// </summary>
    public int MaxEvent;



    public WP_Config() { }
    public WP_Config(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        minEvent = int.Parse(array[0]);
        MaxEvent = int.Parse(array[1]);
    }
}
