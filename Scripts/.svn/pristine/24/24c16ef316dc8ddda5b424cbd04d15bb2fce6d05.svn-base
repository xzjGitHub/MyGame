using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Equip_configConfig配置表
/// </summary>
public partial class Equip_configConfig : IReader
{
    public List<Equip_config> _Equip_config = new List<Equip_config>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Equip_config.Add(new Equip_config(array[i]));
        }
    }
}



/// <summary>
/// Equip_config配置表
/// </summary>
public partial class Equip_config : IReader
{
    /// <summary>
    /// 随机防御
    /// </summary>
    public float baseRndDef;
    /// <summary>
    /// 随机防御恢复
    /// </summary>
    public float baseRndDefReg;
    /// <summary>
    /// 随机伤害
    /// </summary>
    public float baseRndDamage;
    /// <summary>
    /// 每级Def成长
    /// </summary>
    public float addLvRndDef;
    /// <summary>
    /// 每级DMG成长
    /// </summary>
    public float addLvRndDefReg;
    /// <summary>
    /// 随机回能
    /// </summary>
    public float baseRndEnergyReg;
    /// <summary>
    /// 装备加值几率
    /// </summary>
    public List<float> addLevelChance ;



    public Equip_config() { }
    public Equip_config(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        baseRndDef = float.Parse(array[0]);
        baseRndDefReg = float.Parse(array[1]);
        baseRndDamage = float.Parse(array[2]);
        addLvRndDef = float.Parse(array[3]);
        addLvRndDefReg = float.Parse(array[4]);
        baseRndEnergyReg = float.Parse(array[5]);
        //列表addLevelChance 取值
        array[6] = array[6].Replace("[", "").Replace("]", "").Replace(" ","");
        addLevelChance  = new List<float>();
        foreach (var _str in array[6].Split(','))
        {
            try { addLevelChance .Add(float.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
