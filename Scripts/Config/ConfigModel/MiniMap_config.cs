using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// MiniMap_configConfig配置表
/// </summary>
public partial class MiniMap_configConfig : IReader
{
    public List<MiniMap_config> _MiniMap_config = new List<MiniMap_config>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _MiniMap_config.Add(new MiniMap_config(array[i]));
        }
    }
}



/// <summary>
/// MiniMap_config配置表
/// </summary>
public partial class MiniMap_config : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int mapHeight;
    /// <summary>
    /// 
    /// </summary>
    public int mapCopy;



    public MiniMap_config() { }
    public MiniMap_config(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        mapHeight = int.Parse(array[0]);
        mapCopy = int.Parse(array[1]);
    }
}
