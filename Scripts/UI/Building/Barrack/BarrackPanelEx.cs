﻿using Spine;

namespace Barrack.View
{
    public partial class BarrackPanel
    {
        #region 接口

        public void InitCurrentBuildingTypeIndex()
        {
            BuildUIController.Instance.CurreentBuild = BuildingTypeIndex.Barracks;
        }

        public void RegisterBuild()
        {
            BuildUIController.Instance.AddBuild(BuildingTypeIndex.Barracks,this);
        }

        public void OnChangeBuild(bool playHideAnim)
        {
            if(playHideAnim) {
                m_skeletonGraphic.AnimationState.SetAnimation(0,"Hide",false);
                m_skeletonGraphic.AnimationState.Complete += Close;
            }
            else
            {
                Close(null);
            }
        }

        #endregion

        private void Close(TrackEntry trackentry)
        {
            m_light.AnimationState.ClearCompleteStateEvent();
            m_skeletonGraphic.AnimationState.ClearCompleteStateEvent();
            UIPanelManager.Instance.Hide<BarrackPanel>(false);
        }

        private void LightAnim()
        {
            m_light.gameObject.SetActive(true);
            m_light.AnimationState.SetAnimation(0,StringDefine.AnimNameDefine.BuildLightShowName,false);
            m_light.AnimationState.Complete += ShowLightCallBack;
        }

        private void ShowLightCallBack(TrackEntry trackEntry)
        {
            m_light.AnimationState.Complete -= ShowLightCallBack;
            m_light.AnimationState.SetAnimation(0,StringDefine.AnimNameDefine.BuildLightFireName,true);
        }

        private void LightAnim2()
        {
            m_light.gameObject.SetActive(true);
            m_light.AnimationState.SetAnimation(0,StringDefine.AnimNameDefine.BuildLightFireName,true);
        }

    }
}
