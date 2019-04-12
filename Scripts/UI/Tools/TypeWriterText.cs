using System;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriterText: MonoBehaviour
{
    private Text m_text;

    public float Interval = 0.1f;

    private string m_msg;

    private bool m_isStar;

    private float m_timer;

    private int m_currentPos;

    private Action m_endAction;

    private void Awake()
    {
        m_text = GetComponent<Text>();
        m_msg = "";
        m_text.text = "";     
    }

    private void Update()
    {
        Writing();
    }

    public void InitMsg(string msg,Action action=null)
    {
        m_endAction = action;
        m_msg = msg;
    }

    public void StartWrite()
    {
        m_isStar = true;
    }

    private void Writing()
    {
        if(m_isStar)
        {
            m_timer += Time.deltaTime;
            if(m_timer >= Interval)
            {
                m_timer = 0;
                m_currentPos++;

                m_text.text = m_msg.Substring(0,m_currentPos);
                if(m_currentPos >= m_msg.Length)
                {
                    OnFinish();
                }
            }
        }
    }


    public void EndWrite()
    {
        m_isStar = false;
        m_timer = 0;
        m_currentPos = 0;
        m_text.text = m_msg;
    }

    private void OnFinish()
    {
        m_isStar = false;
        m_timer = 0;
        m_currentPos = 0;

        if (m_endAction != null)
        {
            m_endAction();
        }
    }

}
