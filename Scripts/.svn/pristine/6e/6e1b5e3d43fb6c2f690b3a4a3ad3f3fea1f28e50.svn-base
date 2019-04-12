using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// HeroSelect_templateConfig配置表
/// </summary>
public partial class HeroSelect_templateConfig : IReader
{
    public List<HeroSelect_template> _HeroSelect_template = new List<HeroSelect_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _HeroSelect_template.Add(new HeroSelect_template(array[i]));
        }
    }
}



/// <summary>
/// HeroSelect_template配置表
/// </summary>
public partial class HeroSelect_template : IReader
{
    /// <summary>
    /// 编号
    /// </summary>
    public int StateID;
    /// <summary>
    /// 英雄名称
    /// </summary>
    public string StateName;
    /// <summary>
    /// 英雄立绘
    /// </summary>
    public string heroPic;
    /// <summary>
    /// 英雄头像
    /// </summary>
    public string heroHeadIcon;
    /// <summary>
    /// 头像背景
    /// </summary>
    public string heroBg;
    /// <summary>
    /// 英雄描述
    /// </summary>
    public string description1;
    /// <summary>
    /// 英雄专长
    /// </summary>
    public string description2;



    public HeroSelect_template() { }
    public HeroSelect_template(string content)
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
        heroPic = array[2];
        heroHeadIcon = array[3];
        heroBg = array[4];
        description1 = array[5];
        description2 = array[6];
    }
}
