
//--------------------------------------------------------------
//Creator： XZJ
//Data：    2019/5/21 14:23:07
//Note:     
//--------------------------------------------------------------
using Res;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PrefabInfo
{
    public AssetType AsstType; //资源类型 
    public GameObject Obj;     //物体
    public bool CanUse;        //是否可用
    public int InstanceId;     //实例id

    public PrefabInfo(GameObject obj,bool canUse,AssetType type)
    {
        Obj = obj;
        CanUse = canUse;
        InstanceId = Obj.GetInstanceID();
        AsstType = type;
    }
}

/// <summary>
/// 异步加载的回调参数 
/// </summary>
public class LoadDownParams
{
    public string Name;                     //所属对象池名
    public Action<GameObject> LoadCallBack; //回调
    public AssetType AssetType;             //资源类型
    public string Uid;                      //uid 
}

public class PrefabPool: Singleton<PrefabPool>
{
    private PrefabPoolUtil m_poolUtil;
    //[对象次名称 列表]
    private Dictionary<string,List<PrefabInfo>> m_dict;
    //异步加载需要 同步加载不用管
    private List<LoadDownParams> m_loadDownList;

    private PrefabPool()
    {
        m_dict = new Dictionary<string,List<PrefabInfo>>();
        m_loadDownList = new List<LoadDownParams>();
        m_poolUtil = new PrefabPoolUtil();
    }

    //添加到缓存
    private void AddToDict(string name,GameObject obj,AssetType type)
    {
        PrefabInfo info = new PrefabInfo(obj,false,type);
        if(!m_dict.ContainsKey(name))
        {
            m_dict.Add(name,new List<PrefabInfo>());
        }
        m_dict[name].Add(info);
        //todo 后续做一个上限检查
    }

    //从缓存里面获取
    private GameObject GetObj(string name)
    {
        GameObject temp = null;
        if(m_dict.ContainsKey(name))
        {
            List<PrefabInfo> list = m_dict[name];
            for(int i = 0; i < list.Count; i++)
            {
                if(list[i].CanUse)
                {
                    temp = list[i].Obj;
                    list[i].CanUse = false;
                    break;
                }
            }
        }
        return temp;
    }

    /// <summary>
    /// 同步获取 不需要加载 需要自己传一个prefab
    /// </summary>
    /// <param name="poolName">对象池名</param>
    /// <param name="prefab">预制体</param>
    /// <returns></returns>
    public GameObject GetObjSync(string poolName,GameObject prefab)
    {
        GameObject temp = GetObj(poolName);
        if(temp == null)
        {
            temp = GameObject.Instantiate<GameObject>(prefab);
            AddToDict(poolName,temp,AssetType.None);
        }
        return temp;
    }

    /// <summary>
    /// 同步获取 这种方式需要加载 
    /// </summary>
    /// <param name="poolName">要加载的资源名 对象池名</param>
    /// <param name="assetType">资源类型</param>
    /// <returns></returns>
    public GameObject GetObjSync(string poolName,AssetType assetType)
    {
        GameObject temp = GetObj(poolName);
        if(temp == null)
        {
            GameObject prefab = ResManager.Instance.GetAssetSync<GameObject>(assetType,poolName);
            temp = GameObject.Instantiate<GameObject>(prefab);
            AddToDict(poolName,temp,assetType);
        }
        return temp;
    }

    /// <summary>
    /// 异步获取 需要加载
    /// </summary>
    /// <param name="poolName">资源名</param>
    /// <param name="assetType">类型</param>
    /// <param name="loadCallback">异步加载回调</param>
    /// <returns></returns>
    public RequestInfo GetObjAsync(string poolName,AssetType assetType,Action<GameObject> loadCallback)
    {
        RequestInfo requestInfo = null;
        GameObject obj = GetObj(poolName);
        if(obj != null)
        {
            obj.SetActive(true);
            loadCallback(obj);
        }
        else
        {
            LoadDownParams loadDown = new LoadDownParams();
            loadDown.LoadCallBack = loadCallback;
            loadDown.Name = poolName;
            loadDown.AssetType = assetType;
            string uid = Utility.GenerateOnlyId();
            loadDown.Uid = uid;
            m_loadDownList.Add(loadDown);

            requestInfo = ResManager.Instance.GetAssetAsync<GameObject>(assetType,poolName,
                true,uid,LoadObjDone,(string error) => { LogHelperLSK.LogError(error); });
        }
        return requestInfo;
    }

    //异步加载完成
    private void LoadObjDone(UnityEngine.Object obj,string uid)
    {
        LoadDownParams load = null;
        for(int i = 0; i < m_loadDownList.Count; i++)
        {
            if(m_loadDownList[i].Uid == uid)
            {
                load = m_loadDownList[i];
                m_loadDownList.RemoveAt(i);
            }
        }

        GameObject temp = obj as GameObject;
        GameObject prefabObj = GameObject.Instantiate<GameObject>(temp);
        if(load != null && load.LoadCallBack != null)
        {
            load.LoadCallBack(prefabObj);
            AddToDict(load.Name,prefabObj,load.AssetType);
        }
    }

    /// <summary>
    /// 释放某个对象池里面的某个物体
    /// </summary>
    /// <param name="poolName">所属对象池</param>
    /// <param name="obj">要释放的物体</param>
    /// <param name="needDes">是否需要销毁</param>
    public void Free(string poolName,GameObject obj,bool needDes = false)
    {
        if(!m_dict.ContainsKey(poolName))
            return;
        List<PrefabInfo> list = m_dict[poolName];
        for(int i = 0; i < list.Count; i++)
        {
            if(list[i].InstanceId == obj.GetInstanceID())
            {
                if(!needDes)
                {
                    list[i].CanUse = true;
                    m_poolUtil.AddToPool(poolName,list[i].Obj);
                }
                else
                {
                    if(list[i].AsstType != AssetType.None)
                        ResManager.Instance.ReleaseAsset(list[i].AsstType,poolName);
                    GameObject.DestroyImmediate(list[i].Obj);
                    list.RemoveAt(i);
                }
                break;
            }
        }
    }

    /// <summary>
    /// 释放某个对象池里面的所有物体
    /// </summary>
    /// <param name="poolName">所属对象池</param>
    /// <param name="needDes">是否需要销毁</param>
    public void Free(string poolName,bool needDes = false)
    {
        if(!m_dict.ContainsKey(poolName))
            return;
        List<PrefabInfo> list = m_dict[poolName];
        for(int i = list.Count - 1; i >= 0; i--)
        {
            if(!needDes)
            {
                list[i].CanUse = true;
                m_poolUtil.AddToPool(poolName,list[i].Obj);
            }
            else
            {
                if(list[i].AsstType != AssetType.None)
                    ResManager.Instance.ReleaseAsset(list[i].AsstType,poolName);
                GameObject.DestroyImmediate(list[i].Obj);
                list.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// 释放某个对象池里面的所有物体 并且销毁
    /// </summary>
    public void FreeAll()
    {
        List<string> list = new List<string>();
        foreach(var item in m_dict)
        {
            list.Add(item.Key);
        }
        for(int i = 0; i < list.Count; i++)
        {
            Free(list[i],true);
        }
        m_dict.Clear();
    }


    public void AddChild(GameObject obj)
    {
        m_poolUtil.AddChild(obj);
    }
}

