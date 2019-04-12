using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Forge_templateConfig配置表
/// </summary>
public partial class Forge_templateConfig : IReader
{
    public List<Forge_template> _Forge_template = new List<Forge_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Forge_template.Add(new Forge_template(array[i]));
        }
    }
}



/// <summary>
/// Forge_template配置表
/// </summary>
public partial class Forge_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int formulaID;
    /// <summary>
    /// 
    /// </summary>
    public int instance;
    /// <summary>
    /// 
    /// </summary>
    public int materialCost;
    /// <summary>
    /// 
    /// </summary>
    public int partsCost;
    /// <summary>
    /// 
    /// </summary>
    public int goldCost;
    /// <summary>
    /// 
    /// </summary>
    public int manaCost;
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



    public Forge_template() { }
    public Forge_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        formulaID = int.Parse(array[0]);
        instance = int.Parse(array[1]);
        materialCost = int.Parse(array[2]);
        partsCost = int.Parse(array[3]);
        goldCost = int.Parse(array[4]);
        manaCost = int.Parse(array[5]);
        //列表rndAttribute1取值
        array[6] = array[6].Replace("[", "").Replace("]", "").Replace(" ","");
        rndAttribute1 = array[6] != String.Empty ? array[6].Split(',').ToList() : new List<string>();
        //列表rndAttribute2取值
        array[7] = array[7].Replace("[", "").Replace("]", "").Replace(" ","");
        rndAttribute2 = array[7] != String.Empty ? array[7].Split(',').ToList() : new List<string>();
        //列表rndAttribute3取值
        array[8] = array[8].Replace("[", "").Replace("]", "").Replace(" ","");
        rndAttribute3 = array[8] != String.Empty ? array[8].Split(',').ToList() : new List<string>();
    }
}
