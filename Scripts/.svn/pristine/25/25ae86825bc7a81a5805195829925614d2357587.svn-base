using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Bounty_bountySetConfig配置表
/// </summary>
public partial class Bounty_bountySetConfig : IReader
{
    public List<Bounty_bountySet> _Bounty_bountySet = new List<Bounty_bountySet>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Bounty_bountySet.Add(new Bounty_bountySet(array[i]));
        }
    }
}



/// <summary>
/// Bounty_bountySet配置表
/// </summary>
public partial class Bounty_bountySet : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int bountySetID;
    /// <summary>
    /// 
    /// </summary>
    public List<int> bountyList;



    public Bounty_bountySet() { }
    public Bounty_bountySet(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        bountySetID = int.Parse(array[0]);
        //列表bountyList取值
        array[1] = array[1].Replace("[", "").Replace("]", "").Replace(" ","");
        bountyList = new List<int>();
        foreach (var _str in array[1].Split(','))
        {
            try { bountyList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
