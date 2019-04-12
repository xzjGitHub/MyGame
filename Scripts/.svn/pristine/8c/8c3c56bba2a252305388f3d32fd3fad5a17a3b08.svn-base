using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// ObjectEffectConfig配置表
/// </summary>
public partial class ObjectEffectConfig : IReader
{
    public List<ObjectEffect> _ObjectEffect = new List<ObjectEffect>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _ObjectEffect.Add(new ObjectEffect(array[i]));
        }
    }
}



/// <summary>
/// ObjectEffect配置表
/// </summary>
public partial class ObjectEffect : IReader
{
    /// <summary>
    /// 编号
    /// </summary>
    public int effectObjectID;
    /// <summary>
    /// 特效
    /// </summary>
    public string EOEventEffect;
    /// <summary>
    /// 特效播放类型，Case0是普通技能，Case1是吸血等特殊技能（多个，选1个或多个来播放）
    /// </summary>
    public int SECase;
    /// <summary>
    /// 是否简化配置
    /// </summary>
    public int simpleDisplay;
    /// <summary>
    /// 起点位置，我0/敌1
    /// </summary>
    public int castType;
    /// <summary>
    /// 目标位置
    /// </summary>
    public int targetType;
    /// <summary>
    /// 偏移量
    /// </summary>
    public float CSYS_x;
    /// <summary>
    /// 偏移量
    /// </summary>
    public float CSYS_y;
    /// <summary>
    /// 类型否0/是1
    /// </summary>
    public int isBolt;
    /// <summary>
    /// 效果循环，轨迹不循环
    /// </summary>
    public int loop;
    /// <summary>
    /// 直线0/抛物1
    /// </summary>
    public int projectileType;
    /// <summary>
    /// 播放原点-charCenter，onHit，weapon
    /// </summary>
    public string origin;
    /// <summary>
    /// 
    /// </summary>
    public int waitFPS;
    /// <summary>
    /// 
    /// </summary>
    public float moveSpeed;
    /// <summary>
    /// 
    /// </summary>
    public string hitBone;



    public ObjectEffect() { }
    public ObjectEffect(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        effectObjectID = int.Parse(array[0]);
        EOEventEffect = array[1];
        SECase = int.Parse(array[2]);
        simpleDisplay = int.Parse(array[3]);
        castType = int.Parse(array[4]);
        targetType = int.Parse(array[5]);
        CSYS_x = float.Parse(array[6]);
        CSYS_y = float.Parse(array[7]);
        isBolt = int.Parse(array[8]);
        loop = int.Parse(array[9]);
        projectileType = int.Parse(array[10]);
        origin = array[11];
        waitFPS = int.Parse(array[12]);
        moveSpeed = float.Parse(array[13]);
        hitBone = array[14];
    }
}
