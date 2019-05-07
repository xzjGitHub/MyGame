using GameFSM;

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
            //  LogHelperLSK.LogError("进入StartGameState");
            base.OnEnter(preState);
            GameModules.Init();
            UIPanelManager.Instance.Show<StartPanel>();
        }

        public override void OnExit(IState nextState)
        {
            // LogHelperLSK.LogError("退出StartGameState");
            base.OnExit(nextState);
        }
    }
}
