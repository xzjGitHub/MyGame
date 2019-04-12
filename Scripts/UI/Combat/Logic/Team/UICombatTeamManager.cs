using GameEventDispose;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UICombatTeamManager : UICombatTeamBase
{
    public bool IsPlayCharUpgradeShowOk { get { return charUnits.All(a => a.IsPlayCharUpgradeShowOk); } }
    public bool IsFade { get { return isFade; } }
    public bool IsPlayEndEventOk { get { return isPlayEndEventOk; } }
    public bool IsPlayResultEffectOk { get { return isPlayResultEffectOk; } }


    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(TeamType teamType)
    {
        this.teamType = teamType;
        //
        EventDispatcher.Instance.CombatEvent.AddEventListener<CombatTeamEvent, TeamType, object>(EventId.CombatEvent, OnCombatTeamEvent);
        //注册播放Effect事件
        EventDispatcher.Instance.CombatEffect.AddEventListener<PlayCombatEffect, int, object>(EventId.CombatEffect, OnPlayCombatEffect);
    }

    private void NewInit(CombatSystem combatSystem, bool isNew = true, float size = 36)
    {
        gameObject.SetActive(true);
        if (isNew)
        {
            foreach (Transform item in transform)
            {
                item.Find("Move").localScale = Vector3.one*size;
                item.gameObject.AddComponent<UICombatUnitManager>();
            }
        }
        CombatTeamInfo teamInfo = teamType == TeamType.Player ? combatSystem.PlayerTeamInfo : combatSystem.EnemyTeamInfo;
        //
        teamID = teamInfo.teamId;
        combatUnitManagers.Clear();
        //
        for (int i = 0; i < teamInfo.combatUnits.Count; i++)
        {
            combatUnitManagers.Add(transform.GetChild(i).GetComponent<UICombatUnitManager>());
            combatUnitManagers[i].NewInit(teamInfo.combatUnits[i], isNew);
        }
    }

    private void OnPlayCombatEffect(PlayCombatEffect arg1, int teamID, object arg2)
    {
        switch (arg1)
        {
            case PlayCombatEffect.Targetset:
                if (teamID != this.teamID) return;
                targetsetInfo = (CombatEffect.TargetsetInfo)arg2;
                PlayCastSkill(targetsetInfo);
                break;
            case PlayCombatEffect.SkillEffect:
                if (teamID != this.teamID) return;
                skillEffectInfo = (CombatEffect.SkillEffectInfo)arg2;
                PlayResultEffect(skillEffectInfo);
                break;
            case PlayCombatEffect.EndEvent:
                isPlayEndEventOk = true;
                break;
            case PlayCombatEffect.ResultEnd:
                isPlayResultEffectOk = true;
                break;
            case PlayCombatEffect.ImpulseEffect:
                if (teamID != this.teamID) return;
                skillEffectInfo = (CombatEffect.SkillEffectInfo)arg2;
                PlayImpulsionEffect(skillEffectInfo);
                break;
            case PlayCombatEffect.CharDie:
                break;
            case PlayCombatEffect.ImmediateSkillEffect:
                if (teamID != this.teamID) return;
                PlayImmediateSkillEffect(arg2 as List<CRTargetInfo>);
                break;
        }
    }

    /// <summary>
    /// 战斗事件
    /// </summary>
    private void OnCombatTeamEvent(CombatTeamEvent teamEvent, TeamType _type, object param)
    {
        if (_type != teamType) return;
        switch (teamEvent)
        {
            case CombatTeamEvent.Init:
                object[] obj = param as object[];
                NewInit((CombatSystem)obj[0], (bool)obj[1], (float)obj[2]);
                break;
            case CombatTeamEvent.ResetRes:
                ResetRes();
                break;
            case CombatTeamEvent.CombatEnd:
                break;
            case CombatTeamEvent.Move:
                leftEndVector3 = ((OnExploreOneselfMove)param).endVector3;
                aspd = ((OnExploreOneselfMove)param).aspd;
                isMove = true;
                break;
            case CombatTeamEvent.TeamAction:
                foreach (var item in combatUnitManagers)
                {
                    EventDispatcher.Instance.CharEvent.DispatchEvent(EventId.CharEvent, CharActionOperation.Action, item.teamID, item.charID,
                        (object)param);
                }
                break;
            case CombatTeamEvent.SetActive:
                gameObject.SetActive((bool)param);
                break;
            case CombatTeamEvent.SetStartPos:
                NpcSetStartPos();
                break;
            case CombatTeamEvent.SetPos:
                transform.localPosition = (Vector3)param;
                Fade();
                break;
            case CombatTeamEvent.SetSummonPos:
                SetNpcSummonPos();
                break;
            case CombatTeamEvent.UpdateNpcMove:
                UpdateNpcMove((float)param);
                break;
            case CombatTeamEvent.ResetCharPos:
                ResetCharPos();
                break;
            case CombatTeamEvent.OpenUpgradeShow:
                (param as ComabatTeamTest).teamManager = this;
                break;
            case CombatTeamEvent.CloseUpgradeShow:
                CloseUpgradShow();
                break;
        }
    }



    /// <summary>
    /// 播放使用技能
    /// </summary>
    private void PlayCastSkill(CombatEffect.TargetsetInfo info)
    {
        info.teamManager = this;
        isPlayEndEventOk = false;
        castCombatUnit = combatUnitManagers[info.castIndex];
      //  castCombatUnit.PlayTargetsetInfo(info);
    }

    /// <summary>
    /// 播放结果特效
    /// </summary>
    private void PlayResultEffect(CombatEffect.SkillEffectInfo info)
    {
        isPlayResultEffectOk = false;
        //combatUnitManagers[info.hitCharIndex].PlaySkillResultInfo(info);
    }

    /// <summary>
    /// 播放脉冲效果
    /// </summary>
    /// <param name="info"></param>
    private void PlayImpulsionEffect(CombatEffect.SkillEffectInfo info)
    {
        isPlayResultEffectOk = false;
       // combatUnitManagers[info.hitCharIndex].PlaySkillResultInfo(info);
        new CoroutineUtil(CheckImpulsionResult(info));
    }

    private IEnumerator CheckImpulsionResult(CombatEffect.SkillEffectInfo info)
    {
        while (info.skillResultEffect.Any(a => !a.IsPlayEnd))
        {
            yield return null;
        }
        isPlayResultEffectOk = true;
    }
    /// <summary>
    /// 播放立即技能效果
    /// </summary>
    private void PlayImmediateSkillEffect(List<CRTargetInfo> infos)
    {
        CombatEffect.TargetsetInfo info = new CombatEffect.TargetsetInfo
        {
            castSkillID = 0,
            castTeamID = 0,
            castIndex = 0,
            targetsetIndex = 0,
            actionIndex = -1,
            targetInfos = infos,
        };
        castCombatUnit = combatUnitManagers[info.castIndex];
       // castCombatUnit.PlayImmediateSkillEffect(info);
    }

    // 当 MonoBehaviour 将被销毁时调用此函数
    private void OnDestroy()
    {
        EventDispatcher.Instance.CombatEffect.RemoveEventListener<PlayCombatEffect, int, object>(EventId.CombatEffect, OnPlayCombatEffect);
        EventDispatcher.Instance.CombatEvent.RemoveEventListener<CombatTeamEvent, TeamType, object>(EventId.CombatEvent, OnCombatTeamEvent);
    }
    //
    private UICombatUnitManager castCombatUnit;
    //
    private CombatEffect.TargetsetInfo targetsetInfo;
    private CombatEffect.SkillEffectInfo skillEffectInfo;
    //
    private bool isFirst;

}
