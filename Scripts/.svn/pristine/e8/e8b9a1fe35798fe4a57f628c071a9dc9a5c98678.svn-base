using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Char_configConfig配置表
/// </summary>
public partial class Char_configConfig : IReader
{
    public List<Char_config> _Char_config = new List<Char_config>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Char_config.Add(new Char_config(array[i]));
        }
    }
}



/// <summary>
/// Char_config配置表
/// </summary>
public partial class Char_config : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public float expRatio;
    /// <summary>
    /// 等级成长
    /// </summary>
    public float lvRate;
    /// <summary>
    /// 
    /// </summary>
    public float TankEHP;
    /// <summary>
    /// 坦克升级有效生命提升
    /// </summary>
    public float lvTEHP;
    /// <summary>
    /// 标准升级有效生命提升
    /// </summary>
    public float lvDEHP;
    /// <summary>
    /// 强化系数
    /// </summary>
    public float upgradeRate;
    /// <summary>
    /// 最大强化
    /// </summary>
    public int maxUpgrade;
    /// <summary>
    /// 
    /// </summary>
    public float charRankBonus;
    /// <summary>
    /// 最大角色等级
    /// </summary>
    public int maxCharLevel;
    /// <summary>
    /// 每点能量所占的伤害比例
    /// </summary>
    public float energyDP;
    /// <summary>
    /// 复活时恢复的生命值
    /// </summary>
    public float reviveProp;
    /// <summary>
    /// 中间角色等级
    /// </summary>
    public int midCharLevel;
    /// <summary>
    /// 被动技能总等级随机机制
    /// </summary>
    public   List<List<int>> totalPassiveLevel ;
    /// <summary>
    /// 最小被动技能等级
    /// </summary>
    public List<int> minPassiveLevel;
    /// <summary>
    /// 最大被动技能等级
    /// </summary>
    public int maxPassiveLevel;
    /// <summary>
    /// 最大角色军衔
    /// </summary>
    public int maxCharRank;
    /// <summary>
    /// 最大总被动等级
    /// </summary>
    public int maxtotalPassiveLevel ;
    /// <summary>
    /// 等级加值几率，+3的几率约为0.216
    /// </summary>
    public int addLevelChance;
    /// <summary>
    /// 最大等级加值
    /// </summary>
    public int maxAddCount;
    /// <summary>
    /// 取消
    /// </summary>
    public float addDeviation;



    public Char_config() { }
    public Char_config(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        expRatio = float.Parse(array[0]);
        lvRate = float.Parse(array[1]);
        TankEHP = float.Parse(array[2]);
        lvTEHP = float.Parse(array[3]);
        lvDEHP = float.Parse(array[4]);
        upgradeRate = float.Parse(array[5]);
        maxUpgrade = int.Parse(array[6]);
        charRankBonus = float.Parse(array[7]);
        maxCharLevel = int.Parse(array[8]);
        energyDP = float.Parse(array[9]);
        reviveProp = float.Parse(array[10]);
        midCharLevel = int.Parse(array[11]);
        //列表totalPassiveLevel 取值
        array[12] = array[12].Replace("[", "").Replace("]", "").Replace(" ","");
        totalPassiveLevel  = new   List<List<int>>();
        foreach (var str in array[12].Split('-'))
        {
            try 
            {
                totalPassiveLevel .Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表minPassiveLevel取值
        array[13] = array[13].Replace("[", "").Replace("]", "").Replace(" ","");
        minPassiveLevel = new List<int>();
        foreach (var _str in array[13].Split(','))
        {
            try { minPassiveLevel.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        maxPassiveLevel = int.Parse(array[14]);
        maxCharRank = int.Parse(array[15]);
        maxtotalPassiveLevel  = int.Parse(array[16]);
        addLevelChance = int.Parse(array[17]);
        maxAddCount = int.Parse(array[18]);
        addDeviation = float.Parse(array[19]);
    }
}
