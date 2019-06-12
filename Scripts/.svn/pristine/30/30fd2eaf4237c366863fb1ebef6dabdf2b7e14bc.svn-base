using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Script_storyLineConfig配置表
/// </summary>
public partial class Script_storyLineConfig : IReader
{
    public List<Script_storyLine> _Script_storyLine = new List<Script_storyLine>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Script_storyLine.Add(new Script_storyLine(array[i]));
        }
    }
}



/// <summary>
/// Script_storyLine配置表
/// </summary>
public partial class Script_storyLine : IReader
{
    /// <summary>
    /// 流程ID
    /// </summary>
    public int storyLineID;
    /// <summary>
    /// 
    /// </summary>
    public int uid;
    /// <summary>
    /// 
    /// </summary>
    public List<int> dialogID;
    /// <summary>
    /// 
    /// </summary>
    public int bountyReq;



    public Script_storyLine() { }
    public Script_storyLine(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        storyLineID = int.Parse(array[0]);
        uid = int.Parse(array[1]);
        //列表dialogID取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        dialogID = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { dialogID.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        bountyReq = int.Parse(array[3]);
    }
}
