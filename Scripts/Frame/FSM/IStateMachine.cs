
namespace GameFSM
{
    public interface IStateMachine
    {
        IState CurrentState { get; }

        IState DefaultState { get; set; }

        void AddState(IState state);

        void RemoveState(IState state);

        IState GetState(string tag);

        void Reset();

    }

}
