using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Race_listConfig配置表
/// </summary>
public partial class Race_listConfig : IReader
{
    public List<Race_list> _Race_list = new List<Race_list>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Race_list.Add(new Race_list(array[i]));
        }
    }
}



/// <summary>
/// Race_list配置表
/// </summary>
public partial class Race_list : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int charRaceID;
    /// <summary>
    /// 
    /// </summary>
    public string raceName;



    public Race_list() { }
    public Race_list(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        charRaceID = int.Parse(array[0]);
        raceName = array[1];
    }
}
