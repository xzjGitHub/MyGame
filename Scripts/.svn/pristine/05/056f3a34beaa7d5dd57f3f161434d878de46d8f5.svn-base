using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Dialog_templateConfig配置表
/// </summary>
public partial class Dialog_templateConfig : IReader
{
    public List<Dialog_template> _Dialog_template = new List<Dialog_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Dialog_template.Add(new Dialog_template(array[i]));
        }
    }
}



/// <summary>
/// Dialog_template配置表
/// </summary>
public partial class Dialog_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int dialogID;
    /// <summary>
    /// 任务需求
    /// </summary>
    public int bountyReq;
    /// <summary>
    /// 
    /// </summary>
    public List<int> textList;



    public Dialog_template() { }
    public Dialog_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        dialogID = int.Parse(array[0]);
        bountyReq = int.Parse(array[1]);
        //列表textList取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        textList = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { textList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
