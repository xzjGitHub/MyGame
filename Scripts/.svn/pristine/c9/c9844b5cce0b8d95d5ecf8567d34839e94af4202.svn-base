using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// ER_lvupConfig配置表
/// </summary>
public partial class ER_lvupConfig : IReader
{
    public List<ER_lvup> _ER_lvup = new List<ER_lvup>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _ER_lvup.Add(new ER_lvup(array[i]));
        }
    }
}



/// <summary>
/// ER_lvup配置表
/// </summary>
public partial class ER_lvup : IReader
{
    /// <summary>
    /// 当前等级
    /// </summary>
    public int ERLevel;
    /// <summary>
    /// 升级所需的累积值
    /// </summary>
    public float lvupERExp;
    /// <summary>
    /// [最小值加值，最大值加值]
    /// </summary>
    public List<int> addItemLevel;
    /// <summary>
    /// 装备配件上限
    /// </summary>
    public int maxParts;



    public ER_lvup() { }
    public ER_lvup(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        ERLevel = int.Parse(array[0]);
        lvupERExp = float.Parse(array[1]);
        //列表addItemLevel取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        addItemLevel = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { addItemLevel.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        maxParts = int.Parse(array[3]);
    }
}
