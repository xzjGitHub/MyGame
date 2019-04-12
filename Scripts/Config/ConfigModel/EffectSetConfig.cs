using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// EffectSetConfigConfig配置表
/// </summary>
public partial class EffectSetConfigConfig : IReader
{
    public List<EffectSetConfig> _EffectSetConfig = new List<EffectSetConfig>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _EffectSetConfig.Add(new EffectSetConfig(array[i]));
        }
    }
}



/// <summary>
/// EffectSetConfig配置表
/// </summary>
public partial class EffectSetConfig : IReader
{
    /// <summary>
    /// 动作特效集ID
    /// </summary>
    public int effectSetID;
    /// <summary>
    /// 事件1
    /// </summary>
    public   List<List<int>> event1;
    /// <summary>
    /// 事件2
    /// </summary>
    public   List<List<int>> event2;
    /// <summary>
    /// 事件3
    /// </summary>
    public   List<List<int>> event3;



    public EffectSetConfig() { }
    public EffectSetConfig(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        effectSetID = int.Parse(array[0]);
        //列表event1取值
        array[1] = array[1].Replace("[", "").Replace("]", "").Replace(" ","");
        event1 = new   List<List<int>>();
        foreach (var str in array[1].Split('-'))
        {
            try 
            {
                event1.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表event2取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        event2 = new   List<List<int>>();
        foreach (var str in array[2].Split('-'))
        {
            try 
            {
                event2.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表event3取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        event3 = new   List<List<int>>();
        foreach (var str in array[3].Split('-'))
        {
            try 
            {
                event3.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
    }
}
