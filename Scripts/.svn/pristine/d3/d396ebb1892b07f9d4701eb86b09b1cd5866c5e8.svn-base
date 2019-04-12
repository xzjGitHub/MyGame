using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Equip_displayConfig配置表
/// </summary>
public partial class Equip_displayConfig : IReader
{
    public List<Equip_display> _Equip_display = new List<Equip_display>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Equip_display.Add(new Equip_display(array[i]));
        }
    }
}



/// <summary>
/// Equip_display配置表
/// </summary>
public partial class Equip_display : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int id;
    /// <summary>
    /// “伤害 {minDMG}~{maxDMG}”
    /// </summary>
    public string field;
    /// <summary>
    /// 
    /// </summary>
    public string source;
    /// <summary>
    /// 
    /// </summary>
    public List<string> weapon;
    /// <summary>
    /// 
    /// </summary>
    public List<string> armor;
    /// <summary>
    /// 
    /// </summary>
    public List<string> amulet;
    /// <summary>
    /// 
    /// </summary>
    public List<string> ring;
    /// <summary>
    /// 
    /// </summary>
    public int isPercentage;
    /// <summary>
    /// 是否在信息前+1个点
    /// </summary>
    public int doudou;



    public Equip_display() { }
    public Equip_display(string content)
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
        //列表weapon取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        weapon = array[3] != String.Empty ? array[3].Split(',').ToList() : new List<string>();
        //列表armor取值
        array[4] = array[4].Replace("[", "").Replace("]", "").Replace(" ","");
        armor = array[4] != String.Empty ? array[4].Split(',').ToList() : new List<string>();
        //列表amulet取值
        array[5] = array[5].Replace("[", "").Replace("]", "").Replace(" ","");
        amulet = array[5] != String.Empty ? array[5].Split(',').ToList() : new List<string>();
        //列表ring取值
        array[6] = array[6].Replace("[", "").Replace("]", "").Replace(" ","");
        ring = array[6] != String.Empty ? array[6].Split(',').ToList() : new List<string>();
        isPercentage = int.Parse(array[7]);
        doudou = int.Parse(array[8]);
    }
}
