using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// EffectPlayTypeConfig配置表
/// </summary>
public partial class EffectPlayTypeConfig : IReader
{
    public List<EffectPlayType> _EffectPlayType = new List<EffectPlayType>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _EffectPlayType.Add(new EffectPlayType(array[i]));
        }
    }
}



/// <summary>
/// EffectPlayType配置表
/// </summary>
public partial class EffectPlayType : IReader
{
    /// <summary>
    /// 播放类型
    /// </summary>
    public int playType;
    /// <summary>
    /// 播放原点-charCenter、onHit
    /// </summary>
    public string origin;
    /// <summary>
    /// 偏移量
    /// </summary>
    public List<float> CSYS;
    /// <summary>
    /// 是否循环
    /// </summary>
    public int loop;
    /// <summary>
    /// 是否跟随
    /// </summary>
    public int follow;
    /// <summary>
    /// 直线0/抛物1
    /// </summary>
    public int projectileType;
    /// <summary>
    /// 类型否0/是1
    /// </summary>
    public int isBolt;



    public EffectPlayType() { }
    public EffectPlayType(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        playType = int.Parse(array[0]);
        origin = array[1];
        //列表CSYS取值
        array[2] = array[2].Replace("[", "").Replace("]", "").Replace(" ","");
        CSYS = new List<float>();
        foreach (var _str in array[2].Split(','))
        {
            try { CSYS.Add(float.Parse(_str)); }
            catch (Exception) { }
        }
        loop = int.Parse(array[3]);
        follow = int.Parse(array[4]);
        projectileType = int.Parse(array[5]);
        isBolt = int.Parse(array[6]);
    }
}
