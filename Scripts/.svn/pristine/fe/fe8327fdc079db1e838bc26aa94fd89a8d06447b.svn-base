using Altar.View;
using EventCenter;
using Spine.Unity;
using UnityEngine;

namespace Barrack.View
{
    public partial class BarrackPanel: UIPanelBehaviour, IBuild
    {
        private GameObject m_adminChar;
        private GameObject m_charTrainObj;
        private GameObject m_normalCallObj;
        private GameObject m_currentObj;
        private GameObject m_lhObj;
        private GameObject m_btn;

        private SkeletonGraphic m_skeletonGraphic;
        private AdminCharPanel m_adminCharPanel;
        private CharTrain m_charTrain;
        private NewNormalCallPanel m_normalCall;
        private ItemCallChar m_itemCallChar;

        private UINPC m_uiNpc;
        private UINPC2 m_uiNpc2;
        private UINPC2 m_uiNpc3;
        private Animator m_animator;

        protected override void OnAwake()
        {
            Init();
        }


        private void Init()
        {
            InitComponent();
            m_skeletonGraphic = BuilidUtil.Instance.InitEff(
             BuildingTypeIndex.Barracks,m_uiNpc,m_btn,transform.Find("EffPos/Altar").gameObject);
            m_skeletonGraphic.AnimationState.Complete -= Close;
            InitBtnEvent();
        }

        private void InitComponent()
        {
            m_btn = transform.Find("RoomInfo/Btn").gameObject;
            m_btn.SetActive(false);

            m_adminChar = transform.Find("Parent/AdminChar").gameObject;
            m_adminChar.SetActive(false);
            m_adminCharPanel = Utility.RequireComponent<AdminCharPanel>(m_adminChar);

            m_charTrainObj = transform.Find("Parent/TrainChar").gameObject;
            m_charTrainObj.SetActive(false);
            m_charTrain = Utility.RequireComponent<CharTrain>(m_charTrainObj);

            m_normalCallObj = transform.Find("Parent/NewCallPanel").gameObject;
            m_normalCallObj.SetActive(false);
            m_normalCall = Utility.RequireComponent<NewNormalCallPanel>(m_normalCallObj);

            m_lhObj = transform.Find("Parent/ItemCallChar").gameObject;
            m_lhObj.SetActive(false);
            m_itemCallChar = Utility.RequireComponent<ItemCallChar>(m_lhObj);

            m_uiNpc2 = Utility.RequireComponent<UINPC2>(transform.Find("RoomInfo/Btn/Npc1").gameObject);
            m_uiNpc2.InitComponent("Anima_101","Idle2","Xiao2");

            m_uiNpc3 = Utility.RequireComponent<UINPC2>(transform.Find("RoomInfo/Btn/Npc2").gameObject);
            m_uiNpc3.InitComponent("Anima_101","Idle2","Xiao2");

            m_uiNpc = transform.Find("RoomInfo/Npc").GetComponent<UINPC>();
            // m_uiNpc.gameObject.SetActive(true);
            m_uiNpc.Init(() =>
            {
                m_btn.SetActive(true);
                m_uiNpc2.PlayIdle();
                m_uiNpc3.PlayIdle();
            });

            if(BuildUIController.Instance.HaveShowList.Contains(BuildingTypeIndex.Barracks))
            {
                m_uiNpc2.PlayIdle();
                m_uiNpc3.PlayIdle();
            }

            m_animator = gameObject.GetComponent<Animator>();

            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,false,"");

        }

        private void InitBtnEvent()
        {
            Utility.AddButtonListener(transform.Find("RoomInfo/Btn/AdminCharBtn").transform,ClickAdmin);
            Utility.AddButtonListener(transform.Find("RoomInfo/Btn/Train").transform,ClickCharTrain);
            Utility.AddButtonListener(transform.Find("RoomInfo/Btn/NormalCall").transform,ClickNormalCall);
            Utility.AddButtonListener(transform.Find("RoomInfo/Btn/LH").transform,ClickLH);
        }

        protected override void OnHide()
        {
            m_normalCall.Free();
            m_charTrain.Free();
            m_adminCharPanel.Free();
            m_itemCallChar.Free();
            m_uiNpc2.Free();
            m_uiNpc3.Free();
        }

        #region click

        private void UpdateInfoWhenClick(GameObject obj,string s)
        {
            m_btn.SetActive(false);
            obj.SetActive(true);
            m_currentObj = obj;

            EventManager.Instance.TriggerEvent(EventSystemType.UI,
             EventTypeNameDefine.MainPanelAnim,true);
            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,true,s);

            Utility.PlayAnim(m_animator,BuildUIController.ShowAnimName);
            m_uiNpc.gameObject.SetActive(false);

        }

        private void ClickAdmin()
        {
            UpdateInfoWhenClick(m_adminChar,StringDefine.RoomName.Barrack.AdminChar);
            m_adminCharPanel.InitInfo();
        }

        private void ClickCharTrain()
        {
            UpdateInfoWhenClick(m_charTrainObj,StringDefine.RoomName.Barrack.TrainChar);
            m_adminCharPanel.InitInfo();
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
                    m_btn.SetActive(true);
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
                    () => UIPanelManager.Instance.Hide<BarrackPanel>());
            }
        }
    }
    #endregion

}