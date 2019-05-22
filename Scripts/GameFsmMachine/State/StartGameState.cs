﻿using GameFSM;

namespace GameFsmMachine
{
    /// <summary>
    /// 开始游戏
    /// </summary>
    public class StartGameState: FSMState
    {
        public StartGameState(string name) : base(name) { }

        public override void OnEnter(IState preState)
        {
            base.OnEnter(preState);
            GameModules.Init();
            UIPanelManager.Instance.Show<StartPanel>();
           // UIPanelManager.Instance.ShowAsync<StartPanel>();
        }
    }
}
