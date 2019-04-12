using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Targetset_templateConfig配置表
/// </summary>
public partial class Targetset_templateConfig : IReader
{
    public List<Targetset_template> _Targetset_template = new List<Targetset_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Targetset_template.Add(new Targetset_template(array[i]));
        }
    }
}



/// <summary>
/// Targetset_template配置表
/// </summary>
public partial class Targetset_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int targetSetID;
    /// <summary>
    /// 是否有演示效果
    /// </summary>
    public int isAnimated;
    /// <summary>
    /// 触发的TS绑定的显示事件
    /// </summary>
    public int bindingEvent;
    /// <summary>
    /// 生效的最小等级限制
    /// </summary>
    public int charLevelReq;
    /// <summary>
    /// 
    /// </summary>
    public int targetType;
    /// <summary>
    /// 
    /// </summary>
    public int targetCount;
    /// <summary>
    /// 
    /// </summary>
    public List<int> stateList;
    /// <summary>
    /// 
    /// </summary>
    public List<float> stateCoeSet;
    /// <summary>
    /// 触发TS的施放次数需求
    /// </summary>
    public int castCountReq;
    /// <summary>
    /// 触发TS的状态需求
    /// </summary>
    public int stateReq;
    /// <summary>
    /// 触发TS的连击属性需求
    /// </summary>
    public string activateControl;



    public Targetset_template() { }
    public Targetset_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        targetSetID = int.Parse(array[0]);
        isAnimated = int.Parse(array[1]);
        bindingEvent = int.Parse(array[2]);
        charLevelReq = int.Parse(array[3]);
        targetType = int.Parse(array[4]);
        targetCount = int.Parse(array[5]);
        //列表stateList取值
        array[6] = array[6].Replace("[", "").Replace("]", "").Replace(" ","");
        stateList = new List<int>();
        foreach (var _str in array[6].Split(','))
        {
            try { stateList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表stateCoeSet取值
        array[7] = array[7].Replace("[", "").Replace("]", "").Replace(" ","");
        stateCoeSet = new List<float>();
        foreach (var _str in array[7].Split(','))
        {
            try { stateCoeSet.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        castCountReq = int.Parse(array[8]);
        stateReq = int.Parse(array[9]);
        activateControl = array[10];
    }
}
