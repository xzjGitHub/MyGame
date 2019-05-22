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
    /// 
    /// </summary>
    public int selectionType;
    /// <summary>
    /// 
    /// </summary>
    public int textID;
    /// <summary>
    /// 
    /// </summary>
    public List<int> selectionChain;
    /// <summary>
    /// 随机怪物集合
    /// </summary>
    public List<int> mobList;
    /// <summary>
    /// 触发陷阱时添加的事件
    /// </summary>
    public int baseSuccessChance;
    /// <summary>
    /// 成功率公式
    /// </summary>
    public int fscFormula;
    /// <summary>
    /// [成功选项，失败选项]
    /// </summary>
    public List<int> resultSelection;
    /// <summary>
    /// 陷阱生命消耗%
    /// </summary>
    public float HPCost;
    /// <summary>
    /// 基本奖励等级
    /// </summary>
    public float baseRewardLevel;
    /// <summary>
    /// 
    /// </summary>
    public float baseCharExpReward;
    /// <summary>
    /// 
    /// </summary>
    public float baseGoldReward;
    /// <summary>
    /// 奖励等级分配-仅针对物品奖励，金币和经验奖励是固有的
    /// </summary>
    public List<int> itemRewardSetup ;
    /// <summary>
    /// 奖励集合
    /// </summary>
    public List<int> itemRewardSet;
    /// <summary>
    /// 触发伏击时添加的事件
    /// </summary>
    public int ambushChance;
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
    public int trapHPCost;
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
    public int baseTokenReward;
    /// <summary>
    /// 
    /// </summary>
    public int baseGlobReward;



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
        //列表selectionChain取值
        array[4] = array[4].Replace("[", "").Replace("]", "").Replace(" ","");
        selectionChain = new List<int>();
        foreach (var _str in array[4].Split(','))
        {
            try { selectionChain.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表mobList取值
        array[5] = array[5].Replace("[", "").Replace("]", "").Replace(" ","");
        mobList = new List<int>();
        foreach (var _str in array[5].Split(','))
        {
            try { mobList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        baseSuccessChance = int.Parse(array[6]);
        fscFormula = int.Parse(array[7]);
        //列表resultSelection取值
        array[8] = array[8].Replace("[", "").Replace("]", "").Replace(" ","");
        resultSelection = new List<int>();
        foreach (var _str in array[8].Split(','))
        {
            try { resultSelection.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        HPCost = float.Parse(array[9]);
        baseRewardLevel = float.Parse(array[10]);
        baseCharExpReward = float.Parse(array[11]);
        baseGoldReward = float.Parse(array[12]);
        //列表itemRewardSetup 取值
        array[13] = array[13].Replace("[", "").Replace("]", "").Replace(" ","");
        itemRewardSetup  = new List<int>();
        foreach (var _str in array[13].Split(','))
        {
            try { itemRewardSetup .Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表itemRewardSet取值
        array[14] = array[14].Replace("[", "").Replace("]", "").Replace(" ","");
        itemRewardSet = new List<int>();
        foreach (var _str in array[14].Split(','))
        {
            try { itemRewardSet.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        ambushChance = int.Parse(array[15]);
        phaseReq = int.Parse(array[16]);
        //列表mobTeamList取值
        array[17] = array[17].Replace("[", "").Replace("]", "").Replace(" ","");
        mobTeamList = new List<int>();
        foreach (var _str in array[17].Split(','))
        {
            try { mobTeamList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        riskBonus = float.Parse(array[18]);
        trapHPCost = int.Parse(array[19]);
        trapEvent = int.Parse(array[20]);
        ambushEvent = int.Parse(array[21]);
        bonusChance = int.Parse(array[22]);
        addBonus = int.Parse(array[23]);
        tempOARewardBonus = float.Parse(array[24]);
        baseTokenReward = int.Parse(array[25]);
        baseGlobReward = int.Parse(array[26]);
    }
}
