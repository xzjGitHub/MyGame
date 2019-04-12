using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 播放effectObj
/// </summary>
public class UIPlayEffectObj : MonoBehaviour
{
    public delegate void Callback(UIPlayEffectObj param);


    public Callback OnCallPlayEnd;

    public bool IsPlayEnd { get { return isPlayEnd; } }

    public int StateIndex
    {
        get { return stateIndex; }
    }


    public void Init(ObjectEffect objectEffectObject, UICharUnit hitChar, UICharUnit atkChar, int stateIndex, int sourceIndex, CRSkillEffectResult effectResult)
    {
        _objectEffectObject = objectEffectObject;
        hitCharIndex = hitChar.charIndex;
        erCSYS = atkChar.erCSYS;
        this.hitChar = hitChar;
        this.atkChar = atkChar;
        this.stateIndex = stateIndex;
        this.sourceIndex = sourceIndex;
        this.effectResult = effectResult;
    }

    public void StartPlay()
    {
        new CoroutineUtil(PlayEffectObj());
    }

    /// <summary>
    /// 播放特效对象
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayEffectObj()
    {
        isEffectObjOk = false;
        isPlayEffectObj = true;
        isHitOk = false;
        if (_objectEffectObject != null)
        {
            playEffect = PlayEffectTool.PlayObjectEffect(effectResult, _objectEffectObject, transform, hitChar, sourceIndex, atkChar, OnCallEffectObjPlayEffectEnd, erCSYS);
            //等待角色动作事件Common事件 - _objectEffectObject - 播放效果（是否循环）-飞行（原地）-6
            while (!isEffectObjOk)
            {
                yield return null;
            }
        }
        isPlayEnd = true;
        if (OnCallPlayEnd != null)
        {
            OnCallPlayEnd(this);
        }
    }


    /// <summary>
    /// 回调EffectObj播放结束
    /// </summary>
    private void OnCallEffectObjPlayEffectEnd(UnityEngine.Object param)
    {
        isEffectObjOk = true;
        if ((param as UIPlayEffect).IsAutoDestory)
        {
            DestroyImmediate(param);
        }
    }
    /// <summary>
    /// 回调受击特效播放结束
    /// </summary>
    private void OnCallHitPlayEffectEnd(UnityEngine.Object param)
    {
        isHitOk = true;
        DestroyImmediate(param);
    }


    //
    private ObjectEffect _objectEffectObject;
    private UIPlayEffect playEffect;
    private CRSkillEffectResult effectResult;
    //
    private List<float> erCSYS;
    private UICharUnit atkChar;
    private UICharUnit hitChar;
    private int hitCharIndex;
    private bool isEffectObjOk;
    private bool isPlayEffectObj;
    private bool isHitOk;
    private bool isPlayEnd;
    private int stateIndex;
    private int sourceIndex;
    //
    private const string LeftTansformName = "Left";

}
