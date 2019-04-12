using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ClearDatas  {
    private const string DirPath = "/LocalGameData";
    [MenuItem("存档管理/清除存档")]
    private static void Clear()
    {
        if (Directory.Exists(Application.persistentDataPath + DirPath))
        {
            if(EditorUtility.DisplayDialog("清除存档","确定要清除存档记录？","确定","取消"))
            {
                Directory.Delete(Application.persistentDataPath + DirPath,true);
                AssetDatabase.Refresh();
            }
        }
    }

    [MenuItem("存档管理/手动存档存档")]
    private static void Save(){
        ScriptSystem.Instance.SaveData();
    }

    [MenuItem("存档管理/打开存档文件夹")]
    private static void Open()
    {
        //ScriptSystem.Instance.SaveData();
        System.Diagnostics.Process.Start(Application.persistentDataPath);
    }
}
