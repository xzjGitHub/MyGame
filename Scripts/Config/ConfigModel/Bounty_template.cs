using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Bounty_templateConfig配置表
/// </summary>
public partial class Bounty_templateConfig : IReader
{
    public List<Bounty_template> _Bounty_template = new List<Bounty_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Bounty_template.Add(new Bounty_template(array[i]));
        }
    }
}



/// <summary>
/// Bounty_template配置表
/// </summary>
public partial class Bounty_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int bountyID;
    /// <summary>
    /// 
    /// </summary>
    public string bountyName;
    /// <summary>
    /// 
    /// </summary>
    public string bountyDescription;
    /// <summary>
    /// 
    /// </summary>
    public int bountyLevel;
    /// <summary>
    /// 
    /// </summary>
    public int coreLevelReq;
    /// <summary>
    /// 
    /// </summary>
    public List<int> bountyReq;
    /// <summary>
    /// 
    /// </summary>
    public int timeReq;
    /// <summary>
    /// 
    /// </summary>
    public List<int> fortReq;
    /// <summary>
    /// 
    /// </summary>
    public List<int> nextBounty;
    /// <summary>
    /// MapID
    /// </summary>
    public int bountyLocation;
    /// <summary>
    /// 关联事件
    /// </summary>
    public List<int> relatedEvent;
    /// <summary>
    /// 触发对话
    /// </summary>
    public int triggerDialog;
    /// <summary>
    /// 1=事件完成，2=物品拾取，3=路点完成，4=占领堡垒，ID，数量
    /// </summary>
    public   List<List<int>> bountyTarget;
    /// <summary>
    /// 
    /// </summary>
    public int baseGoldReward;
    /// <summary>
    /// 
    /// </summary>
    public int baseTokenReward;
    /// <summary>
    /// 阵营factionType，rewardValue
    /// </summary>
    public   List<List<int>> renownReward;
    /// <summary>
    /// 
    /// </summary>
    public   List<List<int>> itemReward ;



    public Bounty_template() { }
    public Bounty_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        bountyID = int.Parse(array[0]);
        bountyName = array[1];
        bountyDescription = array[2];
        bountyLevel = int.Parse(array[3]);
        coreLevelReq = int.Parse(array[4]);
        //列表bountyReq取值
        array[5] = array[5].Replace("[", "").Replace("]", "").Replace(" ","");
        bountyReq = new List<int>();
        foreach (var _str in array[5].Split(','))
        {
            try { bountyReq.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        timeReq = int.Parse(array[6]);
        //列表fortReq取值
        array[7] = array[7].Replace("[", "").Replace("]", "").Replace(" ","");
        fortReq = new List<int>();
        foreach (var _str in array[7].Split(','))
        {
            try { fortReq.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表nextBounty取值
        array[8] = array[8].Replace("[", "").Replace("]", "").Replace(" ","");
        nextBounty = new List<int>();
        foreach (var _str in array[8].Split(','))
        {
            try { nextBounty.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        bountyLocation = int.Parse(array[9]);
        //列表relatedEvent取值
        array[10] = array[10].Replace("[", "").Replace("]", "").Replace(" ","");
        relatedEvent = new List<int>();
        foreach (var _str in array[10].Split(','))
        {
            try { relatedEvent.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        triggerDialog = int.Parse(array[11]);
        //列表bountyTarget取值
        array[12] = array[12].Replace("[", "").Replace("]", "").Replace(" ","");
        bountyTarget = new   List<List<int>>();
        foreach (var str in array[12].Split('-'))
        {
            try 
            {
                bountyTarget.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        baseGoldReward = int.Parse(array[13]);
        baseTokenReward = int.Parse(array[14]);
        //列表renownReward取值
        array[15] = array[15].Replace("[", "").Replace("]", "").Replace(" ","");
        renownReward = new   List<List<int>>();
        foreach (var str in array[15].Split('-'))
        {
            try 
            {
                renownReward.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表itemReward 取值
        array[16] = array[16].Replace("[", "").Replace("]", "").Replace(" ","");
        itemReward  = new   List<List<int>>();
        foreach (var str in array[16].Split('-'))
        {
            try 
            {
                itemReward .Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
    }
}
