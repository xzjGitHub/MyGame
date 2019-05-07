using Core.View;
using System.Collections;
using UnityEngine;

namespace GameFsmMachine
{
    public class StartToMainState:IMainState
    {
        public override void Action(object obj)
        {
            LodingPanel loding = UIPanelManager.Instance.Show<LodingPanel>(CavasType.PopUI);
            loding.action = () =>
            {
                UIPanelManager.Instance.Hide<StartPanel>();
                Game.Instance.StartCoroutine(Load());
            };

            loding.PlayOpenAnimAction = () =>
            {
                UIPanelManager.Instance.Show<NewMainPanel>();
                BuildUIController.Instance.ChangeBuild(BuildingTypeIndex.Core);
                Resources.UnloadUnusedAssets();
            };

            loding.HidePanelAction = () =>
            {
                CorePanel panel = UIPanelManager.Instance.GetUiPanelBehaviour<CorePanel>();
                if(panel != null)
                    panel.PlayNpcWalk();
            };

            loding.PlayCloseAnim();
        }

        private IEnumerator Load()
        {
            yield return null;
            Game.Instance.StartCoroutine(PreLoadUti.PreLoad(PreLoadEnd));
        }

        public void PreLoadEnd()
        {
            LodingPanel loding = UIPanelManager.Instance.GetUiPanelBehaviour<LodingPanel>();
            loding.Close();
        }
    }
}
