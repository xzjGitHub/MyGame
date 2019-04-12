using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// EventDisplayTemplateConfig配置表
/// </summary>
public partial class EventDisplayTemplateConfig : IReader
{
    public List<EventDisplayTemplate> _EventDisplayTemplate = new List<EventDisplayTemplate>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _EventDisplayTemplate.Add(new EventDisplayTemplate(array[i]));
        }
    }
}



/// <summary>
/// EventDisplayTemplate配置表
/// </summary>
public partial class EventDisplayTemplate : IReader
{
    /// <summary>
    /// 资源包名
    /// </summary>
    public string RP_Name;
    /// <summary>
    /// 层名
    /// </summary>
    public string sortingLayerName;
    /// <summary>
    /// 层顺序
    /// </summary>
    public int sortingOrder;
    /// <summary>
    /// Y轴
    /// </summary>
    public float YValue;



    public EventDisplayTemplate() { }
    public EventDisplayTemplate(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        RP_Name = array[0];
        sortingLayerName = array[1];
        sortingOrder = int.Parse(array[2]);
        YValue = float.Parse(array[3]);
    }
}
