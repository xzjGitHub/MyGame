using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Refine_templateConfig配置表
/// </summary>
public partial class Refine_templateConfig : IReader
{
    public List<Refine_template> _Refine_template = new List<Refine_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Refine_template.Add(new Refine_template(array[i]));
        }
    }
}



/// <summary>
/// Refine_template配置表
/// </summary>
public partial class Refine_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int refineID;
    /// <summary>
    /// 
    /// </summary>
    public int itemCost;
    /// <summary>
    /// 精炼产出物，每个产出1个
    /// </summary>
    public List<int> refineReward;



    public Refine_template() { }
    public Refine_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        refineID = int.Parse(array[0]);
        itemCost = int.Parse(array[1]);
        //列表refineReward取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        refineReward = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { refineReward.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
