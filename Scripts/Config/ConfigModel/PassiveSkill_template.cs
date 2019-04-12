using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// PassiveSkill_templateConfig配置表
/// </summary>
public partial class PassiveSkill_templateConfig : IReader
{
    public List<PassiveSkill_template> _PassiveSkill_template = new List<PassiveSkill_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _PassiveSkill_template.Add(new PassiveSkill_template(array[i]));
        }
    }
}



/// <summary>
/// PassiveSkill_template配置表
/// </summary>
public partial class PassiveSkill_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int passiveSkillID;
    /// <summary>
    /// 
    /// </summary>
    public int personalityID;
    /// <summary>
    /// 
    /// </summary>
    public string skillName;
    /// <summary>
    /// 
    /// </summary>
    public string description;
    /// <summary>
    /// 
    /// </summary>
    public int reqType;
    /// <summary>
    /// 关联的性格
    /// </summary>
    public List<int> personalityList;
    /// <summary>
    /// 1=给自身，2=给对方
    /// </summary>
    public int rewardType;
    /// <summary>
    /// 
    /// </summary>
    public List<int> addTag;
    /// <summary>
    /// 增加的激励个数
    /// </summary>
    public int addEncourage;
    /// <summary>
    /// 对应的强化能力
    /// </summary>
    public List<int> powerUp;



    public PassiveSkill_template() { }
    public PassiveSkill_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        passiveSkillID = int.Parse(array[0]);
        personalityID = int.Parse(array[1]);
        skillName = array[2];
        description = array[3];
        reqType = int.Parse(array[4]);
        //列表personalityList取值
        array[5] = array[5].Replace("[", "").Replace("]", "").Replace(" ","");
        personalityList = new List<int>();
        foreach (var _str in array[5].Split(','))
        {
            try { personalityList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        rewardType = int.Parse(array[6]);
        //列表addTag取值
        array[7] = array[7].Replace("[", "").Replace("]", "").Replace(" ","");
        addTag = new List<int>();
        foreach (var _str in array[7].Split(','))
        {
            try { addTag.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        addEncourage = int.Parse(array[8]);
        //列表powerUp取值
        array[9] = array[9].Replace("[", "").Replace("]", "").Replace(" ","");
        powerUp = new List<int>();
        foreach (var _str in array[9].Split(','))
        {
            try { powerUp.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
