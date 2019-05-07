using EventCenter;
using GameFSM;

namespace GameFsmMachine
{
    public class InitScriptStateToFront: FSMTransition
    {
        public InitScriptStateToFront(string name,IState from,IState to) : base(name,from,to) { }

        protected override bool CanTrainsition()
        {
            if(GameFsmManager.Instance.CurrType == GameFsmType.FrontFight)
            {
                //GameFsmManager.Instance.CurrType = GameFsmType.None;
                EventManager.Instance.TriggerEvent(EventSystemType.FSM,EventTypeNameDefine.UpdateFsm,GameFsmType.None);
                return true;
            }
            return false;
        }
    }
}
