using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Equip_templateConfig配置表
/// </summary>
public partial class Equip_templateConfig : IReader
{
    public List<Equip_template> _Equip_template = new List<Equip_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Equip_template.Add(new Equip_template(array[i]));
        }
    }
}



/// <summary>
/// Equip_template配置表
/// </summary>
public partial class Equip_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int templateID;
    /// <summary>
    /// [初始值，如果有初始值则 = 0，没有则= 1]
    /// </summary>
    public List<int> baseEquipRank;
    /// <summary>
    /// 
    /// </summary>
    public float tempShield;
    /// <summary>
    /// 
    /// </summary>
    public float tempArmor;
    /// <summary>
    /// 
    /// </summary>
    public float addArmor;
    /// <summary>
    /// 
    /// </summary>
    public float tempHP;
    /// <summary>
    /// 
    /// </summary>
    public float tempAttack;
    /// <summary>
    /// 
    /// </summary>
    public float tempAP;
    /// <summary>
    /// 
    /// </summary>
    public float tempSP;
    /// <summary>
    /// 
    /// </summary>
    public float tempSkill;
    /// <summary>
    /// 
    /// </summary>
    public float tempEnergyReg;
    /// <summary>
    /// 
    /// </summary>
    public float rndDef;
    /// <summary>
    /// 
    /// </summary>
    public float rndOff;
    /// <summary>
    /// 
    /// </summary>
    public float rndEnergyReg;
    /// <summary>
    /// 
    /// </summary>
    public float addLvDef;
    /// <summary>
    /// 
    /// </summary>
    public float rndLvDef;
    /// <summary>
    /// 
    /// </summary>
    public float rndLvHP;
    /// <summary>
    /// 
    /// </summary>
    public float UGProp;
    /// <summary>
    /// 
    /// </summary>
    public float UGProp1;
    /// <summary>
    /// 
    /// </summary>
    public float UGProp2;
    /// <summary>
    /// 
    /// </summary>
    public float shieldDB;
    /// <summary>
    /// 
    /// </summary>
    public float armorDB;
    /// <summary>
    /// 
    /// </summary>
    public float HPDB;
    /// <summary>
    /// 
    /// </summary>
    public float fvDB;
    /// <summary>
    /// 
    /// </summary>
    public float HPBonus;
    /// <summary>
    /// 
    /// </summary>
    public float revivePower;
    /// <summary>
    /// 随机属性集合
    /// </summary>
    public List<string> rndAttribute;
    /// <summary>
    /// 随机强化
    /// </summary>
    public List<int> upgrade;
    /// <summary>
    /// 强化中位数
    /// </summary>
    public float midUP;
    /// <summary>
    /// 
    /// </summary>
    public int equipType;
    /// <summary>
    /// 
    /// </summary>
    public int equipSlot;
    /// <summary>
    /// 显示的类型，1=武器，2=盔甲，5=项链，6=戒指
    /// </summary>
    public int displayType;
    /// <summary>
    /// 
    /// </summary>
    public string equipTypeCN;
    /// <summary>
    /// 
    /// </summary>
    public List<int> qualitySelectChance;
    /// <summary>
    /// 研究类型，1-=金属武器，2= 灵木武器，3=盔甲，4=饰品
    /// </summary>
    public int REType;



    public Equip_template() { }
    public Equip_template(string content)
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
        //列表baseEquipRank取值
        array[1] = array[1].Replace("[", "").Replace("]", "").Replace(" ","");
        baseEquipRank = new List<int>();
        foreach (var _str in array[1].Split(','))
        {
            try { baseEquipRank.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        tempShield = float.Parse(array[2]);
        tempArmor = float.Parse(array[3]);
        addArmor = float.Parse(array[4]);
        tempHP = float.Parse(array[5]);
        tempAttack = float.Parse(array[6]);
        tempAP = float.Parse(array[7]);
        tempSP = float.Parse(array[8]);
        tempSkill = float.Parse(array[9]);
        tempEnergyReg = float.Parse(array[10]);
        rndDef = float.Parse(array[11]);
        rndOff = float.Parse(array[12]);
        rndEnergyReg = float.Parse(array[13]);
        addLvDef = float.Parse(array[14]);
        rndLvDef = float.Parse(array[15]);
        rndLvHP = float.Parse(array[16]);
        UGProp = float.Parse(array[17]);
        UGProp1 = float.Parse(array[18]);
        UGProp2 = float.Parse(array[19]);
        shieldDB = float.Parse(array[20]);
        armorDB = float.Parse(array[21]);
        HPDB = float.Parse(array[22]);
        fvDB = float.Parse(array[23]);
        HPBonus = float.Parse(array[24]);
        revivePower = float.Parse(array[25]);
        //列表rndAttribute取值
        array[26] = array[26].Replace("[", "").Replace("]", "").Replace(" ","");
        rndAttribute = array[26] != String.Empty ? array[26].Split(',').ToList() : new List<string>();
        //列表upgrade取值
        array[27] = array[27].Replace("[", "").Replace("]", "").Replace(" ","");
        upgrade = new List<int>();
        foreach (var _str in array[27].Split(','))
        {
            try { upgrade.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        midUP = float.Parse(array[28]);
        equipType = int.Parse(array[29]);
        equipSlot = int.Parse(array[30]);
        displayType = int.Parse(array[31]);
        equipTypeCN = array[32];
        //列表qualitySelectChance取值
        array[33] = array[33].Replace("[", "").Replace("]", "").Replace(" ","");
        qualitySelectChance = new List<int>();
        foreach (var _str in array[33].Split(','))
        {
            try { qualitySelectChance.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        REType = int.Parse(array[34]);
    }
}
