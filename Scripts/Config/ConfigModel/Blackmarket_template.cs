using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Blackmarket_templateConfig配置表
/// </summary>
public partial class Blackmarket_templateConfig : IReader
{
    public List<Blackmarket_template> _Blackmarket_template = new List<Blackmarket_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Blackmarket_template.Add(new Blackmarket_template(array[i]));
        }
    }
}



/// <summary>
/// Blackmarket_template配置表
/// </summary>
public partial class Blackmarket_template : IReader
{
    /// <summary>
    /// 区域名
    /// </summary>
    public int zoneID;
    /// <summary>
    /// 奖励集合
    /// </summary>
    public List<int> itemRewardSet;
    /// <summary>
    /// 奖励值
    /// </summary>
    public List<int> baseRewardLevel;



    public Blackmarket_template() { }
    public Blackmarket_template(string content)
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
        //列表itemRewardSet取值
        array[1] = array[1].Replace("[", "").Replace("]", "").Replace(" ","");
        itemRewardSet = new List<int>();
        foreach (var _str in array[1].Split(','))
        {
            try { itemRewardSet.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表baseRewardLevel取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        baseRewardLevel = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { baseRewardLevel.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
