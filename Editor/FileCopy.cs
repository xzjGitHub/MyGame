using System;
using System.IO;
using UnityEngine;
using UnityEditor;

public class FileCopy
{
   // [MenuItem("ResCopy/SkillIcon")]
    private static void CopySkillIcon()
    {
        string sourcePath = @"G:\Art\UI\SkillIcon";
        string destPath = Application.dataPath + @"\Images\NeedLoad\SkillIcon";
        try
        {
            DirectoryInfo dir = new DirectoryInfo(sourcePath);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos(); 
            foreach(FileSystemInfo i in fileinfo)
            {
                File.Copy(i.FullName,destPath + "\\" + i.Name,true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        catch(Exception e)
        {
            UnityEngine.Debug.LogError(e);
            throw;
        }
    }

}
