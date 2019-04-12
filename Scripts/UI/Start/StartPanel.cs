using System;
using System.Collections;
using Spine;
using Spine.Unity;
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

    private Text m_conText;
    private Text m_playText;
    private Text m_proText;
    private Text m_exitText;
    private Text m_setText;

    private Text m_currentText;

    private Color m_normal = new Color(46 / 255f,42 / 255f,42 / 255f);
    private Color m_highLight = new Color(109 / 255f,38 / 255f,6 / 255f);

   // private SkeletonGraphic m_skeletonGraphic;


    private void Awake()
    {
        m_conText = transform.Find("Btn/ContinueBtn/Text").GetComponent<Text>();
        m_playText = transform.Find("Btn/StartBtn/Text").GetComponent<Text>();
        m_proText = transform.Find("Btn/GetProcessBtn/Text").GetComponent<Text>();
        m_exitText = transform.Find("Btn/ExitBtn/Text").GetComponent<Text>();
        m_setText = transform.Find("Btn/SettingBtn/Text").GetComponent<Text>();

        GameObject conBtn = transform.Find("Btn/ContinueBtn").gameObject;
        Utility.AddButtonListener(transform.Find("Btn/ContinueBtn/Btn"),() => ClickContinue(conBtn));

        GameObject playBtn = transform.Find("Btn/StartBtn").gameObject;
        Utility.AddButtonListener(transform.Find("Btn/StartBtn/Btn"),() => ClickPlay(playBtn));

        GameObject getProcessBtn = transform.Find("Btn/GetProcessBtn").gameObject;
        Utility.AddButtonListener(transform.Find("Btn/GetProcessBtn/Btn"),() => ClickGetProcess(getProcessBtn));

        GameObject setBtn = transform.Find("Btn/SettingBtn").gameObject;
        Utility.AddButtonListener(transform.Find("Btn/SettingBtn/Btn"),() => ClickSetting(setBtn));


        GameObject exitBtn = transform.Find("Btn/ExitBtn").gameObject;
        Utility.AddButtonListener(transform.Find("Btn/ExitBtn/Btn"),() => ClickExit(exitBtn));
        Utility.AddButtonListener(transform.Find("Introdudce/Back"),() => ClickBack());

        m_login = transform.Find("Logo").gameObject;
        m_btn = transform.Find("Btn").gameObject;
        m_specialBg = transform.Find("SpecialBg").gameObject;
        m_normalBg = transform.Find("NormalBg").gameObject;
        m_specialBg.SetActive(false);
        m_normalBg.SetActive(true);

        m_canvasGroup = transform.Find("Introdudce").GetComponent<CanvasGroup>();
        m_canvasGroup.gameObject.SetActive(false);
        m_canvasGroup.alpha = 0;

        //GameObject eff = UIEffectFactory.Instance.GetEffectObj(UIEffectNameDefine.StartEff);
        //Utility.SetParent(eff,transform.Find("Btn/EFPos"),true,new Vector3(0.5f,0.6f,1f));
        //m_skeletonGraphic = eff.GetComponent<SkeletonGraphic>();

      //  StartCoroutine(PlayAnim());
    }

    protected override void OnHide()
    {
        //UIEffectFactory.Instance.FreeEffect(UIEffectNameDefine.StartEff);
        //m_skeletonGraphic.AnimationState.Complete -= PlayIdleComplete;
    }

    //private IEnumerator PlayAnim()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    m_skeletonGraphic.AnimationState.SetAnimation(0,"Idle3",false);
    //    m_skeletonGraphic.AnimationState.Complete += PlayIdleComplete;
    //}

    //private void PlayIdleComplete(TrackEntry trackEntry)
    //{
    //    m_skeletonGraphic.AnimationState.SetAnimation(0,"Idle1",true);
    //}

    private void UpdateText(Text text)
    {
        if(m_currentText != null)
        {
            m_currentText.color = m_normal;
        }
        m_currentText = text;
        m_currentText.color = m_highLight;
    }

    public void ClickContinue(GameObject gameObject)
    {
        UpdateText(m_conText);
        SetSelectShow(gameObject,() => StartCoroutine(LoadNext()));
    }

    public void ClickPlay(GameObject gameObject)
    {
        UpdateText(m_playText);
        SetSelectShow(gameObject,() => StartCoroutine(LoadNext()));
    }

    public void ClickGetProcess(GameObject gameObject)
    {
        UpdateText(m_proText);
        //SetSelectShow(gameObject,() => UIPanelManager.Instance.Show<UIGameData>());
        SetSelectShow(gameObject,null);
    }

    public void ClickSetting(GameObject gameObject)
    {
        UpdateText(m_setText);
        // SetSelectShow(gameObject,null);
        SetSelectShow(gameObject,() => StartCoroutine(LoadNext()));
    }


    public void ClickExit(GameObject gameObject)
    {
        UpdateText(m_exitText);
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

        //m_currentText.color = m_normal;

        IntroducePanel introduce = m_canvasGroup.gameObject.GetComponent<IntroducePanel>();
        string info = Text_templateConfig.GetText_config(1001).text;
        string[] array = info.Split('\n');
        introduce.ShowText(array);
    }

    //测试代码
    private void ClickBack()
    {
        IntroducePanel introduce = m_canvasGroup.gameObject.GetComponent<IntroducePanel>();
        introduce.m_textInfo.Dis();

        //  GameObjectPool.Instance.Free(StringDefine.ObjectPooItemKey.IntroduceItem);
        m_canvasGroup.gameObject.SetActive(false);
        m_login.SetActive(true);
        m_btn.SetActive(true);
        m_specialBg.SetActive(false);
        m_normalBg.SetActive(true);
        m_canvasGroup.alpha = 0;
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
