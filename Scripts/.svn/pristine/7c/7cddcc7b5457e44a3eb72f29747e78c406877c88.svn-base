
//--------------------------------------------------------------
//Creator： XZJ
//Data：    2019/5/20 15:45:38
//Note:     
//--------------------------------------------------------------

namespace Res
{
    public interface IAssetLoader
    {
        /// <summary>
        /// 同步加载
        /// </summary>
        /// <param name="assetType"></param>
        /// <param name="assetName"></param>
        /// <returns></returns>
        AssetInfo LoadAsset(AssetType assetType,string assetName);

        /// <summary>
        /// 异步加载
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetType"></param>
        /// <param name="assetName"></param>
        /// <returns></returns>
        UnityEngine.ResourceRequest LoadAssetAsync<T>(AssetType assetType,string assetName) where T : UnityEngine.Object;
    }
}
