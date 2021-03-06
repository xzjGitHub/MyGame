﻿using EventCenter;
using UnityEngine;
using Guide;
using System.Collections.Generic;

public partial class NewMainPanel: UIPanelBehaviour
{
    public static NewMainPanel Instance;

    private Animator m_animator;

    private NewTitle m_title;

    private GameObject m_charBtn;
    private GameObject m_bagBtn;
    private GameObject m_advBtn;
    private GameObject m_shopBtn;
    private GameObject m_workshopBtn;
    private GameObject m_barrackBtn;
    private GameObject m_hallBtn;
    private GameObject m_coreBtn;

    protected override void OnAwake()
    {
        Instance = this;

        m_charBtn = transform.Find("Btn/Char").gameObject;
        m_charBtn.SetActive(false);
        m_bagBtn = transform.Find("Btn/Bag").gameObject;
        m_bagBtn.SetActive(false);
        m_title = transform.Find("Title").GetComponent<NewTitle>();
        m_title.Init(ClickBack);
        m_title.gameObject.SetActive(false);

        m_animator = gameObject.GetComponent<Animator>();

        UpdateWhenClickBuildFucBtn(false);

        InitBtnClick();

        EventManager.Instance.RegEventListener<bool>(EventSystemType.UI,
          EventTypeNameDefine.MainPanelAnim,UpdateWhenClickBuildFucBtn);
        EventManager.Instance.RegEventListener<bool,string>(EventSystemType.UI,
            EventTypeNameDefine.UpdateTitleName,UpdateName);

        EventManager.Instance.RegEventListener(EventSystemType.UI,
           EventTypeNameDefine.ShowMainPanelBtn,ShowTitle);

        EventManager.Instance.RegEventListener<List<MainPanelDefine>>(EventSystemType.UI,
         EventTypeNameDefine.ShowMainBtn,BtnShow);

        InitBtnObj();
        InitBtnShow();
    }

    protected override void Destroy()
    {
        EventManager.Instance.UnRegEventListener<bool>(EventSystemType.UI,
          EventTypeNameDefine.MainPanelAnim,UpdateWhenClickBuildFucBtn);
        EventManager.Instance.UnRegEventListener<bool,string>(EventSystemType.UI,
         EventTypeNameDefine.UpdateTitleName,UpdateName);

        EventManager.Instance.UnRegEventListener(EventSystemType.UI,
          EventTypeNameDefine.ShowMainPanelBtn,ShowTitle);

        EventManager.Instance.UnRegEventListener<List<MainPanelDefine>>(EventSystemType.UI,
          EventTypeNameDefine.ShowMainBtn,BtnShow);
    }

    private void InitBtnObj()
    {
        m_charBtn = transform.Find("Btn/Char").gameObject;
        m_bagBtn = transform.Find("Btn/Bag").gameObject;
        m_advBtn = transform.Find("Btn/Adventure").gameObject;
        m_coreBtn = transform.Find("Btn/Core").gameObject;
        m_hallBtn = transform.Find("Btn/Hall").gameObject;
        m_barrackBtn = transform.Find("Btn/Barrack").gameObject;
        m_workshopBtn = transform.Find("Btn/WorkShop").gameObject;
        m_shopBtn = transform.Find("Btn/Shop").gameObject;
    }

    private void InitBtnShow()
    {
        m_barrackBtn.SetActive(ControllerCenter.Instance.GuideController.HaveEndStep(GuideStep.Hall));
        m_charBtn.SetActive(ControllerCenter.Instance.GuideController.HaveEndStep(GuideStep.Hall));
        m_bagBtn.SetActive(ControllerCenter.Instance.GuideController.HaveEndStep(GuideStep.Hall));
        m_advBtn.SetActive(ControllerCenter.Instance.GuideController.HaveEndStep(GuideStep.Barrack));
        m_coreBtn.SetActive(ControllerCenter.Instance.GuideController.HaveEndStep(GuideStep.EmptyCity));
        m_hallBtn.SetActive(ControllerCenter.Instance.GuideController.HaveEndStep(GuideStep.Core));
        //这三个todo
        m_workshopBtn.SetActive(ControllerCenter.Instance.GuideController.HaveEndStep(GuideStep.Shop));
        m_shopBtn.SetActive(ControllerCenter.Instance.GuideController.HaveEndStep(GuideStep.WorkShop));
    }

    private void InitBtnClick()
    {
        Utility.AddButtonListener(transform.Find("Btn/Bag/Image"),ClickBag);
        Utility.AddButtonListener(transform.Find("Btn/Char/Image"),ClickChar);
        Utility.AddButtonListener(transform.Find("Btn/Adventure"),ClickAdventure);
        Utility.AddButtonListener(transform.Find("Btn/Core/Image"),() => ClickBuild(BuildingTypeIndex.Core));
        Utility.AddButtonListener(transform.Find("Btn/Hall/Image"),() => ClickBuild(BuildingTypeIndex.TownHall));
        Utility.AddButtonListener(transform.Find("Btn/Barrack/Image"),() => ClickBuild(BuildingTypeIndex.Barracks));
        Utility.AddButtonListener(transform.Find("Btn/WorkShop/Image"),() => ClickBuild(BuildingTypeIndex.WorkShop));
        Utility.AddButtonListener(transform.Find("Btn/Shop/Image"),() => ClickBuild(BuildingTypeIndex.Shop));
    }

    private void UpdateWhenClickBuildFucBtn(bool click)
    {
        UpdateBgPosZ(click);
    }

    private void UpdateBgPosZ(bool enlarge)
    {
        string animName = enlarge ? "NewMainPanelPlay" : "NewMainPanelPlay1";
        m_animator.Play(animName);
    }

    private void ShowTitle()
    {
        m_title.gameObject.SetActive(true);
    }
}
