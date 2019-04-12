using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Char_qualityupConfig配置表
/// </summary>
public partial class Char_qualityupConfig : IReader
{
    public List<Char_qualityup> _Char_qualityup = new List<Char_qualityup>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Char_qualityup.Add(new Char_qualityup(array[i]));
        }
    }
}



/// <summary>
/// Char_qualityup配置表
/// </summary>
public partial class Char_qualityup : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int charQuality;
    /// <summary>
    /// 
    /// </summary>
    public int baseRank;
    /// <summary>
    /// 
    /// </summary>
    public float rankBonus;



    public Char_qualityup() { }
    public Char_qualityup(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        charQuality = int.Parse(array[0]);
        baseRank = int.Parse(array[1]);
        rankBonus = float.Parse(array[2]);
    }
}
