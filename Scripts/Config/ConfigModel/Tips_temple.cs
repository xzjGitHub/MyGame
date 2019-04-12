using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Tips_templeConfig配置表
/// </summary>
public partial class Tips_templeConfig : IReader
{
    public List<Tips_temple> _Tips_temple = new List<Tips_temple>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Tips_temple.Add(new Tips_temple(array[i]));
        }
    }
}



/// <summary>
/// Tips_temple配置表
/// </summary>
public partial class Tips_temple : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int tipID;
    /// <summary>
    /// 
    /// </summary>
    public string tipText;



    public Tips_temple() { }
    public Tips_temple(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        tipID = int.Parse(array[0]);
        tipText = array[1];
    }
}
