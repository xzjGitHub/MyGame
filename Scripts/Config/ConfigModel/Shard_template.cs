using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// Shard_templateConfig配置表
/// </summary>
public partial class Shard_templateConfig : IReader
{
    public List<Shard_template> _Shard_template = new List<Shard_template>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _Shard_template.Add(new Shard_template(array[i]));
        }
    }
}



/// <summary>
/// Shard_template配置表
/// </summary>
public partial class Shard_template : IReader
{
    /// <summary>
    /// 物品ID
    /// </summary>
    public int instanceID;
    /// <summary>
    /// 核心能量
    /// </summary>
    public int tempCorePower;



    public Shard_template() { }
    public Shard_template(string content)
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
        tempCorePower = int.Parse(array[1]);
    }
}
