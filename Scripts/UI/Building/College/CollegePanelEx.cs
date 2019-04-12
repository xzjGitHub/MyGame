using Spine;

namespace College
{

    public partial class CollegePanel
    {
        #region 接口

        public void InitCurrentBuildingTypeIndex()
        {
            BuildUIController.Instance.CurreentBuild = BuildingTypeIndex.College;
        }

        public void RegisterBuild()
        {
            BuildUIController.Instance.AddBuild(BuildingTypeIndex.College,this);
        }

        public void OnChangeBuild(bool playHideAnim)
        {
            UIPanelManager.Instance.RemoveKey<CollegePanel>();
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

        #endregion

        private void Close(TrackEntry trackentry)
        {
            DestroyImmediate(gameObject);
        }
    }
}