using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Mob_skillsetConfig配置表
/// </summary>
public partial class Mob_skillsetConfig : IReader
{
    public List<Mob_skillset> _Mob_skillset = new List<Mob_skillset>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Mob_skillset.Add(new Mob_skillset(array[i]));
        }
    }
}



/// <summary>
/// Mob_skillset配置表
/// </summary>
public partial class Mob_skillset : IReader
{
    /// <summary>
    /// 技能集合ID
    /// </summary>
    public int skillSetID;
    /// <summary>
    /// 技能列表
    /// </summary>
    public List<int> skillList;
    /// <summary>
    /// 检测次数
    /// </summary>
    public List<int> selectCount;
    /// <summary>
    /// 检测周期
    /// </summary>
    public int selectCycle;



    public Mob_skillset() { }
    public Mob_skillset(string content)
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
        //列表skillList取值
        array[1] = array[1].Replace("[", "").Replace("]", "").Replace(" ","");
        skillList = new List<int>();
        foreach (var _str in array[1].Split(','))
        {
            try { skillList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表selectCount取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        selectCount = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { selectCount.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        selectCycle = int.Parse(array[3]);
    }
}
