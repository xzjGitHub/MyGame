using Spine.Unity;
using UnityEngine;

/// <summary>
/// 基本事件
/// </summary>
public class UIExploreBaseEvent : UIExploreEventBase
{

    private void Awake()
    {
        OnOpenUI = OpenUI;
    }

    /// <summary>
    /// 打开UI
    /// </summary>
    private void OpenUI(object param)
    {
        Init(param as EventAttribute);
        iconSkeleton.AnimationState.SetAnimation(0, bxName1Str, true);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init(EventAttribute _eventAttribute)
    {
        if (isFirst) return;
        //    
        base.BaseInit(_eventAttribute);
        //
        OnProgressAchieve = OnEventProgressAchieve;
        OnFade = OnCallFade;
 
        //
        OnOpenShow = OnCallOpenShow;
        //
        isFirst = true;
        //
      //  Scale(1);
      //  FadeFadeIn(iconSkeleton, false, true);
    }

    private void OnCallOpenShow(object param)
    {
        //  particleSystemAlpha.ResetAlpha();
        iconSkeleton.transform.parent.gameObject.SetActive(true);
        iconSkeleton.AnimationState.SetAnimation(0, bxName1Str, true);
        Scale();
    //    FadeFadeIn(iconSkeleton, false, false);
        OnFadeOk = OnCallFadeOk;
    }
    private void OnCallFadeOk(object param)
    {
        iconSkeleton.gameObject.AddComponent<cakeslice.Outline>();
    }

    private void OnCallFade(object param)
    {
        base.FadeFadeIn(iconSkeleton);
    }

    /// <summary>
    /// 事件进度完成
    /// </summary>
    private void OnEventProgressAchieve(object param)
    {
        VisitSucceed(wpVisitEventResult);
    }

    /// <summary>
    /// 访问成功
    /// </summary>
    private void VisitSucceed(WPVisitEventResult _resul)
    {
        //先看是否有物品 得到物品名字列表
        LoadItemReward(_resul);
        //打开宝箱
        particleSystemAlpha.StartUpdate();
        iconSkeleton.AnimationState.SetAnimation(0, bxName2Str, false);
        if (heightLightEffect != null) heightLightEffect.gameObject.SetActive(false);
        if (Text != null) Text.gameObject.SetActive(false);
    }

    //
    private bool isFirst;


}
