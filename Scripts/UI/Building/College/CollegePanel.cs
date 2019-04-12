using College.Research.View;
using EventCenter;
using Spine.Unity;
using UnityEngine;

namespace College
{
    public partial class CollegePanel: UIPanelBehaviour, IBuild
    {
        private GameObject m_currentShowObj;

        private GameObject m_btn;
        private GameObject m_researchObj;
        private GameObject m_fomoObj;

        private EnchanteResearch m_research;
        private Enchant.View.Enchant m_equipEnchant;

        private SkeletonGraphic m_skeletonGraphic;

        private Animator m_animator;

        private UINPC m_uiNpc;
        private UINPC2 m_uiNpc2;

        protected override void OnAwake()
        {
            Init();
            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,false,"");
        }

        private void Init()
        {
            InitComponent();
            m_skeletonGraphic = BuilidUtil.Instance.InitEff(
               BuildingTypeIndex.College,m_uiNpc,m_btn,transform.Find("EffPos/College").gameObject);
            InitBtnEvent();
        }

        protected override void OnHide()
        {
            m_skeletonGraphic.AnimationState.Complete -= Close;

            m_research.Free();
            m_equipEnchant.Free();
            m_uiNpc2.Free();
        }

        private void InitComponent()
        {
            m_btn = transform.Find("RoomInfo/Btn").gameObject;
            m_btn.SetActive(false);

            m_researchObj = transform.Find("Parent/MatResearch").gameObject;
            m_researchObj.SetActive(false);
            m_research = Utility.RequireComponent<EnchanteResearch>(m_researchObj);

            m_fomoObj = transform.Find("Parent/EquipEnchant").gameObject;
            m_fomoObj.SetActive(false);
            m_equipEnchant = Utility.RequireComponent<Enchant.View.Enchant>(m_fomoObj);

            m_uiNpc2 = Utility.RequireComponent<UINPC2>(transform.Find("RoomInfo/Btn/Npc1").gameObject);
            m_uiNpc2.InitComponent("Anima_101","Idle1","Xiao1");

            m_uiNpc = transform.Find("RoomInfo/Npc").GetComponent<UINPC>();
            // m_uiNpc.gameObject.SetActive(true);
            m_uiNpc.Init(() =>
            {
                m_btn.SetActive(true);
                m_uiNpc2.PlayIdle();
            });

            if(BuildUIController.Instance.HaveShowList.Contains(BuildingTypeIndex.College))
            {
                m_uiNpc2.PlayIdle();
            }

            m_animator = gameObject.GetComponent<Animator>();
        }

        private void InitBtnEvent()
        {
            Utility.AddButtonListener(transform.Find("RoomInfo/Btn/ResearchBtn"),ClickResearch);
            Utility.AddButtonListener(transform.Find("RoomInfo/Btn/FomoBtn"),ClickFomo);
        }

        #region clickEvent

        private void UpdateInfoWhenClick(GameObject obj,string s)
        {
            m_btn.SetActive(false);
            obj.SetActive(true);
            m_currentShowObj = obj;

            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.MainPanelAnim,true);
            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,true,s);
            Utility.PlayAnim(m_animator,BuildUIController.ShowAnimName);

            m_uiNpc.gameObject.SetActive(false);

        }

        private void ClickResearch()
        {
            m_research.InitInfo();
            UpdateInfoWhenClick(m_researchObj,StringDefine.RoomName.College.Res);
        }

        private void ClickFomo()
        {
            UpdateInfoWhenClick(m_fomoObj,StringDefine.RoomName.College.Enchant);
            m_equipEnchant.InitInfo();
        }

        public void ClickBack()
        {
            if(m_currentShowObj.activeSelf)
            {
                //if(m_equipEnchant.gameObject.activeSelf)
                //{
                //    m_equipEnchant.SelectPanel.ClickSure();
                //}
                //else
                //{
                m_currentShowObj.SetActive(false);
                m_btn.SetActive(true);
                EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.MainPanelAnim,false);
                EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,false,"");
                Utility.PlayAnim(m_animator,BuildUIController.CloseAnimName);
                m_uiNpc2.PlayIdle();
                m_uiNpc.PlayIdle();
                // }
            }
            else
            {
                BuildUIController.Instance.BackToStart(
            () => UIPanelManager.Instance.Hide<CollegePanel>());
            }
        }

        #endregion
    }
}
