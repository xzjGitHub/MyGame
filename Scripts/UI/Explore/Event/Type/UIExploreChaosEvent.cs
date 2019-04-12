using Spine.Unity;
using UnityEngine;


/// <summary>
/// 混沌事件
/// </summary>
public class UIExploreChaosEvent : UIExploreEventBase
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
        iconSkeleton.AnimationState.SetAnimation(0, bxName1Str, true);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init(EventAttribute _eventAttribute)
    {
        if (isFirst) return;
        base.BaseInit(_eventAttribute);
      //  button.onClick.AddListener(OnClickButton);
        //
        OnProgressAchieve = OnEventProgressAchieve;
        OnFade = OnCallFad;
        OnOpenShow = OnCallOpenShow;

        //
        isFirst = true;
        //
     //   Scale(1);
      //  FadeFadeIn(iconSkeleton, false, true);
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
        LoadItemReward(_resul);
        //
        if (heightLightEffect != null) heightLightEffect.gameObject.SetActive(false);
        if (Text != null) Text.gameObject.SetActive(false);
        //
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
}
