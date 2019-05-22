﻿using GameFSM;
using System.Collections.Generic;
using UnityEngine;

namespace GameFsmMachine
{
    public class MainSceneState: FSMState
    {
        public MainSceneState(string name) : base(name) { }

        public override void OnEnter(IState preState)
        {
            base.OnEnter(preState);

            IMainState mainScene = null;
            if(preState.StateName == GameFsmStateNameDefine.InitScriptState)
            {
                mainScene = new StartToMainState();
            }
            else
            {
                mainScene = new FightToMainSceneState();
            }
            mainScene.Action(GameFsmManager.Instance.IsFrontBack,preState.StateName== GameFsmStateNameDefine.FrontState);
        }

        public override void OnExit(IState nextState)
        {
            base.OnExit(nextState);
            PrefabPool.Instance.FreeAll();
            SpriteManager.Instance.FreeAll();
            UIPanelManager.Instance.DestroyAllPanelNotContain(new List<string>() { "LodingPanel" });
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }

    }
}
