using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Shop_configConfig配置表
/// </summary>
public partial class Shop_configConfig : IReader
{
    public List<Shop_config> _Shop_config = new List<Shop_config>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Shop_config.Add(new Shop_config(array[i]));
        }
    }
}



/// <summary>
/// Shop_config配置表
/// </summary>
public partial class Shop_config : IReader
{
    /// <summary>
    /// 物品列表
    /// </summary>
    public List<int> sellList;
    /// <summary>
    /// 初始供应量
    /// </summary>
    public List<int> baseSupply;
    /// <summary>
    /// 基本金币销售
    /// </summary>
    public int baseGoldSales;
    /// <summary>
    /// 基本魔力消耗
    /// </summary>
    public int baseManaToGold;



    public Shop_config() { }
    public Shop_config(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        //列表sellList取值
        array[0] = array[0].Replace("[", "").Replace("]", "").Replace(" ","");
        sellList = new List<int>();
        foreach (var _str in array[0].Split(','))
        {
            try { sellList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表baseSupply取值
        array[1] = array[1].Replace("[", "").Replace("]", "").Replace(" ","");
        baseSupply = new List<int>();
        foreach (var _str in array[1].Split(','))
        {
            try { baseSupply.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        baseGoldSales = int.Parse(array[2]);
        baseManaToGold = int.Parse(array[3]);
    }
}
