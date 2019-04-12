using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// PowerUp_templateConfig配置表
/// </summary>
public partial class PowerUp_templateConfig : IReader
{
    public List<PowerUp_template> _PowerUp_template = new List<PowerUp_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _PowerUp_template.Add(new PowerUp_template(array[i]));
        }
    }
}



/// <summary>
/// PowerUp_template配置表
/// </summary>
public partial class PowerUp_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int powerUpID;
    /// <summary>
    /// 强化生效的角色等级需求
    /// </summary>
    public int charLevelReq;
    /// <summary>
    /// 
    /// </summary>
    public string description;
    /// <summary>
    /// 需求类型
    /// </summary>
    public int reqType;
    /// <summary>
    /// 关联的性格
    /// </summary>
    public int personality;
    /// <summary>
    /// 增加的激励个数
    /// </summary>
    public int addEncourage;



    public PowerUp_template() { }
    public PowerUp_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        powerUpID = int.Parse(array[0]);
        charLevelReq = int.Parse(array[1]);
        description = array[2];
        reqType = int.Parse(array[3]);
        personality = int.Parse(array[4]);
        addEncourage = int.Parse(array[5]);
    }
}
