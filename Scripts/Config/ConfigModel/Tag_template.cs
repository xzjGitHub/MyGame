using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Tag_templateConfig配置表
/// </summary>
public partial class Tag_templateConfig : IReader
{
    public List<Tag_template> _Tag_template = new List<Tag_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Tag_template.Add(new Tag_template(array[i]));
        }
    }
}



/// <summary>
/// Tag_template配置表
/// </summary>
public partial class Tag_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int tagID;
    /// <summary>
    /// 
    /// </summary>
    public string tagName;
    /// <summary>
    /// 
    /// </summary>
    public int enchantType;
    /// <summary>
    /// 
    /// </summary>
    public int tagReq;
    /// <summary>
    /// 
    /// </summary>
    public int buildingLevelReq;
    /// <summary>
    /// 
    /// </summary>
    public int replacedByTag;
    /// <summary>
    /// [itemID, itemCount]
    /// </summary>
    public List<int> researchCost;
    /// <summary>
    /// 
    /// </summary>
    public int tempResearchLevel;
    /// <summary>
    /// 
    /// </summary>
    public string tagIcon;
    /// <summary>
    /// 
    /// </summary>
    public string tagDescription;
    /// <summary>
    /// 
    /// </summary>
    public int tagPosition;



    public Tag_template() { }
    public Tag_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        tagID = int.Parse(array[0]);
        tagName = array[1];
        enchantType = int.Parse(array[2]);
        tagReq = int.Parse(array[3]);
        buildingLevelReq = int.Parse(array[4]);
        replacedByTag = int.Parse(array[5]);
        //列表researchCost取值
        array[6] = array[6].Replace("[", "").Replace("]", "").Replace(" ","");
        researchCost = new List<int>();
        foreach (var _str in array[6].Split(','))
        {
            try { researchCost.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        tempResearchLevel = int.Parse(array[7]);
        tagIcon = array[8];
        tagDescription = array[9];
        tagPosition = int.Parse(array[10]);
    }
}
