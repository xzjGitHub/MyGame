using GameEventDispose;
using System.Collections.Generic;

/// <summary>
/// 技能结果效果组件
/// </summary>
public class UISkillResultEffectsComponent
{
    /// <summary>
    /// 新建播放结果效果组件
    /// </summary>
    public UISkillResultEffectsComponent(CombatEffect.SkillEffectInfo info, List<UICombatTeam> teams)
    {
        UICombatTeam _team = teams.Find(a => a.teamID == info.hitTeamID);
        //
        UICharUnit charUnit = _team.GetChar(info.hitCharIndex);
        if (charUnit == null)
        {
            return;
        }

        charUnit.CameraShake.Shake();
        //添加播放技能效果组件
        info.skillResultEffect.Add(new PlaySkillResultEffectsObj(info, charUnit.ActionOperation, charUnit.moveTrans, OnCallStartPlay));
    }

    /// <summary>
    /// 回调开始播放
    /// </summary>
    private void OnCallStartPlay(object param)
    {
        CRExecState execState = param as CRExecState;
        //
        EventDispatcher.Instance.CharEvent.DispatchEvent(EventId.CharEvent, CharActionOperation.HPCost, new UICharInfo(execState.effectResult),
            (object)execState);
    }

}
