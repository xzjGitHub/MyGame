using EventCenter;
using GameFSM;

namespace GameFsmMachine
{
    public class DefaultStateToStartGame: FSMTransition
    {
        public DefaultStateToStartGame(string name,IState from,IState to) : base(name,from,to) { }

        protected override bool CanTrainsition()
        {
            if(GameFsmManager.Instance.CurrType == GameFsmType.StartGame)
            {
                EventManager.Instance.TriggerEvent(EventSystemType.FSM,EventTypeNameDefine.UpdateFsm,GameFsmType.None);
                return true;
            }
            return false;
        }
    }
}
