using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Zone_templateConfig配置表
/// </summary>
public partial class Zone_templateConfig : IReader
{
    public List<Zone_template> _Zone_template = new List<Zone_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Zone_template.Add(new Zone_template(array[i]));
        }
    }
}



/// <summary>
/// Zone_template配置表
/// </summary>
public partial class Zone_template : IReader
{
    /// <summary>
    /// 区域ID
    /// </summary>
    public int zoneID;
    /// <summary>
    /// 区域名
    /// </summary>
    public string zoneName;
    /// <summary>
    /// 区域信息
    /// </summary>
    public string zoneDescription;
    /// <summary>
    /// 区域情报
    /// </summary>
    public string zoneIntel;
    /// <summary>
    /// 地图列表
    /// </summary>
    public List<int> mapList;
    /// <summary>
    /// 入侵集合列表
    /// </summary>
    public List<int> invasionSetList ;
    /// <summary>
    /// 随机任务集合列表
    /// </summary>
    public List<int> bountySetList;
    /// <summary>
    /// 探索刷新周期
    /// </summary>
    public int resetCycle;
    /// <summary>
    /// 探索集合
    /// </summary>
    public   List<List<int>> rndMapSet;
    /// <summary>
    /// 选择几率
    /// </summary>
    public List<int> selectChance;



    public Zone_template() { }
    public Zone_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        zoneID = int.Parse(array[0]);
        zoneName = array[1];
        zoneDescription = array[2];
        zoneIntel = array[3];
        //列表mapList取值
        array[4] = array[4].Replace("[", "").Replace("]", "").Replace(" ","");
        mapList = new List<int>();
        foreach (var _str in array[4].Split(','))
        {
            try { mapList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表invasionSetList 取值
        array[5] = array[5].Replace("[", "").Replace("]", "").Replace(" ","");
        invasionSetList  = new List<int>();
        foreach (var _str in array[5].Split(','))
        {
            try { invasionSetList .Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表bountySetList取值
        array[6] = array[6].Replace("[", "").Replace("]", "").Replace(" ","");
        bountySetList = new List<int>();
        foreach (var _str in array[6].Split(','))
        {
            try { bountySetList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        resetCycle = int.Parse(array[7]);
        //列表rndMapSet取值
        array[8] = array[8].Replace("[", "").Replace("]", "").Replace(" ","");
        rndMapSet = new   List<List<int>>();
        foreach (var str in array[8].Split('-'))
        {
            try 
            {
                rndMapSet.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表selectChance取值
        array[9] = array[9].Replace("[", "").Replace("]", "").Replace(" ","");
        selectChance = new List<int>();
        foreach (var _str in array[9].Split(','))
        {
            try { selectChance.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
