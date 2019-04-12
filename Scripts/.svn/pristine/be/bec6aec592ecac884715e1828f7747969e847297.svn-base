using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Invasion_templateConfig配置表
/// </summary>
public partial class Invasion_templateConfig : IReader
{
    public List<Invasion_template> _Invasion_template = new List<Invasion_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Invasion_template.Add(new Invasion_template(array[i]));
        }
    }
}



/// <summary>
/// Invasion_template配置表
/// </summary>
public partial class Invasion_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int invasionID;
    /// <summary>
    /// 
    /// </summary>
    public string invasionName;
    /// <summary>
    /// 
    /// </summary>
    public string invasionDescription;
    /// <summary>
    /// 
    /// </summary>
    public List<int> preTime ;
    /// <summary>
    /// 
    /// </summary>
    public List<int> warningTime ;
    /// <summary>
    /// 
    /// </summary>
    public List<int> mobTeamList ;
    /// <summary>
    /// 
    /// </summary>
    public int baseGoldReward;
    /// <summary>
    /// 
    /// </summary>
    public int baseTokenReward;
    /// <summary>
    /// 
    /// </summary>
    public List<int> itemRewardSet;
    /// <summary>
    /// 
    /// </summary>
    public List<int> baseRewardLevel;



    public Invasion_template() { }
    public Invasion_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        invasionID = int.Parse(array[0]);
        invasionName = array[1];
        invasionDescription = array[2];
        //列表preTime 取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        preTime  = new List<int>();
        foreach (var _str in array[3].Split(','))
        {
            try { preTime .Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表warningTime 取值
        array[4] = array[4].Replace("[", "").Replace("]", "").Replace(" ","");
        warningTime  = new List<int>();
        foreach (var _str in array[4].Split(','))
        {
            try { warningTime .Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表mobTeamList 取值
        array[5] = array[5].Replace("[", "").Replace("]", "").Replace(" ","");
        mobTeamList  = new List<int>();
        foreach (var _str in array[5].Split(','))
        {
            try { mobTeamList .Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        baseGoldReward = int.Parse(array[6]);
        baseTokenReward = int.Parse(array[7]);
        //列表itemRewardSet取值
        array[8] = array[8].Replace("[", "").Replace("]", "").Replace(" ","");
        itemRewardSet = new List<int>();
        foreach (var _str in array[8].Split(','))
        {
            try { itemRewardSet.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表baseRewardLevel取值
        array[9] = array[9].Replace("[", "").Replace("]", "").Replace(" ","");
        baseRewardLevel = new List<int>();
        foreach (var _str in array[9].Split(','))
        {
            try { baseRewardLevel.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
