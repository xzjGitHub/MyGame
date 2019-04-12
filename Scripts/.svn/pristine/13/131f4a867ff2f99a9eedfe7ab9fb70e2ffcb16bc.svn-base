using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Bossskill_templateConfig配置表
/// </summary>
public partial class Bossskill_templateConfig : IReader
{
    public List<Bossskill_template> _Bossskill_template = new List<Bossskill_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Bossskill_template.Add(new Bossskill_template(array[i]));
        }
    }
}



/// <summary>
/// Bossskill_template配置表
/// </summary>
public partial class Bossskill_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int bossSkillID;
    /// <summary>
    /// 
    /// </summary>
    public int skillID;
    /// <summary>
    /// 每{}回合能够使用1次
    /// </summary>
    public int castFrequency;
    /// <summary>
    /// 是否有几率机制，1=是
    /// </summary>
    public int isRandom;
    /// <summary>
    /// 0=不替换targetType
    /// </summary>
    public List<int> altTargeType;
    /// <summary>
    /// 技能会为目标增加的受击计数
    /// </summary>
    public int isMarker;



    public Bossskill_template() { }
    public Bossskill_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        bossSkillID = int.Parse(array[0]);
        skillID = int.Parse(array[1]);
        castFrequency = int.Parse(array[2]);
        isRandom = int.Parse(array[3]);
        //列表altTargeType取值
        array[4] = array[4].Replace("[", "").Replace("]", "").Replace(" ","");
        altTargeType = new List<int>();
        foreach (var _str in array[4].Split(','))
        {
            try { altTargeType.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        isMarker = int.Parse(array[5]);
    }
}
