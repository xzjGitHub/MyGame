using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Mob_mobteamConfig配置表
/// </summary>
public partial class Mob_mobteamConfig : IReader
{
    public List<Mob_mobteam> _Mob_mobteam = new List<Mob_mobteam>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Mob_mobteam.Add(new Mob_mobteam(array[i]));
        }
    }
}



/// <summary>
/// Mob_mobteam配置表
/// </summary>
public partial class Mob_mobteam : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int mobTeamID;
    /// <summary>
    /// 
    /// </summary>
    public string mobTeamName;
    /// <summary>
    /// 
    /// </summary>
    public string mobTeamDescription;
    /// <summary>
    /// 
    /// </summary>
    public List<int> mobList;
    /// <summary>
    /// 
    /// </summary>
    public List<int> mobDelay;
    /// <summary>
    /// 怪物是否先手
    /// </summary>
    public int baseTeamInitiative;
    /// <summary>
    /// 
    /// </summary>
    public int charLevel;
    /// <summary>
    /// 
    /// </summary>
    public float HPConsume;
    /// <summary>
    /// 
    /// </summary>
    public float mobBonus;
    /// <summary>
    /// 
    /// </summary>
    public List<int> blockList;
    /// <summary>
    /// 
    /// </summary>
    public float coreDamage;
    /// <summary>
    /// 
    /// </summary>
    public float recoveryProb;
    /// <summary>
    /// 
    /// </summary>
    public int isRevivable ;
    /// <summary>
    /// 
    /// </summary>
    public float baseCharExpReward;



    public Mob_mobteam() { }
    public Mob_mobteam(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        mobTeamID = int.Parse(array[0]);
        mobTeamName = array[1];
        mobTeamDescription = array[2];
        //列表mobList取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        mobList = new List<int>();
        foreach (var _str in array[3].Split(','))
        {
            try { mobList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表mobDelay取值
        array[4] = array[4].Replace("[", "").Replace("]", "").Replace(" ","");
        mobDelay = new List<int>();
        foreach (var _str in array[4].Split(','))
        {
            try { mobDelay.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        baseTeamInitiative = int.Parse(array[5]);
        charLevel = int.Parse(array[6]);
        HPConsume = float.Parse(array[7]);
        mobBonus = float.Parse(array[8]);
        //列表blockList取值
        array[9] = array[9].Replace("[", "").Replace("]", "").Replace(" ","");
        blockList = new List<int>();
        foreach (var _str in array[9].Split(','))
        {
            try { blockList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        coreDamage = float.Parse(array[10]);
        recoveryProb = float.Parse(array[11]);
        isRevivable  = int.Parse(array[12]);
        baseCharExpReward = float.Parse(array[13]);
    }
}
