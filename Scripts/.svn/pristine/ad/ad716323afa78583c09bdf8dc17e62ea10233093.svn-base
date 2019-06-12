
//--------------------------------------------------------------
//Creator： xzj
//Data：    5/10/2019
//Note:     
//--------------------------------------------------------------

using I2.TextAnimation;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class MutiTextAnim: MonoBehaviour
{
    public List<string> AllText;
    public Text Text;
    public TextAnimation Anim;

    private int m_index = 0;
    private Action m_endAction;

    private bool m_cuurentPlayEnd;
    private bool m_hasClick;

    public void InitAllText(List<string> list = null,Action<int> action = null,Action showEnd = null)
    {
        Text.text = "";
        m_endAction = showEnd;
        m_index = 0;
        m_cuurentPlayEnd = false;
        m_hasClick = false;

        AllText.Clear();
        if(list != null)
            AllText.AddRange(list);
    }

    public void SetAnimCreator(ITextAnimCreator creator,List<float> time)
    {
        creator.CreateTextAnim(Anim,AllText.Count,time);
    }

    public void PlayEndCallBack()
    {
        if(m_hasClick)
            return;
        AutoShowNext();
    }

    public void AutoShowNext()
    {
        if(!CheckAllShowEnd())
        {
            ShowNextText();
        }
        else
        {
            CallEndAction();
        }
    }

    //手动
    public void ShowNextByHand()
    {
        if(CheckAllShowEnd())
            return;
        ShowNextText();
    }

    //只为自动显示点击服务的 其他的重写
    public void Click()
    {
        if(!m_cuurentPlayEnd)
        {
            m_hasClick = true;
            m_cuurentPlayEnd = true;
            Anim.StopAllAnimations();
            if(CheckAllShowEnd())
            {
                CallEndAction();
                return;
            }
            return;
        }
        else
        {
            AutoShowNext();
        }
    }

    public void QuickShow()
    {
        if(!m_cuurentPlayEnd)
        {
            m_hasClick = true;
            m_cuurentPlayEnd = true;
            Anim.StopAllAnimations();
            if(CheckAllShowEnd())
            {
                CallEndAction();
                return;
            }
            return;
        }
    }

    public bool CurrentShowEnd()
    {
        return m_cuurentPlayEnd;
    }

    private void ShowNextText()
    {
        Anim.StopAllAnimations();
        try
        {
            Text.text = AllText[m_index];
            Anim.PlayAnim(m_index);
            m_index++;
            m_cuurentPlayEnd = false;
            m_hasClick = false;
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    private bool CheckAllShowEnd()
    {
        return m_index > AllText.Count - 1;
    }

    private void CallEndAction()
    {
        if(m_endAction != null)
            m_endAction();
    }
}
