using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Forge_configConfig配置表
/// </summary>
public partial class Forge_configConfig : IReader
{
    public List<Forge_config> _Forge_config = new List<Forge_config>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Forge_config.Add(new Forge_config(array[i]));
        }
    }
}



/// <summary>
/// Forge_config配置表
/// </summary>
public partial class Forge_config : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int forgeTypeID;
    /// <summary>
    /// 
    /// </summary>
    public int forgeType;
    /// <summary>
    /// 
    /// </summary>
    public string forgeIcon;
    /// <summary>
    /// 
    /// </summary>
    public string typeName;
    /// <summary>
    /// 
    /// </summary>
    public int ERType;
    /// <summary>
    /// 
    /// </summary>
    public string typeIcon;
    /// <summary>
    /// 
    /// </summary>
    public List<int> materialType;
    /// <summary>
    /// 
    /// </summary>
    public List<int> partsType;



    public Forge_config() { }
    public Forge_config(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        forgeTypeID = int.Parse(array[0]);
        forgeType = int.Parse(array[1]);
        forgeIcon = array[2];
        typeName = array[3];
        ERType = int.Parse(array[4]);
        typeIcon = array[5];
        //列表materialType取值
        array[6] = array[6].Replace("[", "").Replace("]", "").Replace(" ","");
        materialType = new List<int>();
        foreach (var _str in array[6].Split(','))
        {
            try { materialType.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表partsType取值
        array[7] = array[7].Replace("[", "").Replace("]", "").Replace(" ","");
        partsType = new List<int>();
        foreach (var _str in array[7].Split(','))
        {
            try { partsType.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
