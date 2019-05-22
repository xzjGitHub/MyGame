
//--------------------------------------------------------------
//Creator： xzj
//Data：    5/20/2019
//Note:     
//--------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using Res;

/// <summary>
/// 
/// </summary>
public class TestRes: MonoBehaviour
{
    public Image Im;
    public Image Im1;
    private void Start()
    {
        //ResManager.Instance.GetAssetAsync<Sprite>(AssetType.Sprite,"九宫格1",true,(Object obj) =>
        //{
        //    Sprite sp = obj as Sprite;
        //    Im.sprite = sp;
        //    Im.SetNativeSize();
        //},(string msg) =>
        //{
        //    Debug.LogError(msg);
        //});
    }

    public void Update()
    {
        ResManager.Instance.Update();
    }

    public void Click()
    {
        ResManager.Instance.ReleaseAsset(AssetType.Sprite,"九宫格2");
    }

    public void Click1()
    {
        Im1.sprite = ResManager.Instance.GetAssetSync<Sprite>(AssetType.Sprite,"九宫格2");
    }
}
