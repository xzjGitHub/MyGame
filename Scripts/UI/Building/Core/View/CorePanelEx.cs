using Spine;

namespace Core.View
{
    public partial class CorePanel
    {
        #region 接口

        public void InitCurrentBuildingTypeIndex()
        {
            BuildUIController.Instance.CurreentBuild = BuildingTypeIndex.Core;
        }

        public void RegisterBuild()
        {
            BuildUIController.Instance.AddBuild(BuildingTypeIndex.Core,this);
        }

        public void OnChangeBuild(bool playHideAnim)
        {
            //m_core.SetActive(false);
            //m_btn.SetActive(false);
            //m_uiNpc.gameObject.SetActive(false);
            UIPanelManager.Instance.RemoveKey<CorePanel>();
            if(playHideAnim)
            {
                m_skeletonGraphic.AnimationState.SetAnimation(0,"Hide",false);
                m_skeletonGraphic.AnimationState.Complete += Close;
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }

        private void Close(TrackEntry trackentry)
        {
            DestroyImmediate(gameObject);
        }

        #endregion

    }
}
