using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// ActionEffectConfig配置表
/// </summary>
public partial class ActionEffectConfig : IReader
{
    public List<ActionEffect> _ActionEffect = new List<ActionEffect>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _ActionEffect.Add(new ActionEffect(array[i]));
        }
    }
}



/// <summary>
/// ActionEffect配置表
/// </summary>
public partial class ActionEffect : IReader
{
    /// <summary>
    /// 目标集合ID
    /// </summary>
    public int targetSetID;
    /// <summary>
    /// 特效播放类型，Case0是普通技能，Case1是吸血等特殊技能（多个，选1个或多个来播放）
    /// </summary>
    public int SECase;
    /// <summary>
    /// 是否简化配置
    /// </summary>
    public int simpleDisplay;
    /// <summary>
    /// 特效
    /// </summary>
    public string skillEventEffect;
    /// <summary>
    /// 播放原点-charCenter，onHit，weapon
    /// </summary>
    public string origin;



    public ActionEffect() { }
    public ActionEffect(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        targetSetID = int.Parse(array[0]);
        SECase = int.Parse(array[1]);
        simpleDisplay = int.Parse(array[2]);
        skillEventEffect = array[3];
        origin = array[4];
    }
}
