#region Version Info

/* =======================================================================
* 
* 作者：lsk 
* 创建时间：2017/9/18 22:39:40
* 文件名：GameDataManager
* 版本：V0.0.1
*
* ========================================================================
*/

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 游戏数据管理员
/// </summary>
public class GameDataManager
{
    //
    private const string Suffix = ".json";
    //
    public const string LocalGameData = "/LocalGameData/";


    /// <summary>
    /// 存档
    /// </summary>
    /// <param name="saveDataType"></param>
    public static void SaveGameData(SaveDataType saveDataType = SaveDataType.Week)
    {
        //前置期不存档
        if (ScriptSystem.Instance.ScriptPhase == ScriptPhase.Front)
        {
            return;
        }
        //测试期暂时存档所有数据
        PlayerSystem.Instance.SaveData();
        ScriptSystem.Instance.SaveData();
    }

    //读取存档信息
    public static object ReadData<T>(string path) where T : class
    {
        return DataOperation.GetDataJosn(Application.persistentDataPath + LocalGameData + path + Suffix, typeof(T));
        // return DataOperation.GetDataProtoBuf<T>(Application.persistentDataPath + path + Suffix);
    }

    //读取存档信息
    public static void SaveData<T>(string dirpath, string path, T obj)
    {
        //定义存档路径
        dirpath = Application.persistentDataPath + LocalGameData + dirpath;
        //
        DataOperation.SetDataJosn(dirpath, path + Suffix, obj);
        // DataOperation.SetDataProtoBuf<T>(dirpath, path + Suffix, obj);
    }

    //删除存档信息
    public static void DeleteData(string dirpath)
    {
        //定义存档路径
        dirpath = Application.persistentDataPath + LocalGameData + dirpath;
        //
        DataOperation.DeleteDirectory(dirpath);
    }

    //删除存档信息
    public static bool IsDirectoryExists(string dirpath)
    {
        //定义存档路径
        dirpath = Application.persistentDataPath + LocalGameData + dirpath;
        //
        return DataOperation.IsDirectoryExists(dirpath);
    }

    /// <summary>
    /// 读取剧本存档列表
    /// </summary>
    /// <param name="_scriptId"></param>
    /// <returns></returns>
    public static List<ScriptData> GetScriptDatas(int _scriptId)
    {
        List<ScriptData> _list = new List<ScriptData>();
        //定义存档路径
        string dirpath = Application.persistentDataPath + LocalGameData + _scriptId;
        DirectoryInfo TheFolder = new DirectoryInfo(dirpath);
        try
        {
            DirectoryInfo[] directories = TheFolder.GetDirectories();
            if (directories == null)
            {
                return _list;
            }

            foreach (DirectoryInfo item in directories)
            {
                foreach (FileInfo _file in item.GetFiles())
                {
                    if (!string.Equals(_file.Name, "Script" + Suffix))
                    {
                        continue;
                    }

                    _list.Add(ReadData<ScriptData>(_scriptId + "/" + item.Name + "/Script") as ScriptData);
                    break;
                }
            }
        }
        catch (Exception)
        {

            return _list;
        }


        return _list;
    }

    public static void SaveCombatTestData<T>(T obj, string path="", string fileName = "")
    {
        //定义存档路径
        string dirpath = Application.persistentDataPath + LocalGameData+ path;
        //
        DataOperation.SetDataJosn(dirpath, fileName + Suffix, obj);
    }


    public static object GetCombatTestData<T>(string fileName)
    {
        //定义存档路径
        string dirpath = Application.persistentDataPath + LocalGameData;
        //
        return DataOperation.GetDataJosn(dirpath + fileName + Suffix, typeof(T));
    }
}


/// <summary>
/// 存档类型
/// </summary>
public enum SaveDataType
{
    /// <summary>
    /// 周
    /// </summary>
    Week = 1,
    /// <summary>
    /// 探索前
    /// </summary>
    BeforeExploring = 2,
    /// <summary>
    /// 探索结束
    /// </summary>
    ExploreEnd = 3,
    /// <summary>
    /// 退出游戏
    /// </summary>
    QuitGame = 4,
}