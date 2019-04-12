using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using EventDispatch;

public enum EAssetLoadStatus
{
    eNone = 1,
    eLoading,
    eOk,
    eFail
}

public class CAssetLoadInfo
{
    UnityEngine.Object obj;
    EAssetLoadStatus assetLoadStatus;
    string name;

    public CAssetLoadInfo(string name)
    {
        this.name = name;
        assetLoadStatus = EAssetLoadStatus.eNone;
        obj = null;
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public UnityEngine.Object Obj
    {
        get { return obj; }
        set { obj = value; }
    }

    public EAssetLoadStatus AssetLoadStatus
    {
        get { return assetLoadStatus; }
        set { assetLoadStatus = value; }
    }
}


public class AssetLoader : MonoBehaviour, IGameModule
{
    //资源名 -> 资源加载信息
    private Dictionary<string, CAssetLoadInfo> nameDirLoadInfo = new Dictionary<string, CAssetLoadInfo>();

    //资源名 -> 资源包路径
    private Dictionary<string, string> nameDirBundlePath = new Dictionary<string, string>();
    //资源名 ->依赖名字列表
    Dictionary<string, string[]> mDependenciesNameList = new Dictionary<string, string[]>();
    //资源名 ->加载了的AssetBundle
    Dictionary<string, AssetBundle> mExistingAssetBundleList = new Dictionary<string, AssetBundle>();
    //
    private static AssetLoader instance = null;
    private AssetBundleManifest manifest;
    //
    //LoadModule loadModule;

    bool isLoadOk = false;
    float time = 0.0f;


    //场景初始化
    void OnInitScene()
    {
        //   GameModules.AddModule(ModuleName.assetLoader, this);
        AppInfo.Load();
        //
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        LoadAssetManifest();
        StartCoroutine(AutoUpdateLocalAsset());
    }

    void Start()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
        //LoadAssetManifest();
        //StartCoroutine(AutoUpdateLocalAsset());
    }

    //自动更新本地资源
    IEnumerator AutoUpdateLocalAsset()
    {
        while (true)
        {
            List<string> _key = new List<string>();
            List<CAssetLoadInfo> _value = new List<CAssetLoadInfo>();

            _key.AddRange(nameDirLoadInfo.Keys);
            _value.AddRange(nameDirLoadInfo.Values);

            for (int i = 0; i < _value.Count; i++)
            {
                if (_value[i].AssetLoadStatus == EAssetLoadStatus.eNone)
                {
                    isLoadOk = false;
                    yield return StartCoroutine(FromLocalLoadAsset(_key[i]));
                }
            }

            foreach (var item in nameDirLoadInfo)
            {
                if (item.Value.AssetLoadStatus != EAssetLoadStatus.eOk)
                {
                    yield return null;
                }
                isLoadOk = true;
            }
            yield return null;
        }
    }

    //加载资源 用名字来寻找
    public CAssetLoadInfo LoadAsset(string assetName)
    {
        //加载输入路径 res/kind/00000001/prefab/prefab
        //资源存储的路径 bufficon/00000002/prefab/prefab
        // 只需去掉res/即可
        // 
        //string tempPath;
        //int _index = assetName.LastIndexOf("/");
        //string loadName = assetName.Substring(_index + 1, assetName.Length - _index - 1);

        //tempPath = assetName.Remove(_index, assetName.Length - _index);
        //_index = tempPath.LastIndexOf("/");
        //tempPath = tempPath.Remove(_index, tempPath.Length - _index);
        //// 转换  res/res
        //tempPath = tempPath.Replace("res/res", "res");
        ////去掉 res/
        //tempPath = tempPath.Replace("res/", "");

        ////加载的包名+资源名
        //string _assetName = tempPath + "/" + loadName;

        string _assetName = assetName.Replace("res/", "");

        //假如缓存里面巧合就有这个资源，那么立即加载返回
        if (nameDirLoadInfo.ContainsKey(_assetName))
        {
            return nameDirLoadInfo[_assetName];
        }

        if (LocalLoadAsset(_assetName) == null)
        {
            CAssetLoadInfo asset = new CAssetLoadInfo(assetName);
            return ResourcesLoadAsset(assetName, asset);
        }
        return LocalLoadAsset(_assetName);
    }

    //在Resources下加载资源
    CAssetLoadInfo ResourcesLoadAsset(string assetName, CAssetLoadInfo asset)
    {
        //   loadModule =(LoadModule) GameModules.Find(ModuleName.loadModule);
        //   if (loadModule.GetResourcesLoadGameObj(assetName)!=null)
        //   {
        //     asset.Obj = loadModule.GetResourcesLoadGameObj(assetName);
        //        asset.AssetLoadStatus = EAssetLoadStatus.eOk;
        return asset;
        //     }
        //爆警告 先注释
        //if (Resources.Load(assetName) != null)
        //{
        //    asset.Obj = Resources.Load(assetName);
        //    asset.AssetLoadStatus = EAssetLoadStatus.eOk;
        //}
      //  return asset;
    }

    //加载本地资源
    CAssetLoadInfo LocalLoadAsset(string assetName)
    {   //  res/item/00010901/prefab/prefab"

        //int _index = assetName.LastIndexOf("/");
        //string _assetName = assetName.Remove(_index, assetName.Length - _index);

        if (!nameDirBundlePath.ContainsKey(assetName/*_assetName*/))
        {
            return null;
        }

        if (nameDirLoadInfo.ContainsKey(assetName))
        {
            return nameDirLoadInfo[assetName];
        }

        CAssetLoadInfo assetLoadInfo = new CAssetLoadInfo(assetName);
        nameDirLoadInfo.Add(assetName, assetLoadInfo);

        return assetLoadInfo;
    }

    //本地异步加载资源
    IEnumerator/*object*/ FromLocalLoadAsset(string assetName)
    {
        time += Time.deltaTime;
        //  Debug.LogWarning("协程开始加载时间=" + DateTime.Now);
        CAssetLoadInfo cacheInfo = nameDirLoadInfo[assetName];
        cacheInfo.AssetLoadStatus = EAssetLoadStatus.eLoading;

        //// 资源包的名字
        //int _index = assetName.LastIndexOf("/");
        //string _assetName = assetName.Remove(_index, assetName.Length - _index);

        string _assetName = assetName;

        //查找是否有依赖关系表        
        if (mDependenciesNameList.ContainsKey(_assetName))
        {    //有
            foreach (var item in mDependenciesNameList[_assetName])
            {//没有加载所依赖AssetBundle

                //if (!nameDirLoadInfo.ContainsKey(item))
                //{
                //    var _mainBundle1 = AssetBundle.LoadFromFileAsync(nameDirBundlePath[item]);
                //    yield return _mainBundle1;

                //    var _mainBundle = _mainBundle1.assetBundle;

                //    CAssetLoadInfo assetLoadInfo = new CAssetLoadInfo(nameDirBundlePath[item]);
                //    assetLoadInfo.Obj = _mainBundle.LoadAllAssets()[0];
                //    assetLoadInfo.AssetLoadStatus = EAssetLoadStatus.eOk;
                //    nameDirLoadInfo.Add(nameDirBundlePath[item], assetLoadInfo);
                //}
                if (!mExistingAssetBundleList.ContainsKey(item))
                {
                    //                     AssetBundle _mainBundle = AssetBundle.LoadFromFileAsync(nameDirBundlePath[item]).assetBundle;
                    //                     yield return _mainBundle;
                    var _mainBundle1 = AssetBundle.LoadFromFileAsync(nameDirBundlePath[item]);
                    yield return _mainBundle1;

                    var _mainBundle = _mainBundle1.assetBundle;
                    if (mExistingAssetBundleList.ContainsKey(item))
                    {
                        mExistingAssetBundleList[item] = _mainBundle;
                    }
                    else
                    {
                        mExistingAssetBundleList.Add(item, _mainBundle);
                    }
                    //   _mainBundle.Unload(false);
                }
            } //依赖加载完成
        } //加载本身
        //int _mIndex = assetName.LastIndexOf("/");
        //string loadName = assetName.Substring(_mIndex + 1, assetName.Length - _mIndex - 1);

       // string loadName = assetName;

        //加载路径
        string _path = nameDirBundlePath[_assetName];

        //         //检查是否已经加载AssetBundle
        if (mExistingAssetBundleList.ContainsKey(_assetName))
        {
            AssetBundle _tempAssetBundle = mExistingAssetBundleList[_assetName];
            cacheInfo.Obj = _tempAssetBundle.LoadAllAssets()[0];
            //  _tempAssetBundle.LoadAsset(loadName);
            cacheInfo.AssetLoadStatus = EAssetLoadStatus.eOk;
            //  _tempAssetBundle.Unload(false);
        }
        else
        {
            var _assetBundle1 = AssetBundle.LoadFromFileAsync(_path);
            yield return _assetBundle1;

            var _assetBundle = _assetBundle1.assetBundle;

          //  string[] _str = _assetBundle.GetAllAssetNames();

            cacheInfo.Obj = _assetBundle.LoadAllAssets()[0];
            /* _assetBundle.LoadAsset(loadName)*/
            ;
            cacheInfo.AssetLoadStatus = EAssetLoadStatus.eOk;
            if (mExistingAssetBundleList.ContainsKey(_path))
            {
                mExistingAssetBundleList[_path] = _assetBundle;
            }
            else
            {
                mExistingAssetBundleList.Add(_path, _assetBundle);
            }
            _assetBundle.Unload(false);
        }
        yield return null;
    }

    public void AutoLoadAsset(List<string> _assetName)
    {
        bool _isHave = false;
        foreach (var item in nameDirBundlePath)
        {
            foreach (var _name in _assetName)
            {
                if (item.Key.Contains(_name + "/"))
                {
                    CAssetLoadInfo assetLoadInfo = new CAssetLoadInfo(item.Key);
                    nameDirLoadInfo.Add(item.Key, assetLoadInfo);
                    _isHave = true;
                }
            }
        }
        if (!_isHave)
        {
            isLoadOk = true;
        }
    }

    //  清除本地资源缓存
    public void ClearLocalAssets()
    {
        List<AssetBundle> _temp = new List<AssetBundle>();
        foreach (KeyValuePair<string, AssetBundle> item in mExistingAssetBundleList)
        {
            if (item.Value == null)
            {
                continue;
            }
            _temp.Add(item.Value);
            //   item.Value.Unload(true);
        }

        for (int i = 0; i < _temp.Count; i++)
        {
            _temp[i].Unload(true);
        }
        // foreach (KeyValuePair<string, CAssetLoadInfo> pair in nameDirLoadInfo)
        //{
        //    CAssetLoadInfo cacheInfo = pair.Value;
        //    if (cacheInfo.Obj != null)
        //    {
        //        cacheInfo.Obj = null;
        //    }
        //}
        mExistingAssetBundleList.Clear();
        nameDirLoadInfo.Clear();
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
        Caching.ClearCache();
    }

    //读取MANIFEST和资源名字  仅仅是名字而不是资源本身
    void LoadAssetManifest()
    {
        string url = AppInfo.localProtocol + AppInfo.GetLocalPath("res");


        string _path1 = url.Replace("file://", "");
        if (!Directory.Exists(_path1))
        {
            // Debug.LogWarning("文件=" + _path1 + "不存在");
            // return;
        }

        WWW www = new WWW(url);
        //      yield return www;
        if (www.bytes.Length > 0)
        {
            manifest = www.assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

            //加载所有的依赖
            foreach (string name in manifest.GetAllAssetBundles())
            {
                string _path = AppInfo.localProtocol + AppInfo.GetLocalPath(name);


                _path = _path.Replace("file://", "");
                //  Debug.Log(Application.dataPath + "!assets" + "/AssetBundle/res/" + name);

                //   _path = Application.dataPath + "!assets" + "/AssetBundle/res/" + name;
                if (!nameDirBundlePath.ContainsKey(name))
                {
                    nameDirBundlePath.Add(name, _path);
                }


                string[] assetBundle = manifest.GetAllDependencies(name);
                if (assetBundle.Length == 0)
                {
                    continue;
                }
                mDependenciesNameList.Add(name, assetBundle);
            }
            www.assetBundle.Unload(false);
            www.Dispose();
        }


        //  ReadAllAssetName();
    }

    void DisplayAssetName()
    {
        foreach (var go in nameDirBundlePath)
            Debug.Log(go.Key + "  " + go.Value);
    }
    //读取所有的资源名字
    private void ReadAllAssetName()
    {
        foreach (string assetBundleName in manifest.GetAllAssetBundles())
        {
            string assetPath = AppInfo.localProtocol + AppInfo.GetLocalPath(assetBundleName);
            using (WWW www = new WWW(assetPath))
            {
                foreach (string assetName in www.assetBundle.GetAllAssetNames())
                {
                    //					int ix = assetName.LastIndexOf ('/');
                    int jx = assetName.LastIndexOf('.');
                    string nameTemp = assetName.Substring(0, jx).Replace("assets/res/", ""); ;
                    //					string nameTemp = assetName.Substring (ix +1,jx - ix - 1);
                    if (!nameDirBundlePath.ContainsKey(nameTemp))
                        nameDirBundlePath.Add(nameTemp, assetBundleName);
                }

                //此处是释放了的 因此不会占用空间
                www.assetBundle.Unload(true);
                www.Dispose();
            }
        }
    }




    void OnDestroy()
    {
        ClearLocalAssets();
        //   GC.Collect();
        //      GameModules.RemoveModule(ModuleName.assetLoader);
    }

    public static AssetLoader Instance
    {
        get { return instance; }
    }

    public bool IsLoadOk
    {
        get
        {
            return isLoadOk;
        }
    }


    #region 重写接口
    public void BeforeStartModule()
    {

    }

    public void StartModule()
    {

    }

    public void AfterStartModule()
    {

    }

    public void BeforeStopModule()
    {

    }

    public void StopModule()
    {

    }

    public void AfterStopModule()
    {

    }

    public void BeforeUpdateModule()
    {

    }

    public void UpdateModule()
    {

    }

    public void AfterUpdateModule()
    {

    }


    public void OnFreeScene()
    {

    }
    #endregion

}
