using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Event_rndsetConfig配置表
/// </summary>
public partial class Event_rndsetConfig : IReader
{
    public List<Event_rndset> _Event_rndset = new List<Event_rndset>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Event_rndset.Add(new Event_rndset(array[i]));
        }
    }
}



/// <summary>
/// Event_rndset配置表
/// </summary>
public partial class Event_rndset : IReader
{
    /// <summary>
    /// 随机事件集合ID
    /// </summary>
    public int eventSet;
    /// <summary>
    /// 随机事件列表,0的时候为轮空
    /// </summary>
    public   List<List<int>> eventList;
    /// <summary>
    /// 随机几率,10000总值
    /// </summary>
    public   List<List<int>> selectChance;



    public Event_rndset() { }
    public Event_rndset(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        eventSet = int.Parse(array[0]);
        //列表eventList取值
        array[1] = array[1].Replace("[", "").Replace("]", "").Replace(" ","");
        eventList = new   List<List<int>>();
        foreach (var str in array[1].Split('-'))
        {
            try 
            {
                eventList.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表selectChance取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        selectChance = new   List<List<int>>();
        foreach (var str in array[2].Split('-'))
        {
            try 
            {
                selectChance.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
    }
}
