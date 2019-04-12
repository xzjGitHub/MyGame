using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Char_attributeConfig配置表
/// </summary>
public partial class Char_attributeConfig : IReader
{
    public List<Char_attribute> _Char_attribute = new List<Char_attribute>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Char_attribute.Add(new Char_attribute(array[i]));
        }
    }
}



/// <summary>
/// Char_attribute配置表
/// </summary>
public partial class Char_attribute : IReader
{
    /// <summary>
    /// ID
    /// </summary>
    public int Id;
    /// <summary>
    /// 名称
    /// </summary>
    public string name;
    /// <summary>
    /// 字段名
    /// </summary>
    public string filedName;
    /// <summary>
    /// 描述
    /// </summary>
    public string Describe;



    public Char_attribute() { }
    public Char_attribute(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        Id = int.Parse(array[0]);
        name = array[1];
        filedName = array[2];
        Describe = array[3];
    }
}
