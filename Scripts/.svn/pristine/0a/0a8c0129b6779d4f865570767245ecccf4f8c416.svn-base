using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectEffect:MonoBehaviour
{
    private Image m_image;

    public float Speed;

    private bool m_starShowEff;

    private Action m_effEndAction;
    private bool m_notEmitAction;

    public Button m_btn;

    private void Awake()
    {
        m_image=gameObject.GetComponent<Image>();
        m_image.gameObject.SetActive(false);
        m_image.fillAmount = 0;
    }

    public void StartShowEff(Action endAction)
    {
        m_starShowEff = true;
        m_notEmitAction = false;
        m_image.gameObject.SetActive(true);
        m_effEndAction = endAction;
    }

    private void Update()
    {
        if (!m_starShowEff)
            return;
        if (m_image.fillAmount < 1)
        {
            m_btn.enabled = false;
            m_image.fillAmount += Speed*Time.deltaTime;
        }
        else
        {
            if (m_effEndAction != null && !m_notEmitAction)
            {
                m_effEndAction();
                m_notEmitAction = true;
            }
            m_btn.enabled = true;
            m_starShowEff = false;
        }
    }

    private void OnDisable()
    {
        m_image.fillAmount = 0;
        m_btn.enabled = true;
        m_notEmitAction = false;
    }
}

