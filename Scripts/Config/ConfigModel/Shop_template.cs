using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Shop_templateConfig配置表
/// </summary>
public partial class Shop_templateConfig : IReader
{
    public List<Shop_template> _Shop_template = new List<Shop_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Shop_template.Add(new Shop_template(array[i]));
        }
    }
}



/// <summary>
/// Shop_template配置表
/// </summary>
public partial class Shop_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int sellListID;



    public Shop_template() { }
    public Shop_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        sellListID = int.Parse(array[0]);
    }
}
