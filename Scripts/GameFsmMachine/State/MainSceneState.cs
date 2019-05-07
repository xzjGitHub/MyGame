using GameFSM;
using System.Collections.Generic;
using UnityEngine;

namespace GameFsmMachine
{
    public class MainSceneState: FSMState
    {
        public MainSceneState(string name) : base(name) { }

        public override void OnEnter(IState preState)
        {
           // LogHelperLSK.LogError("进入MainState");
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
            mainScene.Action(GameFsmManager.Instance.IsFrontBack);
        }

        public override void OnExit(IState nextState)
        {
           // LogHelperLSK.LogError("退出MainState");
            base.OnExit(nextState);
            GameObjectPool.Instance.DeatroyAllPool();
            PlayerPool.Instance.Clear();
            ItemCostFactory.Instance.Clear();
            SpriteManager.Instance.FreeAll();
            UIEffectFactory.Instance.FreeAll(true);
            UIPanelManager.Instance.DestroyAllPanelNotContain(new List<string>() { "LodingPanel" });

            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }

    }
}
