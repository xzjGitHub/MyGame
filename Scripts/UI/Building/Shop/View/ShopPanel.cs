﻿using EventCenter;
using Guide;
using Shop.Controller;
using Spine.Unity;
using UnityEngine;

namespace Shop.View
{
    public partial class ShopPanel: UIPanelBehaviour, IBuild
    {
        private GameObject m_leftBtn;
        private GameObject m_centerBtn;
        private GameObject m_shopDetialObj;
        private GameObject m_currentObj;

        private SkeletonGraphic m_skeletonGraphic;
        private SkeletonGraphic m_light;

        private ShopDetial m_shopDetial;
        private UINPC m_uiNpc;
        private UINPC2 m_uiNpc2;

        private Animator m_animator;

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
             BuildingTypeIndex.Shop,m_uiNpc,m_leftBtn,transform.Find("EffPos/Shop").gameObject,playNpcWalk,
             ShowComplate,BgComplate);
            InitBtnEvent();
        }

        protected override void OnHide()
        {
            m_shopDetial.RemoveEvent();
            m_shopDetial.Reset();
            m_shopDetial.Free();
        }

        private void InitComponent()
        {
            m_leftBtn = transform.Find("RoomInfo/Btn/LeftBtn").gameObject;
            m_leftBtn.SetActive(false);

            m_centerBtn = transform.Find("RoomInfo/Btn/CenterBtn").gameObject;
            m_centerBtn.SetActive(false);

            m_shopDetialObj = transform.Find("Parent/ShopDetial").gameObject;
            m_shopDetialObj.SetActive(false);

            m_shopDetial = Utility.RequireComponent<ShopDetial>(m_shopDetialObj);

            m_uiNpc2 = Utility.RequireComponent<UINPC2>(transform.Find("RoomInfo/Npc1").gameObject);
            m_uiNpc2.InitComponent("Anima_101","Idle_Shop","Idle");
            m_uiNpc2.gameObject.SetActive(false);

            m_uiNpc = transform.Find("RoomInfo/Npc").GetComponent<UINPC>();
            m_uiNpc.gameObject.SetActive(true);
            m_uiNpc.Init(() => m_leftBtn.SetActive(true));

            m_light = transform.Find("Light/light").GetComponent<SkeletonGraphic>();
            m_light.gameObject.SetActive(false);

            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,false,"");
            m_animator = gameObject.GetComponent<Animator>();
        }

        private void InitBtnEvent()
        {
            Utility.AddButtonListener(transform.Find("RoomInfo/Btn/LeftBtn/ShopBtn"),() => ClickShopBtn(ShopType.NormalShop));
            Utility.AddButtonListener(transform.Find("RoomInfo/Btn/CenterBtn/Black"),() => ClickShopBtn(ShopType.BlackShop));
        }

        private void ShowComplate()
        {
            GuideStep step = ControllerCenter.Instance.GuideController.GetCurrentStep();
            if(step == GuideStep.None)
            {
                m_uiNpc2.PlayEnter();
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
                m_centerBtn.SetActive(true);
            }
            LightAnim2();
        }

        public void PlayNpcWalk()
        {
            m_uiNpc.PlayWalk();
        }

        #region clickEvent

        private void UpdateInfoWhenClick(GameObject obj,string s)
        {
            m_leftBtn.SetActive(false);
            m_centerBtn.SetActive(false);
            m_uiNpc.gameObject.SetActive(false);
            obj.SetActive(true);
            m_currentObj = obj;
            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.MainPanelAnim,true);
            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,true,s);

            Utility.PlayAnim(m_animator,BuildUIController.ShowAnimName);
            m_uiNpc.gameObject.SetActive(false);
            m_uiNpc2.gameObject.SetActive(false);
        }

        private void ClickShopBtn(ShopType shopType)
        {
            m_shopDetial.InitInfo(shopType);
            if(shopType == ShopType.NormalShop)
                UpdateInfoWhenClick(m_shopDetialObj,StringDefine.RoomName.ShopName.Shop);
            else
                UpdateInfoWhenClick(m_shopDetialObj,StringDefine.RoomName.ShopName.Black);
        }

        public void ClickBack()
        {
            if(m_currentObj != null)
            {
                m_currentObj.SetActive(false);
                m_currentObj = null;
                m_leftBtn.SetActive(true);
                m_centerBtn.SetActive(true);

                EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.MainPanelAnim,false);
                EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,false,"");
                Utility.PlayAnim(m_animator,BuildUIController.CloseAnimName);
                m_uiNpc.PlayIdle();
                m_uiNpc2.PlayIdle();
            }
            else
            {
                BuildUIController.Instance.BackToStart(
              () => UIPanelManager.Instance.Hide<ShopPanel>(false));
            }
        }

        #endregion
    }
}
