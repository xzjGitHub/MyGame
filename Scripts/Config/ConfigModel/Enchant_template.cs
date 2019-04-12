using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Enchant_templateConfig配置表
/// </summary>
public partial class Enchant_templateConfig : IReader
{
    public List<Enchant_template> _Enchant_template = new List<Enchant_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Enchant_template.Add(new Enchant_template(array[i]));
        }
    }
}



/// <summary>
/// Enchant_template配置表
/// </summary>
public partial class Enchant_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int templateID;
    /// <summary>
    /// 
    /// </summary>
    public string enchantName;
    /// <summary>
    /// [初始值，如果有初始值则 = 0，没有则= 1]
    /// </summary>
    public List<float> baseEquipRank;
    /// <summary>
    /// 
    /// </summary>
    public float APBonus;
    /// <summary>
    /// 
    /// </summary>
    public float SPBonus;
    /// <summary>
    /// 
    /// </summary>
    public float SkillPB;
    /// <summary>
    /// 
    /// </summary>
    public float ShieldDB;
    /// <summary>
    /// 
    /// </summary>
    public float ArmorDB;
    /// <summary>
    /// 
    /// </summary>
    public float HPDB;
    /// <summary>
    /// 
    /// </summary>
    public float ShieldBonus;
    /// <summary>
    /// 
    /// </summary>
    public float ArmorBonus;
    /// <summary>
    /// 
    /// </summary>
    public float HPBonus;
    /// <summary>
    /// 随机强化
    /// </summary>
    public List<int> upgrade;
    /// <summary>
    /// 
    /// </summary>
    public int enchantGoldCost;
    /// <summary>
    /// 
    /// </summary>
    public int enchantManaCost;



    public Enchant_template() { }
    public Enchant_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        templateID = int.Parse(array[0]);
        enchantName = array[1];
        //列表baseEquipRank取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        baseEquipRank = new List<float>();
        foreach (var _str in array[2].Split(','))
        {
            try { baseEquipRank.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        APBonus = float.Parse(array[3]);
        SPBonus = float.Parse(array[4]);
        SkillPB = float.Parse(array[5]);
        ShieldDB = float.Parse(array[6]);
        ArmorDB = float.Parse(array[7]);
        HPDB = float.Parse(array[8]);
        ShieldBonus = float.Parse(array[9]);
        ArmorBonus = float.Parse(array[10]);
        HPBonus = float.Parse(array[11]);
        //列表upgrade取值
        array[12] = array[12].Replace("[", "").Replace("]", "").Replace(" ","");
        upgrade = new List<int>();
        foreach (var _str in array[12].Split(','))
        {
            try { upgrade.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        enchantGoldCost = int.Parse(array[13]);
        enchantManaCost = int.Parse(array[14]);
    }
}
