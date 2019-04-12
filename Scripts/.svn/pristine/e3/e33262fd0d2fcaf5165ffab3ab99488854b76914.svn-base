using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 播放立即技能效果
/// </summary>
public class UIPlayImmediateSkillEffect : MonoBehaviour
{

    public bool IsPlayImmediateSkillEnd { get { return isPlayImmediateSkillEnd; } }

    /// <summary>
    /// 播放使用技能
    /// </summary>
    public void PlayPlayImmediateSkill(List<CRTargetInfo> infos, List<UICombatTeam> teams)
    {
        this.teams = teams;
        new CoroutineUtil(IEPlayImmediateSkillEffect(infos));
    }



    private IEnumerator IEPlayImmediateSkillEffect(List<CRTargetInfo> infos)
    {
        //开始播放TargetsetInfos
        for (int i = 0; i < infos.Count; i++)
        {
            //
            CombatEffect.TargetsetInfo info = new CombatEffect.TargetsetInfo
            {
                castTeamID = -1,
                targetsetIndex = i,
                targetInfos = infos,
            };
            //得到当前操作的队伍
            var _team = teams.Find(a => a.teamType == TeamType.Thirdparty);
            if (_team == null) continue;
            var targetsetInfo = _team.PlayImmediateSkill(info.castIndex);
            targetsetInfo.info = info;
            //
            var targetsetManager = gameObject.AddComponent<UIPlayTargetsetManager>();
            targetsetManager.OnPlayResultEffect = OnCallPlayResultEffect;
            targetsetManager.PlayImmediateEOEffect(targetsetInfo);
            //
            //检查结果是否播放完成
            while (!targetsetManager.IsPlayResultEffectOk)
            {
                yield return null;
            }
            DestroyImmediate(targetsetManager);
        }
        isPlayImmediateSkillEnd = true;
    }


    /// <summary>
    /// 回调播放结果效果
    /// </summary>
    private void OnCallPlayResultEffect(object param)
    {
        new UISkillResultEffectsComponent(param as CombatEffect.SkillEffectInfo, teams);
    }



    //
    private bool isPlayImmediateSkillEnd;
    private List<UICombatTeam> teams;
}
