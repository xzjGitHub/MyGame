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
                File.Copy(i.FullName,destPath + "\\" + i.Name,true);      //�����ļ��м������ļ���true��ʾ���Ը���ͬ���ļ�
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
