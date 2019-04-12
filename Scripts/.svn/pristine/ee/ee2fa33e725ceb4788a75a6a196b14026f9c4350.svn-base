using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Char_templateConfig配置表
/// </summary>
public partial class Char_templateConfig : IReader
{
    public List<Char_template> _Char_template = new List<Char_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Char_template.Add(new Char_template(array[i]));
        }
    }
}



/// <summary>
/// Char_template配置表
/// </summary>
public partial class Char_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int templateID;
    /// <summary>
    /// 
    /// </summary>
    public string charName;
    /// <summary>
    /// 
    /// </summary>
    public string HeadIcon;
    /// <summary>
    /// 种族
    /// </summary>
    public string charRaceCN;
    /// <summary>
    /// 角色类型
    /// </summary>
    public string charTypeCN;
    /// <summary>
    /// 角色职业
    /// </summary>
    public string charClassCN;
    /// <summary>
    /// 军衔几率[新兵，E，...S]
    /// </summary>
    public List<int> charRankChance ;
    /// <summary>
    /// 指挥官的几率
    /// </summary>
    public int cmderChance;
    /// <summary>
    /// 主手动技能
    /// </summary>
    public List<int> manualSkillList;
    /// <summary>
    /// 随机技能
    /// </summary>
    public List<int> randomSkillSetList;
    /// <summary>
    /// 激励技能
    /// </summary>
    public int commonSkillList;
    /// <summary>
    /// 普攻技能
    /// </summary>
    public List<int> combatSkillList;
    /// <summary>
    /// 生命
    /// </summary>
    public float baseHP;
    /// <summary>
    /// 伤害
    /// </summary>
    public float baseAttack;
    /// <summary>
    /// 
    /// </summary>
    public float baseSkill;
    /// <summary>
    /// 回能
    /// </summary>
    public float baseEnergyReg;
    /// <summary>
    /// 升级生命加值
    /// </summary>
    public float addLvHP;
    /// <summary>
    /// 激活普通治疗所需的血量减少比例
    /// </summary>
    public float activeHealing;
    /// <summary>
    /// 可用武器类型
    /// </summary>
    public int weaponType;
    /// <summary>
    /// 战术技能列表
    /// </summary>
    public List<int> tacticalSkillList;
    /// <summary>
    /// 个性列表
    /// </summary>
    public List<int> personalityList;
    /// <summary>
    /// 角色强化的随机范围
    /// </summary>
    public List<int> upgrade;
    /// <summary>
    /// 强化中位数
    /// </summary>
    public float midUP;
    /// <summary>
    /// 是否可以被解散？1 = 可，0 = 否
    /// </summary>
    public int canbeDisband;
    /// <summary>
    /// 
    /// </summary>
    public int charRace;
    /// <summary>
    /// 
    /// </summary>
    public int charType;
    /// <summary>
    /// 如果charClass = 6，则需要额外显示1个治疗技能；治疗技能是combatSkillList中，index = 1的技能
    /// </summary>
    public int charClass;
    /// <summary>
    /// 
    /// </summary>
    public int charRole;
    /// <summary>
    /// 默认武器模型
    /// </summary>
    public int defaultWeapon;
    /// <summary>
    /// 属性显示
    /// </summary>
    public List<int> attributeSet;
    /// <summary>
    /// 普攻触发机制冷却
    /// </summary>
    public int reactCooldown;



    public Char_template() { }
    public Char_template(string content)
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
        charName = array[1];
        HeadIcon = array[2];
        charRaceCN = array[3];
        charTypeCN = array[4];
        charClassCN = array[5];
        //列表charRankChance 取值
        array[6] = array[6].Replace("[", "").Replace("]", "").Replace(" ","");
        charRankChance  = new List<int>();
        foreach (var _str in array[6].Split(','))
        {
            try { charRankChance .Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        cmderChance = int.Parse(array[7]);
        //列表manualSkillList取值
        array[8] = array[8].Replace("[", "").Replace("]", "").Replace(" ","");
        manualSkillList = new List<int>();
        foreach (var _str in array[8].Split(','))
        {
            try { manualSkillList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表randomSkillSetList取值
        array[9] = array[9].Replace("[", "").Replace("]", "").Replace(" ","");
        randomSkillSetList = new List<int>();
        foreach (var _str in array[9].Split(','))
        {
            try { randomSkillSetList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        commonSkillList = int.Parse(array[10]);
        //列表combatSkillList取值
        array[11] = array[11].Replace("[", "").Replace("]", "").Replace(" ","");
        combatSkillList = new List<int>();
        foreach (var _str in array[11].Split(','))
        {
            try { combatSkillList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        baseHP = float.Parse(array[12]);
        baseAttack = float.Parse(array[13]);
        baseSkill = float.Parse(array[14]);
        baseEnergyReg = float.Parse(array[15]);
        addLvHP = float.Parse(array[16]);
        activeHealing = float.Parse(array[17]);
        weaponType = int.Parse(array[18]);
        //列表tacticalSkillList取值
        array[19] = array[19].Replace("[", "").Replace("]", "").Replace(" ","");
        tacticalSkillList = new List<int>();
        foreach (var _str in array[19].Split(','))
        {
            try { tacticalSkillList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表personalityList取值
        array[20] = array[20].Replace("[", "").Replace("]", "").Replace(" ","");
        personalityList = new List<int>();
        foreach (var _str in array[20].Split(','))
        {
            try { personalityList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表upgrade取值
        array[21] = array[21].Replace("[", "").Replace("]", "").Replace(" ","");
        upgrade = new List<int>();
        foreach (var _str in array[21].Split(','))
        {
            try { upgrade.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        midUP = float.Parse(array[22]);
        canbeDisband = int.Parse(array[23]);
        charRace = int.Parse(array[24]);
        charType = int.Parse(array[25]);
        charClass = int.Parse(array[26]);
        charRole = int.Parse(array[27]);
        defaultWeapon = int.Parse(array[28]);
        //列表attributeSet取值
        array[29] = array[29].Replace("[", "").Replace("]", "").Replace(" ","");
        attributeSet = new List<int>();
        foreach (var _str in array[29].Split(','))
        {
            try { attributeSet.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        reactCooldown = int.Parse(array[30]);
    }
}
