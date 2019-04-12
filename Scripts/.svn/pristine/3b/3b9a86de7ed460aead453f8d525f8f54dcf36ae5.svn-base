using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Char_lvupConfig配置表
/// </summary>
public partial class Char_lvupConfig : IReader
{
    public List<Char_lvup> _Char_lvup = new List<Char_lvup>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Char_lvup.Add(new Char_lvup(array[i]));
        }
    }
}



/// <summary>
/// Char_lvup配置表
/// </summary>
public partial class Char_lvup : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int charLevel;
    /// <summary>
    /// 
    /// </summary>
    public float levelupExp;
    /// <summary>
    /// 
    /// </summary>
    public float DMGRate;
    /// <summary>
    /// 该等级的最大位阶
    /// </summary>
    public int maxCharRank;
    /// <summary>
    /// 位阶提升的几率
    /// </summary>
    public int rkupChance;



    public Char_lvup() { }
    public Char_lvup(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        charLevel = int.Parse(array[0]);
        levelupExp = float.Parse(array[1]);
        DMGRate = float.Parse(array[2]);
        maxCharRank = int.Parse(array[3]);
        rkupChance = int.Parse(array[4]);
    }
}
