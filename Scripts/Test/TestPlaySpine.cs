﻿using Spine;
using Spine.Unity;
using System.Collections;
using UnityEngine;

public class TestPlaySpine : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public SkeletonAnimation skeletonAnimation1;
    private CoroutineUtil onPlay;
    private CoroutineUtil onPlay1;
    public void OnClickPlay()
    {
        //if (onPlay == null)
        //{
        //    onPlay = new CoroutineUtil(IEPlay());
        //}
        //else
        //{
        //    onPlay.Stop();
        //    onPlay = new CoroutineUtil(IEPlay());
        //}
        if (onPlay1 == null)
        {
            onPlay1 = new CoroutineUtil(IEPlay1());
        }
        else
        {
            onPlay1.Stop();
            onPlay1 = new CoroutineUtil(IEPlay1());
        }
    }

    private IEnumerator IEPlay()
    {
        for (int i = 0; i < name.Length; i++)
        {
            playIndex = i;
            LogHelper_MC.Log("Play=" + name[playIndex]);
            SkeletonTool.PlayAnimation(skeletonAnimation, name[playIndex], false);
            skeletonAnimation.state.Complete -= trackentry;
            skeletonAnimation.state.Complete += trackentry;
            isok = false;
            while (!isok)
            {
                yield return null;
            }
        }
    }
    private IEnumerator IEPlay1()
    {
      //  yield break;
        for (int i = 0; i < name1.Length; i++)
        {
            playIndex1 = i;
            LogHelper_MC.Log("Play=" + name1[playIndex1]);
            SkeletonTool.PlayAnimation(skeletonAnimation1, name1[playIndex1], false);
            skeletonAnimation1.state.Complete -= trackentry1;
            skeletonAnimation1.state.Complete += trackentry1;
            isok1 = false;
            while (!isok1)
            {
                yield return null;
            }
        }
    }

    private void trackentry(TrackEntry trackEntry)
    {
        LogHelper_MC.Log("Play=" + name[playIndex] + "完成");
        isok = true;
    }
    private void trackentry1(TrackEntry trackEntry)
    {
        LogHelper_MC.LogError("Play=" + name1[playIndex1] + "完成");
        isok1 = true;
    }
    //
    private string[] name = new string[] {  "Atk_chongci1", "Atk_gongji", "Idle", "Hurt", "Hurt_jitui", "Die", "Celebrate", "Run",  "Dazhao","Fanhui" , "Atk_gongji1" };
    private readonly string[] name1 = new string[] { "death", "hoverboard", "idle", "idle-turn", "jump", "portal", "run", "run-to-idle", "shoot", "walk" };
    private bool isok;
    private bool isok1;
    private int playIndex;
    private int playIndex1;
}
