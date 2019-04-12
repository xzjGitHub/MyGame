using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.IO;

public class SVN: EditorWindow
{
    private static string ResourceDir = "G:/Resource";

    [MenuItem("SVN/Client/Update/All")]
    static void UpdateSVN()
    {

        Process.Start("TortoiseProc.exe","/command:update /path:" + Application.dataPath + " /closeonend:0");

    }

    [MenuItem("SVN/Client/Update/Scripts")]
    static void UpdateScri()
    {
        Process.Start("TortoiseProc.exe","/command:update /path:" + Application.dataPath + "/Scripts" + " /closeonend:0");
    }

    [MenuItem("SVN/Client/Update/Resource")]
    static void UpdateRes()
    {
        Process.Start("TortoiseProc.exe","/command:update /path:" + Application.dataPath + "/Resources" + " /closeonend:0");
    }

    [MenuItem("SVN/Client/Commit/All")]
    static void Commit()
    {
        Process.Start("TortoiseProc.exe","/command:commit /path:" + Application.dataPath + " /closeonend:0");
    }

    [MenuItem("SVN/Client/Commit/Scripts")]
    static void CommitSci()
    {
        Process.Start("TortoiseProc.exe","/command:commit /path:" + Application.dataPath + 
            "/Scripts" + " /closeonend:0");
    }

    [MenuItem("SVN/Client/Commit/Editor")]
    static void CommitEdi()
    {
        Process.Start("TortoiseProc.exe","/command:commit /path:" + Application.dataPath +
            "/Editor" + " /closeonend:0");
    }

    [MenuItem("SVN/Client/Commit/Image")]
    static void CommitImagei()
    {
        Process.Start("TortoiseProc.exe","/command:commit /path:" + 
            Application.dataPath + "/Images" + " /closeonend:0");
    }


    [MenuItem("SVN/Client/Commit/Resources/All")]
    static void CommitRes()
    {
       Process.Start("TortoiseProc.exe","/command:commit /path:" + 
           Application.dataPath + "/Resources" + " /closeonend:0");
    }

    [MenuItem("SVN/Client/Commit/Resources/UIPanel")]
    static void CommitUIPanel()
    {
        Process.Start("TortoiseProc.exe","/command:commit /path:" +
            Application.dataPath + "/Resources/UI/Panel" + " /closeonend:0");
    }

    [MenuItem("SVN/Client/Commit/Resources/Prefab")]
    static void CommitPrefab()
    {
        Process.Start("TortoiseProc.exe","/command:commit /path:" + 
            Application.dataPath + "/Resources/UI/Prefab" + " /closeonend:0");
    }


    [MenuItem("SVN/Client/Commit/Resources/Sprite")]
    static void CommitSprite()
    {
        Process.Start("TortoiseProc.exe","/command:commit /path:" + 
            Application.dataPath + "/Resources/UI/Sprite" + " /closeonend:0");
    }

    [MenuItem("SVN/Client/Commit/Resources/SpriteAssets")]
    static void CommitSpriteAssets()
    {
        Process.Start("TortoiseProc.exe","/command:commit /path:" + 
            Application.dataPath + "/Resources/UI/SpriteAssets" + " /closeonend:0");
    }

    [MenuItem("SVN/Client/Commit/Resources/config")]
    static void Commitconfig()
    {
        Process.Start("TortoiseProc.exe","/command:commit /path:" + 
            Application.dataPath + "/Resources/config" + " /closeonend:0");
    }

    [MenuItem("SVN/Client/Commit/Scene")]
    static void CommitScene()
    {
        Process.Start("TortoiseProc.exe","/command:commit /path:" + 
            Application.dataPath + "/Scenes" + " /closeonend:0");
    }

    [MenuItem("SVN/Client/Commit/SpriteAssets")]
    static void CommitSpriteAsset()
    {
        Process.Start("TortoiseProc.exe","/command:commit /path:" +
            Application.dataPath + "/Resources/UI/SpriteAssets" + " /closeonend:0");
    }

    [MenuItem("SVN/Client/ShowLog")]
    static void ShowLog()
    {
        Process.Start("TortoiseProc.exe","/command:log /path:" + Application.dataPath + " /closeonend:0");
    }

    [MenuItem("SVN/Client/CleanUp")]
    static void CleanUp()
    {
        Process.Start("TortoiseProc.exe","/command:cleanup /path:" + 
            Application.dataPath + " /closeonend:0");
    }

    [MenuItem("SVN/Resource/Update")]
    static void UpdateResourceSVN()
    {
        Process.Start("TortoiseProc.exe","/command:update /path:" + ResourceDir + " /closeonend:0");
    }

    [MenuItem("SVN/Resource/ShowLog")]
    static void ShowLogResourceSVN()
    {
        Process.Start("TortoiseProc.exe","/command:update /path:" + ResourceDir + " /closeonend:0");
    }

    [MenuItem("SVN/Resource/CleanUp")]
    static void CleanResourceSVN()
    {
        Process.Start("TortoiseProc.exe","/command:cleanup /path:" + ResourceDir + " /closeonend:0");
    }


    [MenuItem("SVN/Resource/打开资源文件夹")]
    static void Open()
    {
        System.Diagnostics.Process.Start(@ResourceDir);
    }

    [MenuItem("美术工具/Out")]
    static void Out()
    {
        Process.Start("TortoiseProc.exe","/command:commit /path:" +
                  Application.dataPath + "/Out" + " /closeonend:0");
    }

    [MenuItem("美术工具/Resources/char")]
    static void Char()
    {
        Process.Start("TortoiseProc.exe","/command:commit /path:" +
                  Application.dataPath + "/Resources/char" + " /closeonend:0");
    }

    [MenuItem("美术工具/Resources/UI/Character")]
    static void UIChar()
    {
        Process.Start("TortoiseProc.exe","/command:commit /path:" +
                  Application.dataPath + "/Resources/UI/Character" + " /closeonend:0");
    }
}
