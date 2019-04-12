using Spine;

namespace Shop.View
{
    public partial class ShopPanel
    {
        #region 接口

        public void InitCurrentBuildingTypeIndex()
        {
            BuildUIController.Instance.CurreentBuild = BuildingTypeIndex.Shop;
        }

        public void RegisterBuild()
        {
            BuildUIController.Instance.AddBuild(BuildingTypeIndex.Shop,this);
        }

        public void OnChangeBuild(bool playHideAnim)
        {
            //m_btn.SetActive(false);
            //m_uiNpc.gameObject.SetActive(false);
            UIPanelManager.Instance.RemoveKey<ShopPanel>();
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

