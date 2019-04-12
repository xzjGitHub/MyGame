using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Summon_listConfig配置表
/// </summary>
public partial class Summon_listConfig : IReader
{
    public List<Summon_list> _Summon_list = new List<Summon_list>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Summon_list.Add(new Summon_list(array[i]));
        }
    }
}



/// <summary>
/// Summon_list配置表
/// </summary>
public partial class Summon_list : IReader
{
    /// <summary>
    /// 剧本ID
    /// </summary>
    public int ScriptID;
    /// <summary>
    /// 可召唤角色列表
    /// </summary>
    public List<int> summonList;
    /// <summary>
    /// 选择几率
    /// </summary>
    public List<int> selectChance;



    public Summon_list() { }
    public Summon_list(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        ScriptID = int.Parse(array[0]);
        //列表summonList取值
        array[1] = array[1].Replace("[", "").Replace("]", "").Replace(" ","");
        summonList = new List<int>();
        foreach (var _str in array[1].Split(','))
        {
            try { summonList.Add(int.Parse(_str)); }
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
