using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// PhaseEffectConfigConfig配置表
/// </summary>
public partial class PhaseEffectConfigConfig : IReader
{
    public List<PhaseEffectConfig> _PhaseEffectConfig = new List<PhaseEffectConfig>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _PhaseEffectConfig.Add(new PhaseEffectConfig(array[i]));
        }
    }
}



/// <summary>
/// PhaseEffectConfig配置表
/// </summary>
public partial class PhaseEffectConfig : IReader
{
    /// <summary>
    /// 阶段特效集ID
    /// </summary>
    public int phaseEffectID;
    /// <summary>
    /// 开始特效
    /// </summary>
    public   List<List<int>> startEffect;
    /// <summary>
    /// 过程特效1
    /// </summary>
    public List<int> phaseEffect1;
    /// <summary>
    /// 过程特效2
    /// </summary>
    public List<int> phaseEffect2;
    /// <summary>
    /// 结束特效
    /// </summary>
    public   List<List<int>> endEffect;



    public PhaseEffectConfig() { }
    public PhaseEffectConfig(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        phaseEffectID = int.Parse(array[0]);
        //列表startEffect取值
        array[1] = array[1].Replace("[", "").Replace("]", "").Replace(" ","");
        startEffect = new   List<List<int>>();
        foreach (var str in array[1].Split('-'))
        {
            try 
            {
                startEffect.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表phaseEffect1取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        phaseEffect1 = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { phaseEffect1.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表phaseEffect2取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        phaseEffect2 = new List<int>();
        foreach (var _str in array[3].Split(','))
        {
            try { phaseEffect2.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表endEffect取值
        array[4] = array[4].Replace("[", "").Replace("]", "").Replace(" ","");
        endEffect = new   List<List<int>>();
        foreach (var str in array[4].Split('-'))
        {
            try 
            {
                endEffect.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
    }
}
