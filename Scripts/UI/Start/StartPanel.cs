﻿using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel: UIPanelBehaviour
{
    private GameObject m_currentSelect;
    private GameObject m_login;
    private GameObject m_btn;
    private GameObject m_specialBg;
    private GameObject m_normalBg;
    private CanvasGroup m_canvasGroup;

    private Text m_currentText;

    private Color m_normal = new Color(46 / 255f,42 / 255f,42 / 255f);
    private Color m_highLight = new Color(109 / 255f,38 / 255f,6 / 255f);

    private DOTweenAnimation m_anim;
    private IntroducePanel introduce;

    private void Awake()
    {
        m_anim = transform.Find("Mask").GetComponent<DOTweenAnimation>();
        DoTweenPlayCallBack callBack = m_anim.gameObject.GetComponent<DoTweenPlayCallBack>();
        callBack.ComplateCall = () =>
        {
            UIPanelManager.Instance.Hide<StartPanel>();
        };

        GameObject conBtn = transform.Find("Btn/ContinueBtn").gameObject;
        Text conText = conBtn.transform.Find("Text").GetComponent<Text>();
        Utility.AddButtonListener(conBtn.transform.Find("Btn"),() => ClickContinue(conBtn,conText));

        GameObject playBtn = transform.Find("Btn/StartBtn").gameObject;
        Text playText = playBtn.transform.Find("Text").GetComponent<Text>();
        Utility.AddButtonListener(playBtn.transform.Find("Btn"),() => ClickPlay(playBtn,playText));

        GameObject getProcessBtn = transform.Find("Btn/GetProcessBtn").gameObject;
        Text proText = getProcessBtn.transform.Find("Text").GetComponent<Text>();
        Utility.AddButtonListener(getProcessBtn.transform.Find("Btn"),() => ClickGetProcess(getProcessBtn,proText));

        GameObject setBtn = transform.Find("Btn/SettingBtn").gameObject;
        Text setText = setBtn.transform.Find("Text").GetComponent<Text>();
        Utility.AddButtonListener(setBtn.transform.Find("Btn"),() => ClickSetting(setBtn,setText));

        GameObject exitBtn = transform.Find("Btn/ExitBtn").gameObject;
        Text exitText = exitBtn.transform.Find("Text").GetComponent<Text>();
        Utility.AddButtonListener(exitBtn.transform.Find("Btn"),() => ClickExit(exitBtn,exitText));

        m_login = transform.Find("Logo").gameObject;
        m_btn = transform.Find("Btn").gameObject;
        m_specialBg = transform.Find("SpecialBg").gameObject;
        m_normalBg = transform.Find("NormalBg").gameObject;
        m_specialBg.SetActive(false);
        m_normalBg.SetActive(true);

        m_canvasGroup = transform.Find("Introdudce").GetComponent<CanvasGroup>();
        m_canvasGroup.gameObject.SetActive(false);
        m_canvasGroup.alpha = 0;

        introduce = m_canvasGroup.gameObject.GetComponent<IntroducePanel>();
        introduce.InitComponent(() => m_anim.DOPlay());
    }

    protected override void OnHide()
    {
        m_anim.onComplete.RemoveAllListeners();
    }

    private void UpdateText(Text text)
    {
        if(m_currentText != null)
        {
            m_currentText.color = m_normal;
        }
        m_currentText = text;
        m_currentText.color = m_highLight;
    }

    public void ClickContinue(GameObject gameObject,Text text)
    {
        UpdateText(text);
        SetSelectShow(gameObject,() => StartCoroutine(LoadNext()));
    }

    public void ClickPlay(GameObject gameObject,Text text)
    {
        UpdateText(text);
        SetSelectShow(gameObject,() => StartCoroutine(LoadNext()));
    }

    public void ClickGetProcess(GameObject gameObject,Text text)
    {
        UpdateText(text);
        SetSelectShow(gameObject,null);
    }

    public void ClickSetting(GameObject gameObject,Text text)
    {
        UpdateText(text);
        SetSelectShow(gameObject,null);
    }


    public void ClickExit(GameObject gameObject,Text text)
    {
        UpdateText(text);
        SetSelectShow(gameObject,() => Application.Quit());
    }

    private IEnumerator LoadNext()
    {
        yield return new WaitForSeconds(0.2f);
        m_canvasGroup.gameObject.SetActive(true);
        m_login.SetActive(false);
        m_btn.SetActive(false);
        m_specialBg.SetActive(true);
        m_normalBg.SetActive(false);

        Text_template textInfo = Text_templateConfig.GetText_config(1001);
        string[] array = textInfo.text.Split('\n');
        introduce.ShowText(array);
    }

    private void SetSelectShow(GameObject gameObject,Action action)
    {
        if(m_currentSelect != null)
        {
            m_currentSelect.transform.Find("Select").gameObject.SetActive(false);
        }
        m_currentSelect = gameObject;

        GameObject nowSelect = m_currentSelect.transform.Find("Select").gameObject;
        nowSelect.SetActive(true);
        nowSelect.GetComponent<SelectEffect>().StartShowEff(action);
    }

}
