using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Craft_templateConfig配置表
/// </summary>
public partial class Craft_templateConfig : IReader
{
    public List<Craft_template> _Craft_template = new List<Craft_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Craft_template.Add(new Craft_template(array[i]));
        }
    }
}



/// <summary>
/// Craft_template配置表
/// </summary>
public partial class Craft_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int craftID;
    /// <summary>
    /// 物品奖励
    /// </summary>
    public int instanceID;
    /// <summary>
    /// 核心等级需求
    /// </summary>
    public int coreLevelReq;
    /// <summary>
    /// 物品消耗
    /// </summary>
    public   List<List<int>> itemCost;
    /// <summary>
    /// 物品等级
    /// </summary>
    public int tempItemLevel;
    /// <summary>
    /// 
    /// </summary>
    public int goldCost;
    /// <summary>
    /// 
    /// </summary>
    public int manaCost;



    public Craft_template() { }
    public Craft_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        craftID = int.Parse(array[0]);
        instanceID = int.Parse(array[1]);
        coreLevelReq = int.Parse(array[2]);
        //列表itemCost取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        itemCost = new   List<List<int>>();
        foreach (var str in array[3].Split('-'))
        {
            try 
            {
                itemCost.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        tempItemLevel = int.Parse(array[4]);
        goldCost = int.Parse(array[5]);
        manaCost = int.Parse(array[6]);
    }
}
