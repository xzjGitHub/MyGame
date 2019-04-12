using GameEventDispose;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 动作阶段_状态机
/// </summary>
public class UICharActionPhase : UICharBase
{

    public void Init(UICharBase charBase)
    {
        //
        base.InitInfo(charBase);
        //
        EventDispatcher.Instance.CharEvent.AddEventListener<CharActionOperation, int, int, object>(EventId.CharEvent, OnCharEvent);
    }


    /// <summary>
    /// 初始化阶段状态机——自动播放开始阶段
    /// </summary>
    /// <param name="_effectSetID"></param>
    private void PlayPhaseStart(int _effectSetID)
    {
        DestroyImmediateObj();
        //
        phaseEffectConfig = PhaseEffectConfigConfig.GetPhaseEffectConfig(_effectSetID);
        if (phaseEffectConfig == null) return;
        IE_StartPhasePlayEffect = new CoroutineUtil(PlayEffect(phaseEffectConfig.startEffect));
        IE_PhasePlayEffect = new CoroutineUtil(PlayProcessEffect(phaseEffectConfig.phaseEffect1));
    }


    /// <summary>
    /// 阶段结束
    /// </summary>
    private void PlayPhaseEnd()
    {
        DestroyImmediateObj();
        if (phaseEffectConfig == null) return;
        //
        IE_EndPhasePlayEffect = new CoroutineUtil(PlayEffect(phaseEffectConfig.endEffect));
    }


    /// <summary>
    /// 播放特效
    /// </summary>
    private IEnumerator PlayEffect(List<List<int>> _lists)
    {
        for (int i = 0; i < GetMaxNum(_lists); i++)
        {
            foreach (var item in GetEffectIds(i, _lists))
            {
                StartPlayEffect(CommonEffectConfigConfig.GetCommonEffectConfig(item), effectList);
            }
        }
        yield return null;
    }
    /// <summary>
    /// 播放过程
    /// </summary>
    private IEnumerator PlayProcessEffect(List<int> effectIds)
    {
        UIPlayCharEffect _playCharEffect;
        foreach (var item in effectIds)
        {
            _playCharEffect = StartPlayEffect(CommonEffectConfigConfig.GetCommonEffectConfig(item), effectList);
            if (_playCharEffect == null) continue;
            while (!_playCharEffect.IsOk) yield return null;
            //播放完成就销毁
        }
    }

    /// <summary>
    /// 开始播放特效
    /// </summary>
    /// <param name="_list"></param>
    /// <returns></returns>
    private UIPlayCharEffect StartPlayEffect(CommonEffectConfig commonEffect, List<UIPlayCharEffect> _list = null)
    {
        if (commonEffect == null) return null;
        //
        GameObject _obj = ResourceLoadUtil.LoadSkillEffect(commonEffect.commonEffect);
        if (_obj == null) return null;
        switch (commonEffect.follow)
        {
            case 0:
                ResourceLoadUtil.ObjSetParent(_obj, fixedTrans, Vector3.one, moveTrans.localPosition);
                break;
            case 1:
                ResourceLoadUtil.ObjSetParent(_obj, moveTrans);
                break;
        }
        UIPlayCharEffect _playCharEffect = _obj.AddComponent<UIPlayCharEffect>();
        _playCharEffect.PlayEffect(commonEffect.commonEffect, commonEffect.loop == 1);
        if (_list != null)
        {
            _list.Add(_playCharEffect);
        }
        return _playCharEffect;
    }
    /// <summary>
    /// 获得特效ID
    /// </summary>
    /// <param name="_index"></param>
    /// <param name="_lists"></param>
    /// <returns></returns>
    private List<int> GetEffectIds(int _index, List<List<int>> _lists)
    {
        return (from item in _lists where item.Count - 1 >= _index select item[_index]).ToList();
    }
    /// <summary>
    /// 得到最大数量
    /// </summary>
    /// <param name="_lists"></param>
    /// <returns></returns>
    private int GetMaxNum(List<List<int>> _lists)
    {
        int _num = 0;
        for (int i = 0; i < _lists.Count; i++)
        {
            if (_lists[i].Count < _num) continue;
            _num = _lists[i].Count;
        }
        return _num;
    }

    private void DestroyImmediateObj()
    {
        if (IE_StartPhasePlayEffect != null) IE_StartPhasePlayEffect.Stop();
        if (IE_PhasePlayEffect != null) IE_PhasePlayEffect.Stop();
        if (IE_DuringPhasePlayEffect != null) IE_DuringPhasePlayEffect.Stop();
        if (IE_EndPhasePlayEffect != null) IE_EndPhasePlayEffect.Stop();
        for (int i = 0; i < effectList.Count; i++)
        {
            DestroyImmediate(effectList[i].gameObject);
        }
        effectList.Clear();
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.CharEvent.RemoveEventListener<CharActionOperation, int, int, object>(EventId.CharEvent, OnCharEvent);
    }

    private void OnCharEvent(CharActionOperation arg1, int teamID, int charID, object arg2)
    {
        if (teamID != base.teamID || charID != base.charID) return;
        switch (arg1)
        {
            case CharActionOperation.Init:
            case CharActionOperation.Fade:
            case CharActionOperation.FadeOut:
            case CharActionOperation.Action:
            case CharActionOperation.ResetPos:
            case CharActionOperation.ActionEffect:
            case CharActionOperation.ActonEventEffect:
                break;
            case CharActionOperation.PhaseStartEffect:
                PlayPhaseStart((int)arg2);
                break;
            case CharActionOperation.PhaseEndEffect:
                PlayPhaseEnd();
                break;
        }
    }

    //
    private PhaseEffectConfig phaseEffectConfig;
    //
    private CoroutineUtil IE_StartPhasePlayEffect;
    private CoroutineUtil IE_PhasePlayEffect;
    private CoroutineUtil IE_DuringPhasePlayEffect;
    private CoroutineUtil IE_EndPhasePlayEffect;
    //
    List<UIPlayCharEffect> effectList = new List<UIPlayCharEffect>();
    private string stateName;
}
