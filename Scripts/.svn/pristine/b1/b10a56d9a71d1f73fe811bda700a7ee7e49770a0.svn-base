using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// WP_setupConfig配置表
/// </summary>
public partial class WP_setupConfig : IReader
{
    public List<WP_setup> _WP_setup = new List<WP_setup>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _WP_setup.Add(new WP_setup(array[i]));
        }
    }
}



/// <summary>
/// WP_setup配置表
/// </summary>
public partial class WP_setup : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int ESID;
    /// <summary>
    /// 
    /// </summary>
    public string 说明;
    /// <summary>
    /// EventSetID
    /// </summary>
    public List<int> EventSet;
    /// <summary>
    /// 
    /// </summary>
    public List<int> selectChance;



    public WP_setup() { }
    public WP_setup(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        ESID = int.Parse(array[0]);
        说明 = array[1];
        //列表EventSet取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        EventSet = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { EventSet.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表selectChance取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        selectChance = new List<int>();
        foreach (var _str in array[3].Split(','))
        {
            try { selectChance.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
