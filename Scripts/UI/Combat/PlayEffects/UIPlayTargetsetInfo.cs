﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 播放Targetset信息——单个Set
/// </summary>
public class UIPlayTargetsetInfo : MonoBehaviour
{
    public delegate void CallBack(object param);
    public CallBack OnPlayResultEffect;
    public CallBack OnPlayHitEventOk;
    public bool IsPlayHitOk { get { return _isPlayHitOkOk; } }

    public bool IsPlaySkillResultOk { get { return isPlaySkillResultOk; } }

    public bool IsPlayEoEffectOk { get { return isPlayEOEffectOk; } }

    public int StateIndex { get { return stateIndex; } }

    public int HitCharIndex { get { return hitCharIndex; } }


    /// <summary>
    /// 初始化信息
    /// </summary>
    /// <param name="info"></param>
    public void InitInfo(CombatEffect.TargetsetInfo info)
    {
        targetInfos = info.targetInfos;
    }






    /// <summary>
    /// 开始播放所有结果效果
    /// </summary>
    public void StartAllResultEffect()
    {
        if (CombatSystem.Instance.isTestSkill)
        {
            LogHelper_MC.Log("播放Result");
        }
        foreach (int item in playResultTargetsets)
        {
            PlayResultEffect(item);
        }
    }

    /// <summary>
    /// 播放结果特效
    /// </summary>
    /// <param name="_targetsetIndex"></param>
    private void PlayResultEffect(int _targetsetIndex, int hitIndex = -1)
    {
        resultPlayEffects.Clear();
        isPlaySkillResultOk = false;
        List<CRTargetUnitInfo> targetUnitInfos = targetInfos[_targetsetIndex].targetUnitInfos;
        if (targetUnitInfos == null || targetUnitInfos.Count == 0)
        {
            isPlaySkillResultOk = true;
            return;
        }
        if (hitIndex == -1)
        {
            foreach (CRTargetUnitInfo targetUnitInfo in targetUnitInfos)
            {
                PlayUnitResultEffect(targetUnitInfo);
            }
        }
        else
        {
            PlayUnitResultEffect(targetUnitInfos.Find(a => a.hitIndex == hitIndex));
        }
        //检查是否播放完成
        new CoroutineUtil(IECheckSkillEffectResult());
    }

    /// <summary>
    /// 播放结果特效
    /// </summary>
    private void PlayUnitResultEffect(CRTargetUnitInfo targetUnitInfo)
    {
        if (targetUnitInfo == null)
        {
            isPlaySkillResultOk = true;
            return;
        }
        //开始播放
        CRSkillEffect skillEffect = targetUnitInfo.skillEffect;
        //开始播放技能效果
        CombatEffect.SkillEffectInfo skillEffectInfo = new CombatEffect.SkillEffectInfo
        {
            hitTeamID = targetUnitInfo.hitTeamId,
            hitCharID = targetUnitInfo.hitCharId,
            hitCharIndex = targetUnitInfo.hitIndex,
            skillEffectResults = skillEffect.skillEffectResults,
            sortingOrder = sortingOrder,
        };
        //
        if (OnPlayResultEffect != null)
        {
            OnPlayResultEffect(skillEffectInfo);
        }
        //
        resultPlayEffects.AddRange(skillEffectInfo.skillResultEffect);
    }

    /// <summary>
    /// hit播放结束
    /// </summary>
    /// <param name="playeffect"></param>
    private void OnCallHitPlayEnd(Object playeffect)
    {
        _isPlayHitOkOk = true;
        if (OnPlayHitEventOk!=null)
        {
            OnPlayHitEventOk(this);
        }
        //
        DestroyImmediate(playeffect);
    }
    /// <summary>
    /// hit播放结束
    /// </summary>
    /// <param name="playeffect"></param>
    private void OnCallHitPlayEnd1(int charIndex, int stateIndex)
    {
        foreach (CRTargetInfo crTargetInfo in targetInfos)
        {

        }
    }
    /// <summary>
    /// 检查技能效果播放结果
    /// </summary>
    /// <returns></returns>
    private IEnumerator IECheckSkillEffectResult()
    {
        while (!resultPlayEffects.All(a => a.IsPlayEnd))
        {
            yield return null;
        }
        //
        if (!isPlaySkillResultOk)
        {
            isPlaySkillResultOk = true;
        }

        if (!isPlayEOEffectOk)
        {
            isPlayEOEffectOk = true;
        }

        resultPlayEffects.Clear();
    }

    //
    private List<CRTargetInfo> targetInfos;
    //
    private List<UIPlayEffect> hitPlayEffects = new List<UIPlayEffect>();
    private List<PlaySkillResultEffectsObj> resultPlayEffects = new List<PlaySkillResultEffectsObj>();
    private readonly List<int> playResultTargetsets = new List<int>();
    private Dictionary<int, int> playResultIndexs = new Dictionary<int, int>();
    //
    private List<UIPlayEffectObj> playEffectObjs = new List<UIPlayEffectObj>();
    //
    private bool _isPlayHitOkOk;
    private bool isPlaySkillResultOk;
    private bool isPlayEOEffectOk;
    private int sortingOrder;
    private int stateIndex;
    private int hitCharIndex;
}
