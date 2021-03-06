﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 播放使用技能管理员
/// </summary>
public class UIPlayCastSkillManager : MonoBehaviour
{

    public bool IsPlayCastSkillEnd { get { return isPlayCastSkillEnd; } }

    /// <summary>
    /// 播放使用技能
    /// </summary>
    public void PlayCastSkill(CRCastSkill castSkill, List<UICombatTeam> teams)
    {
        if (CombatSystem.Instance.isTestSkill)
        {
            LogHelper_MC.Log("播放使用技能castSkill=" + castSkill.castSkillId);
        }

        isPlayCastSkillEnd = false;
        this.teams = teams;
        new CoroutineUtil(IEPlayCastSkill(castSkill));
    }


    /// <summary>
    /// 播放使用技能
    /// </summary>
    private IEnumerator IEPlayCastSkill(CRCastSkill castSkill)
    {
        //开始播放TargetsetInfos__只播放有动作的targetsetInfo
        for (int i = 0; i < castSkill.targetInfos.Count; i++)
        {
            //检查是否有动作
            if (castSkill.targetInfos[i].actionIndex == -1)
            {
                continue;
            }
            //
            CombatEffect.TargetsetInfo info = new CombatEffect.TargetsetInfo
            {
                castSkillID = castSkill.castSkillId,
                castTeamID = castSkill.castTeamId,
                castIndex = castSkill.castIndex,
                targetsetIndex = i,
                actionIndex = castSkill.targetInfos[i].actionIndex,
                targetInfos = castSkill.targetInfos,
            };
            //得到当前操作的队伍
            UICombatTeam _team = teams.Find(a => a.teamID == castSkill.castTeamId);
            if (_team == null)
            {
                continue;
            }
            //得到现在使用技能的角色
            PlayTargetsetInfo targetsetInfo = _team.PlayCastSkill(info.castIndex);
            targetsetInfo.info = info;
            //
            UIPlayTargetsetManager targetsetManager = targetsetInfo.charunit.moveTrans.gameObject.AddComponent<UIPlayTargetsetManager>();
            PlayActionEffectInfo actionEffectInfo = targetsetManager.GetPlayActionInfo(targetsetInfo,teams);
            if (actionEffectInfo == null)
            {
                continue;
            }
            //角色播放动作效果
            targetsetInfo.charunit.ActionOperation.PlayActionEffect(actionEffectInfo);
            //检查动作是否播放完成
            while (!targetsetManager.IsPlayEndEventOk)
            {
                yield return null;
            }
            //是否为最后一个
            if (castSkill.targetInfos[i].actionIndex != 0)
            {
                continue;
            }

            while (!targetsetManager.IsPlayResultEffectOk)
            {
                yield return null;
            }
          //  targetsetInfo.charunit.ActionOperation.ResetPos();
            DestroyImmediate(targetsetManager);
   
        }
        //
        isPlayCastSkillEnd = true;
    }

   

    //
    private bool isPlayCastSkillEnd;
    private List<UICombatTeam> teams;

}
