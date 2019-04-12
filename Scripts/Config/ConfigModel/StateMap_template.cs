using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// StateMap_templateConfig配置表
/// </summary>
public partial class StateMap_templateConfig : IReader
{
    public List<StateMap_template> _StateMap_template = new List<StateMap_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _StateMap_template.Add(new StateMap_template(array[i]));
        }
    }
}



/// <summary>
/// StateMap_template配置表
/// </summary>
public partial class StateMap_template : IReader
{
    /// <summary>
    /// 编号
    /// </summary>
    public int StateID;
    /// <summary>
    /// 区域名称
    /// </summary>
    public string StateName;
    /// <summary>
    /// 区域图片
    /// </summary>
    public string StatePic;
    /// <summary>
    /// 区域关卡
    /// </summary>
    public List<int> StateLevel;
    /// <summary>
    /// 解锁需求的mapID
    /// </summary>
    public int unlockReq;
    /// <summary>
    /// [物品ID，消耗数量]
    /// </summary>
    public List<int> entryCost;



    public StateMap_template() { }
    public StateMap_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        StateID = int.Parse(array[0]);
        StateName = array[1];
        StatePic = array[2];
        //列表StateLevel取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        StateLevel = new List<int>();
        foreach (var _str in array[3].Split(','))
        {
            try { StateLevel.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        unlockReq = int.Parse(array[4]);
        //列表entryCost取值
        array[5] = array[5].Replace("[", "").Replace("]", "").Replace(" ","");
        entryCost = new List<int>();
        foreach (var _str in array[5].Split(','))
        {
            try { entryCost.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
