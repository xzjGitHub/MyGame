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
    /// 
    /// </summary>
    public string mapIcon;
    /// <summary>
    /// 地图信息文本ID
    /// </summary>
    public int mapText;
    /// <summary>
    /// 地图环境
    /// </summary>
    public int mapType;
    /// <summary>
    /// 起始路点
    /// </summary>
    public int initialWP;
    /// <summary>
    /// 最后路点
    /// </summary>
    public int EndWP;
    /// <summary>
    /// 标准路点数
    /// </summary>
    public int WPCount;
    /// <summary>
    /// 解锁需求
    /// </summary>
    public int unlockReq;
    /// <summary>
    /// 解锁要塞
    /// </summary>
    public int unlockFort;
    /// <summary>
    /// 可用位置
    /// </summary>
    public List<float> position;
    /// <summary>
    /// 地图品质
    /// </summary>
    public int mapQuality;
    /// <summary>
    /// 地图等级
    /// </summary>
    public int baseWPLevel;
    /// <summary>
    /// 生命周期
    /// </summary>
    public int duration;
    /// <summary>
    /// 可发现的探索
    /// </summary>
    public   List<List<int>> disMapSet;
    /// <summary>
    /// 发现探索几率
    /// </summary>
    public   List<List<int>> disMapChance;



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
        mapType = int.Parse(array[5]);
        initialWP = int.Parse(array[6]);
        EndWP = int.Parse(array[7]);
        WPCount = int.Parse(array[8]);
        unlockReq = int.Parse(array[9]);
        unlockFort = int.Parse(array[10]);
        //列表position取值
        array[11] = array[11].Replace("[", "").Replace("]", "").Replace(" ","");
        position = new List<float>();
        foreach (var _str in array[11].Split(','))
        {
            try { position.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        mapQuality = int.Parse(array[12]);
        baseWPLevel = int.Parse(array[13]);
        duration = int.Parse(array[14]);
        //列表disMapSet取值
        array[15] = array[15].Replace("[", "").Replace("]", "").Replace(" ","");
        disMapSet = new   List<List<int>>();
        foreach (var str in array[15].Split('-'))
        {
            try 
            {
                disMapSet.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表disMapChance取值
        array[16] = array[16].Replace("[", "").Replace("]", "").Replace(" ","");
        disMapChance = new   List<List<int>>();
        foreach (var str in array[16].Split('-'))
        {
            try 
            {
                disMapChance.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
    }
}
