using UnityEngine;


/// <summary>
/// 献祭事件
/// </summary>
public class UIExploreSacrificeEvent : UIExploreEventBase
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
        //
        visitIndex = 0;
        //
        particleSystemAlpha.StartUpdate();
        //
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
        OnFade = OnCallFad;
        OnOpenShow = OnCallOpenShow;

        //
        isFirst = true;
    }
    private void OnCallOpenShow(object param)
    {
        Scale();
        FadeFadeIn(iconSkeleton, false, false);
        OnFadeOk = OnCallFadeOk;
    }
    private void OnCallFadeOk(object param)
    {
        // iconSkeleton.gameObject.AddComponent<cakeslice.Outline>();
    }
    private void OnCallFad(object param)
    {
        base.FadeFadeIn(iconSkeleton);
    }
    /// <summary>
    /// 访问成功
    /// </summary>
    private void VisitSucceed(WPVisitEventResult _resul)
    {
        LoadItemReward(_resul);
        //打开宝箱
        particleSystemAlpha.StartUpdate();
        iconSkeleton.AnimationState.SetAnimation(0, bxName2Str, false);
    }


    /// <summary>
    /// 事件进度完成
    /// </summary>
    private void OnEventProgressAchieve(object param)
    {
        VisitSucceed(wpVisitEventResult);
    }


    //
    private bool isFirst;
    //
    private int visitIndex;
}
