using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;


public partial class RandomMapConfigConfig : IReader
{
    public List<RandomMapConfig> _randomMapConfigs = new List<RandomMapConfig>();

    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 0; i < array.Length; i++)
        {
            _randomMapConfigs.Add(new RandomMapConfig(array[i]));
        }
    }


}

/// <summary>
/// Item_rewardlist配置表
/// </summary>
public partial class RandomMapConfig : IReader
{
    /// <summary>
    /// 地图iD
    /// </summary>
    public int mapID;

    /// <summary>
    /// 路点ID
    /// </summary>
    public int WPId;

    /// <summary>
    /// 索引
    /// </summary>
    public int index;

    /// <summary>
    /// 行
    /// </summary>
    public int row;

    /// <summary>
    /// 列
    /// </summary>
    public int column;

    /// <summary>
    /// 行总数
    /// </summary>
    public int rowSum;

    /// <summary>
    /// 列总数
    /// </summary>
    public int columnSum;

    /// <summary>
    /// 父点信息
    /// </summary>
    public List<List<int>> parentInfos;


    public RandomMapConfig()
    {
    }

    public RandomMapConfig(string content)
    {
        Reader(content);
    }

    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        try
        {
            mapID = int.Parse(array[0]);
            WPId = int.Parse(array[1]);
            index = int.Parse(array[2]);
            row = int.Parse(array[3]);
            column = int.Parse(array[4]);
            rowSum = int.Parse(array[5]);
            columnSum = int.Parse(array[6]);

        }
        catch (Exception )
        {
        }

        //
        array[7] = array[7].Replace("[", "").Replace("]", "").Replace(" ", "");
        parentInfos = new List<List<int>>();
        foreach (var str in array[7].Split('-'))
        {
            try
            {
                parentInfos.Add(str.Replace(" ", "").Split(',').Select(int.Parse).ToList());
            }
            catch (Exception ) { }
        }
    }
}