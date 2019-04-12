using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Summon_remainsConfig配置表
/// </summary>
public partial class Summon_remainsConfig : IReader
{
    public List<Summon_remains> _Summon_remains = new List<Summon_remains>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Summon_remains.Add(new Summon_remains(array[i]));
        }
    }
}



/// <summary>
/// Summon_remains配置表
/// </summary>
public partial class Summon_remains : IReader
{
    /// <summary>
    /// 物品ID
    /// </summary>
    public int formulaID;
    /// <summary>
    /// 是否默认隐藏
    /// </summary>
    public int isHidden;
    /// <summary>
    /// 复生公式，结构为[itemID, costValue]
    /// </summary>
    public   List<List<int>> summonFormula;
    /// <summary>
    /// 
    /// </summary>
    public List<int> summonChar;
    /// <summary>
    /// 
    /// </summary>
    public List<int> selectChance;
    /// <summary>
    /// 
    /// </summary>
    public int goldCost;
    /// <summary>
    /// 
    /// </summary>
    public int manaCost;



    public Summon_remains() { }
    public Summon_remains(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        formulaID = int.Parse(array[0]);
        isHidden = int.Parse(array[1]);
        //列表summonFormula取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        summonFormula = new   List<List<int>>();
        foreach (var str in array[2].Split('-'))
        {
            try 
            {
                summonFormula.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表summonChar取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        summonChar = new List<int>();
        foreach (var _str in array[3].Split(','))
        {
            try { summonChar.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表selectChance取值
        array[4] = array[4].Replace("[", "").Replace("]", "").Replace(" ","");
        selectChance = new List<int>();
        foreach (var _str in array[4].Split(','))
        {
            try { selectChance.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        goldCost = int.Parse(array[5]);
        manaCost = int.Parse(array[6]);
    }
}
