using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Item_startbountyConfig配置表
/// </summary>
public partial class Item_startbountyConfig : IReader
{
    public List<Item_startbounty> _Item_startbounty = new List<Item_startbounty>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Item_startbounty.Add(new Item_startbounty(array[i]));
        }
    }
}



/// <summary>
/// Item_startbounty配置表
/// </summary>
public partial class Item_startbounty : IReader
{
    /// <summary>
    /// 物品ID
    /// </summary>
    public int instanceID;
    /// <summary>
    /// 
    /// </summary>
    public int startBounty;
    /// <summary>
    /// 任务可否重复进行？
    /// </summary>
    public int isRepeatable;



    public Item_startbounty() { }
    public Item_startbounty(string content)
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
        startBounty = int.Parse(array[1]);
        isRepeatable = int.Parse(array[2]);
    }
}
