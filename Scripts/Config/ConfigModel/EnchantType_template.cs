using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// EnchantType_templateConfig配置表
/// </summary>
public partial class EnchantType_templateConfig : IReader
{
    public List<EnchantType_template> _EnchantType_template = new List<EnchantType_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _EnchantType_template.Add(new EnchantType_template(array[i]));
        }
    }
}



/// <summary>
/// EnchantType_template配置表
/// </summary>
public partial class EnchantType_template : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int enchantTypeID;
    /// <summary>
    /// 
    /// </summary>
    public int baseResearchLevel;
    /// <summary>
    /// 
    /// </summary>
    public List<int> initialTagList;
    /// <summary>
    /// 
    /// </summary>
    public List<int> fullTagList;



    public EnchantType_template() { }
    public EnchantType_template(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        enchantTypeID = int.Parse(array[0]);
        baseResearchLevel = int.Parse(array[1]);
        //列表initialTagList取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        initialTagList = new List<int>();
        foreach (var _str in array[2].Split(','))
        {
            try { initialTagList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
        //列表fullTagList取值
        array[3] = array[3].Replace("[", "").Replace("]", "").Replace(" ","");
        fullTagList = new List<int>();
        foreach (var _str in array[3].Split(','))
        {
            try { fullTagList.Add(int.Parse(_str)); }
            catch (Exception) { }
        }
    }
}
