using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Event_templateConfig配置表
/// </summary>
public partial class Event_templateConfig : IReader
{
    public List<Event_template> _Event_template = new List<Event_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Event_template.Add(new Event_template(array[i]));
        }
    }
}



/// <summary>
/// Event_template配置表
/// </summary>
public partial class Event_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int eventID;
    /// <summary>
    /// 
    /// </summary>
    public string eventName;
    /// <summary>
    /// 
    /// </summary>
    public string eventRP;
    /// <summary>
    /// 简化，0=无法被贿赂，1=金币贿赂，2=魔力贿赂（使用）
    /// </summary>
    public int bribeType ;
    /// <summary>
    /// 
    /// </summary>
    public int eventType;
    /// <summary>
    /// 该事件是否是剧本唯一的，必须结合rndSet中的多维
    /// </summary>
    public int isUnique;
    /// <summary>
    /// 陷阱的触发时间
    /// </summary>
    public int visitTime;
    /// <summary>
    /// 随机怪物列表
    /// </summary>
    public List<int> mobTeamList;
    /// <summary>
    /// 最大层数
    /// </summary>
    public int maxPhase;
    /// <summary>
    /// 增加1个层级的几率
    /// </summary>
    public List<int> addPhaseChance;
    /// <summary>
    /// 选项列表，第1个结构对应第1层
    /// </summary>
    public   List<List<int>> selectionList ;
    /// <summary>
    /// 异常事件失败时的选项。index0 = 失败，index1 = 失败-陷阱，index2 = 失败-战斗，
    /// </summary>
    public List<int> faildSelectionList;
    /// <summary>
    /// 
    /// </summary>
    public List<int> rndSelectionList;
    /// <summary>
    /// 大奖几率
    /// </summary>
    public int jackpotChance;
    /// <summary>
    /// 大奖选项
    /// </summary>
    public int jackpotSelection;
    /// <summary>
    /// 文本
    /// </summary>
    public int startDialog ;
    /// <summary>
    /// 
    /// </summary>
    public int endDialog ;
    /// <summary>
    /// 
    /// </summary>
    public string eventInfo1;
    /// <summary>
    /// 
    /// </summary>
    public string eventInfo2;
    /// <summary>
    /// 0=不显示，1=Boss，2=宝藏，3=重要（改为资源），4=祭坛
    /// </summary>
    public int WPIcon;
    /// <summary>
    /// 触发陷阱时添加的事件
    /// </summary>
    public int trapEvent;
    /// <summary>
    /// 触发伏击时添加的事件
    /// </summary>
    public int ambushEvent;
    /// <summary>
    /// 附加奖励物品等级
    /// </summary>
    public int addItemLevel;
    /// <summary>
    /// 
    /// </summary>
    public List<float> addRewardValue;
    /// <summary>
    /// 
    /// </summary>
    public float baseGoldReward;
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
    public List<float> baseRewardLevel;
    /// <summary>
    /// 基本上取消，太复杂
    /// </summary>
    public int baseThreat;
    /// <summary>
    /// [threatType, addValue]
    /// </summary>
    public   List<List<int>> factionThreat;
    /// <summary>
    /// 取消-使用特殊机制进行挑战
    /// </summary>
    public int challengeType ;
    /// <summary>
    /// 取消-全部用选项机制
    /// </summary>
    public List<float> visitHPCost;
    /// <summary>
    /// 取消-全部用选项机制
    /// </summary>
    public float failedHPCost;
    /// <summary>
    /// 无用，现在直接在路点上使用
    /// </summary>
    public int addMobLevel;
    /// <summary>
    /// 各结果的几率
    /// </summary>
    public List<int> resultChance;



    public Event_template() { }
    public Event_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        eventID = int.Parse(array[0]);
        eventName = array[1];
        eventRP = array[2];
        bribeType  = int.Parse(array[3]);
        eventType = int.Parse(array[4]);
        isUnique = int.Parse(array[5]);
        visitTime = int.Parse(array[6]);
        //列表mobTeamList取值
        array[7] = array[7].Replace("[", "").Replace("]", "").Replace(" ","");
        mobTeamList = new List<int>();
        foreach (var _str in array[7].Split(','))
        {
            try { mobTeamList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        maxPhase = int.Parse(array[8]);
        //列表addPhaseChance取值
        array[9] = array[9].Replace("[", "").Replace("]", "").Replace(" ","");
        addPhaseChance = new List<int>();
        foreach (var _str in array[9].Split(','))
        {
            try { addPhaseChance.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表selectionList 取值
        array[10] = array[10].Replace("[", "").Replace("]", "").Replace(" ","");
        selectionList  = new   List<List<int>>();
        foreach (var str in array[10].Split('-'))
        {
            try 
            {
                selectionList .Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表faildSelectionList取值
        array[11] = array[11].Replace("[", "").Replace("]", "").Replace(" ","");
        faildSelectionList = new List<int>();
        foreach (var _str in array[11].Split(','))
        {
            try { faildSelectionList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表rndSelectionList取值
        array[12] = array[12].Replace("[", "").Replace("]", "").Replace(" ","");
        rndSelectionList = new List<int>();
        foreach (var _str in array[12].Split(','))
        {
            try { rndSelectionList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        jackpotChance = int.Parse(array[13]);
        jackpotSelection = int.Parse(array[14]);
        startDialog  = int.Parse(array[15]);
        endDialog  = int.Parse(array[16]);
        eventInfo1 = array[17];
        eventInfo2 = array[18];
        WPIcon = int.Parse(array[19]);
        trapEvent = int.Parse(array[20]);
        ambushEvent = int.Parse(array[21]);
        addItemLevel = int.Parse(array[22]);
        //列表addRewardValue取值
        array[23] = array[23].Replace("[", "").Replace("]", "").Replace(" ","");
        addRewardValue = new List<float>();
        foreach (var _str in array[23].Split(','))
        {
            try { addRewardValue.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        baseGoldReward = float.Parse(array[24]);
        baseGlobReward = int.Parse(array[25]);
        //列表itemRewardSet取值
        array[26] = array[26].Replace("[", "").Replace("]", "").Replace(" ","");
        itemRewardSet = new List<int>();
        foreach (var _str in array[26].Split(','))
        {
            try { itemRewardSet.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表baseRewardLevel取值
        array[27] = array[27].Replace("[", "").Replace("]", "").Replace(" ","");
        baseRewardLevel = new List<float>();
        foreach (var _str in array[27].Split(','))
        {
            try { baseRewardLevel.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        baseThreat = int.Parse(array[28]);
        //列表factionThreat取值
        array[29] = array[29].Replace("[", "").Replace("]", "").Replace(" ","");
        factionThreat = new   List<List<int>>();
        foreach (var str in array[29].Split('-'))
        {
            try 
            {
                factionThreat.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        challengeType  = int.Parse(array[30]);
        //列表visitHPCost取值
        array[31] = array[31].Replace("[", "").Replace("]", "").Replace(" ","");
        visitHPCost = new List<float>();
        foreach (var _str in array[31].Split(','))
        {
            try { visitHPCost.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        failedHPCost = float.Parse(array[32]);
        addMobLevel = int.Parse(array[33]);
        //列表resultChance取值
        array[34] = array[34].Replace("[", "").Replace("]", "").Replace(" ","");
        resultChance = new List<int>();
        foreach (var _str in array[34].Split(','))
        {
            try { resultChance.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
