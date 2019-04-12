using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Perk_templateConfig配置表
/// </summary>
public partial class Perk_templateConfig : IReader
{
    public List<Perk_template> _Perk_template = new List<Perk_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Perk_template.Add(new Perk_template(array[i]));
        }
    }
}



/// <summary>
/// Perk_template配置表
/// </summary>
public partial class Perk_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int perkID;
    /// <summary>
    /// 
    /// </summary>
    public string perkName;
    /// <summary>
    /// 
    /// </summary>
    public string perkNameCN;
    /// <summary>
    /// 
    /// </summary>
    public string perkDescription;
    /// <summary>
    /// 
    /// </summary>
    public int isAddible ;
    /// <summary>
    /// 
    /// </summary>
    public int PPCost;



    public Perk_template() { }
    public Perk_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        perkID = int.Parse(array[0]);
        perkName = array[1];
        perkNameCN = array[2];
        perkDescription = array[3];
        isAddible  = int.Parse(array[4]);
        PPCost = int.Parse(array[5]);
    }
}
