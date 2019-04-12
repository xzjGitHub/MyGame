using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;


public class SpineTest1 : MonoBehaviour
{
    public Sprite newSpr;
    public bool isUpdate = false;
    public string soltName = "wuqi2";
    //
    private SkeletonAnimation skeletonAnimation;

    void Start()
    {
        //读取路径下spine的SkeletonData文件
        //var yourSkeletonDataAsset = Resources.Load<SkeletonDataAsset>("spineboy/spineboy_SkeletonData");
        //SkeletonAnimation.AddToGameObject(this.gameObject, yourSkeletonDataAsset);
        skeletonAnimation = this.gameObject.GetComponent<SkeletonAnimation>();
        //播放动画
        // skeletonAnimation.timeScale = 1.0f;
        //  skeletonAnimation.loop = true;

        //要替换的图片，也可以读取路径加载
     //   newSpr = transform.parent.GetComponent<Image>().sprite;
        newSpr = ResourceLoadUtil.LoadRes<Sprite>("wuqi");
        SkeletonTool.SpineReloading(skeletonAnimation, soltName, newSpr);
    }

    private void Update()
    {
        if (!isUpdate) return;
        isUpdate = false;
        SkeletonTool.SpineReloading(skeletonAnimation, soltName, newSpr);
    }


}