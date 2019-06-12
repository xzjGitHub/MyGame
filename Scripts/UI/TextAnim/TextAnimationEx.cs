
//--------------------------------------------------------------
//Creator： xzj
//Data：    5/10/2019
//Note:     
//--------------------------------------------------------------

using System;
using UnityEngine;

/// <summary>
/// TextAnimation辅助类
/// </summary>
public class TextAnimationEx: MonoBehaviour
{
    [HideInInspector]
    public Action ComplateCall;

    public MutiTextAnim MutilAnim;

    public void OnComplate()
    {
        if(ComplateCall != null)
            ComplateCall();
        if(MutilAnim != null)
            MutilAnim.PlayEndCallBack();
    }
}
