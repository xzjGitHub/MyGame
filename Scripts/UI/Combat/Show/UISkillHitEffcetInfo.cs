using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameEventDispose;
using Spine;
using Spine.Unity;

/// <summary>
/// 技能命中特效操作
/// </summary>
public class UISkillHitEffcetInfo : MonoBehaviour
{
    //
    private SkeletonAnimation skeletonAnimation;

    //
    public List<UIPlayHitlEffect> playHitlEffects = new List<UIPlayHitlEffect>();
    //

    private UIPlayHitlEffect playHitlEffect;
    /// <summary>
    /// 播放技能命中特效——直接加载资源
    /// </summary>
    public void PlayHitEffcet(string _RP_Name, string _effectName, int _castTeam, int _targetIndex)
    {

        GameObject _obj = ResourceLoadUtil.LoadSkillEffect(_RP_Name, transform.Find("Move"), Vector3.one, Vector3.up * 3);
        if (_obj == null)
        {
            EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatEventType.SkillHitOk, _castTeam, (object)_targetIndex);
            return;
        }
        playHitlEffect = _obj.AddComponent<UIPlayHitlEffect>();
        playHitlEffects.Add(playHitlEffect);
        //
        playHitlEffect.PlayHitEffcet(_effectName, _castTeam, _targetIndex);
        EventDispatcher.Instance.CombatEvent.DispatchEvent(EventId.CombatEvent, CombatEventType.SkillHitOk, _castTeam, (object)_targetIndex);
    }

    /// <summary>
    /// 重置资源
    /// </summary>
    public void ResetRes()
    {
        playHitlEffects.Clear();
    }

    public void RestHitEffcet()
    {
        for (int i = 0; i < playHitlEffects.Count; i++)
        {
            if (playHitlEffects[i] == null) continue;
            Destroy(playHitlEffects[i].gameObject);
        }
        playHitlEffects = new List<UIPlayHitlEffect>();
    }

}
