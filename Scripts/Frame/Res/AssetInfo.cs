
namespace Res
{
    /// <summary>
    /// ��Դ��Ϣ
    /// </summary>
    public class AssetInfo
    {
        //��Դ
        private UnityEngine.Object m_asset;
        public UnityEngine.Object Asset { get { return m_asset; } }

        //��Դ��
        private string m_assetName;
        public string AssetName { get { return m_assetName; } }

        //��Դ����
        private AssetType m_assetType;
        public AssetType AssetType { get { return m_assetType; } }

        //���ü���
        private int m_refCount;
        public int RefCount { get { return m_refCount; } }

        public AssetInfo(AssetType type,string name,UnityEngine.Object asset)
        {
            m_asset = asset;
            m_assetType = type;
            m_assetName = name;
            m_refCount = 1; ;
        }

        //��������
        public void AddRef()
        {
            m_refCount++;
        }

        //����
        public void SubRef()
        {
            m_refCount--;
        }

        /// <summary>
        /// �Ƿ���Ա�Unload
        /// </summary>
        /// <returns></returns>
        public bool CanUnLoad()
        {
            return m_assetType == AssetType.Sprite || m_assetType == AssetType.Audio;
        }
    }
}