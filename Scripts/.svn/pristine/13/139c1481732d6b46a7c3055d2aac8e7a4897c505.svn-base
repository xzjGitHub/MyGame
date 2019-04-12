using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// MiniMap_randomConfig配置表
/// </summary>
public partial class MiniMap_randomConfig : IReader
{
    public List<MiniMap_random> _MiniMap_random = new List<MiniMap_random>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _MiniMap_random.Add(new MiniMap_random(array[i]));
        }
    }
}



/// <summary>
/// MiniMap_random配置表
/// </summary>
public partial class MiniMap_random : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int pointID;
    /// <summary>
    /// 
    /// </summary>
    public string pointName;
    /// <summary>
    /// 
    /// </summary>
    public int selectChance;



    public MiniMap_random() { }
    public MiniMap_random(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        pointID = int.Parse(array[0]);
        pointName = array[1];
        selectChance = int.Parse(array[2]);
    }
}
