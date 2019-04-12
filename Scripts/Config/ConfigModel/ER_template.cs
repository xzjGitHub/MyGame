using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// ER_templateConfig配置表
/// </summary>
public partial class ER_templateConfig : IReader
{
    public List<ER_template> _ER_template = new List<ER_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _ER_template.Add(new ER_template(array[i]));
        }
    }
}



/// <summary>
/// ER_template配置表
/// </summary>
public partial class ER_template : IReader
{
    /// <summary>
    /// 模板ID
    /// </summary>
    public int templateID;
    /// <summary>
    /// 
    /// </summary>
    public float addMinItemLevel;
    /// <summary>
    /// [最小有效，最大有效]
    /// </summary>
    public List<int> activeRELevel ;
    /// <summary>
    /// [最小经验，最大经验]
    /// </summary>
    public List<int> REExpReward;
    /// <summary>
    /// 
    /// </summary>
    public List<float> minResearchLevelReq;



    public ER_template() { }
    public ER_template(string content)
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
        addMinItemLevel = float.Parse(array[1]);
        //列表activeRELevel 取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        activeRELevel  = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { activeRELevel .Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表REExpReward取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        REExpReward = new List<int>();
        foreach (var _str in array[3].Split(','))
        {
            try { REExpReward.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表minResearchLevelReq取值
        array[4] = array[4].Replace("[", "").Replace("]", "").Replace(" ","");
        minResearchLevelReq = new List<float>();
        foreach (var _str in array[4].Split(','))
        {
            try { minResearchLevelReq.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
