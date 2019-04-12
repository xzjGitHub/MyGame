using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Token_costConfig配置表
/// </summary>
public partial class Token_costConfig : IReader
{
    public List<Token_cost> _Token_cost = new List<Token_cost>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Token_cost.Add(new Token_cost(array[i]));
        }
    }
}



/// <summary>
/// Token_cost配置表
/// </summary>
public partial class Token_cost : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int endCharCDcost ;
    /// <summary>
    /// 角色训练消耗
    /// </summary>
    public int instantTraining;
    /// <summary>
    /// 代币训练经验
    /// </summary>
    public int tokenExpReward;
    /// <summary>
    /// 续命消耗
    /// </summary>
    public int xuMing;
    /// <summary>
    /// 携款消耗
    /// </summary>
    public int xieKuan;
    /// <summary>
    /// 复活消耗道具
    /// </summary>
    public int reviveItem;
    /// <summary>
    /// 全队复活消耗的道具数
    /// </summary>
    public int teamReviveItemCost;
    /// <summary>
    /// 复活物品消耗
    /// </summary>
    public int reviveTokenCost;
    /// <summary>
    /// 入侵复活消耗道具
    /// </summary>
    public int regeneItem;
    /// <summary>
    /// 入侵全体复活代币消耗
    /// </summary>
    public int invasionTRTokenCost;
    /// <summary>
    /// 重置特性点
    /// </summary>
    public int resetPerk;



    public Token_cost() { }
    public Token_cost(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        endCharCDcost  = int.Parse(array[0]);
        instantTraining = int.Parse(array[1]);
        tokenExpReward = int.Parse(array[2]);
        xuMing = int.Parse(array[3]);
        xieKuan = int.Parse(array[4]);
        reviveItem = int.Parse(array[5]);
        teamReviveItemCost = int.Parse(array[6]);
        reviveTokenCost = int.Parse(array[7]);
        regeneItem = int.Parse(array[8]);
        invasionTRTokenCost = int.Parse(array[9]);
        resetPerk = int.Parse(array[10]);
    }
}
