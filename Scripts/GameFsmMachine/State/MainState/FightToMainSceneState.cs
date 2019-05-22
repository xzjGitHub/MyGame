﻿using UnityEngine;

namespace GameFsmMachine
{
    public class FightToMainSceneState: IMainState
    {
        private bool m_playNpcWalk;

        public override void Action(bool isFront,bool playNpcWalk)
        {
            m_playNpcWalk = playNpcWalk;
            if(isFront)
            {
                FrontPanel dia = UIPanelManager.Instance.Show<FrontPanel>();
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
                if(panel != null && m_playNpcWalk)
                    panel.PlayNpcWalk();
            };

            loding.PlayCloseAnim();
        }

    }
}
