﻿using System.Collections;
using UnityEngine;

namespace GameFsmMachine
{
    public class StartToMainState: IMainState
    {
        public override void Action(bool isFront,bool playNpcWalk)
        {
            loding = UIPanelManager.Instance.Show<LodingPanel>(CavasType.PopUI);
            loding.action = () =>
            {
                UIPanelManager.Instance.Hide<StartPanel>();
                Game.Instance.StartCoroutine(Load());
                Resources.UnloadUnusedAssets();
            };
            Show();
        }

        private IEnumerator Load()
        {
            yield return null;
            Game.Instance.StartCoroutine(PreLoadUti.PreLoad(PreLoadEnd));
        }

        public void PreLoadEnd()
        {
            loding = UIPanelManager.Instance.GetUiPanelBehaviour<LodingPanel>();
            loding.Close();
        }
    }
}
