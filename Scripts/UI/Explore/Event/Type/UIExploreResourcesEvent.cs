using Spine.Unity;
using UnityEngine;


/// <summary>
/// 资源事件
/// </summary>
public class UIExploreResourcesEvent : UIExploreEventBase
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
        //
        base.BaseInit(_eventAttribute);
        //
        //  button.onClick.AddListener(OnClickButton);
        //
        OnProgressAchieve = OnEventProgressAchieve;
        OnFade = OnCallFad;
        OnOpenShow = OnCallOpenShow;

        //
        isFirst = true;
        //
      //  Scale(1);
     //   FadeFadeIn(iconSkeleton, false, true);
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
        visitIndex++;
        LoadItemReward(_resul);
        //
        particleSystemAlpha.StartUpdate();
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

    /// <summary>
    /// 重置事件
    /// </summary>
    private void ResetEvent()
    {
        if (!itemLaunchPosition.IsPlayEnd) return;
        if (visitIndex == visitMaxNum)
        {
            Destroy(gameObject);
            return;
        }
        //
        itemLaunchPosition.Reset();
        topEffectGameObject.SetActive(true);
      //  particleSystemAlpha.ResetAlpha();
        //
        iconSkeleton.AnimationState.SetAnimation(0, bxName1Str, true);
    }
    //
    private bool isFirst;
    //
    private int visitIndex;
    private const int visitMaxNum = 1;

}
