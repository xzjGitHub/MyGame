using System.Collections.Generic;
using UnityEngine;

namespace Res
{
    public class ResManager: Singleton<ResManager>
    {
        //[��Դ���ͣ�[��Դ���ƣ���Դ]]
        private Dictionary<AssetType,Dictionary<string,AssetInfo>> m_allAsset;

        //���ض���
        private Queue<RequestInfo> m_loadQuene;

        //��ǰ���ڼ��ص�
        private RequestInfo m_currentLoad;

        private IAssetLoader m_assetLoader;

        private ResManager()
        {
            m_allAsset = new Dictionary<AssetType,Dictionary<string,AssetInfo>>();
            m_loadQuene = new Queue<RequestInfo>();
            m_assetLoader = new DefaultLoader();
        }

        /// <summary>
        /// ���ü����� ���Զ���
        /// </summary>
        /// <param name="loader"></param>
        public void SetLoader(IAssetLoader loader)
        {
            m_assetLoader = loader;
        }

        /// <summary>
        /// ��ӵ���������
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
        /// �ӻ��������ȡ��Դ
        /// </summary>
        /// <param name="assetType">��Դ����</param>
        /// <param name="assetName">��Դ����</param>
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
        /// ͬ��������Դ
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
        /// �첽������Դ
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
        /// ͬ����ȡ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetType">��Դ����</param>
        /// <param name="assetName">��Դ����</param>
        /// <param name="cach">�Ƿ񻺴�</param>
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
        /// �첽��ȡ
        /// </summary>
        /// <param name="assetType">��Դ����</param>
        /// <param name="assetName">��Դ����</param>
        /// <param name="cach">�Ƿ񻺴�</param>
        /// <param name="sucess">���سɹ��ص�</param>
        /// <param name="failure">����ʧ�ܻص�</param>
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
        /// �ͷ���Դ
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
        /// ����
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
        /// ����������
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
