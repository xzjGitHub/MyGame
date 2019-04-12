using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Buff_templateConfig配置表
/// </summary>
public partial class Buff_templateConfig : IReader
{
    public List<Buff_template> _Buff_template = new List<Buff_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Buff_template.Add(new Buff_template(array[i]));
        }
    }
}



/// <summary>
/// Buff_template配置表
/// </summary>
public partial class Buff_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int buffID;
    /// <summary>
    /// 
    /// </summary>
    public int duration;
    /// <summary>
    /// 
    /// </summary>
    public int addRank;
    /// <summary>
    /// 
    /// </summary>
    public List<int> addTactical;
    /// <summary>
    /// 
    /// </summary>
    public List<int> addPassive;
    /// <summary>
    /// 
    /// </summary>
    public List<int> addTag;
    /// <summary>
    /// 
    /// </summary>
    public float goldCost;
    /// <summary>
    /// 
    /// </summary>
    public float manaCost;
    /// <summary>
    /// [物品ID, 物品个数]
    /// </summary>
    public List<int> itemCost;



    public Buff_template() { }
    public Buff_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        buffID = int.Parse(array[0]);
        duration = int.Parse(array[1]);
        addRank = int.Parse(array[2]);
        //列表addTactical取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        addTactical = new List<int>();
        foreach (var _str in array[3].Split(','))
        {
            try { addTactical.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表addPassive取值
        array[4] = array[4].Replace("[", "").Replace("]", "").Replace(" ","");
        addPassive = new List<int>();
        foreach (var _str in array[4].Split(','))
        {
            try { addPassive.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表addTag取值
        array[5] = array[5].Replace("[", "").Replace("]", "").Replace(" ","");
        addTag = new List<int>();
        foreach (var _str in array[5].Split(','))
        {
            try { addTag.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        goldCost = float.Parse(array[6]);
        manaCost = float.Parse(array[7]);
        //列表itemCost取值
        array[8] = array[8].Replace("[", "").Replace("]", "").Replace(" ","");
        itemCost = new List<int>();
        foreach (var _str in array[8].Split(','))
        {
            try { itemCost.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
