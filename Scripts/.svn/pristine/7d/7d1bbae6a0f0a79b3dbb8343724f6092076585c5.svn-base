using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 播放脉冲效果
/// </summary>
public class UIPlayImpulseEffect : MonoBehaviour
{
    public bool IsPlayImpulseEffectEnd { get { return isPlayImpulseEffectEnd; } }

    /// <summary>
    /// 播放脉冲
    /// </summary>
    public void PlayImpulseEffect(CRImpulseEffect _impulseEffect, List<UICombatTeam> teams)
    {
        this.teams = teams;
        new CoroutineUtil(IEPlayImpulseEffect(_impulseEffect));
    }

    /// <summary>
    /// 播放脉冲
    /// </summary>
    private IEnumerator IEPlayImpulseEffect(CRImpulseEffect _impulseEffect)
    {
        isPlayImpulseEffectEnd = false;

        //开始播放
        CombatEffect.SkillEffectInfo skillEffectInfo = new CombatEffect.SkillEffectInfo
        {
            hitTeamID = _impulseEffect.teamID,
            hitCharID = _impulseEffect.charID,
            hitCharIndex = _impulseEffect.index,
            skillEffectResults = _impulseEffect.skillEffectResults,
        };
        //
        new UISkillResultEffectsComponent(skillEffectInfo, teams);
        //等待状态播放完成
        while (skillEffectInfo.skillResultEffect.Any(a => !a.IsPlayEnd))
        {
            yield return null;
        }
        //目标播放完成
        isPlayImpulseEffectEnd = true;
    }



    //
    private bool isPlayImpulseEffectEnd;
    private List<UICombatTeam> teams;
}
