﻿using Altar.View;
using EventCenter;
using Guide;
using Spine.Unity;
using System;
using UnityEngine;

namespace Barrack.View
{
    public partial class BarrackPanel: UIPanelBehaviour, IBuild
    {
        private GameObject m_charTrainObj;
        private GameObject m_normalCallObj;
        private GameObject m_currentObj;
        private GameObject m_lhObj;
        private GameObject m_leftBtn;
        private GameObject m_centerBtn;

        private SkeletonGraphic m_light;
        private SkeletonGraphic m_skeletonGraphic;
        private CharTrain m_charTrain;
        private NewNormalCallPanel m_normalCall;
        private ItemCallChar m_itemCallChar;

        private UINPC m_uiNpc;
        private UINPC2 m_uiNpc2;
        private UINPC2 m_uiNpc3;
        private Animator m_animator;

        public Action NpcWalkEnd;

        protected override void OnAwake()
        {
            Init();
        }

        protected override void OnReactive()
        {
            base.OnReactive();
            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,false,"");
        }

        private void Init()
        {
            InitComponent();
            GuideStep step = ControllerCenter.Instance.GuideController.GetCurrentStep();
            bool playNpcWalk = step == GuideStep.None ? true : false;

            m_skeletonGraphic = BuilidUtil.Instance.InitEff(
             BuildingTypeIndex.Barracks,m_uiNpc,m_leftBtn,transform.Find("EffPos/Barrack").gameObject,
             playNpcWalk,ShowComplate,BgComplate);

            m_skeletonGraphic.AnimationState.Complete -= Close;
            InitBtnEvent();
        }

        private void InitComponent()
        {
            m_leftBtn = transform.Find("RoomInfo/Btn/LeftBtn").gameObject;
            m_leftBtn.SetActive(false);

            m_centerBtn = transform.Find("RoomInfo/Btn/CenterBtn").gameObject;
            m_centerBtn.SetActive(false);

            m_charTrainObj = transform.Find("Parent/TrainChar").gameObject;
            m_charTrainObj.SetActive(false);
            m_charTrain = Utility.RequireComponent<CharTrain>(m_charTrainObj);

            m_normalCallObj = transform.Find("Parent/NewCallPanel").gameObject;
            m_normalCallObj.SetActive(false);
            m_normalCall = Utility.RequireComponent<NewNormalCallPanel>(m_normalCallObj);

            m_lhObj = transform.Find("Parent/ItemCallChar").gameObject;
            m_lhObj.SetActive(false);
            m_itemCallChar = Utility.RequireComponent<ItemCallChar>(m_lhObj);

            m_uiNpc2 = Utility.RequireComponent<UINPC2>(transform.Find("RoomInfo/Npc1").gameObject);
            m_uiNpc2.InitComponent("Anima_101","Idle_Barrack","Idle");
            m_uiNpc2.gameObject.SetActive(false);

            m_uiNpc3 = Utility.RequireComponent<UINPC2>(transform.Find("RoomInfo/Npc2").gameObject);
            m_uiNpc3.InitComponent("Anima_101","Idle_Barrack","Idle");
            m_uiNpc3.gameObject.SetActive(false);

            m_uiNpc = transform.Find("RoomInfo/Npc").GetComponent<UINPC>();
            m_uiNpc.Init(() =>
            {
                m_leftBtn.SetActive(true);
                if(NpcWalkEnd != null)
                    NpcWalkEnd();
            });

            m_light = transform.Find("Light/light").GetComponent<SkeletonGraphic>();
            m_light.gameObject.SetActive(false);

            m_animator = gameObject.GetComponent<Animator>();
            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,false,"");
        }

        private void InitBtnEvent()
        {
            Utility.AddButtonListener(transform.Find("RoomInfo/Btn/LeftBtn/Train").transform,ClickCharTrain);
            Utility.AddButtonListener(transform.Find("RoomInfo/Btn/CenterBtn/NormalCall").transform,ClickNormalCall);
            Utility.AddButtonListener(transform.Find("RoomInfo/Btn/CenterBtn/LH").transform,ClickLH);
        }

        protected override void OnHide()
        {
            m_normalCall.Free();
            m_charTrain.Free();
            m_itemCallChar.Free();
        }

        private void ShowComplate()
        {
            GuideStep step = ControllerCenter.Instance.GuideController.GetCurrentStep();
            if(step == GuideStep.None)
            {
                m_uiNpc2.PlayEnter();
                m_uiNpc3.PlayEnter();
                m_centerBtn.SetActive(true);
            }
            LightAnim();
        }

        private void BgComplate()
        {
            GuideStep step = ControllerCenter.Instance.GuideController.GetCurrentStep();
            if(step == GuideStep.None)
            {
                m_uiNpc2.PlayEnter();
                m_uiNpc3.PlayEnter();
                m_centerBtn.SetActive(true);
            }
            LightAnim2();
        }

        #region click

        private void UpdateInfoWhenClick(GameObject obj,string s)
        {
            m_centerBtn.SetActive(false);
            m_leftBtn.SetActive(false);
            obj.SetActive(true);
            m_currentObj = obj;

            EventManager.Instance.TriggerEvent(EventSystemType.UI,
             EventTypeNameDefine.MainPanelAnim,true);
            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,true,s);

            Utility.PlayAnim(m_animator,BuildUIController.ShowAnimName);
            m_uiNpc.gameObject.SetActive(false);
            m_uiNpc2.gameObject.SetActive(false);
            m_uiNpc3.gameObject.SetActive(false);

        }

        private void ClickCharTrain()
        {
            UpdateInfoWhenClick(m_charTrainObj,StringDefine.RoomName.Barrack.TrainChar);
            m_charTrain.InitInfo();
        }

        private void ClickNormalCall()
        {
            UpdateInfoWhenClick(m_normalCallObj,StringDefine.RoomName.Barrack.AltarChar);
            m_normalCall.InitInfo();
        }

        private void ClickLH()
        {
            UpdateInfoWhenClick(m_lhObj,StringDefine.RoomName.Barrack.LHChar);
            m_itemCallChar.InitInfo();
        }

        public void ClickBack()
        {
            if(m_currentObj != null)
            {
                if(m_normalCall.CharIsShow())
                {
                    m_normalCall.FreeCharModel();
                }
                else
                {
                    m_currentObj.SetActive(false);
                    m_currentObj = null;
                    m_centerBtn.SetActive(true);
                    m_leftBtn.SetActive(true);
                    EventManager.Instance.TriggerEvent(EventSystemType.UI,
                      EventTypeNameDefine.MainPanelAnim,false);
                    EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,false,"");
                    Utility.PlayAnim(m_animator,BuildUIController.CloseAnimName);

                    m_uiNpc.PlayIdle();
                    m_uiNpc2.PlayIdle();
                    m_uiNpc3.PlayIdle();
                }
            }
            else
            {
                BuildUIController.Instance.BackToStart(
                    () => UIPanelManager.Instance.Hide<BarrackPanel>(false));
            }
        }

        public void PlayNpcWalk()
        {
            m_uiNpc.PlayWalk();
            BgComplate();
        }
    }
    #endregion
}