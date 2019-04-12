using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Event_selectionConfig配置表
/// </summary>
public partial class Event_selectionConfig : IReader
{
    public List<Event_selection> _Event_selection = new List<Event_selection>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Event_selection.Add(new Event_selection(array[i]));
        }
    }
}



/// <summary>
/// Event_selection配置表
/// </summary>
public partial class Event_selection : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int selectionID;
    /// <summary>
    /// 
    /// </summary>
    public string selectionName;
    /// <summary>
    /// 选项类型1=深入，2=特殊深入，3=奖励，4=战斗，5= 召唤，6=潜行
    /// </summary>
    public int selectionType;
    /// <summary>
    /// 
    /// </summary>
    public int textID;
    /// <summary>
    /// 
    /// </summary>
    public int phaseReq;
    /// <summary>
    /// 随机怪物列表
    /// </summary>
    public List<int> mobTeamList;
    /// <summary>
    /// 
    /// </summary>
    public float riskBonus;
    /// <summary>
    /// 触发陷阱时添加的事件
    /// </summary>
    public int trapChance;
    /// <summary>
    /// 触发陷阱时添加的事件
    /// </summary>
    public int trapHPCost;
    /// <summary>
    /// 触发伏击时添加的事件
    /// </summary>
    public int ambushChance;
    /// <summary>
    /// 触发陷阱时添加的事件
    /// </summary>
    public int trapEvent;
    /// <summary>
    /// 触发伏击时添加的事件
    /// </summary>
    public int ambushEvent;
    /// <summary>
    /// 奖励增加几率
    /// </summary>
    public int bonusChance;
    /// <summary>
    /// 奖励增加选项
    /// </summary>
    public int addBonus;
    /// <summary>
    /// 全局奖励加值
    /// </summary>
    public float tempOARewardBonus;
    /// <summary>
    /// 
    /// </summary>
    public float baseGoldReward;
    /// <summary>
    /// 
    /// </summary>
    public int baseTokenReward;
    /// <summary>
    /// 
    /// </summary>
    public int baseGlobReward;
    /// <summary>
    /// 
    /// </summary>
    public List<int> itemRewardSet;
    /// <summary>
    /// 
    /// </summary>
    public List<int> itemRewardSetup ;
    /// <summary>
    /// 
    /// </summary>
    public float baseRewardLevel;
    /// <summary>
    /// 
    /// </summary>
    public float baseCharExpReward;



    public Event_selection() { }
    public Event_selection(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        selectionID = int.Parse(array[0]);
        selectionName = array[1];
        selectionType = int.Parse(array[2]);
        textID = int.Parse(array[3]);
        phaseReq = int.Parse(array[4]);
        //列表mobTeamList取值
        array[5] = array[5].Replace("[", "").Replace("]", "").Replace(" ","");
        mobTeamList = new List<int>();
        foreach (var _str in array[5].Split(','))
        {
            try { mobTeamList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        riskBonus = float.Parse(array[6]);
        trapChance = int.Parse(array[7]);
        trapHPCost = int.Parse(array[8]);
        ambushChance = int.Parse(array[9]);
        trapEvent = int.Parse(array[10]);
        ambushEvent = int.Parse(array[11]);
        bonusChance = int.Parse(array[12]);
        addBonus = int.Parse(array[13]);
        tempOARewardBonus = float.Parse(array[14]);
        baseGoldReward = float.Parse(array[15]);
        baseTokenReward = int.Parse(array[16]);
        baseGlobReward = int.Parse(array[17]);
        //列表itemRewardSet取值
        array[18] = array[18].Replace("[", "").Replace("]", "").Replace(" ","");
        itemRewardSet = new List<int>();
        foreach (var _str in array[18].Split(','))
        {
            try { itemRewardSet.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表itemRewardSetup 取值
        array[19] = array[19].Replace("[", "").Replace("]", "").Replace(" ","");
        itemRewardSetup  = new List<int>();
        foreach (var _str in array[19].Split(','))
        {
            try { itemRewardSetup .Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        baseRewardLevel = float.Parse(array[20]);
        baseCharExpReward = float.Parse(array[21]);
    }
}
