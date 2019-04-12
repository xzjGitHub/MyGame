using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Script_templateConfig配置表
/// </summary>
public partial class Script_templateConfig : IReader
{
    public List<Script_template> _Script_template = new List<Script_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Script_template.Add(new Script_template(array[i]));
        }
    }
}



/// <summary>
/// Script_template配置表
/// </summary>
public partial class Script_template : IReader
{
    /// <summary>
    /// 剧本ID
    /// </summary>
    public int templateID;
    /// <summary>
    /// 主城名
    /// </summary>
    public string capitalName;
    /// <summary>
    /// 初始区域
    /// </summary>
    public int initialZone;
    /// <summary>
    /// 开场地图
    /// </summary>
    public int initialMap;
    /// <summary>
    /// [初始角色ID，初始等级]
    /// </summary>
    public   List<List<int>> initialCharList;
    /// <summary>
    /// [性格，强化]
    /// </summary>
    public   List<List<int>> initialPersonality;
    /// <summary>
    /// 初始装备，武器、盔甲、项链、戒指
    /// </summary>
    public   List<List<int>> initialEquipment;
    /// <summary>
    /// 物品ID，物品数量
    /// </summary>
    public   List<List<int>> initialItemList;
    /// <summary>
    /// 
    /// </summary>
    public float initialGold;
    /// <summary>
    /// 
    /// </summary>
    public float initialMana;
    /// <summary>
    /// 年，月，日
    /// </summary>
    public List<int> initialDate;
    /// <summary>
    /// 初始任务
    /// </summary>
    public int initialBounty;
    /// <summary>
    /// 任务列表
    /// </summary>
    public List<int> bountyList;
    /// <summary>
    /// 初始解锁的建筑
    /// </summary>
    public List<int> initialBuilding;



    public Script_template() { }
    public Script_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        templateID = int.Parse(array[0]);
        capitalName = array[1];
        initialZone = int.Parse(array[2]);
        initialMap = int.Parse(array[3]);
        //列表initialCharList取值
        array[4] = array[4].Replace("[", "").Replace("]", "").Replace(" ","");
        initialCharList = new   List<List<int>>();
        foreach (var str in array[4].Split('-'))
        {
            try 
            {
                initialCharList.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表initialPersonality取值
        array[5] = array[5].Replace("[", "").Replace("]", "").Replace(" ","");
        initialPersonality = new   List<List<int>>();
        foreach (var str in array[5].Split('-'))
        {
            try 
            {
                initialPersonality.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表initialEquipment取值
        array[6] = array[6].Replace("[", "").Replace("]", "").Replace(" ","");
        initialEquipment = new   List<List<int>>();
        foreach (var str in array[6].Split('-'))
        {
            try 
            {
                initialEquipment.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表initialItemList取值
        array[7] = array[7].Replace("[", "").Replace("]", "").Replace(" ","");
        initialItemList = new   List<List<int>>();
        foreach (var str in array[7].Split('-'))
        {
            try 
            {
                initialItemList.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        initialGold = float.Parse(array[8]);
        initialMana = float.Parse(array[9]);
        //列表initialDate取值
        array[10] = array[10].Replace("[", "").Replace("]", "").Replace(" ","");
        initialDate = new List<int>();
        foreach (var _str in array[10].Split(','))
        {
            try { initialDate.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        initialBounty = int.Parse(array[11]);
        //列表bountyList取值
        array[12] = array[12].Replace("[", "").Replace("]", "").Replace(" ","");
        bountyList = new List<int>();
        foreach (var _str in array[12].Split(','))
        {
            try { bountyList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表initialBuilding取值
        array[13] = array[13].Replace("[", "").Replace("]", "").Replace(" ","");
        initialBuilding = new List<int>();
        foreach (var _str in array[13].Split(','))
        {
            try { initialBuilding.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
