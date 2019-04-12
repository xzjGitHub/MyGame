using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Material_templateConfig配置表
/// </summary>
public partial class Material_templateConfig : IReader
{
    public List<Material_template> _Material_template = new List<Material_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Material_template.Add(new Material_template(array[i]));
        }
    }
}



/// <summary>
/// Material_template配置表
/// </summary>
public partial class Material_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int instanceID;
    /// <summary>
    /// 
    /// </summary>
    public string preffix;
    /// <summary>
    /// 
    /// </summary>
    public int materialType;
    /// <summary>
    /// 制造材料最大支持的物品等级
    /// </summary>
    public int maxItemLevel;
    /// <summary>
    /// 最大强化加值
    /// </summary>
    public int addMaxUpgrade;
    /// <summary>
    /// 
    /// </summary>
    public int charLevelReq;
    /// <summary>
    /// 
    /// </summary>
    public float materialLevel;
    /// <summary>
    /// 
    /// </summary>
    public int materialRank;
    /// <summary>
    /// 
    /// </summary>
    public List<string> rndAttribute1;
    /// <summary>
    /// 
    /// </summary>
    public List<string> rndAttribute2;
    /// <summary>
    /// 
    /// </summary>
    public List<string> rndAttribute3;
    /// <summary>
    /// [最小值加值，最大值加值]
    /// </summary>
    public List<int> addItemLevel;



    public Material_template() { }
    public Material_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        instanceID = int.Parse(array[0]);
        preffix = array[1];
        materialType = int.Parse(array[2]);
        maxItemLevel = int.Parse(array[3]);
        addMaxUpgrade = int.Parse(array[4]);
        charLevelReq = int.Parse(array[5]);
        materialLevel = float.Parse(array[6]);
        materialRank = int.Parse(array[7]);
        //列表rndAttribute1取值
        array[8] = array[8].Replace("[", "").Replace("]", "").Replace(" ","");
        rndAttribute1 = array[8] != String.Empty ? array[8].Split(',').ToList() : new List<string>();
        //列表rndAttribute2取值
        array[9] = array[9].Replace("[", "").Replace("]", "").Replace(" ","");
        rndAttribute2 = array[9] != String.Empty ? array[9].Split(',').ToList() : new List<string>();
        //列表rndAttribute3取值
        array[10] = array[10].Replace("[", "").Replace("]", "").Replace(" ","");
        rndAttribute3 = array[10] != String.Empty ? array[10].Split(',').ToList() : new List<string>();
        //列表addItemLevel取值
        array[11] = array[11].Replace("[", "").Replace("]", "").Replace(" ","");
        addItemLevel = new List<int>();
        foreach (var _str in array[11].Split(','))
        {
            try { addItemLevel.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
