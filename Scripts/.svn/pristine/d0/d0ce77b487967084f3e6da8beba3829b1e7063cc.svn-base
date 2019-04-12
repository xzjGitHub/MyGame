using System.Collections;
using UnityEngine;

public class UIPlayMainEOEvent : MonoBehaviour
{

    public bool IsPlayOk { get { return isPlayOk; } }

    public int StateIndex { get { return stateIndex; } }

    public int HitCharIndex { get { return hitCharIndex; } }

    public UICharUnit HitChar { get { return hitChar; } }

    public delegate void CallBack(UIPlayMainEOEvent param);
    public CallBack OnPlayEOEventOk;

    public void Init(ObjectEffect objectEffect, CRTargetUnitInfo targetUnitInfo, UICharUnit atkChar, UICharUnit hitChar, int stateIndex)
    {
        this.targetUnitInfo = targetUnitInfo;
        this.objectEffect = objectEffect;
        this.atkChar = atkChar;
        this.hitChar = hitChar;
        hitCharIndex = targetUnitInfo.hitIndex;
        this.stateIndex = stateIndex;
        hitTransform = hitChar.ActionOperation.moveTrans;
        sourceIndex = targetUnitInfo.sourceIndex;
    }
    /// <summary>
    /// 开始播放
    /// </summary>
    public void StarPlay()
    {
        UIPlayEffectObj playEffectObj = (atkChar == null ? gameObject : atkChar.fixedTrans.gameObject).AddComponent<UIPlayEffectObj>();
        playEffectObj.Init(objectEffect, hitChar, atkChar, stateIndex, sourceIndex, targetUnitInfo.skillEffect.skillEffectResults[stateIndex]);
        playEffectObj.OnCallPlayEnd = OnCallPlayEnd;
        //
        //    int sortingOrder = atkChar != null ? atkChar.ActionOperation.SortingOrder + 1 : 21;
        playEffectObj.StartPlay();
    }

    private void OnCallPlayEnd(UIPlayEffectObj playEffectObj)
    {
        if (IECheckResult == null)
        {
            IECheckResult = new CoroutineUtil(CheckResult());
        }
        isEOEffectOk = true;
        DestroyImmediate(playEffectObj);
    }



    private IEnumerator CheckResult()
    {
        while (!isEOEffectOk)
        {
            yield return null;
        }

        //指定人 指定状态
        PlayResultEffect(targetUnitInfo.skillEffect.skillEffectResults[stateIndex]);
    }

    /// <summary>
    /// 播放主技能效果结果
    /// </summary>
    private void PlayResultEffect(CRSkillEffectResult crSkillEffectResult)
    {
        isPlayMainResultEffectOk = false;
        PlaySkillResultEffectObj playSkillResult = hitChar.moveTrans.gameObject.AddComponent<PlaySkillResultEffectObj>();
        playSkillResult.Init(crSkillEffectResult, atkChar.ActionOperation.SortingOrder, hitChar);
        playSkillResult.OnEndPlay = OnCallMainPlaySkillResultEffectEnd;
        playSkillResult.StartPlay();
    }

    /// <summary>
    /// 回调主_技能结果效果播放完成
    /// </summary>
    /// <param name="playSkillResult"></param>
    private void OnCallMainPlaySkillResultEffectEnd(PlaySkillResultEffectObj playSkillResult)
    {
        isPlayMainResultEffectOk = true;
        isPlayOk = true;
        if (OnPlayEOEventOk != null)
        {
            OnPlayEOEventOk(this);
        }
    }




    //
    private CRTargetUnitInfo targetUnitInfo;
    private ObjectEffect objectEffect;
    private UICharUnit atkChar;
    private UICharUnit hitChar;
    private int stateIndex;
    private  int sourceIndex;
    private int hitCharIndex;
    private Transform hitTransform;
    private  int sortingOrder;
    private bool isPlayOk;
    private bool isPlayMainResultEffectOk;
    private bool isEOEffectOk;
    //
    private CoroutineUtil IECheckResult;

}
