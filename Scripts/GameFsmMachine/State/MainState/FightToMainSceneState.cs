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
                FrontPanel dia = UIPanelManager.Instance.Show<FrontPanel>(CavasType.Three,new System.Collections.Generic.List<object> { true});
                dia.CloseCallBack = ToMain;
            }
            else
            {
                ToMain();
            }
        }

        private void ToMain()
        {
            loding = UIPanelManager.Instance.Show<LodingPanel>(CavasType.PopUI);
            loding.action = () =>
            {
                Resources.UnloadUnusedAssets();
                System.GC.Collect();
                loding.Close();
            };

            Show();
        }

    }
}
