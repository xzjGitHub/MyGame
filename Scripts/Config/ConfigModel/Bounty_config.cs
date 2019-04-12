using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Bounty_configConfig配置表
/// </summary>
public partial class Bounty_configConfig : IReader
{
    public List<Bounty_config> _Bounty_config = new List<Bounty_config>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Bounty_config.Add(new Bounty_config(array[i]));
        }
    }
}



/// <summary>
/// Bounty_config配置表
/// </summary>
public partial class Bounty_config : IReader
{
    /// <summary>
    /// 能够获取奖励的声望值
    /// </summary>
    public int rewardRenown;
    /// <summary>
    /// 重置的人情最大值
    /// </summary>
    public int maxFavor;
    /// <summary>
    /// 
    /// </summary>
    public float deviation;



    public Bounty_config() { }
    public Bounty_config(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        rewardRenown = int.Parse(array[0]);
        maxFavor = int.Parse(array[1]);
        deviation = float.Parse(array[2]);
    }
}
