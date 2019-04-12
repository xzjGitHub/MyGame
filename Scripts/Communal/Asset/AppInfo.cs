using UnityEngine;

//  引用程序配置信息信息
public class AppInfo
{
    //  资源远程根目录
    public static string assetRemoteRoot;
    //  资源远程根目录
    public static string assetEditorRoot;
    //  资源本地根目录
    public static string assetLocalRoot;
    //  资源内建跟目录
    public static string assetInsideRoot;
    //  平台名称
    public static string platformName;
    //  本地文件协议
    public static string localProtocol = "file://";
    //  是否加载配置
    private static bool isLoad = false;

    //  返回资源在远程服务器上的地址
    public static string GetRemotePath(string assetPath)
    {
     //   Uri uriRes = new Uri(new Uri(AppInfo.assetRemoteRoot), assetPath);
        return AppInfo.assetRemoteRoot + assetPath;
        //   return uriRes.AbsoluteUri;
    }
    //  返回资源在本地的地址
    public static string GetLocalPath(string assetPath)
    {
        return AppInfo.assetLocalRoot + assetPath;
    }
    //  返回资源在resources目录地址
    public static string GetInsidePath(string assetPath)
    {
        return AppInfo.assetInsideRoot + assetPath;
    }

    //  从配置文件加载
    public static void Load()
    {
        if (isLoad) return;
        isLoad = true;
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                platformName = "android";
                break;
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                platformName = "windows";
                break;
            case RuntimePlatform.IPhonePlayer:
                platformName = "ios";
                break;
            default:
                platformName = "common";
                break;
        }
        //
        var assetInfo = AppAssetInfoConfig.GetAssetInfo();
        assetRemoteRoot = assetInfo.remoteRoot;
        assetLocalRoot = assetInfo.localRoot;
        assetEditorRoot = assetInfo.editorRoot;
        //
        if (!System.IO.Directory.Exists(assetLocalRoot)) System.IO.Directory.CreateDirectory(assetLocalRoot);
    }
}
