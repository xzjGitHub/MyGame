using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Enchant_configConfig配置表
/// </summary>
public partial class Enchant_configConfig : IReader
{
    public List<Enchant_config> _Enchant_config = new List<Enchant_config>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Enchant_config.Add(new Enchant_config(array[i]));
        }
    }
}



/// <summary>
/// Enchant_config配置表
/// </summary>
public partial class Enchant_config : IReader
{
    /// <summary>
    /// 附魔稀素消耗
    /// </summary>
    public int enchantCost;
    /// <summary>
    /// 
    /// </summary>
    public List<int> goldCost;
    /// <summary>
    /// 
    /// </summary>
    public List<int> manaCost;
    /// <summary>
    /// 研究稀素消耗
    /// </summary>
    public int researchCost;
    /// <summary>
    /// 研究值随机
    /// </summary>
    public float researchDeviation;
    /// <summary>
    /// 最大的附魔可选项数量
    /// </summary>
    public int maxEnchantSelection;
    /// <summary>
    /// 附魔的数值随机偏移
    /// </summary>
    public float enchantDeviation;



    public Enchant_config() { }
    public Enchant_config(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        enchantCost = int.Parse(array[0]);
        //列表goldCost取值
        array[1] = array[1].Replace("[", "").Replace("]", "").Replace(" ","");
        goldCost = new List<int>();
        foreach (var _str in array[1].Split(','))
        {
            try { goldCost.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表manaCost取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        manaCost = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { manaCost.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        researchCost = int.Parse(array[3]);
        researchDeviation = float.Parse(array[4]);
        maxEnchantSelection = int.Parse(array[5]);
        enchantDeviation = float.Parse(array[6]);
    }
}
