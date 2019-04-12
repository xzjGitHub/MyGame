using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Char_skillsetConfig配置表
/// </summary>
public partial class Char_skillsetConfig : IReader
{
    public List<Char_skillset> _Char_skillset = new List<Char_skillset>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Char_skillset.Add(new Char_skillset(array[i]));
        }
    }
}



/// <summary>
/// Char_skillset配置表
/// </summary>
public partial class Char_skillset : IReader
{
    /// <summary>
    /// 技能集合ID
    /// </summary>
    public int skillSetID;
    /// <summary>
    /// 技能列表
    /// </summary>
    public int manualSkill;
    /// <summary>
    /// 
    /// </summary>
    public List<int> tacticalSkill;
    /// <summary>
    /// 
    /// </summary>
    public List<int> passiveSkill;



    public Char_skillset() { }
    public Char_skillset(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        skillSetID = int.Parse(array[0]);
        manualSkill = int.Parse(array[1]);
        //列表tacticalSkill取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        tacticalSkill = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { tacticalSkill.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表passiveSkill取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        passiveSkill = new List<int>();
        foreach (var _str in array[3].Split(','))
        {
            try { passiveSkill.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
