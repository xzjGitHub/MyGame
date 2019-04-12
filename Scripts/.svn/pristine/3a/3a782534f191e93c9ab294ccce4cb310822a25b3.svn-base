using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Char_passivesetConfig配置表
/// </summary>
public partial class Char_passivesetConfig : IReader
{
    public List<Char_passiveset> _Char_passiveset = new List<Char_passiveset>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Char_passiveset.Add(new Char_passiveset(array[i]));
        }
    }
}



/// <summary>
/// Char_passiveset配置表
/// </summary>
public partial class Char_passiveset : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int passiveSetID;
    /// <summary>
    /// 
    /// </summary>
    public List<int> passiveSkillList;
    /// <summary>
    /// 
    /// </summary>
    public List<int> selectChance;



    public Char_passiveset() { }
    public Char_passiveset(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        passiveSetID = int.Parse(array[0]);
        //列表passiveSkillList取值
        array[1] = array[1].Replace("[", "").Replace("]", "").Replace(" ","");
        passiveSkillList = new List<int>();
        foreach (var _str in array[1].Split(','))
        {
            try { passiveSkillList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表selectChance取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        selectChance = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { selectChance.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
