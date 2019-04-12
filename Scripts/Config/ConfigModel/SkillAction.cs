using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// SkillActionConfig配置表
/// </summary>
public partial class SkillActionConfig : IReader
{
    public List<SkillAction> _SkillAction = new List<SkillAction>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _SkillAction.Add(new SkillAction(array[i]));
        }
    }
}



/// <summary>
/// SkillAction配置表
/// </summary>
public partial class SkillAction : IReader
{
    /// <summary>
    /// 技能ID
    /// </summary>
    public int skillID;
    /// <summary>
    /// 每个actionEffect对应1个isAnimated = 1的targetSet；支持同1个技能有多个isAnimated的targetSet，每个targetSet对应1个actionEffect；依次播放，以动作的endEvent切换
    /// </summary>
    public List<int> skillAction;



    public SkillAction() { }
    public SkillAction(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        skillID = int.Parse(array[0]);
        //列表skillAction取值
        array[1] = array[1].Replace("[", "").Replace("]", "").Replace(" ","");
        skillAction = new List<int>();
        foreach (var _str in array[1].Split(','))
        {
            try { skillAction.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
