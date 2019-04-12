using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// PerkSet_listConfig配置表
/// </summary>
public partial class PerkSet_listConfig : IReader
{
    public List<PerkSet_list> _PerkSet_list = new List<PerkSet_list>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _PerkSet_list.Add(new PerkSet_list(array[i]));
        }
    }
}



/// <summary>
/// PerkSet_list配置表
/// </summary>
public partial class PerkSet_list : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int PerkSetID;
    /// <summary>
    /// 
    /// </summary>
    public List<int> PerkList;



    public PerkSet_list() { }
    public PerkSet_list(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        PerkSetID = int.Parse(array[0]);
        //列表PerkList取值
        array[1] = array[1].Replace("[", "").Replace("]", "").Replace(" ","");
        PerkList = new List<int>();
        foreach (var _str in array[1].Split(','))
        {
            try { PerkList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
