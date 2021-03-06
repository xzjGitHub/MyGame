﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SpriteInfo
{
    public Sprite Spri;
    public string TypeName;

    public SpriteInfo(Sprite sp,string name)
    {
        Spri = sp;
        TypeName = name;
    }
}

public class AddNeedLoadSprite
{
    [MenuItem("ScriptableObject/CreatSpriteAssets")]
    public static void CreatSpriteAsset()
    {
        if(Directory.Exists(SpritePathDefine.SpritePackagePrefabPath))
        {
            Directory.Delete(SpritePathDefine.SpritePackagePrefabPath,true);
        }
        Directory.CreateDirectory(SpritePathDefine.SpritePackagePrefabPath);

        //需要打包资源的文件夹根目录
        string path = Application.dataPath + SpritePathDefine.SpritePath;
        //
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        //根目录下 所有场景目录
        DirectoryInfo[] dirScenensDir = directoryInfo.GetDirectories();

        List<string> allName = new List<string>();

        List<SpriteInfo> list = new List<SpriteInfo>();

        foreach(var currentDir in dirScenensDir)
        {
            string tempScenesDir = path + "/" + currentDir.Name;
            int tempScenesIndex = tempScenesDir.LastIndexOf("/");
            string typeName = tempScenesDir.Substring(tempScenesIndex + 1);

            DirectoryInfo dirInfo = currentDir as DirectoryInfo;
            FileSystemInfo[] fileSysArr = dirInfo.GetFileSystemInfos();
            foreach(FileSystemInfo fileInfo in fileSysArr)
            {
                FileInfo file = fileInfo as FileInfo;
                if(file != null && file.Extension != ".meta")
                {
                    int tempIndex = file.FullName.IndexOf("Assets");
                    string assetFilePath = file.FullName.Substring(tempIndex);
                    Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetFilePath);
                    list.Add(new SpriteInfo(sprite,typeName));
                    if(!allName.Contains(typeName))
                        allName.Add(typeName);
                }
            }
        }


        int index = 0;
        int count = list.Count;
        EditorApplication.update = delegate ()
        {
            EditorUtility.DisplayProgressBar("CreatSpriteAssets ing..",list[index].Spri.name,
                index / (float)count);

            SpriteData spriteData = ScriptableObject.CreateInstance<SpriteData>();
            spriteData.Spri = list[index].Spri;


            if(!Directory.Exists(SpritePathDefine.SpritePackagePrefabPath + "/" + list[index].TypeName))
                Directory.CreateDirectory(SpritePathDefine.SpritePackagePrefabPath + "/" + list[index].TypeName);

            AssetDatabase.CreateAsset(spriteData,SpritePathDefine.SpritePackagePrefabPath + "/"
                + list[index].TypeName + "/" + list[index].Spri.name + ".asset");

            index++;

            if(index >= count)
            {
                EditorUtility.ClearProgressBar();
                EditorApplication.update = null;

                list.Clear();
                allName.Clear();
                index = 0;

                AssetDatabase.Refresh();
                AssetDatabase.SaveAssets();
            }
        };

        WirteCode(allName);
    }


    private static void WirteCode(List<string> allName)
    {

        CodeGenerator code = new CodeGenerator();
        DateTime dt = DateTime.Now;
        code.PrintLine();
        code.PrintLine("//--------------------------------------------------------------");
        code.PrintLine("//Creator： ",System.Environment.UserName);
        code.PrintLine("//Data：    ",DateTime.Now.ToString());
        code.PrintLine("//Note:     Create by tools");
        code.PrintLine("//--------------------------------------------------------------");
        code.PrintLine();
        code.PrintLine(string.Format("public class {0}","SpriteTypeNameDefine"));
        code.PrintLine("{");
        code.In();

        for(int i = 0; i < allName.Count; i++)
        {
            code.PrintLine("public const string ",allName[i]," = ","\"",allName[i],"\";");
        }

        code.Out();
        code.PrintLine("}");

        code.WriteFile(Application.dataPath + "/Scripts/Frame/Sprite/" + "SpriteTypeNameDefine" + ".cs");
    }
}
