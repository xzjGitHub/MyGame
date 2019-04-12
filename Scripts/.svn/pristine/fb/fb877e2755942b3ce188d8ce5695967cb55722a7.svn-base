using System.Collections.Generic;
using UnityEngine;

public class GameObjectInfo
{
    public int InstanceId;
    public string Name;     //释放当个的时候要用
    public GameObject Obj;
    public GameObjectInfo(string s,GameObject obj)
    {
        Name = s;
        Obj = obj;
        InstanceId = Obj.GetInstanceID();
    }
}

public class PoolInfo
{
    public List<GameObjectInfo> m_usingList;
    public List<GameObjectInfo> m_freeList;

    public PoolInfo()
    {
        m_usingList = new List<GameObjectInfo>();
        m_freeList = new List<GameObjectInfo>();
    }
}

public class GameObjectPool: Singleton<GameObjectPool>
{
    private GameObject m_poolParent;
    private GameObjectPool()
    {
        m_poolParent = new GameObject("[GameObjectPool]");
        GameObject.DontDestroyOnLoad(m_poolParent);
    }

    private Dictionary<string,PoolInfo> m_pool = new Dictionary<string,PoolInfo>();

    /// <summary>
    /// 获取一个物体 需要自己传一个预制过来 
    /// </summary>
    /// <param name="name">物体所在分类 即key</param>
    /// <param name="prefab">预制体</param>
    ///  <param objName="prefab">需要释放当个物体的时候传 默认不要传 要确保唯一行</param>
    /// <returns></returns>
    public GameObject GetObject(string poolName,GameObject prefab,string objName = "")
    {
        if(!m_pool.ContainsKey(poolName))
        {
            m_pool[poolName] = new PoolInfo();
        }

        PoolInfo poolInfo = m_pool[poolName];
        if(poolInfo.m_freeList.Count > 0)
        {
            GameObjectInfo info = poolInfo.m_freeList[0];
            info.Name = objName;
            poolInfo.m_freeList.RemoveAt(0);
            poolInfo.m_usingList.Add(info);
            return info.Obj;
        }
        else
        {
            GameObject obj = GameObject.Instantiate(prefab) as GameObject;
            poolInfo.m_usingList.Add(new GameObjectInfo(objName,obj));
            return obj;
        }
    }

    /// <summary>
    /// 通过路径加载
    /// </summary>
    /// <param name="poolName"></param>
    /// <param name="path"></param>
    /// <param name="objName"></param>
    /// <returns></returns>
    public GameObject GetObjectByPrefabPath(string poolName,string path,
        string objName = "")
    {
        if(!m_pool.ContainsKey(poolName))
        {
            m_pool[poolName] = new PoolInfo();
        }

        PoolInfo poolInfo = m_pool[poolName];
        if(poolInfo.m_freeList.Count > 0)
        {
            GameObjectInfo info = poolInfo.m_freeList[0];
            info.Name = objName;
            poolInfo.m_freeList.RemoveAt(0);
            poolInfo.m_usingList.Add(info);
            return info.Obj;
        }
        else
        {
            GameObject temp = ResourceLoadUtil.LoadPrefab(path);
            poolInfo.m_usingList.Add(new GameObjectInfo(objName,temp));
            return temp;
        }
    }

    /// <summary>
    /// 隐藏 并没有销毁 释放整个pool
    /// </summary>
    /// <param name="name"></param>
    public void FreePool(string name)
    {
        if(!m_pool.ContainsKey(name))
        {
            return;
        }

        PoolInfo poolInfo = m_pool[name];
        for(int i = 0; i < poolInfo.m_usingList.Count; i++)
        {
            poolInfo.m_usingList[i].Obj.SetActive(false);
            poolInfo.m_freeList.Add(poolInfo.m_usingList[i]);
            AddChild(poolInfo.m_usingList[i].Obj);
        }
        poolInfo.m_usingList.Clear();
    }

    /// <summary>
    /// 释放当个物体
    /// </summary>
    /// <param name="poolName"></param>
    /// <param name="objName"></param>
    public void FreeGameObjectByName(string poolName,string objName)
    {
        if(!m_pool.ContainsKey(poolName))
        {
            return;
        }
        PoolInfo poolInfo = m_pool[poolName];
        GameObjectInfo gameObjectInfo = poolInfo.m_usingList.Find(a => a.Name == objName);
        poolInfo.m_usingList.Remove(gameObjectInfo);
        poolInfo.m_freeList.Add(gameObjectInfo);
        AddChild(gameObjectInfo.Obj);
    }

    /// <summary>
    /// 释放当个物体
    /// </summary>
    /// <param name="poolName"></param>
    /// <param name="obj"></param>
    public void FreeGameObjectByObj(string poolName,GameObject obj,bool needDis=false)
    {
        if(!m_pool.ContainsKey(poolName))
        {
            return;
        }
        PoolInfo poolInfo = m_pool[poolName];
        GameObjectInfo gameObjectInfo = poolInfo.m_usingList.
            Find(a => a.InstanceId == obj.GetInstanceID());
        poolInfo.m_usingList.Remove(gameObjectInfo);
        if (!needDis)
        {
            poolInfo.m_freeList.Add(gameObjectInfo);
            AddChild(gameObjectInfo.Obj);
        }
    }

    /// <summary>
    /// 释放某一个 并销毁
    /// </summary>
    /// <param name="name"></param>
    public void DestroyPoolByPoolName(string name)
    {
        if(!m_pool.ContainsKey(name))
        {
            return;
        }
        PoolInfo poolInfo = m_pool[name];
        for(int i = 0; i < poolInfo.m_usingList.Count; i++)
        {
            Object.DestroyImmediate(poolInfo.m_usingList[i].Obj);
        }
        poolInfo.m_usingList.Clear();

        for(int i = 0; i < poolInfo.m_freeList.Count; i++)
        {
            Object.DestroyImmediate(poolInfo.m_freeList[i].Obj);
        }
        poolInfo.m_freeList.Clear();
        m_pool.Remove(name);
    }

    /// <summary>
    /// 释放所有
    /// </summary>
    public void DeatroyAllPool()
    {
        List<string> keys = new List<string>();
        keys.AddRange(m_pool.Keys);
        for(int i = 0; i < keys.Count; i++)
        {
            DestroyPoolByPoolName(keys[i]);
        }
        m_pool.Clear();
    }

    public void AddChild(GameObject child)
    {
        if(child != null && m_poolParent != null)
            Utility.SetParent(child,m_poolParent,false);
    }
}

