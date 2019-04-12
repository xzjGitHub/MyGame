using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Equip_qualityupConfig配置表
/// </summary>
public partial class Equip_qualityupConfig : IReader
{
    public List<Equip_qualityup> _Equip_qualityup = new List<Equip_qualityup>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Equip_qualityup.Add(new Equip_qualityup(array[i]));
        }
    }
}



/// <summary>
/// Equip_qualityup配置表
/// </summary>
public partial class Equip_qualityup : IReader
{
    /// <summary>
    /// 装备品质
    /// </summary>
    public int equipQuality;
    /// <summary>
    /// 品质等级修正
    /// </summary>
    public int addLevel;



    public Equip_qualityup() { }
    public Equip_qualityup(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        equipQuality = int.Parse(array[0]);
        addLevel = int.Parse(array[1]);
    }
}
