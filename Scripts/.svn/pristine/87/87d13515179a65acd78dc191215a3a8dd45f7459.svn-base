using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Combat_configConfig配置表
/// </summary>
public partial class Combat_configConfig : IReader
{
    public List<Combat_config> _Combat_config = new List<Combat_config>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Combat_config.Add(new Combat_config(array[i]));
        }
    }
}



/// <summary>
/// Combat_config配置表
/// </summary>
public partial class Combat_config : IReader
{
    /// <summary>
    /// 伤害偏移量
    /// </summary>
    public float DMGDev;
    /// <summary>
    /// 魔能恢复
    /// </summary>
    public int baseEnergyReg;
    /// <summary>
    /// 最大魔能
    /// </summary>
    public int maxEnergy;
    /// <summary>
    /// 必定先攻的先攻值比例
    /// </summary>
    public float initiativeRatio;
    /// <summary>
    /// 
    /// </summary>
    public int retreatReq;
    /// <summary>
    /// 
    /// </summary>
    public float globHealing;
    /// <summary>
    /// ？
    /// </summary>
    public float roundPause;
    /// <summary>
    /// 
    /// </summary>
    public float resurrectProp;
    /// <summary>
    /// 
    /// </summary>
    public List<int> dispelSkill;
    /// <summary>
    /// 队伍激励
    /// </summary>
    public int commandSkill;
    /// <summary>
    /// 队长的额外强化
    /// </summary>
    public List<int> commandPU;
    /// <summary>
    /// 
    /// </summary>
    public List<int> baseEncourage ;



    public Combat_config() { }
    public Combat_config(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        DMGDev = float.Parse(array[0]);
        baseEnergyReg = int.Parse(array[1]);
        maxEnergy = int.Parse(array[2]);
        initiativeRatio = float.Parse(array[3]);
        retreatReq = int.Parse(array[4]);
        globHealing = float.Parse(array[5]);
        roundPause = float.Parse(array[6]);
        resurrectProp = float.Parse(array[7]);
        //列表dispelSkill取值
        array[8] = array[8].Replace("[", "").Replace("]", "").Replace(" ","");
        dispelSkill = new List<int>();
        foreach (var _str in array[8].Split(','))
        {
            try { dispelSkill.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        commandSkill = int.Parse(array[9]);
        //列表commandPU取值
        array[10] = array[10].Replace("[", "").Replace("]", "").Replace(" ","");
        commandPU = new List<int>();
        foreach (var _str in array[10].Split(','))
        {
            try { commandPU.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表baseEncourage 取值
        array[11] = array[11].Replace("[", "").Replace("]", "").Replace(" ","");
        baseEncourage  = new List<int>();
        foreach (var _str in array[11].Split(','))
        {
            try { baseEncourage .Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
