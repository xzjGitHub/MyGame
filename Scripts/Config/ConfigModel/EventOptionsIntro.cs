using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// EventOptionsIntroConfig配置表
/// </summary>
public partial class EventOptionsIntroConfig : IReader
{
    public List<EventOptionsIntro> _EventOptionsIntro = new List<EventOptionsIntro>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _EventOptionsIntro.Add(new EventOptionsIntro(array[i]));
        }
    }
}



/// <summary>
/// EventOptionsIntro配置表
/// </summary>
public partial class EventOptionsIntro : IReader
{
    /// <summary>
    /// 编号
    /// </summary>
    public int ID;
    /// <summary>
    /// 内容
    /// </summary>
    public string content;



    public EventOptionsIntro() { }
    public EventOptionsIntro(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        ID = int.Parse(array[0]);
        content = array[1];
    }
}
