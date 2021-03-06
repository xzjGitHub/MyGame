using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Combatskill_templateConfig配置表
/// </summary>
public partial class Combatskill_templateConfig : IReader
{
    public List<Combatskill_template> _Combatskill_template = new List<Combatskill_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Combatskill_template.Add(new Combatskill_template(array[i]));
        }
    }
}



/// <summary>
/// Combatskill_template配置表
/// </summary>
public partial class Combatskill_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int skillID;
    /// <summary>
    /// 
    /// </summary>
    public string 角色;
    /// <summary>
    /// 
    /// </summary>
    public string skillName;
    /// <summary>
    /// 战斗技能 = 1，战术技能 = 2，被动技能 = 3
    /// </summary>
    public int skillCategory;
    /// <summary>
    /// 
    /// </summary>
    public string skillTypeCN;
    /// <summary>
    /// 技能定位
    /// </summary>
    public string skillIcon;
    /// <summary>
    /// 
    /// </summary>
    public int initialCooldown;
    /// <summary>
    /// 
    /// </summary>
    public int Cooldown;
    /// <summary>
    /// 在进入冷却前，能够施放的次数
    /// </summary>
    public int skillCharge;
    /// <summary>
    /// 在[1]回合中，使用[0]次
    /// </summary>
    public List<int> maxCast;
    /// <summary>
    /// 重置施放计数的上限
    /// </summary>
    public int castCount;
    /// <summary>
    /// 施放阶段
    /// </summary>
    public int castType ;
    /// <summary>
    /// 能量消耗
    /// </summary>
    public int energyCost;
    /// <summary>
    /// 
    /// </summary>
    public List<int> targetSetList;
    /// <summary>
    /// 
    /// </summary>
    public string skillDescription1;
    /// <summary>
    /// 30级时的进阶说明
    /// </summary>
    public string skillDescription2;
    /// <summary>
    /// 在说明中显示的属性数值
    /// </summary>
    public   List<List<int>> valueAttribute;
    /// <summary>
    /// 信息中的参数是否需要显示为%，0不是，1=%
    /// </summary>
    public List<int> isPercentage;
    /// <summary>
    /// 0=非高威胁，1=高威胁
    /// </summary>
    public int isHighThreat;
    /// <summary>
    /// 
    /// </summary>
    public int isAoE;
    /// <summary>
    /// 击晕难度
    /// </summary>
    public float stunValue;
    /// <summary>
    /// 
    /// </summary>
    public int skillType;
    /// <summary>
    /// 激励增伤
    /// </summary>
    public float encourageDB;
    /// <summary>
    /// 替换的技能
    /// </summary>
    public int alternativeSkill;
    /// <summary>
    /// 1=个人激励；2=指挥
    /// </summary>
    public int commonType;



    public Combatskill_template() { }
    public Combatskill_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        skillID = int.Parse(array[0]);
        角色 = array[1];
        skillName = array[2];
        skillCategory = int.Parse(array[3]);
        skillTypeCN = array[4];
        skillIcon = array[5];
        initialCooldown = int.Parse(array[6]);
        Cooldown = int.Parse(array[7]);
        skillCharge = int.Parse(array[8]);
        //列表maxCast取值
        array[9] = array[9].Replace("[", "").Replace("]", "").Replace(" ","");
        maxCast = new List<int>();
        foreach (var _str in array[9].Split(','))
        {
            try { maxCast.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        castCount = int.Parse(array[10]);
        castType  = int.Parse(array[11]);
        energyCost = int.Parse(array[12]);
        //列表targetSetList取值
        array[13] = array[13].Replace("[", "").Replace("]", "").Replace(" ","");
        targetSetList = new List<int>();
        foreach (var _str in array[13].Split(','))
        {
            try { targetSetList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        skillDescription1 = array[14];
        skillDescription2 = array[15];
        //列表valueAttribute取值
        array[16] = array[16].Replace("[", "").Replace("]", "").Replace(" ","");
        valueAttribute = new   List<List<int>>();
        foreach (var str in array[16].Split('-'))
        {
            try 
            {
                valueAttribute.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表isPercentage取值
        array[17] = array[17].Replace("[", "").Replace("]", "").Replace(" ","");
        isPercentage = new List<int>();
        foreach (var _str in array[17].Split(','))
        {
            try { isPercentage.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        isHighThreat = int.Parse(array[18]);
        isAoE = int.Parse(array[19]);
        stunValue = float.Parse(array[20]);
        skillType = int.Parse(array[21]);
        encourageDB = float.Parse(array[22]);
        alternativeSkill = int.Parse(array[23]);
        commonType = int.Parse(array[24]);
    }
}
