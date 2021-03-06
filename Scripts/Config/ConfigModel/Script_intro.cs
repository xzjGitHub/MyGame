using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Script_introConfig配置表
/// </summary>
public partial class Script_introConfig : IReader
{
    public List<Script_intro> _Script_intro = new List<Script_intro>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Script_intro.Add(new Script_intro(array[i]));
        }
    }
}



/// <summary>
/// Script_intro配置表
/// </summary>
public partial class Script_intro : IReader
{
    /// <summary>
    /// 序章标题
    /// </summary>
    public int introText1;
    /// <summary>
    /// 妙妙位置旁白
    /// </summary>
    public int introText2;
    /// <summary>
    /// 黑屏中的过渡文字
    /// </summary>
    public List<int> introText3;
    /// <summary>
    /// 黑屏中的角色对话，为空则不显示
    /// </summary>
    public int introDialog1;
    /// <summary>
    /// 
    /// </summary>
    public int introBounty;
    /// <summary>
    /// 入城的文本
    /// </summary>
    public List<int> introText4;
    /// <summary>
    /// 入城
    /// </summary>
    public int introDialog2;



    public Script_intro() { }
    public Script_intro(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        introText1 = int.Parse(array[0]);
        introText2 = int.Parse(array[1]);
        //列表introText3取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        introText3 = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { introText3.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        introDialog1 = int.Parse(array[3]);
        introBounty = int.Parse(array[4]);
        //列表introText4取值
        array[5] = array[5].Replace("[", "").Replace("]", "").Replace(" ","");
        introText4 = new List<int>();
        foreach (var _str in array[5].Split(','))
        {
            try { introText4.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        introDialog2 = int.Parse(array[6]);
    }
}
