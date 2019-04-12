using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// MR_templateConfig配置表
/// </summary>
public partial class MR_templateConfig : IReader
{
    public List<MR_template> _MR_template = new List<MR_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _MR_template.Add(new MR_template(array[i]));
        }
    }
}



/// <summary>
/// MR_template配置表
/// </summary>
public partial class MR_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int instanceID;
    /// <summary>
    /// 金属武器附魔 = 1，灵木武器附魔=2，轻甲附魔=3，重甲附魔=4
    /// </summary>
    public int enchantType;
    /// <summary>
    /// 
    /// </summary>
    public int researchCost;
    /// <summary>
    /// 
    /// </summary>
    public int enchantCost;
    /// <summary>
    /// 最小研究等级提升
    /// </summary>
    public float addMinEnchantLevel;
    /// <summary>
    /// 
    /// </summary>
    public List<int> activeEnchantLevel ;
    /// <summary>
    /// 
    /// </summary>
    public List<int> enchantExpReward ;
    /// <summary>
    /// 
    /// </summary>
    public List<float> minResearchLevelReq;
    /// <summary>
    /// 附魔金币消耗
    /// </summary>
    public int goldCost;
    /// <summary>
    /// 附魔魔力消耗
    /// </summary>
    public int manaCost;



    public MR_template() { }
    public MR_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        instanceID = int.Parse(array[0]);
        enchantType = int.Parse(array[1]);
        researchCost = int.Parse(array[2]);
        enchantCost = int.Parse(array[3]);
        addMinEnchantLevel = float.Parse(array[4]);
        //列表activeEnchantLevel 取值
        array[5] = array[5].Replace("[", "").Replace("]", "").Replace(" ","");
        activeEnchantLevel  = new List<int>();
        foreach (var _str in array[5].Split(','))
        {
            try { activeEnchantLevel .Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表enchantExpReward 取值
        array[6] = array[6].Replace("[", "").Replace("]", "").Replace(" ","");
        enchantExpReward  = new List<int>();
        foreach (var _str in array[6].Split(','))
        {
            try { enchantExpReward .Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表minResearchLevelReq取值
        array[7] = array[7].Replace("[", "").Replace("]", "").Replace(" ","");
        minResearchLevelReq = new List<float>();
        foreach (var _str in array[7].Split(','))
        {
            try { minResearchLevelReq.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        goldCost = int.Parse(array[8]);
        manaCost = int.Parse(array[9]);
    }
}
