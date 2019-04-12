using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Invasion_setConfig配置表
/// </summary>
public partial class Invasion_setConfig : IReader
{
    public List<Invasion_set> _Invasion_set = new List<Invasion_set>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Invasion_set.Add(new Invasion_set(array[i]));
        }
    }
}



/// <summary>
/// Invasion_set配置表
/// </summary>
public partial class Invasion_set : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int invasionSetID;
    /// <summary>
    /// 
    /// </summary>
    public List<int> invasionList;



    public Invasion_set() { }
    public Invasion_set(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        invasionSetID = int.Parse(array[0]);
        //列表invasionList取值
        array[1] = array[1].Replace("[", "").Replace("]", "").Replace(" ","");
        invasionList = new List<int>();
        foreach (var _str in array[1].Split(','))
        {
            try { invasionList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
