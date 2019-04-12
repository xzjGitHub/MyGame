using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 角色结束阶段特效
/// </summary>
public class UICharEndPhaseEffect : MonoBehaviour
{
    private Transform moveTransform;
    private Transform fixedTransform;
    private bool isOk;
    //
    private PhaseEffectConfig phaseEffectConfig;
    private StateEffect_show stateEffectShow;
    //
    private List<UIPlayCharEffect> playCharPhaseEffects = new List<UIPlayCharEffect>();
    private UIPlayCharEffect _playCharEffect;
    //
    private CoroutineUtil IE_PlayEffect;


    public bool IsOk
    {
        get { return isOk; }
    }

    private int teamId;
    private int charIndex;
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(int _teamId, int _charIndex)
    {
        teamId = _teamId;
        charIndex = _charIndex;
        moveTransform = transform.Find("Move");
        fixedTransform = transform.Find("Fixed");
    }

    /// <summary>
    /// 播放特效
    /// </summary>
    public IEnumerator PlayEffect(int _phaseId)
    {
        for (int i = 0; i < playCharPhaseEffects.Count; i++)
        {
            DestroyImmediate(playCharPhaseEffects[i].gameObject);
        }
        playCharPhaseEffects.Clear();
        //
        phaseEffectConfig = PhaseEffectConfigConfig.GetPhaseEffectConfig(_phaseId);
        if (phaseEffectConfig == null)
        {
            isOk = true;
            yield break;
        }
        //
        for (int i = 0; i < GetMaxNum(phaseEffectConfig.endEffect); i++)
        {
            playCharPhaseEffects.Clear();
            foreach (var item in GetEffectIds(i, phaseEffectConfig.endEffect))
            {
                stateEffectShow = StateEffect_showConfig.GetStateEffectShow(item);
                if (stateEffectShow == null) continue;
                StartPlayEffect();
            }
            while (!playCharPhaseEffects.All(t => t.IsOk))
            {
                yield return null;
            }
        }
        isOk = true;
    }

    /// <summary>
    /// 重置资源
    /// </summary>
    public void ResetRes()
    {
        playCharPhaseEffects.Clear();
    }

    private void StartPlayEffect()
    {
        GameObject _obj = ResourceLoadUtil.LoadSkillEffect(stateEffectShow.RP_Name);
        if (_obj == null) return;
        switch (stateEffectShow.Follow)
        {
            case 0:
                ResourceLoadUtil.ObjSetParent(_obj, fixedTransform, Vector3.one, moveTransform.localPosition);
                break;
            case 1:
                ResourceLoadUtil.ObjSetParent(_obj, moveTransform);
                break;
        }
        _playCharEffect = _obj.AddComponent<UIPlayCharEffect>();
        _playCharEffect.PlayEffect(stateEffectShow.EffectName, stateEffectShow.Loop == 1);
        playCharPhaseEffects.Add(_playCharEffect);
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
            if (_lists[i].Count < _num) continue;
            _num = _lists[i].Count;
        }
        return _num;
    }

}
