using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Game_configConfig配置表
/// </summary>
public partial class Game_configConfig : IReader
{
    public List<Game_config> _Game_config = new List<Game_config>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Game_config.Add(new Game_config(array[i]));
        }
    }
}



/// <summary>
/// Game_config配置表
/// </summary>
public partial class Game_config : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int maxChar;
    /// <summary>
    /// 
    /// </summary>
    public int maxSummonChar;
    /// <summary>
    /// 
    /// </summary>
    public int maxItemStack;
    /// <summary>
    /// 
    /// </summary>
    public int roundPause;
    /// <summary>
    /// 
    /// </summary>
    public int siegeTime;
    /// <summary>
    /// 
    /// </summary>
    public int coreHP;
    /// <summary>
    /// 
    /// </summary>
    public int maxExpedition;
    /// <summary>
    /// 
    /// </summary>
    public int expeditionTime;
    /// <summary>
    /// 
    /// </summary>
    public int sndRuneSlot;
    /// <summary>
    /// 
    /// </summary>
    public int trdRuneSlot;
    /// <summary>
    /// 
    /// </summary>
    public int PUPointMinCharLevel;
    /// <summary>
    /// 
    /// </summary>
    public int maxWarningTime;
    /// <summary>
    /// 
    /// </summary>
    public int invasionInterval;
    /// <summary>
    /// 成功率偏差
    /// </summary>
    public float successDevi;
    /// <summary>
    /// 
    /// </summary>
    public int maxSuccessChance;
    /// <summary>
    /// 
    /// </summary>
    public int minAmbushChance ;
    /// <summary>
    /// 
    /// </summary>
    public string worldName;
    /// <summary>
    /// 
    /// </summary>
    public string regionName;
    /// <summary>
    /// 主城坐标
    /// </summary>
    public List<int> capitalSet;
    /// <summary>
    /// 
    /// </summary>
    public int initialZone;
    /// <summary>
    /// 将魔力转化为金币
    /// </summary>
    public List<int> manaToGold;
    /// <summary>
    /// 标准生产周期(周)
    /// </summary>
    public int productionCycle ;
    /// <summary>
    /// 研究随机范围
    /// </summary>
    public float researchDeviation;
    /// <summary>
    /// 召唤随机范围
    /// </summary>
    public float summonDeviation;
    /// <summary>
    /// 装备研究时，奖励最小制造/附魔等级的几率
    /// </summary>
    public int minLvRewardChance;
    /// <summary>
    /// 
    /// </summary>
    public float standardFloat;



    public Game_config() { }
    public Game_config(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        maxChar = int.Parse(array[0]);
        maxSummonChar = int.Parse(array[1]);
        maxItemStack = int.Parse(array[2]);
        roundPause = int.Parse(array[3]);
        siegeTime = int.Parse(array[4]);
        coreHP = int.Parse(array[5]);
        maxExpedition = int.Parse(array[6]);
        expeditionTime = int.Parse(array[7]);
        sndRuneSlot = int.Parse(array[8]);
        trdRuneSlot = int.Parse(array[9]);
        PUPointMinCharLevel = int.Parse(array[10]);
        maxWarningTime = int.Parse(array[11]);
        invasionInterval = int.Parse(array[12]);
        successDevi = float.Parse(array[13]);
        maxSuccessChance = int.Parse(array[14]);
        minAmbushChance  = int.Parse(array[15]);
        worldName = array[16];
        regionName = array[17];
        //列表capitalSet取值
        array[18] = array[18].Replace("[", "").Replace("]", "").Replace(" ","");
        capitalSet = new List<int>();
        foreach (var _str in array[18].Split(','))
        {
            try { capitalSet.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        initialZone = int.Parse(array[19]);
        //列表manaToGold取值
        array[20] = array[20].Replace("[", "").Replace("]", "").Replace(" ","");
        manaToGold = new List<int>();
        foreach (var _str in array[20].Split(','))
        {
            try { manaToGold.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        productionCycle  = int.Parse(array[21]);
        researchDeviation = float.Parse(array[22]);
        summonDeviation = float.Parse(array[23]);
        minLvRewardChance = int.Parse(array[24]);
        standardFloat = float.Parse(array[25]);
    }
}
