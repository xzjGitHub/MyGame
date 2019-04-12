using System.Linq;
using GameEventDispose;
using UnityEngine;

public class UICombatTeam : UICombatTeamBase
{
    public bool IsPlayCharUpgradeShowOk { get { return charUnits.All(a => a.IsPlayCharUpgradeShowOk); } }
    public bool IsFade { get { return isFade; } }
    public bool IsPlayEndEventOk { get { return isPlayEndEventOk; } }
    public bool IsPlayResultEffectOk { get { return isPlayResultEffectOk; } }

    public Transform TeamTrans { get { return teamTrans; } }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(CombatTeamInfo teamInfo, Transform parent)
    {
        base.InitInfo(teamInfo);
        //得到当前Transform
        this.parent = GetTransform(teamType, parent);
        teamTrans = this.parent.Find("Team");
        //
        EventDispatcher.Instance.CombatEvent.AddEventListener<CombatTeamEvent, TeamType, object>(EventId.CombatEvent, OnCombatTeamEvent);
        //
        //销毁资源
        foreach (UICharUnit item in charUnits)
        {
            item.DestroyRes();
        }
        charUnits.Clear();
        if (teamInfo.combatUnits == null)
        {
            return;
        }
        //初始化资源
        foreach (CombatUnit item in teamInfo.combatUnits)
        {
            if (item.initIndex > this.parent.childCount - 1)
            {
                continue;
            }
            charUnits.Add(gameObject.AddComponent<UICharUnit>());
            charUnits.Last().Init(item, this.parent);
        }
    }

    /// <summary>
    /// 播放使用技能
    /// </summary>
    public PlayTargetsetInfo PlayCastSkill(int castIndex)
    {
        UICharUnit currentCharUnit = charUnits.Find(a => a.charIndex == castIndex);
        if (currentCharUnit == null)
        {
            return null;
        }
        //
        isPlayEndEventOk = false;
        return new PlayTargetsetInfo { charunit = currentCharUnit, type = PlayTargetseType.CastSkill, };
    }


    /// <summary>
    /// 播放使用技能
    /// </summary>
    public PlayTargetsetInfo PlayImmediateSkill(int castIndex)
    {
        UICharUnit currentCharUnit = charUnits.Find(a => a.charIndex == castIndex);
        //
        isPlayEndEventOk = false;
        return new PlayTargetsetInfo { charunit = currentCharUnit, type = PlayTargetseType.ImmediateSkill, };
    }



    /// <summary>
    /// 播放结果特效
    /// </summary>
    public UICharUnit GetChar(int index)
    {
        return charUnits.Find(a => a.charIndex == index);
    }


    /// <summary>
    /// 得到当前Transform
    /// </summary>
    private Transform GetTransform(TeamType teamType, Transform parent)
    {
        switch (teamType)
        {
            case TeamType.Player:
                return parent.Find("Left");
            case TeamType.Enemy:
                return parent.Find("Right");
            case TeamType.Thirdparty:
                return parent.Find("Thirdparty");
            default:
                return parent.Find("Thirdparty");
        }
    }

    /// <summary>
    /// 战斗事件
    /// </summary>
    private void OnCombatTeamEvent(CombatTeamEvent teamEvent, TeamType _type, object param)
    {
        if (_type != teamType)
        {
            return;
        }

        switch (teamEvent)
        {
            case CombatTeamEvent.Init:
                break;
            case CombatTeamEvent.ResetRes:
                break;
            case CombatTeamEvent.CombatEnd:
                break;
            case CombatTeamEvent.Move:
                break;
            case CombatTeamEvent.TeamAction:
                foreach (UICharUnit item in charUnits)
                {
                    EventDispatcher.Instance.CharEvent.DispatchEvent(EventId.CharEvent, CharActionOperation.Action,
                        new UICharInfo(teamID, item.charID, item.charIndex), param);
                }
                break;
            case CombatTeamEvent.SetActive:
                break;
            case CombatTeamEvent.SetStartPos:
                break;
            case CombatTeamEvent.SetPos:
                break;
            case CombatTeamEvent.SetSummonPos:
                break;
            case CombatTeamEvent.UpdateNpcMove:
                break;
            case CombatTeamEvent.ResetCharPos:
                ResetCharPos();
                break;
            case CombatTeamEvent.OpenUpgradeShow:
                (param as ComabatTeamTest).combatTeam = this;
                break;
            case CombatTeamEvent.CloseUpgradeShow:
                CloseUpgradShow();
                break;
        }
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.CombatEvent.RemoveEventListener<CombatTeamEvent, TeamType, object>(EventId.CombatEvent, OnCombatTeamEvent);
    }


    //
    private Transform teamTrans;
    private Transform parent;
    //
    private readonly UICharUnit currentCharUnit;
}
