using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// AttributeDescriptionConfig配置表
/// </summary>
public partial class AttributeDescriptionConfig : IReader
{
    public List<AttributeDescription> _AttributeDescription = new List<AttributeDescription>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _AttributeDescription.Add(new AttributeDescription(array[i]));
        }
    }
}



/// <summary>
/// AttributeDescription配置表
/// </summary>
public partial class AttributeDescription : IReader
{
    /// <summary>
    /// ID
    /// </summary>
    public int ID;
    /// <summary>
    /// 名称
    /// </summary>
    public string attributeName;
    /// <summary>
    /// 字段名
    /// </summary>
    public string attribute;
    /// <summary>
    /// 描述
    /// </summary>
    public string Description;
    /// <summary>
    /// 
    /// </summary>
    public List<string> valueAttribute;
    /// <summary>
    /// 
    /// </summary>
    public List<int> isPer;
    /// <summary>
    /// 
    /// </summary>
    public string valuePotential;



    public AttributeDescription() { }
    public AttributeDescription(string content)
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
        attributeName = array[1];
        attribute = array[2];
        Description = array[3];
        //列表valueAttribute取值
        array[4] = array[4].Replace("[", "").Replace("]", "").Replace(" ","");
        valueAttribute = array[4] != String.Empty ? array[4].Split(',').ToList() : new List<string>();
        //列表isPer取值
        array[5] = array[5].Replace("[", "").Replace("]", "").Replace(" ","");
        isPer = new List<int>();
        foreach (var _str in array[5].Split(','))
        {
            try { isPer.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        valuePotential = array[6];
    }
}
