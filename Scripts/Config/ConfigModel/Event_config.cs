using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Event_configConfig配置表
/// </summary>
public partial class Event_configConfig : IReader
{
    public List<Event_config> _Event_config = new List<Event_config>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Event_config.Add(new Event_config(array[i]));
        }
    }
}



/// <summary>
/// Event_config配置表
/// </summary>
public partial class Event_config : IReader
{
    /// <summary>
    /// 选项奖励浮动
    /// </summary>
    public float selectionDeviation;
    /// <summary>
    /// 固有选项[无视，返回，复活，撤退]
    /// </summary>
    public List<int> defaultSelection;
    /// <summary>
    /// 30秒 = 1天
    /// </summary>
    public int baseTimeCost;



    public Event_config() { }
    public Event_config(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        selectionDeviation = float.Parse(array[0]);
        //列表defaultSelection取值
        array[1] = array[1].Replace("[", "").Replace("]", "").Replace(" ","");
        defaultSelection = new List<int>();
        foreach (var _str in array[1].Split(','))
        {
            try { defaultSelection.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        baseTimeCost = int.Parse(array[2]);
    }
}
