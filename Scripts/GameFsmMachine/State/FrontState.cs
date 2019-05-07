﻿using System.Collections.Generic;
using GameFSM;
using System.Collections;
using UnityEngine;

namespace GameFsmMachine
{
    public class FrontState: FSMState
    {
        public FrontState(string name) : base(name) { }

        public override void OnEnter(IState preState)
        {
            base.OnEnter(preState);

            LodingPanel loding = UIPanelManager.Instance.Show<LodingPanel>(CavasType.PopUI);
            loding.PlayOpenAnimAction = () =>
            {
                DiaLogPanel dia = UIPanelManager.Instance.Show<DiaLogPanel>();
                dia.CloseCallBack = StartLoadFront;
            };
            loding.PlayCloseAnim();

            loding.Close();
        }

        public override void OnExit(IState nextState)
        {
            base.OnExit(nextState);
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }

        private void StartLoadFront()
        {
            LodingPanel loding = UIPanelManager.Instance.Show<LodingPanel>(CavasType.PopUI);
            loding.action = () =>
            {
                Game.Instance.StartCoroutine(LoadFront());
            };
            loding.PlayCloseAnim();
        }


        private IEnumerator LoadFront()
        {
            var mapID = ScriptSystem.Instance.ScriptTemplate.initialMap;
            //
            var zone = FortSystem.Instance.NewZone;
            FortSystem.Instance.PrepareExplore(new ExploreMap(mapID,0,0,zone));
            yield return null;
            ResourceLoadUtil.LoadExploreModule1();
            UIPanelManager.Instance.DestroyAllPanelNotContain(new List<string>() { "LodingPanel" });
            Resources.UnloadUnusedAssets();
            yield return null;

            LodingPanel loding = UIPanelManager.Instance.GetUiPanelBehaviour<LodingPanel>();
            loding.Close();
        }
    }
}
