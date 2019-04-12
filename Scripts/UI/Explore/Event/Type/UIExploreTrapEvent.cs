using GameEventDispose;
using Spine.Unity;
using UnityEngine;

/// <summary>
/// 陷阱事件
/// </summary>
public class UIExploreTrapEvent : UIExploreEventBase
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
        gameObject.SetActive(false);
        Init(param as EventAttribute);
        gameObject.SetActive(true);
        //
        if (isAutoVisit) return;
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
        OnProgressing = OnCallProgressing;
        OnFade = OnCallFade;
        //
        OnOpenShow = OnCallOpenShow;

        //
        isFirst = true;
        ShowTranS.gameObject.SetActive(false);
    }

    /// <summary>
    /// 访问中
    /// </summary>
    private void OnCallProgressing(object param)
    {
        button.onClick.RemoveAllListeners();
        if (isAutoVisit) return;
        button.onClick.AddListener(CancelVisit);
        buttonImage.raycastTarget = true;
    }

    private void OnCallOpenShow(object param)
    {

        if (isAutoVisit)
        {
            OnEventProgressAchieve(null);
            return;
        }
        iconSkeleton.transform.parent.gameObject.SetActive(true);
        iconSkeleton.AnimationState.SetAnimation(0, bxName1Str, true);
        Scale();
        FadeFadeIn(iconSkeleton, false, false);
        OnFadeOk = OnCallFadeOk;
        buttonImage.raycastTarget = true;
        OnClickOpenOption(null);
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
        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.VisitEvent, (object)eventAttribute);
        buttonImage.raycastTarget = false;
        wpVisitEventResult = ExploreSystem.Instance.VisitEvent(eventAttribute.waypointId, eventIndex, WPEventVisitType.Normal);
        VisitSucceed(wpVisitEventResult);
    }

    /// <summary>
    /// 访问成功
    /// </summary>
    private void VisitSucceed(WPVisitEventResult _result)
    {
        //打开宝箱
        particleSystemAlpha.StartUpdate();
        iconSkeleton.AnimationState.SetAnimation(0, bxName2Str, false);
        if (heightLightEffect != null) heightLightEffect.gameObject.SetActive(false);
        buttonImage.raycastTarget = false;
        if (Text != null) Text.gameObject.SetActive(false);
        //
        LoadItemReward(_result);

        //访问成功损失生命
        if (_result.visitResult != WPEventVisitResult.Success) return;
        TeamSystem.Instance.EventVisitHPCost(eventAttribute.VisitHPCost, out _result.charCostHP);
        TeamSystem.Instance.CharCostHP_UI(wpVisitEventResult.charCostHP);
    }


    private void CancelVisit()
    {
        buttonImage.raycastTarget = false;
        ExploreSystem.Instance.VisitEvent(eventAttribute.waypointId, eventIndex, WPEventVisitType.Abandon);
        if (eventProgress != null) eventProgress.StopOpen();
        iconSkeleton.AnimationState.SetAnimation(0, bxName1Str, true);
        FadeFadeIn(iconSkeleton);
        base.VisitEventEnd();
    }

    //
    private bool isFirst;


}
