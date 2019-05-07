using EventCenter;
using GameFSM;

namespace GameFsmMachine
{
    public class MainToFight: FSMTransition
    {
        public MainToFight(string name,IState from,IState to) : base(name,from,to) { }

        protected override bool CanTrainsition()
        {
            if(GameFsmManager.Instance.CurrType == GameFsmType.Fight)
            {
               // GameFsmManager.Instance.CurrType = GameFsmType.None;
                EventManager.Instance.TriggerEvent(EventSystemType.FSM,EventTypeNameDefine.UpdateFsm,GameFsmType.None);
                return true;
            }
            return false;
        }
    }
}
