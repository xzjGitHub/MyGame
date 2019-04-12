using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Enchant_template_bkConfig配置表
/// </summary>
public partial class Enchant_template_bkConfig : IReader
{
    public List<Enchant_template_bk> _Enchant_template_bk = new List<Enchant_template_bk>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Enchant_template_bk.Add(new Enchant_template_bk(array[i]));
        }
    }
}



/// <summary>
/// Enchant_template_bk配置表
/// </summary>
public partial class Enchant_template_bk : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int effectID;
    /// <summary>
    /// 
    /// </summary>
    public string effectName;
    /// <summary>
    /// 
    /// </summary>
    public int enchantType;
    /// <summary>
    /// 
    /// </summary>
    public int PerkID;
    /// <summary>
    /// 
    /// </summary>
    public string description;



    public Enchant_template_bk() { }
    public Enchant_template_bk(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        effectID = int.Parse(array[0]);
        effectName = array[1];
        enchantType = int.Parse(array[2]);
        PerkID = int.Parse(array[3]);
        description = array[4];
    }
}
