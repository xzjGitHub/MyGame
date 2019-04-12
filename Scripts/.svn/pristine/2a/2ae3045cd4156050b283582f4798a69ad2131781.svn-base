using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalDialog: MonoBehaviour
{
    public float ShowSpeed = 2;
    public float HideSpeed = 2;
    public float FirstWaitTime = 1;
    public float AutoChangeWaitTime = 1;

    private CanvasGroup m_canvas;
    private Text m_infoText;

  //  private float m_waitTime = 1f;
    private float m_autoTimer;

    private int m_showIndex;

    private bool m_hasInitComponent;
    private bool m_isAdd;
    private bool m_change;

    private bool m_startCountDown;

    private List<string> m_needShowInfo = new List<string>();

    private Action CloseAction;

    private void InitComponent()
    {
        m_infoText = transform.Find("Info").GetComponent<Text>();
        m_infoText.text = "";
        m_canvas = m_infoText.GetComponent<CanvasGroup>();
        m_canvas.alpha = 0;

        Utility.AddButtonListener(transform.Find("DiaBg"),ClickDia);

        m_hasInitComponent = true;
    }


    public void InitInfo(List<string> info,Action action)
    {
        CloseAction = action;
        if(!m_hasInitComponent)
        {
            InitComponent();
        }
        m_needShowInfo.Clear();
        m_needShowInfo.AddRange(info);

        StartCoroutine(InitText());
    }

    private void Update()
    {
        if (m_startCountDown)
        {
            m_autoTimer += Time.deltaTime;
            if (m_autoTimer >= AutoChangeWaitTime)
            {
                m_autoTimer = 0;
                m_startCountDown = false;
                ClickDia();
            }
        }

        if(m_change)
        {
            if(m_isAdd)
            {
                if(m_canvas.alpha < 1)
                {
                    if(m_infoText.text == "")
                        m_infoText.text = m_needShowInfo[m_showIndex];
                    m_canvas.alpha += ShowSpeed * Time.deltaTime;
                }
                else
                {
                    m_startCountDown = true;
                    m_change = false;
                }
            }
            else
            {
                if(m_canvas.alpha > 0)
                {
                    m_canvas.alpha -= HideSpeed * Time.deltaTime;
                }
                else
                {
                    m_infoText.text = "";
                    m_isAdd = true;
                }
            }
        }
    }

    private void ClickDia()
    {
        m_startCountDown = false;
        UpdateText(false);
    }

    private IEnumerator InitText()
    {
        yield return new WaitForSeconds(FirstWaitTime);
        UpdateText(true);
    }

    private void UpdateText(bool isAdd)
    {
        if (m_showIndex < m_needShowInfo.Count - 1)
        {
            m_change = true;
            m_isAdd = isAdd;
            m_showIndex++;
        }
        else
        {
            if (CloseAction != null)
            {
                m_showIndex = 0;
                CloseAction();
            }
        }
    }

}
