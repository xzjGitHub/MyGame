using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Effect_templateConfig配置表
/// </summary>
public partial class Effect_templateConfig : IReader
{
    public List<Effect_template> _Effect_template = new List<Effect_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Effect_template.Add(new Effect_template(array[i]));
        }
    }
}



/// <summary>
/// Effect_template配置表
/// </summary>
public partial class Effect_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int effectID;
    /// <summary>
    /// 
    /// </summary>
    public float shieldMod;
    /// <summary>
    /// 
    /// </summary>
    public float armorMod;



    public Effect_template() { }
    public Effect_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        effectID = int.Parse(array[0]);
        shieldMod = float.Parse(array[1]);
        armorMod = float.Parse(array[2]);
    }
}
