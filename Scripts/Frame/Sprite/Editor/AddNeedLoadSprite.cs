using System.IO;
using UnityEditor;
using UnityEngine;

public class AddNeedLoadSprite
{
    [MenuItem("ScriptableObject/CreatSpriteAssets")]
    public static void CreatSpriteAsset()
    {
        if (Directory.Exists(SpritePathDefine.SpritePackagePrefabPath))
        {
            Directory.Delete(SpritePathDefine.SpritePackagePrefabPath, true);
        }
        Directory.CreateDirectory(SpritePathDefine.SpritePackagePrefabPath);

        //需要打包资源的文件夹根目录
        string path = Application.dataPath + SpritePathDefine.SpritePath;
        //
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        //根目录下 所有场景目录
        DirectoryInfo[] dirScenensDir = directoryInfo.GetDirectories();

        foreach(var currentDir in dirScenensDir)
        {
            string tempScenesDir = path + "/" + currentDir.Name;
            int tempScenesIndex = tempScenesDir.LastIndexOf("/");
            string tempScenesName = tempScenesDir.Substring(tempScenesIndex + 1);

            SpriteData spriteData = ScriptableObject.CreateInstance<SpriteData>();

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
                    spriteData.List.Add(sprite);
                }
            }
            AssetDatabase.CreateAsset(spriteData,SpritePathDefine.SpritePackagePrefabPath +"/"+tempScenesName + ".asset");
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
