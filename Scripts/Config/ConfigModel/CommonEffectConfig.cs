using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// CommonEffectConfigConfig配置表
/// </summary>
public partial class CommonEffectConfigConfig : IReader
{
    public List<CommonEffectConfig> _CommonEffectConfig = new List<CommonEffectConfig>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _CommonEffectConfig.Add(new CommonEffectConfig(array[i]));
        }
    }
}



/// <summary>
/// CommonEffectConfig配置表
/// </summary>
public partial class CommonEffectConfig : IReader
{
    /// <summary>
    /// 编号
    /// </summary>
    public int commonEffectID;
    /// <summary>
    /// 特效名称
    /// </summary>
    public string  commonEffect;
    /// <summary>
    /// 是否循环
    /// </summary>
    public int loop;
    /// <summary>
    /// 是否跟随
    /// </summary>
    public int follow;
    /// <summary>
    /// 播放原点-charCenter、onHit
    /// </summary>
    public string origin;
    /// <summary>
    /// 
    /// </summary>
    public float CSYS_x;
    /// <summary>
    /// 
    /// </summary>
    public float CSYS_y;
    /// <summary>
    /// 
    /// </summary>
    public int SOAmend;



    public CommonEffectConfig() { }
    public CommonEffectConfig(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        commonEffectID = int.Parse(array[0]);
         commonEffect = array[1];
        loop = int.Parse(array[2]);
        follow = int.Parse(array[3]);
        origin = array[4];
        CSYS_x = float.Parse(array[5]);
        CSYS_y = float.Parse(array[6]);
        SOAmend = int.Parse(array[7]);
    }
}
