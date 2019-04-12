using UnityEngine;
using System.Collections;
using Spine;
using Spine.Unity;


/// <summary>
/// 技能效果特效操作
/// </summary>
public class UIStateEffcetInfo : MonoBehaviour
{
    private bool isOk;
    //
    private SkeletonAnimation skeletonAnimation;
    //
    private StateEffect_show stateEffectShow;
    private State_template stateTemplate;
    //
    public bool IsOk
    {
        get { return isOk; }
    }

    /// <summary>
    /// 播放状态特效——直接加载资源
    /// </summary>
    /// <param name="_effectId"></param>
    /// <returns></returns>
    public string PlayStateEffect(int _effectId)
    {
        isOk = false;
        //
        stateEffectShow = StateEffect_showConfig.GetStateEffectShow(_effectId);
        if (stateEffectShow == null)
        {
            isOk = true;
            return stateEffectShow.CharActionName;
        }
        //加载效果
        GameObject _obj = ResourceLoadUtil.LoadSkillEffect(stateEffectShow.RP_Name, transform.Find("Move"));
        if (_obj == null)
        {
            isOk = true;
            return stateEffectShow.CharActionName;
        }
        skeletonAnimation = _obj.GetComponent<SkeletonAnimation>();
        skeletonAnimation.state.Complete += PlayEnd;
        SkeletonTool.PlayAnimation(skeletonAnimation, stateEffectShow.EffectName, false);
        return stateEffectShow.CharActionName;
    }

    /// <summary>
    /// 重置资源
    /// </summary>
    public void ResetRes()
    {
        
    }

    private void PlayEnd(TrackEntry trackEntry)
    {
        isOk = true;
        skeletonAnimation.state.Complete -= PlayEnd;
        Destroy(skeletonAnimation.gameObject);
    }


}
