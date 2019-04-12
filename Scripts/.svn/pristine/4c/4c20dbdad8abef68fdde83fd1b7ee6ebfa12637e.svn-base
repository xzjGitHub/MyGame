using College.Enchant.View;
using EventCenter;
using Spine.Unity;
using UnityEngine;

namespace WorkShop
{
    public partial class NewWorkShopPanel: UIPanelBehaviour, IBuild
    {
        private GameObject m_current;
        private GameObject m_btn;
        private GameObject m_research;
        private GameObject m_equipMake;
        private GameObject m_equipRecast;
        private GameObject m_fomoObj;

        private EquipResearch.View.EquipResearch m_equipResearch;
        private EquipMake.View.EquipMake m_equipMakePanel;
        private Recast.View.Recast m_recastInfo;
        private Enchant m_equipEnchant;

        private SkeletonGraphic m_skeletonGraphic;

        private UINPC m_uiNpc;
        private UINPC2 m_uiNpc2;
        private UINPC2 m_uiNpc3;
        private Animator m_animator;

        protected override void OnAwake()
        {
            Init();
            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,false,"");
        }

        private void Init()
        {
            InitComponent();
            m_skeletonGraphic = BuilidUtil.Instance.InitEff(
              BuildingTypeIndex.WorkShop,m_uiNpc,m_btn,transform.Find("EffPos/WorkShop").gameObject);
            m_skeletonGraphic.AnimationState.Complete -= Close;
            InitBtnEvent();
        }

        protected override void OnHide()
        {
            m_equipResearch.Free();
            m_recastInfo.Free();
            m_equipMakePanel.Free();
            m_equipEnchant.Free();
        }

        private void InitComponent()
        {
            m_research = transform.Find("Parent/EquipResearch").gameObject;
            m_research.SetActive(false);
            m_equipResearch = Utility.RequireComponent<EquipResearch.View.EquipResearch>(m_research);

            m_equipMake = transform.Find("Parent/EquipMake").gameObject;
            m_equipMake.SetActive(false);
            m_equipMakePanel = Utility.RequireComponent<EquipMake.View.EquipMake>(m_equipMake);

            m_equipRecast = transform.Find("Parent/EquipRecast").gameObject;
            m_equipRecast.SetActive(false);
            m_recastInfo = Utility.RequireComponent<Recast.View.Recast>(m_equipRecast);

            m_fomoObj = transform.Find("Parent/EquipEnchant").gameObject;
            m_fomoObj.SetActive(false);
            m_equipEnchant = Utility.RequireComponent<Enchant>(m_fomoObj);

            m_btn = transform.Find("RoomInfo/Btn").gameObject;
            m_btn.SetActive(false);

            m_uiNpc2 = Utility.RequireComponent<UINPC2>(transform.Find("RoomInfo/Btn/Npc1").gameObject);
            m_uiNpc2.InitComponent("Anima_101","Idle2","Xiao2");

            m_uiNpc3 = Utility.RequireComponent<UINPC2>(transform.Find("RoomInfo/Btn/Npc2").gameObject);
            m_uiNpc3.InitComponent("Anima_101","Idle1","Xiao1");

            m_uiNpc = transform.Find("RoomInfo/Npc").GetComponent<UINPC>();
            m_uiNpc.gameObject.SetActive(true);
            m_uiNpc.Init(() =>
            {
                m_btn.SetActive(true);
                m_uiNpc2.PlayIdle();
                m_uiNpc3.PlayIdle();
            });

            if(BuildUIController.Instance.HaveShowList.Contains(BuildingTypeIndex.WorkShop))
            {
                m_uiNpc2.PlayIdle();
                m_uiNpc3.PlayIdle();
            }

            m_animator = gameObject.GetComponent<Animator>();
        }

        private void InitBtnEvent()
        {
            Utility.AddButtonListener(transform.Find("RoomInfo/Btn/Research"),ClickResearch);
            Utility.AddButtonListener(transform.Find("RoomInfo/Btn/Make"),ClickEquipMake);
            Utility.AddButtonListener(transform.Find("RoomInfo/Btn/Recast"),ClickEquipRecast);
            Utility.AddButtonListener(transform.Find("RoomInfo/Btn/Enchant"),ClickEquipEnchant);
        }


        #region click

        private void ClickResearch()
        {
            UpdateShow(m_research,StringDefine.RoomName.WorkShop.EquipRes);
            m_equipResearch.InitInfo();
        }

        private void ClickEquipMake()
        {
            UpdateShow(m_equipMake,StringDefine.RoomName.WorkShop.EquipMake);
            m_equipMakePanel.InitInfo();
        }


        private void ClickEquipRecast()
        {
            UpdateShow(m_equipRecast,StringDefine.RoomName.WorkShop.EquipRecast);
            m_recastInfo.UpdateInfo();
        }

        private void ClickEquipEnchant()
        {
            UpdateShow(m_fomoObj,StringDefine.RoomName.College.Enchant);
            m_equipEnchant.InitInfo();
        }

        private void UpdateShow(GameObject obj,string s)
        {
            m_btn.SetActive(false);
            m_uiNpc.gameObject.SetActive(false);
            m_current = obj;
            m_current.SetActive(true);

            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.MainPanelAnim,true);
            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,true,s);
            Utility.PlayAnim(m_animator,BuildUIController.ShowAnimName);

            m_uiNpc.gameObject.SetActive(false);
        }

        public void ClickBack()
        {
            if(m_current != null && m_current.activeSelf)
            {
                m_current.SetActive(false);
                m_btn.SetActive(true);
                m_uiNpc.gameObject.SetActive(true);
                EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.MainPanelAnim,false);
                EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,true,"");
                Utility.PlayAnim(m_animator,BuildUIController.CloseAnimName);
                m_uiNpc2.PlayIdle();
                m_uiNpc3.PlayIdle();
            }
            else
            {
                BuildUIController.Instance.BackToStart(
              () => UIPanelManager.Instance.Hide<NewWorkShopPanel>());
            }
        }

        #endregion
    }
}