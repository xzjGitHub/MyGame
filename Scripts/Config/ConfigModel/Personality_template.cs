using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Personality_templateConfig配置表
/// </summary>
public partial class Personality_templateConfig : IReader
{
    public List<Personality_template> _Personality_template = new List<Personality_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Personality_template.Add(new Personality_template(array[i]));
        }
    }
}



/// <summary>
/// Personality_template配置表
/// </summary>
public partial class Personality_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int personalityID;
    /// <summary>
    /// 
    /// </summary>
    public string personalityName;
    /// <summary>
    /// 被动技能列表
    /// </summary>
    public List<int> passiveSkillList ;



    public Personality_template() { }
    public Personality_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        personalityID = int.Parse(array[0]);
        personalityName = array[1];
        //列表passiveSkillList 取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        passiveSkillList  = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { passiveSkillList .Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
