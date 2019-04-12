using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// AppAssetInfoConfig配置表
/// </summary>
public partial class AppAssetInfoConfig : IReader
{
    public List<AppAssetInfo> _AppAssetInfo = new List<AppAssetInfo>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _AppAssetInfo.Add(new AppAssetInfo(array[i]));
        }
    }
}



/// <summary>
/// AppAssetInfo配置表
/// </summary>
public partial class AppAssetInfo : IReader
{
    /// <summary>
    /// 远程根目录
    /// </summary>
    public string remoteRoot;
    /// <summary>
    /// 本地根目录
    /// </summary>
    public string localRoot;
    /// <summary>
    /// 编辑器根目录
    /// </summary>
    public string editorRoot;



    public AppAssetInfo() { }
    public AppAssetInfo(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        remoteRoot = array[0];
        localRoot = array[1];
        editorRoot = array[2];
    }
}
