using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Invasion_invasionsetConfig配置表
/// </summary>
public partial class Invasion_invasionsetConfig : IReader
{
    public List<Invasion_invasionset> _Invasion_invasionset = new List<Invasion_invasionset>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Invasion_invasionset.Add(new Invasion_invasionset(array[i]));
        }
    }
}



/// <summary>
/// Invasion_invasionset配置表
/// </summary>
public partial class Invasion_invasionset : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int invasionSetID;
    /// <summary>
    /// 
    /// </summary>
    public int coreLevelReq;
    /// <summary>
    /// 
    /// </summary>
    public List<int> bountyReq;
    /// <summary>
    /// 
    /// </summary>
    public int timeReq;
    /// <summary>
    /// 
    /// </summary>
    public int timeExpire;
    /// <summary>
    /// 
    /// </summary>
    public List<int> fortReq;
    /// <summary>
    /// 
    /// </summary>
    public int baseRndChance;
    /// <summary>
    /// 
    /// </summary>
    public List<int> invasionRndList;



    public Invasion_invasionset() { }
    public Invasion_invasionset(string content)
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
        coreLevelReq = int.Parse(array[1]);
        //列表bountyReq取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        bountyReq = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { bountyReq.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        timeReq = int.Parse(array[3]);
        timeExpire = int.Parse(array[4]);
        //列表fortReq取值
        array[5] = array[5].Replace("[", "").Replace("]", "").Replace(" ","");
        fortReq = new List<int>();
        foreach (var _str in array[5].Split(','))
        {
            try { fortReq.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        baseRndChance = int.Parse(array[6]);
        //列表invasionRndList取值
        array[7] = array[7].Replace("[", "").Replace("]", "").Replace(" ","");
        invasionRndList = new List<int>();
        foreach (var _str in array[7].Split(','))
        {
            try { invasionRndList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
