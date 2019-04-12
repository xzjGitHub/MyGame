using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class CreateMyScripts
{
    [MenuItem("Assets/Create/UIPanelBehaviour",false,70)]
    public static void CreateUIPanelBehaviour()
    {
        //参数为传递给CreateEventCSScriptAsset类action方法的参数  
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
           ScriptableObject.CreateInstance<CreateEventCSScriptAsset>(),
           GetSelectPathOrFallback() + "/New Script.cs",null,
           "Assets/Editor/ScriptTemplate/MyPanelTemplate1.cs");
    }

    [MenuItem("Assets/Create/MonoBehaviour",false,70)]
    public static void CreateNormal()
    {
        //参数为传递给CreateEventCSScriptAsset类action方法的参数  
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
          ScriptableObject.CreateInstance<CreateEventCSScriptAsset>(),
           GetSelectPathOrFallback() + "/New Script.cs",null,
           "Assets/Editor/ScriptTemplate/MyPanelTemplate2.cs");
    }

    //取得要创建文件的路径  
    public static string GetSelectPathOrFallback()
    {
        string path = "Assets";
        //遍历选中的资源以获得路径  
        //Selection.GetFiltered是过滤选择文件或文件夹下的物体，assets表示只返回选择对象本身  
        foreach(UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object),SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);
            if(!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
                break;
            }
        }
        return path;
    }
}

//要创建模板文件必须继承EndNameEditAction，重写action方法  
class CreateEventCSScriptAsset: EndNameEditAction
{
    public override void Action(int instanceId,string pathName,string resourceFile)
    {
        //创建资源  
        UnityEngine.Object obj = CreateScriptAssetFromTemplate(pathName,resourceFile);
        ProjectWindowUtil.ShowCreatedAsset(obj);//高亮显示资源  
    }

    internal static UnityEngine.Object CreateScriptAssetFromTemplate(string pathName,string resourceFile)
    {
        //获取要创建资源的绝对路径  
        string fullPath = Path.GetFullPath(pathName);
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathName);
        //读取本地的模板文件  
        StreamReader streamReader = new StreamReader(resourceFile);
        string text = streamReader.ReadToEnd();
        streamReader.Close();

        //将模板类中的类名替换成你创建的文件名  
        text = Regex.Replace(text,"MyPanelTemplate1",fileNameWithoutExtension);
        text = Regex.Replace(text,"MyPanelTemplate2",fileNameWithoutExtension);
        bool encoderShouldEmitUTF8Identifier = true; //参数指定是否提供 Unicode 字节顺序标记  
        bool throwOnInvalidBytes = false;//是否在检测到无效的编码时引发异常  
        UTF8Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier,throwOnInvalidBytes);
        bool append = false;
        //写入文件  
        StreamWriter streamWriter = new StreamWriter(fullPath,append,encoding);
        streamWriter.Write(text);
        streamWriter.Close();
        //刷新资源管理器  
        AssetDatabase.ImportAsset(pathName);
        AssetDatabase.Refresh();
        return AssetDatabase.LoadAssetAtPath(pathName,typeof(UnityEngine.Object));
    }
}

