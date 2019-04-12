using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogPanel:UIPanelBehaviour
{
    private Animator m_animator;
    private Action m_showEndAction;

    private GameObject m_dia;

    private NormalDialog m_dialog;

    protected override void OnAwake()
    {
        m_animator = gameObject.GetComponent<Animator>();
        gameObject.SetActive(false);
        m_dia = transform.Find("RormalDialog").gameObject;
        m_dialog = m_dia.GetComponent<NormalDialog>();
    }

    public void Init(List<string> list,Action showEndAction)
    {
        gameObject.SetActive(true);
        m_dia.SetActive(true);
        m_showEndAction = showEndAction;
        m_dialog.InitInfo(list,CloseAction);
    }

    public void PlayAnim(string animName)
    {
        m_animator.Play(animName);
    }

    private void CloseAction()
    {
        if(m_showEndAction != null)
        {
            m_showEndAction();
        }
    }
}

