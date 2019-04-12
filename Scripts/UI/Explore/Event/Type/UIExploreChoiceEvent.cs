using UnityEngine;


/// <summary>
/// 抉择事件
/// </summary>
public class UIExploreChoiceEvent : UIExploreEventBase
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
        //
        base.BaseInit(_eventAttribute);
        //
        // button.onClick.AddListener(OnClickButton);
        //
        OnProgressAchieve = OnEventProgressAchieve;
        OnFade = OnCallFad;
        OnOpenPrepare = OnCallOpenPrepare;
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
        //打开宝箱
        particleSystemAlpha.StartUpdate();
        iconSkeleton.AnimationState.SetAnimation(0, bxName2Str, false);
    }



    /// <summary>
    /// 事件进度完成
    /// </summary>
    private void OnEventProgressAchieve(object param)
    {
      //  iconSkeleton.gameObject.SetActive(false);
        VisitSucceed(wpVisitEventResult);
    }
    /// <summary>
    /// 事件进度完成
    /// </summary>
    private void OnCallOpenPrepare(object param)
    {
        //   iconSkeleton.AnimationState.SetAnimation(0, bxName2Str, false);
    }


    //
    private bool isFirst;
}
