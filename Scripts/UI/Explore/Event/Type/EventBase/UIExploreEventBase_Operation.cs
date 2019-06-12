using GameEventDispose;
using UnityEngine;

public partial class UIExploreEventBase
{

    /// <summary>
    /// 得到最近的点
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public Vector3 GetRecentlyPos(Vector2 pos, out int posType)
    {
        posType = 0;
        InitPos();
        float distance = GameTools.GetVector3Distance(pos, _leftPos);
        float tempLength1 = GameTools.GetVector3Distance(pos, _middlePos);
        float tempLength2 = GameTools.GetVector3Distance(pos, _rightPos);
        if (distance > tempLength1)
        {
            if (tempLength1 > tempLength2)
            {
                posType = 1;
                return _rightPos;
            }
            return _middlePos;
        }

        if (distance > tempLength2)
        {
            posType = 1;
            return _rightPos;
        }
        posType = -1;
        return _leftPos;
    }

    private void OnCallFade(object param)
    {
        FadeFadeInEvent();
    }
    private void OnCallOpenShow(object param)
    {
        UpdatEventIconShow();
        PlayEventOpenAnimation();
        Scale();
    }

    /// <summary>
    /// 战斗事件
    /// </summary>
    protected void OnCombatEvent(CombatEventType arg1, object arg2)
    {
        switch (arg1)
        {
            case CombatEventType.CombatResult:
                if ((bool)arg2)
                {
                    gameObject.SetActive(true);
                    UpdatEventIconShow();
                    //打开宝箱 
                    PlayEventOpenAnimation();
                    return;
                }
                PlayEventFailedAnimation();
                Destroy(gameObject);
                break;
        }
    }

    private void OnDestroy()
    {
        //移除战斗事件托管
        switch (_eventAttribute.EventType)
        {
            case WPEventType.Combat:
                EventDispatcher.Instance.CombatEvent.RemoveEventListener<CombatEventType, object>(EventId.CombatEvent, OnCombatEvent);
                break;
        }
        EventDispatcher.Instance.ExploreEvent.DispatchEvent(EventId.ExploreEvent, ExploreEventType.EventQuit, (object)null);
    }
}