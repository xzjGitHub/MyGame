using Spine.Unity;
using UnityEngine;

/// <summary>
/// 草丛事件
/// </summary>
public class UIExploreGrassEvent : UIExploreEventBase
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
    }

    private void OnCallOpenShow(object param)
    {
        //  particleSystemAlpha.ResetAlpha();
        //iconSkeleton.transform.parent.gameObject.SetActive(true);
        //iconSkeleton.AnimationState.SetAnimation(0, bxName1Str, true);
        //Scale();
        //Fade(iconSkeleton, false, false);
        //OnFadeOk = OnCallFadeOk;
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
        //打开宝箱
        particleSystemAlpha.StartUpdate();
        iconSkeleton.AnimationState.SetAnimation(0, bxName2Str, false);
        if (heightLightEffect != null) heightLightEffect.gameObject.SetActive(false);
        if (Text != null) Text.gameObject.SetActive(false);
    }

    /// <summary>
    /// 访问成功
    /// </summary>
    private void VisitSucceed(WPVisitEventResult _result)
    {
        LoadItemReward(_result);
    }
    //
    private bool isFirst;

}
