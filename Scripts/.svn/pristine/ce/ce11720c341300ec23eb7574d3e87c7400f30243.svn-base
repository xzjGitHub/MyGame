using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Char_attributesetConfig配置表
/// </summary>
public partial class Char_attributesetConfig : IReader
{
    public List<Char_attributeset> _Char_attributeset = new List<Char_attributeset>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Char_attributeset.Add(new Char_attributeset(array[i]));
        }
    }
}



/// <summary>
/// Char_attributeset配置表
/// </summary>
public partial class Char_attributeset : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int attributeID;
    /// <summary>
    /// 
    /// </summary>
    public int attributeCategory;
    /// <summary>
    /// 
    /// </summary>
    public string attributeName;
    /// <summary>
    /// 
    /// </summary>
    public string attributeNameCN;



    public Char_attributeset() { }
    public Char_attributeset(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        attributeID = int.Parse(array[0]);
        attributeCategory = int.Parse(array[1]);
        attributeName = array[2];
        attributeNameCN = array[3];
    }
}
