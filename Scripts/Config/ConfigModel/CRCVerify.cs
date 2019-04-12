using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// CRCVerifyConfig配置表
/// </summary>
public partial class CRCVerifyConfig : IReader
{
    public List<CRCVerify> _CRCVerify = new List<CRCVerify>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _CRCVerify.Add(new CRCVerify(array[i]));
        }
    }
}



/// <summary>
/// CRCVerify配置表
/// </summary>
public partial class CRCVerify : IReader
{
    /// <summary>
    /// 文件名
    /// </summary>
    public string fileName;
    /// <summary>
    /// CRC
    /// </summary>
    public string CRC;



    public CRCVerify() { }
    public CRCVerify(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        fileName = array[0];
        CRC = array[1];
    }
}
