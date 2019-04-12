using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class FindPanelPath
{

    private static List<string> m_all = new List<string>();
    private static Dictionary<string,string> dict = new Dictionary<string,string>();

    [MenuItem("ScriptableObject/CreatPanelAssets")]
    public static void AutoFindPath()
    {
        string panelRoot = Application.dataPath + "/Resources/UI/Panel/";
        CheckPath(panelRoot);
        SetPanelPath();
        SavePanelPath();
        m_all.Clear();
        dict.Clear();
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
    }


    private static void CheckPath(string path)
    {
        DirectoryInfo theFolder = new DirectoryInfo(path);

        //遍历文件
        FileInfo[] thefileInfo = theFolder.GetFiles("*.*",SearchOption.TopDirectoryOnly);
        GetAllFile(thefileInfo);

        //遍历子文件夹
        DirectoryInfo[] dirInfo = theFolder.GetDirectories();
        foreach(DirectoryInfo dir in dirInfo)
        {
            FileInfo[] fileInfo = dir.GetFiles("*.*",SearchOption.AllDirectories);
            GetAllFile(fileInfo);
        }
    }

    private static void GetAllFile(FileInfo[] infos)
    {
        foreach(FileInfo file in infos)
        {
            if(file.Extension == ".meta")
                continue;
            m_all.Add(file.FullName);
        }
    }


    private static void SetPanelPath()
    {
        for(int i = 0; i < m_all.Count; i++)
        {
            string tempUnityPath = m_all[i].Replace("\\","/");
            //  “/Panel/”的长度为6
            int tempIndex = tempUnityPath.IndexOf("/Panel/") + 7;
            string panelPath = m_all[i].Substring(tempIndex);

            // ".Prefab"长度为7
            panelPath = panelPath.Substring(0,panelPath.Length - 7);
            panelPath.Replace("\\","/");

            int tempIndex1 = panelPath.LastIndexOf("\\",StringComparison.Ordinal);

            if(tempIndex1 == -1)
            {
                if(!dict.ContainsKey(panelPath))
                    dict.Add(panelPath,panelPath);
            }
            else
            {
               string path= panelPath.Replace("\\","/");
                string panelName = panelPath.Substring(tempIndex1 + 1);
                if(!dict.ContainsKey(panelName))
                    dict.Add(panelName,path);
            }
        }

    }

    private static void SavePanelPath()
    {

        PanelConfigData data = ScriptableObject.CreateInstance<PanelConfigData>();
        foreach (var info in dict)
        {
           // data.Dict.Add(info.Key,info.Value);
            PanelInfo panel = new PanelInfo(info.Key,info.Value);
            data.PanelPathList.Add(panel);
        }

        // data.name = "111";

        if (!Directory.Exists(PanelDefine.PanelConfigDir))
            Directory.CreateDirectory(PanelDefine.PanelConfigDir);
   
        AssetDatabase.CreateAsset(data,PanelDefine.PanelConfigDir + 
            "/" + PanelDefine.PanelCofigName + ".asset");

    }
}