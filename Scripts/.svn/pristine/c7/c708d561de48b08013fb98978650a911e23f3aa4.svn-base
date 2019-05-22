
//--------------------------------------------------------------
//Creator： XZJ
//Data：    2019/5/20 15:48:20
//Note:     
//--------------------------------------------------------------

using UnityEngine;

namespace Res
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultLoader: IAssetLoader
    {
        public AssetInfo LoadAsset(AssetType assetType,string assetName)
        {
            UnityEngine.Object obj = null;
            switch(assetType)
            {
                case AssetType.Effect:
                case AssetType.Panel:
                case AssetType.Prefab:
                case AssetType.UIChar:
                    obj = Resources.Load<GameObject>(AssetPath.GetAssetPath(assetType) + assetName);
                    break;
                case AssetType.Audio:
                    obj = Resources.Load<AudioClip>(AssetPath.GetAssetPath(assetType) + assetName);
                    break;
                case AssetType.Sprite:
                    obj = Resources.Load<Sprite>(AssetPath.GetAssetPath(assetType) + assetName);
                    break;
            }
            if(obj == null)
            {
                Debug.LogError("LoadRes Eoor,the Res is Null");
                return null;
            }
            AssetInfo assetInfo = new AssetInfo(assetType,assetName,obj);
            return assetInfo;
        }

        public ResourceRequest LoadAssetAsync<T>(AssetType assetType,string assetName) where T : UnityEngine.Object
        {
            ResourceRequest request = Resources.LoadAsync<T>(AssetPath.GetAssetPath(assetType) + assetName);
            return request;
        }
    }
}
