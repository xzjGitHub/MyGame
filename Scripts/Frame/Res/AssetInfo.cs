
namespace Res
{
    /// <summary>
    /// 资源信息
    /// </summary>
    public class AssetInfo
    {
        //资源
        private UnityEngine.Object m_asset;
        public UnityEngine.Object Asset { get { return m_asset; } }

        //资源名
        private string m_assetName;
        public string AssetName { get { return m_assetName; } }

        //资源类型
        private AssetType m_assetType;
        public AssetType AssetType { get { return m_assetType; } }

        //引用计数
        private int m_refCount;
        public int RefCount { get { return m_refCount; } }

        public AssetInfo(AssetType type,string name,UnityEngine.Object asset)
        {
            m_asset = asset;
            m_assetType = type;
            m_assetName = name;
            m_refCount = 1; ;
        }

        //增加引用
        public void AddRef()
        {
            m_refCount++;
        }

        //减少
        public void SubRef()
        {
            m_refCount--;
        }

        /// <summary>
        /// 是否可以被Unload
        /// </summary>
        /// <returns></returns>
        public bool CanUnLoad()
        {
            return m_assetType == AssetType.Sprite || m_assetType == AssetType.Audio;
        }
    }
}