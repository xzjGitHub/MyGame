using UnityEngine;


public delegate void LoadSucess(Object asset,string uid);
public delegate void LoadFailure(string info);

namespace Res
{
    public class RequestInfo
    {
        //异步加载返回
        private ResourceRequest m_request;
        public ResourceRequest Request { get { return m_request; } }

        //资源类型
        private AssetType m_type;
        public AssetType Type { get { return m_type; } }

        //资源名称
        private string m_assetName;
        public string AssetName { get { return m_assetName; } }

        //uid
        private string m_uid;
        public string UID { get { return m_uid; } }

        //是否缓存
        private bool m_cache;
        public bool Cache { get { return m_cache; } }

        //加载成功回调
        private LoadSucess m_loadSuc;
        public LoadSucess LoadSucessCallBack { get { return m_loadSuc; } }

        //加载失败回调
        private LoadFailure m_loadFail;
        public LoadFailure LoadFailureCallBack { get { return m_loadFail; } }

        public RequestInfo(ResourceRequest re,AssetType assetType,string assetName,bool cach,
            LoadSucess suc,LoadFailure fail,string uid)
        {
            m_request = re;
            m_type = assetType;
            m_assetName = assetName;
            m_cache = cach;
            m_loadSuc = suc;
            m_loadFail = fail;
            m_uid = uid;
        }

        /// <summary>
        /// 是否加载完成
        /// </summary>
        public bool IsDone
        {
            get
            {
                return Request != null && Request.isDone;
            }
        }


        /// <summary>
        /// 获取加载的资源
        /// </summary>
        public Object Asset
        {
            get
            {
                return Request != null ? Request.asset : null;
            }
        }
    }
}