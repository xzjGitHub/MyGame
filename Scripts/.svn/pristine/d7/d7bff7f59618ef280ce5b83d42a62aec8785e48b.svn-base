using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using EventDispatch;

//  关卡资源管理
public partial class AssetBundleDownload : MonoBehaviour, IGameModule
{
    //  更新资源列表
    List<string> updateList = new List<string>();
    //  远程服务器资源配置
    AssetConfig remoteConfig = new AssetConfig();
    //  本地资源配置
    AssetConfig localConfig = new AssetConfig();
    //  当前进度
    float progress_ = 0;
    //  是否下载成功完成
    bool isComplete_ = true;
    //开始下载
    bool isStartDown = false;
    //开始下载
    bool isStartDownLocal = false;
    //  下载中的错误
    string error_;

    List<string> filesList = new List<string>();
    //  配置文件名

    static string assetConfigName = "VersionCRC32";
    //"AssetConfig.xml";



    void OnInitScene()
    {
        GameModules.AddModule("", this);
        AppInfo.Load();
        LoadConfig();
    }

    void Awake()
    {
       
    }

    public void StopUpdateAssets()
    {
        updateList.Clear();
        if (!isComplete_)
        {
            StopCoroutine("StartUpdateAssets");
            isComplete_ = true;
        }
        progress_ = 0;
        error_ = string.Empty;
    }

    //  开始加载资源
    public void StartUpdateAssets()
    {
        StartCoroutine(StartUpdateAssets(filesList));
    }
    IEnumerator StartUpdateAssets(List<string> assetList)
    {
        updateList.Clear();
        foreach (string assetPath in assetList)
        {
            updateList.Add(assetPath);
        }

        isComplete_ = false;
        error_ = string.Empty;
        progress_ = 0;

    
        yield return StartCoroutine(DoUpdateAssets(assetList));
        //		AssetLoader.InitInstance ();
    }

    //  更新游戏资源
    IEnumerator DoUpdateAssets(List<string> assetList)
    {
        //progress_ = 0;
        isComplete_ = false;
        error_ = string.Empty;
        foreach (string assetBundleName in updateList)
        {
            progress_++;

            //  如果本地存在资源，先从本地加载
            AssetInfo localInfo = localConfig.FindAsset(assetBundleName);
            AssetInfo remoteInfo = remoteConfig.FindAsset(assetBundleName);
            //  对本地文件进行校验
            string localFileName = AppInfo.GetLocalPath(assetBundleName);
            localFileName = localFileName.Replace("res/res", "res");
            if (
                (localInfo != null && remoteInfo == null)
                || (localInfo != null && remoteInfo != null && localInfo.assetCrc32 == remoteInfo.assetCrc32))
            {
                if (File.Exists(localFileName))
                {
                    string assetCrc32 = CRC32.GetCRC32S(localFileName);

                    if (assetCrc32 == localInfo.assetCrc32)
                    {
                        continue;
                    }
                }
            }
            //  本地加载失败或以过时，从远程加载
            if (remoteInfo == null)
            {
                error_ = "remote asset not found : " + assetBundleName;
                isComplete_ = true;
                yield break;
            }

            string remoteFileName = AppInfo.GetRemotePath(assetBundleName);

            using (WWW www = new WWW(remoteFileName))
            {
                yield return www;
                //  下载出错，直接返回失败
                if (!string.IsNullOrEmpty(www.error))
                {
                    error_ = "remote asset not found : " + www.error;
                    isComplete_ = true;
                    www.Dispose();
                    yield break;
                }
                //  创建缓存目录
                CreateFilePath(localFileName);
                //  缓存到本地
                System.IO.File.WriteAllBytes(localFileName, www.bytes);
                //  添加到本地配置
                localConfig.AddAsset(assetBundleName, remoteInfo.assetCrc32);
                string localConfigPath = AppInfo.GetLocalPath(assetConfigName);
                CreateFilePath(localConfigPath);
                localConfig.Save(localConfigPath);
                www.Dispose();
            }
        }
        isComplete_ = true;
    }

    //  创建文件所在的目录
    private void CreateFilePath(string filePath)
    {
        string dirName = System.IO.Path.GetDirectoryName(filePath);
        if (!Directory.Exists(dirName))
        {
            System.IO.Directory.CreateDirectory(dirName);
        }
    }

    //获得远程字节            //添加本地资源
    private int GetRemoteBytes(List<string> allList)
    {
        //  需要从远程加载的资源
        List<string> remoteList = new List<string>();

        if (localConfig.isOK)
        {
            foreach (string assetPath in allList)
            {
                if (!localConfig.IsAssetExist(assetPath))
                {
                    remoteList.Add(assetPath);
                }
            }
        }
        else
        {
            remoteList = allList;
        }

        int totalBytes = 0;

        if (remoteList.Count > 0)
        {
            if (!remoteConfig.isOK)
            {
                LoadConfig();
                return -1;
            }

            foreach (string assetPath in remoteList)
            {
                AssetInfo assetInfo = remoteConfig.FindAsset(assetPath);
                if (assetInfo == null)
                {
                    return -1;
                }
            }
        }
        return totalBytes;
    }

    void Start()
    {

    }
    //加载配置
    void LoadConfig()
    {
        if (!remoteConfig.isOK && !remoteConfig.isLoading)
        {
            isStartDown = true;
            StartCoroutine(remoteConfig.Load(AppInfo.GetRemotePath(assetConfigName), filesList));
        }

        if (!isStartDown)
        {
            return;
        }
        if (!localConfig.isLocalOK && !localConfig.isLoadingLocal)
        {
            isStartDownLocal = true;
            StartCoroutine(localConfig.LoadLocalConfig(AppInfo.localProtocol + AppInfo.GetLocalPath(assetConfigName)));
        }
    }

    public float progress
    {
        get
        {
            float totalCount = updateList.Count;
            if (totalCount < 1)
            {
                return 1;
            }
            return progress_ / totalCount;
        }
    }

    private string error
    {
        get { return error_; }
    }

    private bool isComplete
    {
        get { return isComplete_; }
    }



    void IGameModule.BeforeStartModule()
    {

    }

    void IGameModule.StartModule()
    {
       
    }

    void IGameModule.AfterStartModule()
    {

    }

    void IGameModule.BeforeStopModule()
    {
       
    }

    void IGameModule.StopModule()
    {
       
    }

    void IGameModule.AfterStopModule()
    {
       
    }

    void IGameModule.BeforeUpdateModule()
    {
       
    }

    void IGameModule.UpdateModule()
    {
        if (isStartDown)
        {
            if (!remoteConfig.isLoading && !remoteConfig.isOK)
            {
                isStartDown = false;
                //quitGame = GameObject.Find("Pop/popup/QuitGame(Clone)/").GetComponent<UIQuitGame>();

                //quitGame.msg_text.text = "　　获取服务器资源出错，退出游戏，重新进入！";
                //quitGame.msg_text.alignment = TextAnchor.UpperLeft;
                //quitGame.uiQuitGame.SetActive(true);
            //    popupRoot.doErrorShow.Dispatch("网络连接错误");
            }
        }

        if (isStartDownLocal)
        {
            isStartDownLocal = false;
        }
    }

    void IGameModule.AfterUpdateModule()
    {
       
    }

    void IGameModule.OnFreeScene()
    {
       
    }
}
