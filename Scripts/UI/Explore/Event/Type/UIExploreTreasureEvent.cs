using UnityEngine;

/// <summary>
/// 探索宝藏事件
/// </summary>
public class UIExploreTreasureEvent : UIExploreEventBase
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
        particleSystemAlpha.StartUpdate();
        //
        iconSkeleton.AnimationState.SetAnimation(0, bxName1Str, true);
    }

    /// <summary>
    ///初始化
    /// </summary>
    private void Init(EventAttribute _eventAttribute)
    {
        if (isFirst)
        {
            return;
        }
        //
        base.BaseInit(_eventAttribute);
        //  button.onClick.AddListener(OnClickButton);
        OnProgressAchieve = OnEventProgressAchieve;
        OnFade = OnCallFad;
        OnOpenShow = OnCallOpenShow;

        //
        isFirst = true;
        //
        //Scale(1);
        //FadeFadeIn(iconSkeleton, false, true);
    }
    private void OnCallOpenShow(object param)
    {
        Scale();
        FadeFadeIn(iconSkeleton, false, false);
        OnFadeOk = OnCallFadeOk;
    }
    private void OnCallFadeOk(object param)
    {
        iconSkeleton.gameObject.AddComponent<cakeslice.Outline>();
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
        //先看是否有物品 得到物品名字列表
        LoadItemReward(_resul);
        //
        iconSkeleton.AnimationState.SetAnimation(0, bxName2Str, false);
    }
    /// <summary>
    /// 访问失败
    /// </summary>
    private void VisitFailed(WPVisitEventResult _resul)
    {
        iconSkeleton.AnimationState.SetAnimation(0, bxName3Str, false);
        //更新粒子渐隐
        base.VisitEventEnd();
    }

    /// <summary>
    /// 事件进度完成
    /// </summary>
    private void OnEventProgressAchieve(object param)
    {
        particleSystemAlpha.StartUpdate();

        if (wpVisitEventResult.isSucceed)
        {
            VisitSucceed(wpVisitEventResult);
        }
        else
        {
            VisitFailed(wpVisitEventResult);
        }
    }


    private void OnDestroy()
    {
    }

    //
    private bool isFirst;
}
