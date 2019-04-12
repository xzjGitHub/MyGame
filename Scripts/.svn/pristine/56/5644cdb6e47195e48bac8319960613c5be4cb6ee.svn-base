using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// StateEffectConfigConfig配置表
/// </summary>
public partial class StateEffectConfigConfig : IReader
{
    public List<StateEffectConfig> _StateEffectConfig = new List<StateEffectConfig>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _StateEffectConfig.Add(new StateEffectConfig(array[i]));
        }
    }
}



/// <summary>
/// StateEffectConfig配置表
/// </summary>
public partial class StateEffectConfig : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int stateEffectID;
    /// <summary>
    /// 命中动作
    /// </summary>
    public string action_state;
    /// <summary>
    /// 
    /// </summary>
    public string stateEffect;
    /// <summary>
    /// 类型
    /// </summary>
    public int textType;
    /// <summary>
    /// 
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



    public StateEffectConfig() { }
    public StateEffectConfig(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        stateEffectID = int.Parse(array[0]);
        action_state = array[1];
        stateEffect = array[2];
        textType = int.Parse(array[3]);
        origin = array[4];
        CSYS_x = float.Parse(array[5]);
        CSYS_y = float.Parse(array[6]);
        SOAmend = int.Parse(array[7]);
    }
}
