using EventCenter;
using Spine.Unity;
using UnityEngine;

namespace Core.View
{
    public partial class CorePanel: UIPanelBehaviour, IBuild
    {
        private GameObject m_coreInfoPanel;
        private GameObject m_levelUpPanel;
        private GameObject m_btn;
        private GameObject m_levelUpBtn;

        //  private GameObject m_core;
        //    private CoreResearchPanel m_corePanel;
        private SkeletonGraphic m_skeletonGraphic;

        private CoreLevelUpInfo m_levelUpInfo;
        private CoreInfoPanel m_coreInfo;

        private Animator m_animator;

        private UINPC m_uiNpc;

        protected override void OnAwake()
        {
            InitComponent();

            m_skeletonGraphic = BuilidUtil.Instance.InitEff(
              BuildingTypeIndex.Core,m_uiNpc,m_btn,transform.Find("EffPos/Core").gameObject,false);
        }

        private void InitComponent()
        {
            m_btn = transform.Find("RoomInfo/Btn").gameObject;
            m_btn.SetActive(false);

            m_levelUpBtn = transform.Find("RoomInfo/Btn/LevelUp").gameObject;
            UpdateLevelBtnShow();
            //m_core = transform.Find("Parent/CorePanel").gameObject;
            //m_core.SetActive(false);

            //m_corePanel = Utility.RequireComponent<CoreResearchPanel>(m_core);
            //m_corePanel.InitComponent();

            m_coreInfoPanel = transform.Find("CoreInfo").gameObject;
            m_coreInfo = Utility.RequireComponent<CoreInfoPanel>(m_coreInfoPanel);
            m_coreInfo.InitComponent();
            m_coreInfoPanel.SetActive(false);

            m_levelUpPanel = transform.Find("CoreLevelUpInfo").gameObject;
            m_levelUpInfo = Utility.RequireComponent<CoreLevelUpInfo>(m_levelUpPanel);
            m_levelUpInfo.InitComponent();
            m_levelUpPanel.SetActive(false);

            m_uiNpc = transform.Find("RoomInfo/Npc").GetComponent<UINPC>();
            m_uiNpc.Init(ShowBtn);

            Utility.AddButtonListener(transform.Find("RoomInfo/Btn/LevelUp"),ClickLevelUp);
            Utility.AddButtonListener(transform.Find("RoomInfo/Btn/Info"),ClickInfo);

            m_animator = gameObject.GetComponent<Animator>();
            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,false,"");
            EventManager.Instance.RegEventListener(EventSystemType.UI,EventTypeNameDefine.UpdateCoreLevelInfo,UpdateLevelBtnShow);
        }


        protected override void OnHide()
        {
            //UIEffectFactory.Instance.FreeEffect(StringDefine.UIEffectNameDefine.CoreEff);
            // m_skeletonGraphic.AnimationState.Complete -= Close;
            // BuilidUtil.Instance.RemoveEvent(m_skeletonGraphic);
            // m_corePanel.Free();
            EventManager.Instance.UnRegEventListener(EventSystemType.UI, EventTypeNameDefine.UpdateCoreLevelInfo,UpdateLevelBtnShow);
        }


        private void ShowBtn()
        {
            m_btn.SetActive(true);
        }

        private void ClickLevelUp()
        {
            //UpdateShow(false);
            //m_corePanel.InitInfo();
            //EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.MainPanelAnim,true);
            //EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,true,StringDefine.RoomName.CoreName);
            //Utility.PlayAnim(m_animator,BuildUIController.ShowAnimName);
            //m_uiNpc.gameObject.SetActive(false);

            ControllerCenter.Instance.CoreController.CoreLevelUp();
            UpdateLevelBtnShow();
        }

        private void ClickInfo()
        {
            // UpdateShow(false);
            //m_corePanel.InitInfo();
            //EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.MainPanelAnim,true);
            //EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,true,StringDefine.RoomName.CoreName);
            //Utility.PlayAnim(m_animator,BuildUIController.ShowAnimName);
            // m_uiNpc.gameObject.SetActive(false);

            m_coreInfoPanel.SetActive(true);
            m_coreInfo.UpdateInfo();
        }


        public void ClickBack()
        {
            // m_corePanel.Free();
            //if(!m_core.activeSelf)
            //{
            BuildUIController.Instance.BackToStart(
             () => UIPanelManager.Instance.Hide<CorePanel>());
            // m_corePanel.Free();
            // }
            //else
            //{
            //    m_btn.SetActive(true);
            //    EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.MainPanelAnim,false);
            //    EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,false,"");
            //    UpdateShow(true);
            //    Utility.PlayAnim(m_animator,BuildUIController.CloseAnimName);
            //    m_uiNpc.PlayIdle();
            //}
        }


        private void UpdateShow(bool showInfoPanel)
        {
            //m_core.SetActive(!showInfoPanel);
        }

        private void UpdateLevelBtnShow() {
            m_levelUpBtn.SetActive(ControllerCenter.Instance.CoreController.CanLevelUp());
        }

        public void PlayNpcWalk()
        {
            // m_uiNpc.gameObject.SetActive(true);
            m_uiNpc.PlayWalk();
        }
    }
}
