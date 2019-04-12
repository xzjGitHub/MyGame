using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Event_infoConfig配置表
/// </summary>
public partial class Event_infoConfig : IReader
{
    public List<Event_info> _Event_info = new List<Event_info>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Event_info.Add(new Event_info(array[i]));
        }
    }
}



/// <summary>
/// Event_info配置表
/// </summary>
public partial class Event_info : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int eventType;
    /// <summary>
    /// 
    /// </summary>
    public string typeName;
    /// <summary>
    /// 层名
    /// </summary>
    public string sortingLayer;
    /// <summary>
    /// 层顺序
    /// </summary>
    public int sortingOrder;
    /// <summary>
    /// 偏移量
    /// </summary>
    public int OffSet;
    /// <summary>
    /// 访问触发时间
    /// </summary>
    public float visitTime;



    public Event_info() { }
    public Event_info(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        eventType = int.Parse(array[0]);
        typeName = array[1];
        sortingLayer = array[2];
        sortingOrder = int.Parse(array[3]);
        OffSet = int.Parse(array[4]);
        visitTime = float.Parse(array[5]);
    }
}
