using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Building_templateConfig配置表
/// </summary>
public partial class Building_templateConfig : IReader
{
    public List<Building_template> _Building_template = new List<Building_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Building_template.Add(new Building_template(array[i]));
        }
    }
}



/// <summary>
/// Building_template配置表
/// </summary>
public partial class Building_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int templateID;
    /// <summary>
    /// 
    /// </summary>
    public string buildingName;
    /// <summary>
    /// 
    /// </summary>
    public string buildingInfo;
    /// <summary>
    /// 
    /// </summary>
    public int buildingType;
    /// <summary>
    /// 
    /// </summary>
    public int productionCycle;



    public Building_template() { }
    public Building_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        templateID = int.Parse(array[0]);
        buildingName = array[1];
        buildingInfo = array[2];
        buildingType = int.Parse(array[3]);
        productionCycle = int.Parse(array[4]);
    }
}
