using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Mob_timelineConfig配置表
/// </summary>
public partial class Mob_timelineConfig : IReader
{
    public List<Mob_timeline> _Mob_timeline = new List<Mob_timeline>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Mob_timeline.Add(new Mob_timeline(array[i]));
        }
    }
}



/// <summary>
/// Mob_timeline配置表
/// </summary>
public partial class Mob_timeline : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int  timeLineID;
    /// <summary>
    /// [skillSet]
    /// </summary>
    public   List<List<int>> round1;
    /// <summary>
    /// 
    /// </summary>
    public   List<List<int>> round2;
    /// <summary>
    /// 
    /// </summary>
    public   List<List<int>> round3;
    /// <summary>
    /// 
    /// </summary>
    public   List<List<int>> round4;
    /// <summary>
    /// 
    /// </summary>
    public   List<List<int>> round5;
    /// <summary>
    /// 
    /// </summary>
    public   List<List<int>> round6;
    /// <summary>
    /// 
    /// </summary>
    public   List<List<int>> round7;
    /// <summary>
    /// 
    /// </summary>
    public   List<List<int>> round8;
    /// <summary>
    /// 
    /// </summary>
    public   List<List<int>> round9;
    /// <summary>
    /// 
    /// </summary>
    public   List<List<int>> round10;
    /// <summary>
    /// 
    /// </summary>
    public   List<List<int>> round11;
    /// <summary>
    /// 
    /// </summary>
    public   List<List<int>> round12;



    public Mob_timeline() { }
    public Mob_timeline(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
         timeLineID = int.Parse(array[0]);
        //列表round1取值
        array[1] = array[1].Replace("[", "").Replace("]", "").Replace(" ","");
        round1 = new   List<List<int>>();
        foreach (var str in array[1].Split('-'))
        {
            try 
            {
                round1.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表round2取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        round2 = new   List<List<int>>();
        foreach (var str in array[2].Split('-'))
        {
            try 
            {
                round2.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表round3取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        round3 = new   List<List<int>>();
        foreach (var str in array[3].Split('-'))
        {
            try 
            {
                round3.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表round4取值
        array[4] = array[4].Replace("[", "").Replace("]", "").Replace(" ","");
        round4 = new   List<List<int>>();
        foreach (var str in array[4].Split('-'))
        {
            try 
            {
                round4.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表round5取值
        array[5] = array[5].Replace("[", "").Replace("]", "").Replace(" ","");
        round5 = new   List<List<int>>();
        foreach (var str in array[5].Split('-'))
        {
            try 
            {
                round5.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表round6取值
        array[6] = array[6].Replace("[", "").Replace("]", "").Replace(" ","");
        round6 = new   List<List<int>>();
        foreach (var str in array[6].Split('-'))
        {
            try 
            {
                round6.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表round7取值
        array[7] = array[7].Replace("[", "").Replace("]", "").Replace(" ","");
        round7 = new   List<List<int>>();
        foreach (var str in array[7].Split('-'))
        {
            try 
            {
                round7.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表round8取值
        array[8] = array[8].Replace("[", "").Replace("]", "").Replace(" ","");
        round8 = new   List<List<int>>();
        foreach (var str in array[8].Split('-'))
        {
            try 
            {
                round8.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表round9取值
        array[9] = array[9].Replace("[", "").Replace("]", "").Replace(" ","");
        round9 = new   List<List<int>>();
        foreach (var str in array[9].Split('-'))
        {
            try 
            {
                round9.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表round10取值
        array[10] = array[10].Replace("[", "").Replace("]", "").Replace(" ","");
        round10 = new   List<List<int>>();
        foreach (var str in array[10].Split('-'))
        {
            try 
            {
                round10.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表round11取值
        array[11] = array[11].Replace("[", "").Replace("]", "").Replace(" ","");
        round11 = new   List<List<int>>();
        foreach (var str in array[11].Split('-'))
        {
            try 
            {
                round11.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
        //列表round12取值
        array[12] = array[12].Replace("[", "").Replace("]", "").Replace(" ","");
        round12 = new   List<List<int>>();
        foreach (var str in array[12].Split('-'))
        {
            try 
            {
                round12.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception) { }
        }
    }
}
