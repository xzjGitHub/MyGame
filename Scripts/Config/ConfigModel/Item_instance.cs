using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Item_instanceConfig配置表
/// </summary>
public partial class Item_instanceConfig : IReader
{
    public List<Item_instance> _Item_instance = new List<Item_instance>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Item_instance.Add(new Item_instance(array[i]));
        }
    }
}



/// <summary>
/// Item_instance配置表
/// </summary>
public partial class Item_instance : IReader
{
    /// <summary>
    /// 物品ID
    /// </summary>
    public int instanceID;
    /// <summary>
    /// 物品名称
    /// </summary>
    public string itemName;
    /// <summary>
    /// 物品类型
    /// </summary>
    public string itemTypeCN;
    /// <summary>
    /// 
    /// </summary>
    public int itemType;
    /// <summary>
    /// 
    /// </summary>
    public int equipType;
    /// <summary>
    /// 金属武器附魔 = 1，灵木武器附魔=2，轻甲附魔=3，重甲附魔=4
    /// </summary>
    public int enchantType;
    /// <summary>
    /// 
    /// </summary>
    public List<string> itemIcon;
    /// <summary>
    /// 
    /// </summary>
    public string itemDescription;
    /// <summary>
    /// 
    /// </summary>
    public string itemDialog ;
    /// <summary>
    /// 装备及附魔的模板
    /// </summary>
    public List<int> template;
    /// <summary>
    /// 最小物品等级=装备的物品等级
    /// </summary>
    public float baseItemLevel;
    /// <summary>
    /// 主要用于附魔，决定附魔的最大强度
    /// </summary>
    public int maxItemLevel;
    /// <summary>
    /// 不能被卖出？0 = 可以，1 = 不行
    /// </summary>
    public int notTradable;
    /// <summary>
    /// 购买价格
    /// </summary>
    public float basePurchasePrice;
    /// <summary>
    /// 售出价格
    /// </summary>
    public float baseSellPrice;
    /// <summary>
    /// 
    /// </summary>
    public int isRelic;
    /// <summary>
    /// 合成列表
    /// </summary>
    public List<int> craftList;
    /// <summary>
    /// 
    /// </summary>
    public int charIDReq;
    /// <summary>
    /// 
    /// </summary>
    public int charLevelReq;
    /// <summary>
    /// 
    /// </summary>
    public int ERTemplate;



    public Item_instance() { }
    public Item_instance(string content)
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
        itemTypeCN = array[2];
        itemType = int.Parse(array[3]);
        equipType = int.Parse(array[4]);
        enchantType = int.Parse(array[5]);
        //列表itemIcon取值
        array[6] = array[6].Replace("[", "").Replace("]", "").Replace(" ","");
        itemIcon = array[6] != String.Empty ? array[6].Split(',').ToList() : new List<string>();
        itemDescription = array[7];
        itemDialog  = array[8];
        //列表template取值
        array[9] = array[9].Replace("[", "").Replace("]", "").Replace(" ","");
        template = new List<int>();
        foreach (var _str in array[9].Split(','))
        {
            try { template.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        baseItemLevel = float.Parse(array[10]);
        maxItemLevel = int.Parse(array[11]);
        notTradable = int.Parse(array[12]);
        basePurchasePrice = float.Parse(array[13]);
        baseSellPrice = float.Parse(array[14]);
        isRelic = int.Parse(array[15]);
        //列表craftList取值
        array[16] = array[16].Replace("[", "").Replace("]", "").Replace(" ","");
        craftList = new List<int>();
        foreach (var _str in array[16].Split(','))
        {
            try { craftList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        charIDReq = int.Parse(array[17]);
        charLevelReq = int.Parse(array[18]);
        ERTemplate = int.Parse(array[19]);
    }
}
