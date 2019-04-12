using System;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;

public class UIPlayCharEffect: MonoBehaviour
{
    private bool isOk;
    //
    private SkeletonAnimation skeletonAnimation;
    public bool IsOk
    {
        get { return isOk; }
    }

    public void PlayEffect(string _name,bool _loop)
    {
        skeletonAnimation = gameObject.GetComponent<SkeletonAnimation>();
        skeletonAnimation.state.Complete += PlayEnd;
        SkeletonTool.PlayAnimation(skeletonAnimation, _name,_loop);
    }

    private void PlayEnd(TrackEntry trackentry)
    {
        skeletonAnimation.state.Complete -= PlayEnd;
        isOk = true;
    }
}

