using Spine;
using Spine.Unity;
using UnityEngine;

public class UIPlayHitlEffect : MonoBehaviour
{
    public delegate void Callback(int a, int b);

    public Callback PlayEndCallback;
    //
    private SkeletonAnimation skeletonAnimation;
    private bool isOk;
    private int castTeam;
    private int targetIndex;
    //

    public bool IsOk
    {
        get { return isOk; }
    }



    /// <summary>
    /// 播放技能命中特效——直接加载资源
    /// </summary>
    public void PlayHitEffcet(string _effectName, int _castTeam, int _targetIndex)
    {
        castTeam = _castTeam;
        targetIndex = _targetIndex;
        skeletonAnimation = transform.GetComponent<SkeletonAnimation>();
        skeletonAnimation.state.Complete += PlayEnd;
        SkeletonTool.PlayAnimation(skeletonAnimation, _effectName, false);
    }


    void PlayEnd(TrackEntry trackEntry)
    {
        isOk = true;
        skeletonAnimation.state.Complete -= PlayEnd;
        gameObject.SetActive(false);
    }
}
