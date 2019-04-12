using System;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;
using EventCenter;

public class LodingPanel: UIPanelBehaviour
{
    private float m_allTime = 2.5f;

    private float m_timer;

    private bool m_canClose;
    private bool m_startTime;

    private SkeletonGraphic m_skeletonGraphic;
    private Animator m_animator;

    public Action action;
    public Action HideAction;

    private void OnDisable()
    {
       // Debug.Log("aa");
    }

    protected override void OnShow(List<object> parmers = null)
    {
        m_animator = transform.Find("Loading").GetComponent<Animator>();
        m_animator.gameObject.SetActive(false);
        m_skeletonGraphic = transform.Find("Bg").GetComponent<SkeletonGraphic>();
    }

    protected override void OnHide()
    {
        if(HideAction != null)
            HideAction();
        EventManager.Instance.TriggerEvent(EventSystemType.UI, EventTypeNameDefine.ShowMainPanelBtn);
    }

    public void PlayClose()
    {
        m_skeletonGraphic.AnimationState.Complete += PlayCloseComplete;
        m_skeletonGraphic.AnimationState.SetAnimation(0,"Close",false);
    }

    private void PlayCloseComplete(TrackEntry trackEntry)
    {
        m_startTime = true;
        m_animator.gameObject.SetActive(true);
        m_animator.Play("Loading");
        if (action != null)
        {
            action();
        }
    }

    private void PlayOpenComplete(TrackEntry trackEntry)
    {
        UIPanelManager.Instance.Hide<LodingPanel>();
    }

    private void Update()
    {
        if (!m_startTime)
            return;
        if(m_timer < m_allTime)
        {
            m_timer += Time.deltaTime;
        }
        else
        {
            if(m_canClose)
            {
                ClosePanel();
                m_canClose = false;
            }
        }
    }

    public void Close()
    {
        if(m_timer >= m_allTime)
        {
            ClosePanel();
        }
        else
        {
            m_canClose = true;
        }
    }

    private void ClosePanel()
    {
        m_animator.gameObject.SetActive(false);
        m_skeletonGraphic.AnimationState.Complete -= PlayCloseComplete;
        m_skeletonGraphic.AnimationState.SetAnimation(0,"Open",false);
        m_skeletonGraphic.AnimationState.Complete += PlayOpenComplete;
    }
}
