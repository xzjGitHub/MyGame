using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Text_templateConfig配置表
/// </summary>
public partial class Text_templateConfig : IReader
{
    public List<Text_template> _Text_template = new List<Text_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Text_template.Add(new Text_template(array[i]));
        }
    }
}



/// <summary>
/// Text_template配置表
/// </summary>
public partial class Text_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int textID;
    /// <summary>
    /// 
    /// </summary>
    public string title;
    /// <summary>
    /// 
    /// </summary>
    public int textType;
    /// <summary>
    /// 
    /// </summary>
    public string charIcon;
    /// <summary>
    /// 
    /// </summary>
    public int position;
    /// <summary>
    /// 
    /// </summary>
    public string text;



    public Text_template() { }
    public Text_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        textID = int.Parse(array[0]);
        title = array[1];
        textType = int.Parse(array[2]);
        charIcon = array[3];
        position = int.Parse(array[4]);
        text = array[5];
    }
}
