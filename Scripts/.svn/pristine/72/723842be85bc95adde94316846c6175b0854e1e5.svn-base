using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Consumable_instanceConfig配置表
/// </summary>
public partial class Consumable_instanceConfig : IReader
{
    public List<Consumable_instance> _Consumable_instance = new List<Consumable_instance>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Consumable_instance.Add(new Consumable_instance(array[i]));
        }
    }
}



/// <summary>
/// Consumable_instance配置表
/// </summary>
public partial class Consumable_instance : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int instanceID;
    /// <summary>
    /// 
    /// </summary>
    public string itemName;
    /// <summary>
    /// 
    /// </summary>
    public List<int> itemRewardSet;
    /// <summary>
    /// 
    /// </summary>
    public List<float> baseRewardLevel;
    /// <summary>
    /// 
    /// </summary>
    public   List<List<int>> itemCost;



    public Consumable_instance() { }
    public Consumable_instance(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        instanceID = int.Parse(array[0]);
        itemName = array[1];
        //列表itemRewardSet取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        itemRewardSet = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { itemRewardSet.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表baseRewardLevel取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        baseRewardLevel = new List<float>();
        foreach (var _str in array[3].Split(','))
        {
            try { baseRewardLevel.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        //列表itemCost取值
        array[4] = array[4].Replace("[", "").Replace("]", "").Replace(" ","");
        itemCost = new   List<List<int>>();
        foreach (var str in array[4].Split('-'))
        {
            try 
            {
                itemCost.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
    }
}
