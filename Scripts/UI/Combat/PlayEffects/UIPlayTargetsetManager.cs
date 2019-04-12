using GameEventDispose;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PlayTargetseType
{
    CastSkill,
    ImmediateSkill,
    ImpulseEffect,
}

public class PlayTargetsetInfo
{
    public CombatEffect.TargetsetInfo info;
    public PlayTargetseType type;
    public UICharUnit charunit;
}

public class PlayActionEffectInfo
{
    public Action_template actionEffect;
    public int targetIndex;
    public bool isLastAction;
    public List<float> erCSYS;
    public UICharUnit hitCharUnit;
    public CREffectResult FirstEffectResult;
}

/// <summary>
/// 播放Targetset管理员
/// </summary>
public class UIPlayTargetsetManager : MonoBehaviour
{
    public bool IsPlayEndEventOk { get { return isPlayEndEventOk; } }

    public bool IsPlayResultEffectOk { get { return isPlayResultEffectOk; } }

    public bool IsLastAction { get { return isLastAction; } }

    public delegate void CallBack(object param);
    public CallBack OnPlayResultEffect;



    /// <summary>
    /// 获得播放动作信息
    /// </summary>
    public PlayActionEffectInfo GetPlayActionInfo(PlayTargetsetInfo info, List<UICombatTeam> combatTeams)
    {
        isPlayEndEventOk = false;
        isPlayResultEffectOk = false;
        this.combatTeams = combatTeams;
        Init(info);
        //注册动作事件
        EventDispatcher.Instance.CombatEvent.AddEventListener<CombatCharActionEvent, object>(EventId.CombatEffect, OnCombatCharActionEvent);
        return new PlayActionEffectInfo
        {
            actionEffect = actionEffect,
            targetIndex = targetIndex,
            isLastAction = isLastAction,
            hitCharUnit = hitCharUnit,
            FirstEffectResult = _firstEffectResult,
        };
    }

    public void PlayImmediateEOEffect(PlayTargetsetInfo info)
    {
        LogHelperLSK.LogError("PlayImmediateEOEffect未做");
        isPlayEndEventOk = true;
        isPlayResultEffectOk = false;
        //
        Init(info);
        //
        foreach (CRTargetInfo targetInfo in targetInfos)
        {
            _action = ActionEffectConfig.GetConfig(targetInfo.targetId);
            if (_action == null)
            {
                continue;
            }

            // _objectEffectObject = ObjectEffectConfig.GetEffectObjectConfig(_action.effectObjectID);
            if (_objectEffectObject == null)
            {
                continue;
            }
            //多个目标
            foreach (CRTargetUnitInfo item in targetInfo.targetUnitInfos)
            {
                PlayEffectObj(_objectEffectObject, item);
            }
        }
    }


    /// <summary>
    /// 初始化
    /// </summary>
    private void Init(PlayTargetsetInfo info)
    {
        CharRPack charShow = CharRPackConfig.GeCharShowTemplate(info.charunit.TemplateID);
        //得到技能动作配置
        skillAction = SkillActionConfig.GetSkillActionConfig(info.info.castSkillID);
        //
        targetsetInfo = info.info;
        atkUnit = info.charunit;
        targetInfos = targetsetInfo.targetInfos;
        targetsetIndex = targetsetInfo.targetsetIndex;
        //初始化
        mainTargetSet = info.info.targetInfos[targetsetIndex];
        if (skillAction.skillAction.Count < targetsetInfo.actionIndex + 1)
        {
            throw new ArgumentException("动作索引出错：" + targetsetInfo.actionIndex, "actionIndex");
        }
        //得到动作效果配置ID
        actionEffect = Action_templateConfig.GetActionEffectConfig(skillAction.skillAction[targetsetInfo.actionIndex]);
        if (actionEffect != null && actionEffect.subTargetSet != null)
        {
            foreach (int item in actionEffect.subTargetSet)
            {
                CRTargetInfo targetInfo = info.info.targetInfos.Find(a => a.targetId == item);
                if (targetInfo != null)
                {
                    subTargetsets.Add(targetInfo);
                }
            }
        }
        //
        targetIndex = 0;
        int hitTeam = atkUnit.teamID;
        if (mainTargetSet.targetUnitInfos.Count >= 1)
        {
            CRTargetUnitInfo temp = targetsetInfo.targetInfos[targetsetInfo.targetsetIndex].targetUnitInfos[0];
            targetIndex = temp.hitIndex;
            hitTeam = temp.hitTeamId;
            if (temp != null && temp.skillEffect != null && temp.skillEffect.skillEffectResults != null)
            {
                if (temp.skillEffect.skillEffectResults.Count > 0)
                {
                    _firstEffectResult = temp.skillEffect.skillEffectResults[0].execState.effectResult;
                }
            }
        }
        hitCharUnit = UICombatTool.Instance.GetCharUI(hitTeam, targetIndex);
        //TODO 是否为最后一个动作 需要确认计算targetset顺序
        isLastAction = targetsetInfo.actionIndex == skillAction.skillAction.Count - 1;
        //得到主状态总数
        foreach (CRTargetUnitInfo item in mainTargetSet.targetUnitInfos)
        {
            mainTargetStateSum += item.skillEffect.skillEffectResults.Count;
            break;
        }
    }

    #region 播放SkillEvent

    /// <summary>
    /// 播放技能特效
    /// </summary>
    private void PlaySkillEffect()
    {
        //得到TargetsetEffect配置
        if (actionEffect.skillEffect == null || actionEffect.skillEffect.Count == 0 || actionEffect.skillEffect.Count <= skillEventIndex)
        {
            return;
        }
        //新建播放效果
        int actionEffectID = actionEffect.skillEffect[skillEventIndex];
        ActionEffect effect = ActionEffectConfig.GetConfig(actionEffectID);
        if (effect == null)
        {
            LogHelperLSK.Log("播放ActionEffect" + actionEffectID + "为空");
        }

        PlayEffectTool.PlayActionEffect(gameObject, OnCallSkillPlayEffectEnd, effect, atkUnit, mainTargetSet.targetUnitInfos[0].hitTeamId);
    }

    #endregion

    #region 播放EOEvent

    /// <summary>
    /// 播放EffectObject
    /// </summary>
    private void PlayEOEffect()
    {
        isPlayResultEffectOk = false;
        //得到TargetsetEffect配置
        if (actionEffect.EOEffect.Count - 1 < nowEOEIndex)
        {
            throw new ArgumentException("EO索引出错：" + nowEOEIndex, "nowEOEIndex");
        }

        ObjectEffect objectEffectObject = ObjectEffectConfig.GetEffectObjectConfig(actionEffect.EOEffect[nowEOEIndex]);
        string resStr = objectEffectObject.EOEventEffect;
        if (objectEffectObject.simpleDisplay == 1)
        {
            resStr += "_" + atkUnit.combatUnit.charAttribute.templateID;
        }
        if (CombatSystem.Instance.isTestSkill)
        {
            LogHelperLSK.Log("播放EO:PlayEOEffect=" + resStr);
        }

        //多个目标
        foreach (CRTargetUnitInfo item in mainTargetSet.targetUnitInfos)
        {
            if (GetTargetState(item, stateIndex) == null)
            {
                continue;
            }
            PlayEffectObj(objectEffectObject, item);
        }
    }

    private CRExecState GetTargetState(CRTargetUnitInfo targetUnitInfo, int stateIndex)
    {
        if (targetUnitInfo.skillEffect.skillEffectResults.Count - 1 < stateIndex)
        {
            return null;
        }
        return targetUnitInfo.skillEffect.skillEffectResults[stateIndex].execState;
    }

    /// <summary>
    /// 播放特效对象
    /// </summary>
    private void PlayEffectObj(ObjectEffect objectEffect, CRTargetUnitInfo targetUnitInfo)
    {
        UICombatTeam hitTeam = combatTeams.Find(a => a.teamID == targetUnitInfo.hitTeamId);
        //
        UICharUnit hitChar = hitTeam.GetChar(targetUnitInfo.hitIndex);
        //
        UIPlayMainEOEvent playMainEOEvent = atkUnit.moveTrans.gameObject.AddComponent<UIPlayMainEOEvent>();
        playMainEOEvent.Init(objectEffect, targetUnitInfo, atkUnit, hitChar, stateIndex);
        mainEOEvents.Add(playMainEOEvent);
        //
        mainEOEvents.Last().OnPlayEOEventOk = OnCallMainPlayEOEventOk;
        mainEOEvents.Last().StarPlay();
    }
    /// <summary>
    /// 回调主播放EOEventOk
    /// </summary>
    /// <param name="playMainEoEvent"></param>
    private void OnCallMainPlayEOEventOk(UIPlayMainEOEvent playMainEoEvent)
    {
        if (waitMainPlayEOEventOk == null)
        {
            waitMainPlayEOEventOk = new CoroutineUtil(IEWaitMainPlayEOEventEnd());
        }
        DestroyImmediate(playMainEoEvent);
    }
    /// <summary>
    /// 等待主播放EOEventOk
    /// </summary>
    /// <returns></returns>
    private IEnumerator IEWaitMainPlayEOEventEnd()
    {
        while (mainEOEvents.Count < actionEffect.EOEffect.Count)
        {
            yield return null;
        }

        while (!mainEOEvents.All(a => a.IsPlayOk))
        {
            yield return null;
        }

        for (int i = 0; i < mainEOEvents.Count; i++)
        {
            DestroyImmediate(mainEOEvents[i]);
        }
        mainEOEvents.Clear();
        //播放子技能效果结果
        PlaySubResultEffect();
    }
    #endregion

    #region 播放hitEvent_skillResultEffect

    /// <summary>
    /// 播放主要Hit特效
    /// </summary>
    private void PlaySkillMainHitEffect()
    {
        isPlayMainResultEffectOk = false;
        if (waitMainPlayResultOk == null)
        {
            waitMainPlayResultOk = new CoroutineUtil(IEWaitMainPlayResultOk());
        }
        //每个目标播放hit
        for (int i = 0; i < mainTargetSet.targetUnitInfos.Count; i++)
        {
            CRTargetUnitInfo crTargetUnitInfo = mainTargetSet.targetUnitInfos[i];
            if (GetTargetState(crTargetUnitInfo, stateIndex) == null)
            {
                continue;
            }
            PlayMainResultEffect(crTargetUnitInfo.hitIndex, stateIndex);
        }
    }


    /// <summary>
    /// 播放主技能效果结果
    /// </summary>
    private void PlayMainResultEffect(int hitIndex = -1, int stateIndex = -1)
    {
        //播放主命中结果 每个人 每个状态
        foreach (CRTargetUnitInfo targetUnitInfo in mainTargetSet.targetUnitInfos)
        {
            if (hitIndex == -1)
            {
                if (stateIndex == -1)
                {
                    foreach (CRSkillEffectResult crSkillEffectResult in targetUnitInfo.skillEffect.skillEffectResults)
                    {
                        //每个人 每个状态
                        PlayMainResultEffect(targetUnitInfo.hitTeamId, targetUnitInfo.hitIndex, crSkillEffectResult);
                    }
                }
                else
                {//每个人 指定状态
                    PlayMainResultEffect(targetUnitInfo.hitTeamId, targetUnitInfo.hitIndex, targetUnitInfo.skillEffect.skillEffectResults[stateIndex]);
                }
            }
            else
            {
                if (targetUnitInfo.hitIndex == hitIndex)
                {
                    if (stateIndex == -1)
                    {
                        foreach (CRSkillEffectResult crSkillEffectResult in targetUnitInfo.skillEffect.skillEffectResults)
                        {//指定人 每个状态
                            PlayMainResultEffect(targetUnitInfo.hitTeamId, targetUnitInfo.hitIndex, crSkillEffectResult);
                        }
                    }
                    else
                    {//指定人 指定状态
                        PlayMainResultEffect(targetUnitInfo.hitTeamId, targetUnitInfo.hitIndex, targetUnitInfo.skillEffect.skillEffectResults[stateIndex]);
                    }
                }
            }
        }
    }


    /// <summary>
    /// 播放主技能效果结果
    /// </summary>
    private void PlayMainResultEffect(int hitTeamID, int hitCharIndex, CRSkillEffectResult crSkillEffectResult)
    {
        UICombatTeam hitTeam = combatTeams.Find(a => a.teamID == hitTeamID);
        //
        UICharUnit hitChar = hitTeam.GetChar(hitCharIndex);
        if (hitChar == null)
        {
            return;
        }
        PlaySkillResultEffectObj playSkillResult = hitChar.moveTrans.gameObject.AddComponent<PlaySkillResultEffectObj>();
        playSkillResult.Init(crSkillEffectResult, atkUnit.ActionOperation.SortingOrder, hitChar);
        mainPlaySkillResultEffects.Add(playSkillResult);
        mainPlaySkillResultEffects.Last().OnEndPlay = OnCallMainPlaySkillResultEffectEnd;
        mainPlaySkillResultEffects.Last().StartPlay();
    }

    /// <summary>
    /// 回调主_技能结果效果播放完成
    /// </summary>
    /// <param name="playSkillResult"></param>
    private void OnCallMainPlaySkillResultEffectEnd(PlaySkillResultEffectObj playSkillResult)
    {
        isPlayMainResultEffectOk = false;
        if (waitMainPlayResultOk == null)
        {
            waitMainPlayResultOk = new CoroutineUtil(IEWaitMainPlayResultOk());
        }
    }
    /// <summary>
    /// 等待主结果播放完
    /// </summary>
    /// <returns></returns>
    private IEnumerator IEWaitMainPlayResultOk()
    {
        //还未添加完
        while (mainPlaySkillResultEffects.Count < mainTargetStateSum || mainPlaySkillResultEffects.Count < actionEffect.hitEffect.Count)
        {
            yield return null;
        }
        //没有全部播放完成
        while (!mainPlaySkillResultEffects.All(a => a.IsPlayEnd))
        {
            yield return null;
        }
        //销毁播放组组件
        for (int i = 0; i < mainPlaySkillResultEffects.Count; i++)
        {
            DestroyImmediate(mainPlaySkillResultEffects[i]);
        }
        mainPlaySkillResultEffects.Clear();
        //播放子技能效果结果
        PlaySubResultEffect();
    }

    /// <summary>
    /// 播放子技能效果结果
    /// </summary>
    private void PlaySubResultEffect()
    {
        if (subTargetsets == null || subTargetsets.Count == 0)
        {
            isPlaySubResultEffectOk = true;
            isPlayResultEffectOk = true;
            return;
        }
        //播放subResult
        foreach (CRTargetInfo crTargetInfo in subTargetsets)
        {
            foreach (CRTargetUnitInfo item in crTargetInfo.targetUnitInfos)
            {
                if (item.skillEffect.skillEffectResults == null || item.skillEffect.skillEffectResults.Count == 0)
                {
                    continue;
                }
                subStateSum++;
                UICombatTeam hitTeam = combatTeams.Find(a => a.teamID == item.hitTeamId);
                //
                UICharUnit hitChar = hitTeam.GetChar(item.hitIndex);
                new CoroutineUtil(IEPlaySubCharResultEffect(item.skillEffect.skillEffectResults, hitChar));
            }
        }

        if (subStateSum == 0)
        {
            isPlaySubResultEffectOk = true;
            isPlayResultEffectOk = true;
        }
    }
    /// <summary>
    /// 播放子技能效果结果
    /// </summary>
    /// <param name="skillEffectResults"></param>
    /// <param name="hitChar"></param>
    /// <returns></returns>
    private IEnumerator IEPlaySubCharResultEffect(List<CRSkillEffectResult> skillEffectResults, UICharUnit hitChar)
    {
        if (skillEffectResults == null || skillEffectResults.Count == 0)
        {
            yield break;
        }
        //每个状态演示间隔waitForSeconds
        foreach (CRSkillEffectResult skillEffectResult in skillEffectResults)
        {
            PlaySkillResultEffectObj playSkillResult = hitChar.moveTrans.gameObject.AddComponent<PlaySkillResultEffectObj>();
            playSkillResult.Init(skillEffectResult, atkUnit.ActionOperation.SortingOrder, hitChar);
            subPlaySkillResultEffects.Add(playSkillResult);
            //
            subPlaySkillResultEffects.Last().OnEndPlay = OnCallSubPlaySkillResultEffectEnd;
            subPlaySkillResultEffects.Last().StartPlay();
            yield return waitForSeconds;
        }
    }
    /// <summary>
    /// 回调主_技能结果效果播放完成
    /// </summary>
    /// <param name="playSkillResult"></param>
    private void OnCallSubPlaySkillResultEffectEnd(PlaySkillResultEffectObj playSkillResult)
    {
        isPlaySubResultEffectOk = false;
        if (waitSubPlayResultOk == null)
        {
            waitSubPlayResultOk = new CoroutineUtil(IEWaitSubPlayResultOk());
        }
    }
    /// <summary>
    /// 等待子结果播放完
    /// </summary>
    /// <returns></returns>
    private IEnumerator IEWaitSubPlayResultOk()
    {
        //还未添加完
        while (subPlaySkillResultEffects.Count < subStateSum)
        {
            yield return null;
        }
        //没有全部播放完成
        while (!subPlaySkillResultEffects.All(a => a.IsPlayEnd))
        {
            yield return null;
        }
        //销毁播放组组件
        for (int i = 0; i < subPlaySkillResultEffects.Count; i++)
        {
            DestroyImmediate(subPlaySkillResultEffects[i]);
        }
        subPlaySkillResultEffects.Clear();
        isPlaySubResultEffectOk = true;
        isPlayResultEffectOk = true;
    }

    #endregion


    #region 事件回调

    /// <summary>
    /// 播放动作特效
    /// </summary>
    /// <param name="param"></param>
    private void OnCallPalySkill(object param)
    {
        skillEventIndex++;
        PlaySkillEffect();
    }
    /// <summary>
    /// 播放命中
    /// </summary>
    /// <param name="param"></param>
    private void OnCallPalyHit(object param)
    {
        stateIndex++;
        PlaySkillMainHitEffect();
    }
    /// <summary>
    /// 播放EOEffect
    /// </summary>
    private void OnCallPalyEOEffect(int index)
    {
        stateIndex++;
        nowEOEIndex = index;
        nowEOESum++;
        PlayEOEffect();
    }
    /// <summary>
    /// 动作播放结束
    /// </summary>
    /// <param name="param"></param>
    private void OnCallPlayActionEnd(object param)
    {
        PlayActionEnd();
        //
        if (isPlayResultEffectOk)
        {
            DestroyImmediate(this);
        }
    }
    #endregion


    /// <summary>
    /// 动作特效播放结束
    /// </summary>
    /// <param name="playEffect"></param>
    private void OnCallSkillPlayEffectEnd(UnityEngine.Object playEffect)
    {
        if ((playEffect as UIPlayEffect).IsAutoDestory)
        {
            DestroyImmediate(playEffect);
        }
    }

    /// <summary>
    /// 动作播放完成
    /// </summary>
    private void PlayActionEnd()
    {
        isPlayEndEventOk = true;
        // EventDispatcher.Instance.CombatEvent.RemoveEventListener<CombatCharActionEvent, object>(EventId.CombatEffect, OnCombatCharActionEvent);
    }

    /// <summary>
    /// 是否生效
    /// </summary>
    /// <returns></returns>
    private bool IsTargetsetEffective(int _startIndex, int _targetID, List<CRTargetInfo> targetInfos, out int _nowIndex)
    {
        _nowIndex = _startIndex;
        for (int i = _startIndex; i < targetInfos.Count; i++)
        {
            if (targetInfos[i].targetId != _targetID)
            {
                continue;
            }

            _nowIndex = i;
            return true;
        }
        return false;
    }
    /// <summary>
    /// 得到现在信息
    /// </summary>
    private List<CRTargetInfo> GetNowTargetInfo(List<int> targetsets, List<CRTargetInfo> targetInfos)
    {
        return targetsets.Select(t => targetInfos.Find(a => a.targetId == t)).ToList();
    }


    /// <summary>
    /// 播放动作事件特效
    /// </summary>
    private void OnPlayActonEventEffect(object param)
    {
        EventDispatcher.Instance.CharEvent.DispatchEvent(EventId.CharEvent, CharActionOperation.ActonEventEffect, atkUnit.teamID,
          atkUnit.charID, (object)new object[] { atkUnit.ActionOperation.ActionEventEffectSetID, (int)param });
    }
    /// <summary>
    /// 动作动画事件
    /// </summary>
    private void OnCombatCharActionEvent(CombatCharActionEvent arg1, object arg2)
    {
        switch (arg1)
        {
            case CombatCharActionEvent.Skill:
                OnCallPalySkill(arg2);
                break;
            case CombatCharActionEvent.EffectObj1:
                OnCallPalyEOEffect((int)arg2);
                break;
            case CombatCharActionEvent.EffectObj2:
                OnCallPalyEOEffect(1);
                break;
            case CombatCharActionEvent.EffectObj3:
                OnCallPalyEOEffect(2);
                break;
            case CombatCharActionEvent.EffectObj4:
                OnCallPalyEOEffect(3);
                break;
            case CombatCharActionEvent.Hit:
                OnCallPalyHit(arg2);
                break;
            case CombatCharActionEvent.End:
                OnCallPlayActionEnd(arg2);
                break;
            case CombatCharActionEvent.Event1:
                OnPlayActonEventEffect(arg2);
                break;
            case CombatCharActionEvent.Event2:
                OnPlayActonEventEffect(arg2);
                break;
            case CombatCharActionEvent.Event3:
                OnPlayActonEventEffect(arg2);
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.CombatEvent.RemoveEventListener<CombatCharActionEvent, object>(EventId.CombatEffect, OnCombatCharActionEvent);
    }

    //
    private UICharUnit atkUnit;
    //
    private SkillAction skillAction;
    private Action_template actionEffect;
    private ActionEffect _action;
    private readonly ObjectEffect _objectEffectObject;
    //
    private List<CRTargetInfo> targetInfos;
    private List<UICombatTeam> combatTeams;
    private UICharUnit hitCharUnit;
    private CREffectResult _firstEffectResult;
    //
    private int targetsetIndex;
    /// <summary>
    /// 目标位置
    /// </summary>
    private int targetIndex;
    private int nowEOESum = -1;
    private int nowEOEIndex;
    private int skillEventIndex = -1;
    private int stateIndex = -1;
    private int mainTargetStateSum;

    //
    private CRTargetInfo mainTargetSet;
    private List<CRTargetInfo> subTargetsets = new List<CRTargetInfo>();
    //
    private readonly List<UIPlayMainHitEvent> mainPlayHitEvents = new List<UIPlayMainHitEvent>();
    private List<UIPlayMainEOEvent> mainEOEvents = new List<UIPlayMainEOEvent>();
    private List<PlaySkillResultEffectObj> mainPlaySkillResultEffects = new List<PlaySkillResultEffectObj>();
    private List<PlaySkillResultEffectObj> subPlaySkillResultEffects = new List<PlaySkillResultEffectObj>();
    //
    private bool isLastAction;
    private bool isPlayEndEventOk;
    private bool isPlayResultEffectOk;
    private bool isPlayMainResultEffectOk;
    private bool isPlaySubResultEffectOk;
    private int subStateSum;
    //
    private CombatEffect.TargetsetInfo targetsetInfo;
    private CoroutineUtil waitMainPlayResultOk;
    private CoroutineUtil waitSubPlayResultOk;
    private CoroutineUtil waitMainPlayEOEventOk;
    private readonly WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);

}
