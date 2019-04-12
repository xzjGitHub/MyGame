using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Research_lvupConfig配置表
/// </summary>
public partial class Research_lvupConfig : IReader
{
    public List<Research_lvup> _Research_lvup = new List<Research_lvup>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Research_lvup.Add(new Research_lvup(array[i]));
        }
    }
}



/// <summary>
/// Research_lvup配置表
/// </summary>
public partial class Research_lvup : IReader
{
    /// <summary>
    /// 当前等级
    /// </summary>
    public int researchLevel;
    /// <summary>
    /// 升级所需的累积值
    /// </summary>
    public int lvupExp;
    /// <summary>
    ///  [制造、附魔]的最大等级
    /// </summary>
    public List<int> maxItemLevel;
    /// <summary>
    /// [最小值加值，最大值加值]
    /// </summary>
    public List<int> addItemLevel;
    /// <summary>
    /// 装备配件上限
    /// </summary>
    public int maxParts;
    /// <summary>
    /// [最小值加值，最大值加值]
    /// </summary>
    public List<int> addEnchantLevel;
    /// <summary>
    ///  制造物品等级
    /// </summary>
    public int finalItemLevel;
    /// <summary>
    /// 制造物品品质
    /// </summary>
    public List<int> upgrade;



    public Research_lvup() { }
    public Research_lvup(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        researchLevel = int.Parse(array[0]);
        lvupExp = int.Parse(array[1]);
        //列表maxItemLevel取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        maxItemLevel = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { maxItemLevel.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表addItemLevel取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        addItemLevel = new List<int>();
        foreach (var _str in array[3].Split(','))
        {
            try { addItemLevel.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        maxParts = int.Parse(array[4]);
        //列表addEnchantLevel取值
        array[5] = array[5].Replace("[", "").Replace("]", "").Replace(" ","");
        addEnchantLevel = new List<int>();
        foreach (var _str in array[5].Split(','))
        {
            try { addEnchantLevel.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        finalItemLevel = int.Parse(array[6]);
        //列表upgrade取值
        array[7] = array[7].Replace("[", "").Replace("]", "").Replace(" ","");
        upgrade = new List<int>();
        foreach (var _str in array[7].Split(','))
        {
            try { upgrade.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
