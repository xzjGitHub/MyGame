using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Shop_sellstackConfig配置表
/// </summary>
public partial class Shop_sellstackConfig : IReader
{
    public List<Shop_sellstack> _Shop_sellstack = new List<Shop_sellstack>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Shop_sellstack.Add(new Shop_sellstack(array[i]));
        }
    }
}



/// <summary>
/// Shop_sellstack配置表
/// </summary>
public partial class Shop_sellstack : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int sellStack;
    /// <summary>
    /// [itemID, stackCount]
    /// </summary>
    public List<int> itemStack;



    public Shop_sellstack() { }
    public Shop_sellstack(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        sellStack = int.Parse(array[0]);
        //列表itemStack取值
        array[1] = array[1].Replace("[", "").Replace("]", "").Replace(" ","");
        itemStack = new List<int>();
        foreach (var _str in array[1].Split(','))
        {
            try { itemStack.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
