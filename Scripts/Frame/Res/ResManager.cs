using System.Collections.Generic;
using UnityEngine;

namespace Res
{
    public class ResManager: Singleton<ResManager>
    {
        //[资源类型，[资源名称，资源]]
        private Dictionary<AssetType,Dictionary<string,AssetInfo>> m_allAsset;

        //加载队列
        private Queue<RequestInfo> m_loadQuene;

        //当前正在加载的
        private RequestInfo m_currentLoad;

        private IAssetLoader m_assetLoader;

        private ResManager()
        {
            m_allAsset = new Dictionary<AssetType,Dictionary<string,AssetInfo>>();
            m_loadQuene = new Queue<RequestInfo>();
            m_assetLoader = new DefaultLoader();
        }

        /// <summary>
        /// 设置加载器 可自定义
        /// </summary>
        /// <param name="loader"></param>
        public void SetLoader(IAssetLoader loader)
        {
            m_assetLoader = loader;
        }

        /// <summary>
        /// 添加到缓存里面
        /// </summary>
        /// <param name="asstInfo"></param>
        private void AddToCache(AssetInfo asstInfo)
        {
            Dictionary<string,AssetInfo> dict = null;
            if(!m_allAsset.ContainsKey(asstInfo.AssetType))
            {
                dict = new Dictionary<string,AssetInfo>();
                m_allAsset.Add(asstInfo.AssetType,dict);
            }
            else
            {
                dict = m_allAsset[asstInfo.AssetType];
            }
            if(!dict.ContainsKey(asstInfo.AssetName))
            {
                dict.Add(asstInfo.AssetName,asstInfo);
            }
        }

        /// <summary>
        /// 从缓存里面获取资源
        /// </summary>
        /// <param name="assetType">资源类型</param>
        /// <param name="assetName">资源名称</param>
        /// <returns></returns>
        private AssetInfo GetAssetCache(AssetType assetType,string assetName)
        {
            AssetInfo assetInfo = null;
            if(m_allAsset.ContainsKey(assetType))
            {
                if(m_allAsset[assetType].ContainsKey(assetName))
                {
                    assetInfo = m_allAsset[assetType][assetName];
                    assetInfo.AddRef();
                }
            }
            return assetInfo;
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <param name="assetType"></param>
        /// <param name="assetName"></param>
        /// <param name="cach"></param>
        /// <returns></returns>
        private AssetInfo LoadAsset(AssetType assetType,string assetName,bool cach)
        {
            AssetInfo assetInfo = m_assetLoader.LoadAsset(assetType,assetName);
            if(cach)
            {
                AddToCache(assetInfo);
            }
            return assetInfo;
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="assetType"></param>
        /// <param name="assetName"></param>
        /// <param name="cach"></param>
        /// <param name="sucess"></param>
        /// <param name="failure"></param>
        private RequestInfo LoadAssetAsyc<T>(AssetType assetType,string assetName,bool cach
            ,LoadSucess sucess,LoadFailure failure,string uid) where T : Object
        {
            ResourceRequest request = m_assetLoader.LoadAssetAsync<T>(assetType,assetName);
            RequestInfo info = new RequestInfo(request,assetType,assetName,cach,sucess,failure,uid);
            m_loadQuene.Enqueue(info);
            return info;
        }

        /// <summary>
        /// 同步获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetType">资源类型</param>
        /// <param name="assetName">资源名称</param>
        /// <param name="cach">是否缓存</param>
        /// <returns></returns>
        public T GetAssetSync<T>(AssetType assetType,string assetName,bool cach = true) where T : Object
        {
            AssetInfo assetInfo = GetAssetCache(assetType,assetName);
            if(assetInfo == null)
                assetInfo = LoadAsset(assetType,assetName,cach);
            if(assetInfo != null)
                return assetInfo.Asset as T;
            return null;
        }

        /// <summary>
        /// 异步获取
        /// </summary>
        /// <param name="assetType">资源类型</param>
        /// <param name="assetName">资源名称</param>
        /// <param name="cach">是否缓存</param>
        /// <param name="sucess">加载成功回调</param>
        /// <param name="failure">加载失败回调</param>
        public RequestInfo GetAssetAsync<T>(AssetType assetType,string assetName,bool cach,string uid
            ,LoadSucess sucess,LoadFailure failure = null) where T : Object
        {
            RequestInfo info = null;
            AssetInfo assetInfo = GetAssetCache(assetType,assetName);
            if(assetInfo != null)
            {
                sucess(assetInfo.Asset,uid);
            }
            else
            {
                info = LoadAssetAsyc<T>(assetType,assetName,cach,sucess,failure,uid);
            }
            return info;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="assetType"></param>
        /// <param name="assetName"></param>
        public void ReleaseAsset(AssetType assetType,string assetName)
        {
            if(!m_allAsset.ContainsKey(assetType))
                return;
            if(!m_allAsset[assetType].ContainsKey(assetName))
                return;
            AssetInfo assetInfo = m_allAsset[assetType][assetName];
            assetInfo.SubRef();
            if(assetInfo.RefCount <= 0)
            {
                m_allAsset[assetType].Remove(assetName);
                if(m_allAsset[assetType].Count <= 0)
                {
                    m_allAsset.Remove(assetType);
                }
                if(assetInfo.CanUnLoad())
                {
                    Resources.UnloadAsset(assetInfo.Asset);
                }
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            if(m_currentLoad != null)
            {
                if(m_currentLoad.IsDone)
                {
                    HandleLoadDone();
                }
            }
            else
            {
                m_currentLoad = m_loadQuene.Count > 0 ? m_loadQuene.Dequeue() : null;
            }
        }

        /// <summary>
        /// 处理加载完成
        /// </summary>
        private void HandleLoadDone()
        {
            if(m_currentLoad.Asset == null)
            {
                if(m_currentLoad.LoadFailureCallBack != null)
                {
                    string errorMsg = string.Format("Load Res Error, ResType : {0},  ResName : {1}",m_currentLoad.Type,m_currentLoad.AssetName);
                    m_currentLoad.LoadFailureCallBack(errorMsg);
                    m_currentLoad = null;
                }
            }
            else
            {
                if(m_currentLoad.Cache)
                {
                    AssetInfo asset = new AssetInfo(m_currentLoad.Type,m_currentLoad.AssetName,m_currentLoad.Asset);
                    AddToCache(asset);
                }
                if(m_currentLoad.LoadSucessCallBack != null)
                {
                    m_currentLoad.LoadSucessCallBack(m_currentLoad.Asset,m_currentLoad.UID);
                    m_currentLoad = null;
                }
            }
        }
    }
}
