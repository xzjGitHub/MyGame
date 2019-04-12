using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Item_rewardlistConfig配置表
/// </summary>
public partial class Item_rewardlistConfig : IReader
{
    public List<Item_rewardlist> _Item_rewardlist = new List<Item_rewardlist>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Item_rewardlist.Add(new Item_rewardlist(array[i]));
        }
    }
}



/// <summary>
/// Item_rewardlist配置表
/// </summary>
public partial class Item_rewardlist : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int rewardListID;
    /// <summary>
    /// 
    /// </summary>
    public int bountyReq;
    /// <summary>
    /// 
    /// </summary>
    public List<int> itemList;
    /// <summary>
    /// 
    /// </summary>
    public List<int> selectChance;
    /// <summary>
    /// 
    /// </summary>
    public List<int> rewardCost;



    public Item_rewardlist() { }
    public Item_rewardlist(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        rewardListID = int.Parse(array[0]);
        bountyReq = int.Parse(array[1]);
        //列表itemList取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        itemList = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { itemList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表selectChance取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        selectChance = new List<int>();
        foreach (var _str in array[3].Split(','))
        {
            try { selectChance.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表rewardCost取值
        array[4] = array[4].Replace("[", "").Replace("]", "").Replace(" ","");
        rewardCost = new List<int>();
        foreach (var _str in array[4].Split(','))
        {
            try { rewardCost.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
