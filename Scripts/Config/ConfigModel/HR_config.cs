using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// HR_configConfig配置表
/// </summary>
public partial class HR_configConfig : IReader
{
    public List<HR_config> _HR_config = new List<HR_config>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _HR_config.Add(new HR_config(array[i]));
        }
    }
}



/// <summary>
/// HR_config配置表
/// </summary>
public partial class HR_config : IReader
{
    /// <summary>
    /// 工作的每单位法力消耗
    /// </summary>
    public float unitManaCost;
    /// <summary>
    /// 
    /// </summary>
    public float baseGoldOutput;
    /// <summary>
    /// 单位研究增幅
    /// </summary>
    public float researchBonus;



    public HR_config() { }
    public HR_config(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        unitManaCost = float.Parse(array[0]);
        baseGoldOutput = float.Parse(array[1]);
        researchBonus = float.Parse(array[2]);
    }
}
