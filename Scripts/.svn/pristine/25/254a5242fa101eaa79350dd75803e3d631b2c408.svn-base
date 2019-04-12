using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Mob_templateConfig配置表
/// </summary>
public partial class Mob_templateConfig : IReader
{
    public List<Mob_template> _Mob_template = new List<Mob_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Mob_template.Add(new Mob_template(array[i]));
        }
    }
}



/// <summary>
/// Mob_template配置表
/// </summary>
public partial class Mob_template : IReader
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
    /// 
    /// </summary>
    public string charNickName;
    /// <summary>
    /// 1=是Boss
    /// </summary>
    public int isBoss;
    /// <summary>
    /// 
    /// </summary>
    public string charRaceCN;
    /// <summary>
    /// 
    /// </summary>
    public string charTypeCN;
    /// <summary>
    /// 
    /// </summary>
    public string charClassCN;
    /// <summary>
    /// 
    /// </summary>
    public int timeLine;
    /// <summary>
    /// 
    /// </summary>
    public List<int> combatSkillList;
    /// <summary>
    /// 
    /// </summary>
    public List<int> combatSkillList2;
    /// <summary>
    /// 
    /// </summary>
    public List<int> combatSkillList3;
    /// <summary>
    /// 
    /// </summary>
    public List<int> combatSkillList4;
    /// <summary>
    /// 
    /// </summary>
    public int activeHealing;
    /// <summary>
    /// 
    /// </summary>
    public List<int> upgrade;
    /// <summary>
    /// 
    /// </summary>
    public float charRace;
    /// <summary>
    /// 
    /// </summary>
    public float charType;
    /// <summary>
    /// 
    /// </summary>
    public float charClass;
    /// <summary>
    /// 
    /// </summary>
    public float charRole;
    /// <summary>
    /// 
    /// </summary>
    public float baseHP;
    /// <summary>
    /// 
    /// </summary>
    public float baseAttack;
    /// <summary>
    /// 
    /// </summary>
    public float baseShield;
    /// <summary>
    /// 
    /// </summary>
    public float baseArmor;
    /// <summary>
    /// 
    /// </summary>
    public float baseShieldReg;
    /// <summary>
    /// 
    /// </summary>
    public float baseArmorReg;
    /// <summary>
    /// 
    /// </summary>
    public float baseEnergyReg;
    /// <summary>
    /// 
    /// </summary>
    public float addLvHP;
    /// <summary>
    /// 
    /// </summary>
    public float charDP;
    /// <summary>
    /// 
    /// </summary>
    public int energyCompensate;
    /// <summary>
    /// 
    /// </summary>
    public List<int> tacticalSkillList;
    /// <summary>
    /// 
    /// </summary>
    public int reactCooldown;



    public Mob_template() { }
    public Mob_template(string content)
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
        charNickName = array[3];
        isBoss = int.Parse(array[4]);
        charRaceCN = array[5];
        charTypeCN = array[6];
        charClassCN = array[7];
        timeLine = int.Parse(array[8]);
        //列表combatSkillList取值
        array[9] = array[9].Replace("[", "").Replace("]", "").Replace(" ","");
        combatSkillList = new List<int>();
        foreach (var _str in array[9].Split(','))
        {
            try { combatSkillList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表combatSkillList2取值
        array[10] = array[10].Replace("[", "").Replace("]", "").Replace(" ","");
        combatSkillList2 = new List<int>();
        foreach (var _str in array[10].Split(','))
        {
            try { combatSkillList2.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表combatSkillList3取值
        array[11] = array[11].Replace("[", "").Replace("]", "").Replace(" ","");
        combatSkillList3 = new List<int>();
        foreach (var _str in array[11].Split(','))
        {
            try { combatSkillList3.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表combatSkillList4取值
        array[12] = array[12].Replace("[", "").Replace("]", "").Replace(" ","");
        combatSkillList4 = new List<int>();
        foreach (var _str in array[12].Split(','))
        {
            try { combatSkillList4.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        activeHealing = int.Parse(array[13]);
        //列表upgrade取值
        array[14] = array[14].Replace("[", "").Replace("]", "").Replace(" ","");
        upgrade = new List<int>();
        foreach (var _str in array[14].Split(','))
        {
            try { upgrade.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        charRace = float.Parse(array[15]);
        charType = float.Parse(array[16]);
        charClass = float.Parse(array[17]);
        charRole = float.Parse(array[18]);
        baseHP = float.Parse(array[19]);
        baseAttack = float.Parse(array[20]);
        baseShield = float.Parse(array[21]);
        baseArmor = float.Parse(array[22]);
        baseShieldReg = float.Parse(array[23]);
        baseArmorReg = float.Parse(array[24]);
        baseEnergyReg = float.Parse(array[25]);
        addLvHP = float.Parse(array[26]);
        charDP = float.Parse(array[27]);
        energyCompensate = int.Parse(array[28]);
        //列表tacticalSkillList取值
        array[29] = array[29].Replace("[", "").Replace("]", "").Replace(" ","");
        tacticalSkillList = new List<int>();
        foreach (var _str in array[29].Split(','))
        {
            try { tacticalSkillList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        reactCooldown = int.Parse(array[30]);
    }
}
