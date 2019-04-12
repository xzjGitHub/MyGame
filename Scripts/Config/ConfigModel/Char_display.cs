using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Char_displayConfig配置表
/// </summary>
public partial class Char_displayConfig : IReader
{
    public List<Char_display> _Char_display = new List<Char_display>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Char_display.Add(new Char_display(array[i]));
        }
    }
}



/// <summary>
/// Char_display配置表
/// </summary>
public partial class Char_display : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int attributeID;
    /// <summary>
    /// 1 = 显示图标，2 = 简略显示
    /// </summary>
    public int attributeCategory;
    /// <summary>
    /// 
    /// </summary>
    public string attributeIcon;
    /// <summary>
    /// 用于复活界面
    /// </summary>
    public string attributeIcon2;
    /// <summary>
    /// 
    /// </summary>
    public string attributeName;
    /// <summary>
    /// 
    /// </summary>
    public string source;
    /// <summary>
    /// 
    /// </summary>
    public string attributeNameCN;
    /// <summary>
    /// 
    /// </summary>
    public int isPercentage;
    /// <summary>
    /// 
    /// </summary>
    public string attributeDescription;
    /// <summary>
    /// 详细属性列表
    /// </summary>
    public List<int> details;



    public Char_display() { }
    public Char_display(string content)
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
        attributeIcon = array[2];
        attributeIcon2 = array[3];
        attributeName = array[4];
        source = array[5];
        attributeNameCN = array[6];
        isPercentage = int.Parse(array[7]);
        attributeDescription = array[8];
        //列表details取值
        array[9] = array[9].Replace("[", "").Replace("]", "").Replace(" ","");
        details = new List<int>();
        foreach (var _str in array[9].Split(','))
        {
            try { details.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
