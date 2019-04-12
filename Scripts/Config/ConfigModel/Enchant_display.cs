using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Enchant_displayConfig配置表
/// </summary>
public partial class Enchant_displayConfig : IReader
{
    public List<Enchant_display> _Enchant_display = new List<Enchant_display>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Enchant_display.Add(new Enchant_display(array[i]));
        }
    }
}



/// <summary>
/// Enchant_display配置表
/// </summary>
public partial class Enchant_display : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int id;
    /// <summary>
    /// 
    /// </summary>
    public string field;
    /// <summary>
    /// 
    /// </summary>
    public string source;
    /// <summary>
    /// 
    /// </summary>
    public List<string> enchant;
    /// <summary>
    /// 
    /// </summary>
    public List<string> enchant2;
    /// <summary>
    /// 
    /// </summary>
    public int isPercentage;
    /// <summary>
    /// 是否在信息前+1个点
    /// </summary>
    public int doudou;



    public Enchant_display() { }
    public Enchant_display(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        id = int.Parse(array[0]);
        field = array[1];
        source = array[2];
        //列表enchant取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        enchant = array[3] != String.Empty ? array[3].Split(',').ToList() : new List<string>();
        //列表enchant2取值
        array[4] = array[4].Replace("[", "").Replace("]", "").Replace(" ","");
        enchant2 = array[4] != String.Empty ? array[4].Split(',').ToList() : new List<string>();
        isPercentage = int.Parse(array[5]);
        doudou = int.Parse(array[6]);
    }
}
