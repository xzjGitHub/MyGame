using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Building_levelupConfig配置表
/// </summary>
public partial class Building_levelupConfig : IReader
{
    public List<Building_levelup> _Building_levelup = new List<Building_levelup>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Building_levelup.Add(new Building_levelup(array[i]));
        }
    }
}



/// <summary>
/// Building_levelup配置表
/// </summary>
public partial class Building_levelup : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int leveupID;
    /// <summary>
    /// 
    /// </summary>
    public int THLevelReq;
    /// <summary>
    /// 
    /// </summary>
    public float goldCost;
    /// <summary>
    /// 
    /// </summary>
    public float manaCost;
    /// <summary>
    /// 
    /// </summary>
    public   List<List<int>> itemCost;
    /// <summary>
    /// 
    /// </summary>
    public int maxCharCount;
    /// <summary>
    /// 
    /// </summary>
    public List<int> itemRewardSet;
    /// <summary>
    /// 
    /// </summary>
    public List<float> baseRewardLevel;
    /// <summary>
    /// 
    /// </summary>
    public List<float> rewardValue;
    /// <summary>
    /// 
    /// </summary>
    public int sellList;
    /// <summary>
    /// 
    /// </summary>
    public int sellStackCount;
    /// <summary>
    /// 
    /// </summary>
    public float discount;
    /// <summary>
    /// 
    /// </summary>
    public float cycleReduction;
    /// <summary>
    /// 
    /// </summary>
    public int maxItemLevel;
    /// <summary>
    /// 
    /// </summary>
    public int repairCost;
    /// <summary>
    /// 
    /// </summary>
    public float minPotential;
    /// <summary>
    /// 
    /// </summary>
    public float maxPotential;
    /// <summary>
    /// 
    /// </summary>
    public int initialCharLevel;
    /// <summary>
    /// 
    /// </summary>
    public int maxCharLevel;
    /// <summary>
    /// 
    /// </summary>
    public float manaCostUnit;
    /// <summary>
    /// 
    /// </summary>
    public int maxProductionLine;
    /// <summary>
    /// 
    /// </summary>
    public int maxCraftLevel;
    /// <summary>
    /// 
    /// </summary>
    public int goldProduced;



    public Building_levelup() { }
    public Building_levelup(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        leveupID = int.Parse(array[0]);
        THLevelReq = int.Parse(array[1]);
        goldCost = float.Parse(array[2]);
        manaCost = float.Parse(array[3]);
        //列表itemCost取值
        array[4] = array[4].Replace("[", "").Replace("]", "").Replace(" ","");
        itemCost = new   List<List<int>>();
        foreach (var str in array[4].Split('-'))
        {
            try 
            {
                itemCost.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        maxCharCount = int.Parse(array[5]);
        //列表itemRewardSet取值
        array[6] = array[6].Replace("[", "").Replace("]", "").Replace(" ","");
        itemRewardSet = new List<int>();
        foreach (var _str in array[6].Split(','))
        {
            try { itemRewardSet.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表baseRewardLevel取值
        array[7] = array[7].Replace("[", "").Replace("]", "").Replace(" ","");
        baseRewardLevel = new List<float>();
        foreach (var _str in array[7].Split(','))
        {
            try { baseRewardLevel.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        //列表rewardValue取值
        array[8] = array[8].Replace("[", "").Replace("]", "").Replace(" ","");
        rewardValue = new List<float>();
        foreach (var _str in array[8].Split(','))
        {
            try { rewardValue.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        sellList = int.Parse(array[9]);
        sellStackCount = int.Parse(array[10]);
        discount = float.Parse(array[11]);
        cycleReduction = float.Parse(array[12]);
        maxItemLevel = int.Parse(array[13]);
        repairCost = int.Parse(array[14]);
        minPotential = float.Parse(array[15]);
        maxPotential = float.Parse(array[16]);
        initialCharLevel = int.Parse(array[17]);
        maxCharLevel = int.Parse(array[18]);
        manaCostUnit = float.Parse(array[19]);
        maxProductionLine = int.Parse(array[20]);
        maxCraftLevel = int.Parse(array[21]);
        goldProduced = int.Parse(array[22]);
    }
}
