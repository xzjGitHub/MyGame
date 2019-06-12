
//--------------------------------------------------------------
//Creator： XZJ
//Data：    2019/5/28 11:03:20
//Note:     
//--------------------------------------------------------------

using EventCenter;
using GameFSM;
using GameFsmMachine;

/// <summary>
/// 
/// </summary>
public class AnyStateToExitGameState: FSMTransition
{
    public AnyStateToExitGameState(string name,IState from,IState to) : base(name,from,to) { }

    protected override bool CanTrainsition()
    {
        if(GameFsmManager.Instance.CurrType == GameFsmType.ExitGame)
        {
            EventManager.Instance.TriggerEvent(EventSystemType.FSM,EventTypeNameDefine.UpdateFsm,GameFsmType.None);
            return true;
        }
        return false;
    }
}

