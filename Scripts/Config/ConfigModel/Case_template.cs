using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Case_templateConfig配置表
/// </summary>
public partial class Case_templateConfig : IReader
{
    public List<Case_template> _Case_template = new List<Case_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Case_template.Add(new Case_template(array[i]));
        }
    }
}



/// <summary>
/// Case_template配置表
/// </summary>
public partial class Case_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int caseID;
    /// <summary>
    /// 1 = 触发剧情；2 = 阵营游戏；3 = 触发悬赏
    /// </summary>
    public int caseType;
    /// <summary>
    /// caseType = 1，触发剧情的textID；caseType = 2，阵营游戏的caseID；caseType = 3，触发悬赏的bountyID
    /// </summary>
    public int subjectID;



    public Case_template() { }
    public Case_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        caseID = int.Parse(array[0]);
        caseType = int.Parse(array[1]);
        subjectID = int.Parse(array[2]);
    }
}
