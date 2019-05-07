using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Map_templateConfig配置表
/// </summary>
public partial class Map_templateConfig : IReader
{
    public List<Map_template> _Map_template = new List<Map_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Map_template.Add(new Map_template(array[i]));
        }
    }
}



/// <summary>
/// Map_template配置表
/// </summary>
public partial class Map_template : IReader
{
    /// <summary>
    /// 地图ID
    /// </summary>
    public int mapID;
    /// <summary>
    /// 地图名
    /// </summary>
    public string mapName;
    /// <summary>
    /// 地图等级
    /// </summary>
    public string mapLevel;
    /// <summary>
    /// 地图图标
    /// </summary>
    public string mapIcon;
    /// <summary>
    /// 地图文本ID
    /// </summary>
    public int mapText;
    /// <summary>
    /// 起始路点
    /// </summary>
    public int startWP;
    /// <summary>
    /// 最后路点
    /// </summary>
    public int endWP;
    /// <summary>
    /// 路点数
    /// </summary>
    public int WPCount;
    /// <summary>
    /// 解锁要塞
    /// </summary>
    public int unlockFort;
    /// <summary>
    /// 可用位置
    /// </summary>
    public List<float> position;



    public Map_template() { }
    public Map_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        mapID = int.Parse(array[0]);
        mapName = array[1];
        mapLevel = array[2];
        mapIcon = array[3];
        mapText = int.Parse(array[4]);
        startWP = int.Parse(array[5]);
        endWP = int.Parse(array[6]);
        WPCount = int.Parse(array[7]);
        unlockFort = int.Parse(array[8]);
        //列表position取值
        array[9] = array[9].Replace("[", "").Replace("]", "").Replace(" ","");
        position = new List<float>();
        foreach (var _str in array[9].Split(','))
        {
            try { position.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
