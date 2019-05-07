using EventCenter;
using GameFSM;

namespace GameFsmMachine
{
    class StartGameStateToInitScriptState: FSMTransition
    {
        public StartGameStateToInitScriptState(string name,IState from,IState to) : base(name,from,to) { }

        protected override bool CanTrainsition()
        {
            if(GameFsmManager.Instance.CurrType == GameFsmType.InitScript)
            {
                //GameFsmManager.Instance.CurrType = GameFsmType.None;
                EventManager.Instance.TriggerEvent(EventSystemType.FSM,EventTypeNameDefine.UpdateFsm,GameFsmType.None);
                return true;
            }
            return false;
        }

        protected override bool Transitioning()
        {
            return base.Transitioning();
        }

    }
}
