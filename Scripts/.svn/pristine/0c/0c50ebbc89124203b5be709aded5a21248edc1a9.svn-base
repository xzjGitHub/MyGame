using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Fort_templateConfig配置表
/// </summary>
public partial class Fort_templateConfig : IReader
{
    public List<Fort_template> _Fort_template = new List<Fort_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Fort_template.Add(new Fort_template(array[i]));
        }
    }
}



/// <summary>
/// Fort_template配置表
/// </summary>
public partial class Fort_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int fortID;
    /// <summary>
    /// 要塞所属的环境
    /// </summary>
    public int zoneType;
    /// <summary>
    /// 
    /// </summary>
    public string fortName;
    /// <summary>
    /// 
    /// </summary>
    public string fortIcon;
    /// <summary>
    /// 要塞信息文本ID
    /// </summary>
    public int fortText;
    /// <summary>
    /// 
    /// </summary>
    public int fortType;
    /// <summary>
    /// 
    /// </summary>
    public List<int> coordinate;
    /// <summary>
    /// 
    /// </summary>
    public List<int> mapReq;
    /// <summary>
    /// 
    /// </summary>
    public List<int> fortReq;
    /// <summary>
    /// 
    /// </summary>
    public List<int> mobTeamList ;
    /// <summary>
    /// 
    /// </summary>
    public List<int> retaliationID;
    /// <summary>
    /// 
    /// </summary>
    public int baseGoldReward;
    /// <summary>
    /// 
    /// </summary>
    public int baseTokenReward;
    /// <summary>
    /// 
    /// </summary>
    public List<int> itemRewardSet;
    /// <summary>
    /// 
    /// </summary>
    public List<int> baseRewardLevel;
    /// <summary>
    /// 
    /// </summary>
    public int cycleGoldReward;
    /// <summary>
    /// 
    /// </summary>
    public int cycleTokenReward;
    /// <summary>
    /// 
    /// </summary>
    public List<int> cycleRewardSet;
    /// <summary>
    /// 
    /// </summary>
    public int unlockZone;
    /// <summary>
    /// 
    /// </summary>
    public List<int> addSupply;



    public Fort_template() { }
    public Fort_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        fortID = int.Parse(array[0]);
        zoneType = int.Parse(array[1]);
        fortName = array[2];
        fortIcon = array[3];
        fortText = int.Parse(array[4]);
        fortType = int.Parse(array[5]);
        //列表coordinate取值
        array[6] = array[6].Replace("[", "").Replace("]", "").Replace(" ","");
        coordinate = new List<int>();
        foreach (var _str in array[6].Split(','))
        {
            try { coordinate.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表mapReq取值
        array[7] = array[7].Replace("[", "").Replace("]", "").Replace(" ","");
        mapReq = new List<int>();
        foreach (var _str in array[7].Split(','))
        {
            try { mapReq.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表fortReq取值
        array[8] = array[8].Replace("[", "").Replace("]", "").Replace(" ","");
        fortReq = new List<int>();
        foreach (var _str in array[8].Split(','))
        {
            try { fortReq.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表mobTeamList 取值
        array[9] = array[9].Replace("[", "").Replace("]", "").Replace(" ","");
        mobTeamList  = new List<int>();
        foreach (var _str in array[9].Split(','))
        {
            try { mobTeamList .Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表retaliationID取值
        array[10] = array[10].Replace("[", "").Replace("]", "").Replace(" ","");
        retaliationID = new List<int>();
        foreach (var _str in array[10].Split(','))
        {
            try { retaliationID.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        baseGoldReward = int.Parse(array[11]);
        baseTokenReward = int.Parse(array[12]);
        //列表itemRewardSet取值
        array[13] = array[13].Replace("[", "").Replace("]", "").Replace(" ","");
        itemRewardSet = new List<int>();
        foreach (var _str in array[13].Split(','))
        {
            try { itemRewardSet.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表baseRewardLevel取值
        array[14] = array[14].Replace("[", "").Replace("]", "").Replace(" ","");
        baseRewardLevel = new List<int>();
        foreach (var _str in array[14].Split(','))
        {
            try { baseRewardLevel.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        cycleGoldReward = int.Parse(array[15]);
        cycleTokenReward = int.Parse(array[16]);
        //列表cycleRewardSet取值
        array[17] = array[17].Replace("[", "").Replace("]", "").Replace(" ","");
        cycleRewardSet = new List<int>();
        foreach (var _str in array[17].Split(','))
        {
            try { cycleRewardSet.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        unlockZone = int.Parse(array[18]);
        //列表addSupply取值
        array[19] = array[19].Replace("[", "").Replace("]", "").Replace(" ","");
        addSupply = new List<int>();
        foreach (var _str in array[19].Split(','))
        {
            try { addSupply.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
