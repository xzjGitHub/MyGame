using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// CharRPackConfig配置表
/// </summary>
public partial class CharRPackConfig : IReader
{
    public List<CharRPack> _CharRPack = new List<CharRPack>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _CharRPack.Add(new CharRPack(array[i]));
        }
    }
}



/// <summary>
/// CharRPack配置表
/// </summary>
public partial class CharRPack : IReader
{
    /// <summary>
    /// 编号
    /// </summary>
    public int templateD;
    /// <summary>
    /// 名称
    /// </summary>
    public string charName;
    /// <summary>
    /// 资源包名
    /// </summary>
    public string charRP;
    /// <summary>
    /// 技能列表
    /// </summary>
    public List<int> skillList;



    public CharRPack() { }
    public CharRPack(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        templateD = int.Parse(array[0]);
        charName = array[1];
        charRP = array[2];
        //列表skillList取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        skillList = new List<int>();
        foreach (var _str in array[3].Split(','))
        {
            try { skillList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
