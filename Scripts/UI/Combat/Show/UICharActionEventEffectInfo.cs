﻿using GameEventDispose;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class UICharActionEventEffectInfo : UICharBase
{

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(UICharBase charBase)
    {
        base.InitInfo(charBase);
        //
        EventDispatcher.Instance.CharEvent.AddEventListener<CharActionOperation, int, int, object>(EventId.CharEvent, OnCharEvent);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(CombatUnit combatUnit, Transform moveTrans = null, Transform fixedTrans = null)
    {
        this.moveTrans = moveTrans;
        this.fixedTrans = fixedTrans;
        //
        base.InitInfo(combatUnit);
        //
        EventDispatcher.Instance.CharEvent.AddEventListener<CharActionOperation, int, int, object>(EventId.CharEvent, OnCharEvent);
    }

    public void Init(UICharBase charBase, UICharActionOperation actionOperation)
    {
        this.actionOperation = actionOperation;
        //
        base.InitInfo(charBase);
        //
        EventDispatcher.Instance.CharEvent.AddEventListener<CharActionOperation, int, int, object>(EventId.CharEvent, OnCharEvent);
    }

    private void PlayEventEffect(int _effectSetID, int _index)
    {
        effectSetConfig = EffectSetConfigConfig.GetEffectSetConfig(_effectSetID);
        if (effectSetConfig == null)
        {
            return;
        }
        //
        switch (_index)
        {
            case 0:
                IE_PlayEffect = new CoroutineUtil(PlayEffect(playCharEffects1, effectSetConfig.event1));
                break;
            case 1:
                IE_PlayEffect = new CoroutineUtil(PlayEffect(playCharEffects2, effectSetConfig.event2));
                break;
            case 2:
                IE_PlayEffect = new CoroutineUtil(PlayEffect(playCharEffects3, effectSetConfig.event3));
                break;
        }
    }


    private IEnumerator PlayEffect(List<UIPlayCharEffect> _playCharEffects, List<List<int>> _lists)
    {
        for (int i = 0; i < _playCharEffects.Count; i++)
        {
            DestroyImmediate(_playCharEffects[i].gameObject);
        }
        for (int i = 0; i < GetMaxNum(_lists); i++)
        {
            _playCharEffects.Clear();
            foreach (int item in GetEffectIds(i, _lists))
            {
                commonEffectConfig = CommonEffectConfigConfig.GetCommonEffectConfig(item);
                if (commonEffectConfig == null)
                {
                    continue;
                }

                StartPlayEffect(_playCharEffects);
            }
            while (!_playCharEffects.All(t => t.IsOk))
            {
                yield return null;
            }
        }
    }

    private void StartPlayEffect(List<UIPlayCharEffect> _playCharEffects)
    {
        if (charUnit==null)
        {
            charUnit = UICombatTool.Instance.GetTeamUI(teamID).GetChar(charIndex);
        }
        UIPlayEffect playEffect = gameObject.AddComponent<UIPlayEffect>();
        playEffect.OnPlayEnd = null;
        //播完完成就要播放结果
        playEffect.PlayCommonEffect(PlayEffectTool.CreateEffectInfo(commonEffectConfig.commonEffect, commonEffectConfig.commonEffect, moveTrans, charUnit.AtkSortingOrder), commonEffectConfig, actionOperation);


        LogHelper_MC.Log("角色动作事件修改");
        //_playCharEffect = _obj.AddComponent<UIPlayCharEffect>();
        //_playCharEffect.PlayEffect(commonEffectConfig.commonEffect, commonEffectConfig.loop == 1);
        //_playCharEffects.Add(_playCharEffect);
    }

    private List<int> GetEffectIds(int _index, List<List<int>> _lists)
    {
        return (from item in _lists where item.Count - 1 >= _index select item[_index]).ToList();
    }

    private int GetMaxNum(List<List<int>> _lists)
    {
        int _num = 0;
        for (int i = 0; i < _lists.Count; i++)
        {
            if (_lists[i].Count < _num)
            {
                continue;
            }

            _num = _lists[i].Count;
        }
        return _num;
    }

    private void OnCharEvent(CharActionOperation arg1, int teamID, int charID, object arg2)
    {
        if (teamID != base.teamID || charID != base.charID)
        {
            return;
        }
        switch (arg1)
        {
            case CharActionOperation.Init:
            case CharActionOperation.Fade:
            case CharActionOperation.FadeOut:
            case CharActionOperation.Action:
            case CharActionOperation.ResetPos:
            case CharActionOperation.ActionEffect:
                break;
            case CharActionOperation.ActonEventEffect:
                object[] obj = arg2 as object[];
                PlayEventEffect((int)obj[0], (int)obj[1]);
                break;
            case CharActionOperation.PhaseStartEffect:
                break;
        }
    }

    /// <summary>
    /// 停止所有协成
    /// </summary>
    private void StopAllCoroutine()
    {
        if (IE_PlayEffect != null)
        {
            IE_PlayEffect.Stop();
        }

        IE_PlayEffect = null;
    }

    private void OnDestroy()
    {
        StopAllCoroutine();
        EventDispatcher.Instance.CharEvent.RemoveEventListener<CharActionOperation, int, int, object>(EventId.CharEvent, OnCharEvent);
    }

    private UICharActionOperation actionOperation;
    //
    private CommonEffectConfig commonEffectConfig;
    private EffectSetConfig effectSetConfig;
    private readonly UIPlayCharEffect _playCharEffect;
    //
    private readonly List<UIPlayCharEffect> playCharEffects1 = new List<UIPlayCharEffect>();
    private readonly List<UIPlayCharEffect> playCharEffects2 = new List<UIPlayCharEffect>();
    private readonly List<UIPlayCharEffect> playCharEffects3 = new List<UIPlayCharEffect>();
    //
    private CoroutineUtil IE_PlayEffect;
    private UICharUnit charUnit;
}
