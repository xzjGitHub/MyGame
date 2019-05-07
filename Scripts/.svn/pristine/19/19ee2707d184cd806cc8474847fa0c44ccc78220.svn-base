using UnityEngine;

namespace GameFsmMachine
{
    public class FightToMainSceneState:IMainState
    {
        public override void Action(object obj)
        {
            bool isFront = obj == null ? false : (bool)obj;
            if(isFront)
            {
                DiaLogPanel dia = UIPanelManager.Instance.Show<DiaLogPanel>();
                dia.CloseCallBack = ToMain;
            }
            else
            {
                ToMain();
            }
        }

        private void ToMain()
        {
            LodingPanel loding = UIPanelManager.Instance.Show<LodingPanel>(CavasType.PopUI);
            loding.action = () =>
            {
                Resources.UnloadUnusedAssets();
                System.GC.Collect();

                loding.Close();
            };

            loding.PlayOpenAnimAction = () =>
            {
                UIPanelManager.Instance.Show<NewMainPanel>();
                EventCenter.EventManager.Instance.TriggerEvent(EventCenter.EventSystemType.UI,
                    EventCenter.EventTypeNameDefine.ShowMainPanelBtn);
                BuildUIController.Instance.ChangeBuild(BuildingTypeIndex.Core);
            };

            loding.HidePanelAction = () =>
            {
                Core.View.CorePanel panel = UIPanelManager.Instance.GetUiPanelBehaviour<Core.View.CorePanel>();
                if(panel != null)
                    panel.PlayNpcWalk();
            };

            loding.PlayCloseAnim();
        }

    }
}
