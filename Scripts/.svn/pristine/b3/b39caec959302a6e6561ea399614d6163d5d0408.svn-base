using EventCenter;
using Shop.Controller;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Shop.View
{
    public partial class ShopPanel: UIPanelBehaviour, IBuild
    {
        private GameObject m_btn;
        private GameObject m_shopDetialObj;
        private GameObject m_currentObj;

        private SkeletonGraphic m_skeletonGraphic;

        private ShopDetial m_shopDetial;
        private UINPC m_uiNpc;
        private UINPC2 m_uiNpc2;

        private Animator m_animator;

        protected override void OnAwake()
        {
            Init();
        }

        private void Init()
        {
            InitComponent();
            m_skeletonGraphic = BuilidUtil.Instance.InitEff(
             BuildingTypeIndex.Shop,m_uiNpc,m_btn,transform.Find("EffPos/Shop").gameObject);
            InitBtnEvent();
        }

        protected override void OnHide()
        {
            m_shopDetial.RemoveEvent();
            m_shopDetial.Reset();
            m_uiNpc2.Free();
        }

        private void InitComponent()
        {
            m_btn = transform.Find("RoomInfo/Btn").gameObject;
            m_btn.SetActive(false);

            m_shopDetialObj = transform.Find("Parent/ShopDetial").gameObject;
            m_shopDetialObj.SetActive(false);

            m_shopDetial = Utility.RequireComponent<ShopDetial>(m_shopDetialObj);

            m_uiNpc2 = Utility.RequireComponent<UINPC2>(transform.Find("RoomInfo/Btn/Npc1").gameObject);
            m_uiNpc2.InitComponent("Anima_101","Idle2","Xiao2");

            m_uiNpc = transform.Find("RoomInfo/Npc").GetComponent<UINPC>();
            m_uiNpc.gameObject.SetActive(true);
            m_uiNpc.Init(() =>
            {
                m_btn.SetActive(true);
                m_uiNpc2.PlayIdle();
            });

            if(BuildUIController.Instance.HaveShowList.Contains(BuildingTypeIndex.Shop))
            {
                m_uiNpc2.PlayIdle();
            }

            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,false,"");
            m_animator = gameObject.GetComponent<Animator>();
        }

        private void InitBtnEvent()
        {
            Utility.AddButtonListener(transform.Find("RoomInfo/Btn/ShopBtn"),() => ClickShopBtn(ShopType.NormalShop));
            Utility.AddButtonListener(transform.Find("RoomInfo/Btn/Black"),() => ClickShopBtn(ShopType.BlackShop));
        }



        #region clickEvent

        private void UpdateInfoWhenClick(GameObject obj,string s)
        {
            m_btn.SetActive(false);
            m_uiNpc.gameObject.SetActive(false);
            obj.SetActive(true);
            m_currentObj = obj;
            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.MainPanelAnim,true);
            EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,true,s);

            Utility.PlayAnim(m_animator,BuildUIController.ShowAnimName);
            m_uiNpc.gameObject.SetActive(false);
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
                m_btn.SetActive(true);
                EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.MainPanelAnim,false);
                EventManager.Instance.TriggerEvent(EventSystemType.UI,EventTypeNameDefine.UpdateTitleName,false,"");
                Utility.PlayAnim(m_animator,BuildUIController.CloseAnimName);
                // m_uiNpc.gameObject.SetActive(true);
                m_uiNpc.PlayIdle();
                m_uiNpc2.PlayIdle();
            }
            else
            {
                BuildUIController.Instance.BackToStart(
              () => UIPanelManager.Instance.Hide<ShopPanel>());
            }
        }

        #endregion
    }
}
