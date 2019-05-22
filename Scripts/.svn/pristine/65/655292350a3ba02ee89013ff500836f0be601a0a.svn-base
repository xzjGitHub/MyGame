
//--------------------------------------------------------------
//Creator： XZJ
//Data：    2019/5/20 15:51:52
//Note:     
//--------------------------------------------------------------


namespace Res
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResManager
    {
        /// <summary>
        /// 设置加载器
        /// </summary>
        /// <param name="loader"></param>
        void SetLoader(IAssetLoader loader);

        /// <summary>
        /// 添加到缓存
        /// </summary>
        /// <param name="asstInfo"></param>
        void AddToCache(AssetInfo asstInfo);

        /// <summary>
        /// 从缓存中获取
        /// </summary>
        /// <param name="assetType"></param>
        /// <param name="assetName"></param>
        /// <returns></returns>
        AssetInfo GetAssetCache(AssetType assetType,string assetName);

        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <param name="assetType"></param>
        /// <param name="assetName"></param>
        /// <param name="cach"></param>
        /// <returns></returns>
        AssetInfo LoadAsset(AssetType assetType,string assetName,bool cach);

        /// <summary>
        /// 异步获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetType"></param>
        /// <param name="assetName"></param>
        /// <param name="cach"></param>
        /// <param name="sucess"></param>
        /// <param name="failure"></param>
        void LoadAssetAsyc<T>(AssetType assetType,string assetName,bool cach
           ,LoadSucess sucess,LoadFailure failure) where T : UnityEngine.Object;

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="assetType"></param>
        /// <param name="assetName"></param>
        void ReleaseAsset(AssetType assetType,string assetName);

        /// <summary>
        /// 更新
        /// </summary>
        void Update();

        /// <summary>
        ///加载完成处理
        /// </summary>
        void HandleLoadDone();
    }
}
