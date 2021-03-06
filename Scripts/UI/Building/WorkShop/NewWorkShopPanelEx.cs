﻿using Spine;

namespace WorkShop
{
    public partial class NewWorkShopPanel
    {
        #region 接口

        public void InitCurrentBuildingTypeIndex()
        {
            BuildUIController.Instance.CurreentBuild = BuildingTypeIndex.WorkShop;
        }

        public void RegisterBuild()
        {
            BuildUIController.Instance.AddBuild(BuildingTypeIndex.WorkShop,this);
        }

        public void OnChangeBuild(bool playHideAnim)
        {
            //m_btn.SetActive(false);
            //m_uiNpc.gameObject.SetActive(false);
           // UIPanelManager.Instance.RemoveKey<NewWorkShopPanel>();
            if(playHideAnim)
            {
                m_skeletonGraphic.AnimationState.SetAnimation(0,"Hide",false);
                m_skeletonGraphic.AnimationState.Complete += Close;
            }
            else
            {
               // DesObj();
                Close(null);
            }
        }

        #endregion

        private void Close(TrackEntry trackentry)
        {
            m_skeletonGraphic.AnimationState.ClearCompleteStateEvent();
            UIPanelManager.Instance.Hide<NewWorkShopPanel>(false);
           // DesObj();
        }

        private void DesObj()
        {
            m_skeletonGraphic.AnimationState.ClearCompleteStateEvent();
            DestroyImmediate(gameObject);
        }
    }
}
