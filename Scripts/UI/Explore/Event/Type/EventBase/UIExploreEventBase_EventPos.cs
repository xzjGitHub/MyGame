using System;
using GameEventDispose;

public partial class UIExploreEventBase
{


    /// <summary>
    ///  回调事件显示
    /// </summary>
    private void OnCallEventShow()
    {
        if (ShowTranS != null) ShowTranS.gameObject.SetActive(true);
        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.EventShow, (object)null);
    }
    /// <summary>
    ///  回调事件显示1_显示雾
    /// </summary>
    private void OnCallEventShow1()
    {
        if (eventType == WPEventType.Grass) return;
        if (!isHaveSmoke) return;
        if (heiObj != null) heiObj.gameObject.SetActive(true);
    }

    /// <summary>
    ///  回调事件能访问访问
    /// </summary>
    private void OnCallEventCanVisit()
    {
        if (eventType == WPEventType.Trap) return;
        //
        OpenEventShow();
        buttonImage.raycastTarget = true;
    }
    /// <summary>
    ///  回调事件自动访问
    /// </summary>
    private void OnCallEventAutoVisit()
    {
        switch (eventType)
        {
            case WPEventType.Combat:
            case WPEventType.Boss:
                OnClickButton();
                break;
        }
        if (eventType != WPEventType.Trap) return;
        OpenEventShow();
    }

    /// <summary>
    /// 回调事件自动放弃访问_距离角色很近
    /// </summary>
    private void OnCallEventAutoAbandonVisit1()
    {
        ExploreSystem.Instance.VisitEvent(eventAttribute.waypointId, eventIndex, WPEventVisitType.Abandon);
        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.EventQuit, (object)null);
        //
        exploreEventPopup.CancelVisit();
        exploreEventPopup.OnOpen -= OnClickOpenOption;
        switch (eventType)
        {
            case WPEventType.Trap:
                return;
        }
        if (OnFade != null) OnFade(null);
        EventDispatcher.Instance.CharEvent.DispatchEvent(EventId.CharEvent, CharActionOperation.TestShow, 0, 0, (object)null);
    }
    /// <summary>
    /// 回调事件自动放弃访问_离开了屏幕
    /// </summary>
    private void OnCallEventAutoAbandonVisit2()
    {
        switch (eventType)
        {
            case WPEventType.Combat:
            case WPEventType.Boss:
                buttonImage.raycastTarget = true;
                return;
        }
        exploreEventPopup.CancelVisit();
        exploreEventPopup.OnOpen -= OnClickOpenOption;
        exploreEventPopup.OnExit -= OnClickExitOption;
        VisitEventEnd();
        Destroy(gameObject);
    }
}
