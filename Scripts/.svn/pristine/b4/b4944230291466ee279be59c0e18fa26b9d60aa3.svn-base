using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Tag_listConfig配置表
/// </summary>
public partial class Tag_listConfig : IReader
{
    public List<Tag_list> _Tag_list = new List<Tag_list>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Tag_list.Add(new Tag_list(array[i]));
        }
    }
}



/// <summary>
/// Tag_list配置表
/// </summary>
public partial class Tag_list : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int charRace;
    /// <summary>
    /// 
    /// </summary>
    public string raceName;



    public Tag_list() { }
    public Tag_list(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        charRace = int.Parse(array[0]);
        raceName = array[1];
    }
}
