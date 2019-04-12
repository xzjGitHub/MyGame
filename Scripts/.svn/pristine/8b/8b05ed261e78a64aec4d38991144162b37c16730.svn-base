using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// WP_templateConfig配置表
/// </summary>
public partial class WP_templateConfig : IReader
{
    public List<WP_template> _WP_template = new List<WP_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _WP_template.Add(new WP_template(array[i]));
        }
    }
}



/// <summary>
/// WP_template配置表
/// </summary>
public partial class WP_template : IReader
{
    /// <summary>
    /// 路点ID
    /// </summary>
    public int WPID;
    /// <summary>
    /// 路点名
    /// </summary>
    public string WPName;
    /// <summary>
    /// 地图目标
    /// </summary>
    public string WPTarget;
    /// <summary>
    /// 路点说明
    /// </summary>
    public string WPDescription;
    /// <summary>
    /// 
    /// </summary>
    public int WPDialog ;
    /// <summary>
    /// 路点类别(道路/房间)
    /// </summary>
    public int WPCategory;
    /// <summary>
    /// 环境类型
    /// </summary>
    public int envirType;
    /// <summary>
    /// 下个路点
    /// </summary>
    public List<int> nextWP;
    /// <summary>
    /// 路点等级
    /// </summary>
    public int WPLevel;
    /// <summary>
    /// 怪物等级
    /// </summary>
    public int mobLevel;
    /// <summary>
    /// 路点长度（屏数）
    /// </summary>
    public int WPLength;
    /// <summary>
    /// 在指定位置放置指定事件，第1屏2个位置，第2~4屏3个位置，[1,1]-[1,1,1]-[1,1,1]-[1,1,1]，值为事件ID
    /// </summary>
    public   List<List<int>> WPEvent;
    /// <summary>
    /// 道路中的事件总数，随机数量[3~6]，[最小，最大]
    /// </summary>
    public List<int> eventCount;
    /// <summary>
    /// 使用单一集合，尽量不连续重复，但允许重复
    /// </summary>
    public int eventSet;
    /// <summary>
    /// 
    /// </summary>
    public int maxBush;
    /// <summary>
    /// 最大草丛事件数=5
    /// </summary>
    public int bushChance;
    /// <summary>
    /// 
    /// </summary>
    public int bushSet;
    /// <summary>
    /// 
    /// </summary>
    public int maxTrap;
    /// <summary>
    /// 最大陷阱数 = 2
    /// </summary>
    public int trapChance;
    /// <summary>
    /// 
    /// </summary>
    public int trapSet;



    public WP_template() { }
    public WP_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        WPID = int.Parse(array[0]);
        WPName = array[1];
        WPTarget = array[2];
        WPDescription = array[3];
        WPDialog  = int.Parse(array[4]);
        WPCategory = int.Parse(array[5]);
        envirType = int.Parse(array[6]);
        //列表nextWP取值
        array[7] = array[7].Replace("[", "").Replace("]", "").Replace(" ","");
        nextWP = new List<int>();
        foreach (var _str in array[7].Split(','))
        {
            try { nextWP.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        WPLevel = int.Parse(array[8]);
        mobLevel = int.Parse(array[9]);
        WPLength = int.Parse(array[10]);
        //列表WPEvent取值
        array[11] = array[11].Replace("[", "").Replace("]", "").Replace(" ","");
        WPEvent = new   List<List<int>>();
        foreach (var str in array[11].Split('-'))
        {
            try 
            {
                WPEvent.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表eventCount取值
        array[12] = array[12].Replace("[", "").Replace("]", "").Replace(" ","");
        eventCount = new List<int>();
        foreach (var _str in array[12].Split(','))
        {
            try { eventCount.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        eventSet = int.Parse(array[13]);
        maxBush = int.Parse(array[14]);
        bushChance = int.Parse(array[15]);
        bushSet = int.Parse(array[16]);
        maxTrap = int.Parse(array[17]);
        trapChance = int.Parse(array[18]);
        trapSet = int.Parse(array[19]);
    }
}
