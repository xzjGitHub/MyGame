using UnityEngine;


public delegate void LoadSucess(Object asset,string uid);
public delegate void LoadFailure(string info);

namespace Res
{
    public class RequestInfo
    {
        //�첽���ط���
        private ResourceRequest m_request;
        public ResourceRequest Request { get { return m_request; } }

        //��Դ����
        private AssetType m_type;
        public AssetType Type { get { return m_type; } }

        //��Դ����
        private string m_assetName;
        public string AssetName { get { return m_assetName; } }

        //uid
        private string m_uid;
        public string UID { get { return m_uid; } }

        //�Ƿ񻺴�
        private bool m_cache;
        public bool Cache { get { return m_cache; } }

        //���سɹ��ص�
        private LoadSucess m_loadSuc;
        public LoadSucess LoadSucessCallBack { get { return m_loadSuc; } }

        //����ʧ�ܻص�
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
        /// �Ƿ�������
        /// </summary>
        public bool IsDone
        {
            get
            {
                return Request != null && Request.isDone;
            }
        }


        /// <summary>
        /// ��ȡ���ص���Դ
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