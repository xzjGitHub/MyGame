﻿using Spine;
using Spine.Unity;
using UnityEngine;

public class UINPC2: MonoBehaviour
{
    private GameObject m_npc;
    private SkeletonGraphic m_npcGraphic;

    private string m_idelAnimName;
    private string m_randomAnimName;

    private float m_randomTime;
    private float m_timer;
    private bool m_startCountDown;
    private bool m_playRandomAnim;

    private string EnterAnimName = "Enter";

    public void InitComponent(string npcName,string idleAnimName,string randomAnimName)
    {
        m_idelAnimName = idleAnimName;
        m_randomAnimName = randomAnimName;

        m_npcGraphic = transform.GetComponentInChildren<SkeletonGraphic>();
    }

    public void PlayEnter()
    {
        gameObject.SetActive(true);
        m_npcGraphic.AnimationState.Complete += PlayWalkComplete;
        m_npcGraphic.AnimationState.SetAnimation(0,EnterAnimName,false);
    }

    private void PlayWalkComplete(TrackEntry trackEntry)
    {
        m_npcGraphic.AnimationState.Complete -= PlayWalkComplete;
        PlayIdle();
    }

    public void PlayIdle()
    {
        gameObject.SetActive(true);
        m_npcGraphic.AnimationState.SetAnimation(0,m_idelAnimName,true);
        InitTime();
    }


    private void InitTime()
    {
        m_randomTime = UnityEngine.Random.Range(5,10);
        m_timer = 0;
        m_startCountDown = true;
    }

    private void Update()
    {
        if(m_startCountDown)
        {
            if(m_timer <= m_randomTime)
            {
                m_timer += Time.deltaTime;
                //Debug.LogError("m_timer: " + m_timer);
            }
            else
            {
                m_playRandomAnim = true;
                m_startCountDown = false;
                // Debug.LogError("可以播放笑2动画: ");
            }
        }
        if(m_playRandomAnim)
        {
            // Debug.LogError("开始播放笑2动画: ");
            m_npcGraphic.AnimationState.Complete += PlayRandomComplete;
            m_npcGraphic.AnimationState.SetAnimation(0,m_randomAnimName,false);
            m_playRandomAnim = false;
        }
    }

    private void PlayRandomComplete(TrackEntry trackEntry)
    {
        m_npcGraphic.AnimationState.Complete -= PlayRandomComplete;
        PlayIdle();
    }

}

