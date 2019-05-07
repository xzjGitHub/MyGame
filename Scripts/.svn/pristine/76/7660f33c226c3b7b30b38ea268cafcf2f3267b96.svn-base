using System.Collections.Generic;

namespace GameFSM
{
    /// <summary>
    /// 状态接口
    /// </summary>
    public interface IState
    {
        string StateName { get; }

        string StateTag { set; get; }

        /// <summary>
        /// 当前状态的所属状态机
        /// </summary>
        IStateMachine Parent { get; set; }

        /// <summary>
        /// 从进入状态开始计算的时间
        /// </summary>
        float Timer { get; }

        /// <summary>
        /// 当前状态的所有过渡
        /// </summary>
        List<ITransition> AllTransition { get; }

        void AddTransition(ITransition transition);

        void RemoveTransition(ITransition transition);

        void OnEnter(IState preState);

        void OnUpdate(float delaTime);

        void OnLateUpdate(float delaTime);

        void OnFixedUpdate();

        void OnExit(IState nextState);

        void Reset();
    }
}