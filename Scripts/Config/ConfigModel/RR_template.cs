using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// RR_templateConfig配置表
/// </summary>
public partial class RR_templateConfig : IReader
{
    public List<RR_template> _RR_template = new List<RR_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _RR_template.Add(new RR_template(array[i]));
        }
    }
}



/// <summary>
/// RR_template配置表
/// </summary>
public partial class RR_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int templateID;
    /// <summary>
    /// 区域ID
    /// </summary>
    public int zoneID;
    /// <summary>
    /// 声望类型
    /// </summary>
    public int factionType;
    /// <summary>
    /// 金币奖励
    /// </summary>
    public int goldReward;
    /// <summary>
    /// 物品奖励
    /// </summary>
    public List<int> itemRewardSet;
    /// <summary>
    /// 奖励等级
    /// </summary>
    public List<float> baseRewardLevel;
    /// <summary>
    /// 角色奖励几率
    /// </summary>
    public int charRewardChance;
    /// <summary>
    /// 角色列表
    /// </summary>
    public List<int> charList;



    public RR_template() { }
    public RR_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        templateID = int.Parse(array[0]);
        zoneID = int.Parse(array[1]);
        factionType = int.Parse(array[2]);
        goldReward = int.Parse(array[3]);
        //列表itemRewardSet取值
        array[4] = array[4].Replace("[", "").Replace("]", "").Replace(" ","");
        itemRewardSet = new List<int>();
        foreach (var _str in array[4].Split(','))
        {
            try { itemRewardSet.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表baseRewardLevel取值
        array[5] = array[5].Replace("[", "").Replace("]", "").Replace(" ","");
        baseRewardLevel = new List<float>();
        foreach (var _str in array[5].Split(','))
        {
            try { baseRewardLevel.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        charRewardChance = int.Parse(array[6]);
        //列表charList取值
        array[7] = array[7].Replace("[", "").Replace("]", "").Replace(" ","");
        charList = new List<int>();
        foreach (var _str in array[7].Split(','))
        {
            try { charList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
