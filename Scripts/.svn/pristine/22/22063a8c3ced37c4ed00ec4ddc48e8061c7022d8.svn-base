using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class IntroducePanel: MonoBehaviour
{
    public float Speed = 1f;
    public float ShowTextAlpha = 0.7f;
    public float WaitShowTip = 0.7f;
    public float WaitShowEffTime=1;

    private CanvasGroup canvasGroup;
    private GameObject m_tip;
    private SkeletonGraphic m_skeletonGraphic;

    private bool m_show;
    private bool m_hasStartShowText;

    [HideInInspector]
    public TextInfo m_textInfo;
    private List<string> m_infoList = new List<string>();

    private string m_animName="jieshao2";

    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        m_skeletonGraphic = transform.Find("jieshao2").GetComponent<SkeletonGraphic>();

        m_tip = transform.Find("Tip").gameObject;
        m_tip.GetComponent<Text>().color=new Color(1,1,1,0);
        m_tip.SetActive(false);

        m_textInfo = transform.Find("Parent").GetComponent<TextInfo>();

        Utility.AddButtonListener(transform.Find("Btn"),Click);
    }

    private void Update()
    {
        if(m_show)
        {
            if(canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Speed*Time.deltaTime;
            }
            else
            {
                m_show = false;
            }
            if(canvasGroup.alpha >= ShowTextAlpha && !m_hasStartShowText)
            {
                m_textInfo.ShowText(m_infoList,WriteEnd);
                m_hasStartShowText = true;
            }
        }
    }


    public void ShowText(string[] list)
    {
        m_show = true;
        m_infoList.Clear();
        m_infoList.AddRange(list);
    }


    private void Click()
    {
        if(!m_tip.activeSelf)
        {
            m_textInfo.NeedWait = false;
        }
        else
        {
            EnterGame();
        }
    }

    private void WriteEnd()
    {
        StartCoroutine(ShowEff());
        StartCoroutine(ShowTip());
    }

    private IEnumerator ShowEff()
    {
        yield return new WaitForSeconds(WaitShowEffTime);
        m_skeletonGraphic.AnimationState.SetAnimation(0,m_animName,false);
    }

    private IEnumerator ShowTip()
    {
        yield return new WaitForSeconds(WaitShowTip);
        m_tip.SetActive(true);
    }

    private void EnterGame()
    {
        UIPanelManager.Instance.Hide<StartPanel>();
        SceneManager.Instance.EnterGame();
    }


    private void OnDisable()
    {
        m_hasStartShowText = false;
        m_tip.SetActive(false);
    }

}

