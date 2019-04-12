using UnityEngine;
using System.Collections;
using GameEventDispose;


/// <summary>
/// 事件访问角色生命值消耗
/// </summary>
public class UIEventVisitCharHPCost : UICharBase
{

    public void Init(CombatUnit combatUnit)
    {
        base.InitInfo(combatUnit);
        EventDispatcher.Instance.CharEvent.AddEventListener<CharActionOperation, int, int, object>(EventId.CharEvent, OnCharEvent);
    }


    private void OnCharEvent(CharActionOperation arg1, int teamID, int charID, object arg2)
    {
        if (teamID != base.teamID || charID != base.charID) return;
        switch (arg1)
        {
            case CharActionOperation.HPCost:
                object[] obj = arg2 as object[];
                int maxHP = (int)obj[0];
                int primevalHP = (int)obj[1];
                int costHP = (int)obj[2];
                break;
        }
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.CharEvent.RemoveEventListener<CharActionOperation, int, int, object>(EventId.CharEvent, OnCharEvent);
    }
}
