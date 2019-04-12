using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Parts_templateConfig配置表
/// </summary>
public partial class Parts_templateConfig : IReader
{
    public List<Parts_template> _Parts_template = new List<Parts_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Parts_template.Add(new Parts_template(array[i]));
        }
    }
}



/// <summary>
/// Parts_template配置表
/// </summary>
public partial class Parts_template : IReader
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
    public int partsType;
    /// <summary>
    /// 
    /// </summary>
    public int rndItemLevel;
    /// <summary>
    /// 最小强化加值
    /// </summary>
    public int addMinUpgrade;
    /// <summary>
    /// 随机属性
    /// </summary>
    public List<string> rndAttribute1;
    /// <summary>
    /// 配件支持的最大强化等级
    /// </summary>
    public int maxUpgrade;
    /// <summary>
    /// 
    /// </summary>
    public List<float> addMaterialLevel;
    /// <summary>
    /// 
    /// </summary>
    public int partsRank;
    /// <summary>
    /// 
    /// </summary>
    public List<string> rndAttribute2;
    /// <summary>
    /// 
    /// </summary>
    public List<string> rndAttribute3;
    /// <summary>
    /// [minValue, maxValue]
    /// </summary>
    public List<float> addEND;
    /// <summary>
    /// 
    /// </summary>
    public List<float> addSHI;
    /// <summary>
    /// 
    /// </summary>
    public List<float> addARM;
    /// <summary>
    /// 
    /// </summary>
    public List<float> addBLO;
    /// <summary>
    /// 
    /// </summary>
    public List<float> addAP;
    /// <summary>
    /// 
    /// </summary>
    public List<float> addPRE;
    /// <summary>
    /// 
    /// </summary>
    public List<float> addCRT;



    public Parts_template() { }
    public Parts_template(string content)
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
        partsType = int.Parse(array[2]);
        rndItemLevel = int.Parse(array[3]);
        addMinUpgrade = int.Parse(array[4]);
        //列表rndAttribute1取值
        array[5] = array[5].Replace("[", "").Replace("]", "").Replace(" ","");
        rndAttribute1 = array[5] != String.Empty ? array[5].Split(',').ToList() : new List<string>();
        maxUpgrade = int.Parse(array[6]);
        //列表addMaterialLevel取值
        array[7] = array[7].Replace("[", "").Replace("]", "").Replace(" ","");
        addMaterialLevel = new List<float>();
        foreach (var _str in array[7].Split(','))
        {
            try { addMaterialLevel.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        partsRank = int.Parse(array[8]);
        //列表rndAttribute2取值
        array[9] = array[9].Replace("[", "").Replace("]", "").Replace(" ","");
        rndAttribute2 = array[9] != String.Empty ? array[9].Split(',').ToList() : new List<string>();
        //列表rndAttribute3取值
        array[10] = array[10].Replace("[", "").Replace("]", "").Replace(" ","");
        rndAttribute3 = array[10] != String.Empty ? array[10].Split(',').ToList() : new List<string>();
        //列表addEND取值
        array[11] = array[11].Replace("[", "").Replace("]", "").Replace(" ","");
        addEND = new List<float>();
        foreach (var _str in array[11].Split(','))
        {
            try { addEND.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        //列表addSHI取值
        array[12] = array[12].Replace("[", "").Replace("]", "").Replace(" ","");
        addSHI = new List<float>();
        foreach (var _str in array[12].Split(','))
        {
            try { addSHI.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        //列表addARM取值
        array[13] = array[13].Replace("[", "").Replace("]", "").Replace(" ","");
        addARM = new List<float>();
        foreach (var _str in array[13].Split(','))
        {
            try { addARM.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        //列表addBLO取值
        array[14] = array[14].Replace("[", "").Replace("]", "").Replace(" ","");
        addBLO = new List<float>();
        foreach (var _str in array[14].Split(','))
        {
            try { addBLO.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        //列表addAP取值
        array[15] = array[15].Replace("[", "").Replace("]", "").Replace(" ","");
        addAP = new List<float>();
        foreach (var _str in array[15].Split(','))
        {
            try { addAP.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        //列表addPRE取值
        array[16] = array[16].Replace("[", "").Replace("]", "").Replace(" ","");
        addPRE = new List<float>();
        foreach (var _str in array[16].Split(','))
        {
            try { addPRE.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        //列表addCRT取值
        array[17] = array[17].Replace("[", "").Replace("]", "").Replace(" ","");
        addCRT = new List<float>();
        foreach (var _str in array[17].Split(','))
        {
            try { addCRT.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
