using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Event_configConfig配置表
/// </summary>
public partial class Event_configConfig : IReader
{
    public List<Event_config> _Event_config = new List<Event_config>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Event_config.Add(new Event_config(array[i]));
        }
    }
}



/// <summary>
/// Event_config配置表
/// </summary>
public partial class Event_config : IReader
{
    /// <summary>
    /// 生命球恢复的生命比例
    /// </summary>
    public float globHealing ;
    /// <summary>
    /// 
    /// </summary>
    public int addLevelChance;
    /// <summary>
    /// 
    /// </summary>
    public int maxAddLevel;
    /// <summary>
    /// addLevel的增幅
    /// </summary>
    public float addRLBonus ;
    /// <summary>
    /// 每种词缀的几率
    /// </summary>
    public List<int> rndPrefixChance ;
    /// <summary>
    /// [幸运时奖励翻倍，奖励时成功率提高20%]
    /// </summary>
    public List<float> RLBonus;
    /// <summary>
    /// 基本成功率，轻易成功率加值，额外奖励成功率
    /// </summary>
    public List<int> baseSuccessChance;
    /// <summary>
    /// 事件成功、奖励浮动
    /// </summary>
    public float eventDeviation;
    /// <summary>
    /// 保证必定成功的物品消耗
    /// </summary>
    public int tokenItemCost ;
    /// <summary>
    /// [2个选项的几率，3个选项的几率]
    /// </summary>
    public List<int> pathCountChance  ;
    /// <summary>
    /// [2个选项的几率，3个选项的几率]
    /// </summary>
    public List<int> endCountChance  ;
    /// <summary>
    /// 
    /// </summary>
    public List<int> ventureSelection;
    /// <summary>
    /// 各层风险
    /// </summary>
    public int ventureRisk;
    /// <summary>
    /// 选项奖励浮动
    /// </summary>
    public float selectionDeviation;



    public Event_config() { }
    public Event_config(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        globHealing  = float.Parse(array[0]);
        addLevelChance = int.Parse(array[1]);
        maxAddLevel = int.Parse(array[2]);
        addRLBonus  = float.Parse(array[3]);
        //列表rndPrefixChance 取值
        array[4] = array[4].Replace("[", "").Replace("]", "").Replace(" ","");
        rndPrefixChance  = new List<int>();
        foreach (var _str in array[4].Split(','))
        {
            try { rndPrefixChance .Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表RLBonus取值
        array[5] = array[5].Replace("[", "").Replace("]", "").Replace(" ","");
        RLBonus = new List<float>();
        foreach (var _str in array[5].Split(','))
        {
            try { RLBonus.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        //列表baseSuccessChance取值
        array[6] = array[6].Replace("[", "").Replace("]", "").Replace(" ","");
        baseSuccessChance = new List<int>();
        foreach (var _str in array[6].Split(','))
        {
            try { baseSuccessChance.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        eventDeviation = float.Parse(array[7]);
        tokenItemCost  = int.Parse(array[8]);
        //列表pathCountChance  取值
        array[9] = array[9].Replace("[", "").Replace("]", "").Replace(" ","");
        pathCountChance   = new List<int>();
        foreach (var _str in array[9].Split(','))
        {
            try { pathCountChance  .Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表endCountChance  取值
        array[10] = array[10].Replace("[", "").Replace("]", "").Replace(" ","");
        endCountChance   = new List<int>();
        foreach (var _str in array[10].Split(','))
        {
            try { endCountChance  .Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表ventureSelection取值
        array[11] = array[11].Replace("[", "").Replace("]", "").Replace(" ","");
        ventureSelection = new List<int>();
        foreach (var _str in array[11].Split(','))
        {
            try { ventureSelection.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        ventureRisk = int.Parse(array[12]);
        selectionDeviation = float.Parse(array[13]);
    }
}
