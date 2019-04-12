using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// BuildingControlConfig配置表
/// </summary>
public partial class BuildingControlConfig : IReader
{
    public List<BuildingControl> _BuildingControl = new List<BuildingControl>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _BuildingControl.Add(new BuildingControl(array[i]));
        }
    }
}



/// <summary>
/// BuildingControl配置表
/// </summary>
public partial class BuildingControl : IReader
{
    /// <summary>
    /// 编号
    /// </summary>
    public int ID;
    /// <summary>
    /// 名称
    /// </summary>
    public string name;
    /// <summary>
    /// 图标
    /// </summary>
    public string icon;
    /// <summary>
    /// 类型
    /// </summary>
    public int type;
    /// <summary>
    /// 组别
    /// </summary>
    public int group;



    public BuildingControl() { }
    public BuildingControl(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        ID = int.Parse(array[0]);
        name = array[1];
        icon = array[2];
        type = int.Parse(array[3]);
        group = int.Parse(array[4]);
    }
}
