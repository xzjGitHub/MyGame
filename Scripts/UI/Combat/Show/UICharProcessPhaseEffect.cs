using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 角色过程阶段特效
/// </summary>
public class UICharProcessPhaseEffect : MonoBehaviour
{
    private int teamId;
    private int charIndex;
    private Transform moveTransform;
    private Transform fixedTransform;
    private bool isOk;
    //
    private PhaseEffectConfig phaseEffectConfig;
    private StateEffect_show stateEffectShow;
    //
    private UIPlayCharEffect _playCharEffect;
    private List<GameObject> objLists=new List<GameObject>();
    //
    private CoroutineUtil IE_PlayProcess1Effect;
    private CoroutineUtil IE_PlayProcess2Effect;

    public bool IsOk
    {
        get { return isOk; }
    }

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
    public IEnumerator PlayEffect(int _phaseId, CharPhaseStateType _type)
    {
        for (int i = 0; i < objLists.Count; i++)
        {
            DestroyImmediate(objLists[i]);
        }
        objLists.Clear();
        phaseEffectConfig = PhaseEffectConfigConfig.GetPhaseEffectConfig(_phaseId);
        if (phaseEffectConfig == null)
        {
            isOk = true;
            yield break;
        }
        switch (_type)
        {
            case CharPhaseStateType.Start:
                break;
            case CharPhaseStateType.Process1:
                IE_PlayProcess1Effect=new CoroutineUtil(PlayProcessEffect(phaseEffectConfig.phaseEffect1));
                break;
            case CharPhaseStateType.Process2:
                IE_PlayProcess2Effect = new CoroutineUtil(PlayProcessEffect(phaseEffectConfig.phaseEffect2));
                break;
            case CharPhaseStateType.END:
                break;
        }
    }

    /// <summary>
    /// 播放过程
    /// </summary>
    private IEnumerator PlayProcessEffect(List<int> effectIds)
    {
        isOk = false;
        foreach (var item in effectIds)
        {
            stateEffectShow = StateEffect_showConfig.GetStateEffectShow(item);
            if (stateEffectShow == null) continue;
            StartPlayEffect();
            if (_playCharEffect == null) continue;
            while (!_playCharEffect.IsOk)
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
        objLists.Clear();
    }

    private void StartPlayEffect()
    {
        GameObject _obj = ResourceLoadUtil.LoadSkillEffect(stateEffectShow.RP_Name);
        if (_obj==null) return;
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
        objLists.Add(_obj);
    }

    /// <summary>
    /// 停止所有协成
    /// </summary>
    private void StopAllCoroutine()
    {
        if (IE_PlayProcess1Effect != null) IE_PlayProcess1Effect.Stop();
        IE_PlayProcess1Effect = null;
        if (IE_PlayProcess2Effect != null) IE_PlayProcess2Effect.Stop();
        IE_PlayProcess2Effect = null;

    }

    // 当 MonoBehaviour 将被销毁时调用此函数
    private void OnDestroy()
    {
        StopAllCoroutine();
    }
}