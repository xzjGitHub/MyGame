using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using LskConfig;



/// <summary>
/// ResultFormulaConfig配置表
/// </summary>
public partial class ResultFormulaConfig : IReader
{
    public List<ResultFormula> _ResultFormula = new List<ResultFormula>();
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = Regex.Split(content, "\r\n");
        for (int i = 2; i < array.Length; i++)
        {
            _ResultFormula.Add(new ResultFormula(array[i]));
        }
    }
}



/// <summary>
/// ResultFormula配置表
/// </summary>
public partial class ResultFormula : IReader
{
    /// <summary>
    /// 
    /// </summary>
    public int formulaID;
    /// <summary>
    /// 
    /// </summary>
    public string resultFormula;



    public ResultFormula() { }
    public ResultFormula(string content)
    {
        Reader(content);
    }
    /// <summary>
    /// 读取配置表
    /// </summary>
    public void Reader(string content)
    {
        string[] array = content.Split('\t');
        formulaID = int.Parse(array[0]);
        resultFormula = array[1];
    }
}
