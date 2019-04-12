using System.Collections.Generic;

namespace FSM
{
    /// <summary>
    /// 状态接口
    /// </summary>
    public interface IFSMState
    {
        /// <summary>
        /// 状态名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 状态标签
        /// </summary>
        string Tag { set; get; }

        /// <summary>
        /// 当前状态的状态机
        /// </summary>
        IFSMStateMachine Parent { get; set; }

        /// <summary>
        /// 从进入状态开始计算的时长
        /// </summary>
        float Timer { get; }

        /// <summary>
        /// 状态过渡列表
        /// </summary>
        List<IFSMTransition> Transitions { get; }

        /// <summary>
        /// 进入状态时回调
        /// </summary>
        /// <param name="prevState">上一个状态</param>
        void EnterCallBack(IFSMState prevState);

        /// <summary>
        /// 退出状态时回调
        /// </summary>
        /// <param name="nextState">下一个状态</param>
        void ExitCallBack(IFSMState nextState);

        /// <summary>
        /// Update的回调
        /// </summary>
        /// <param name="deltaTime">Time.deltaTime</param>
        void UpdateCallBack(float deltaTime,object param=null);

        /// <summary>
        /// LateUpdate的回调
        /// </summary>
        /// <param name="deltaTime">Time.deltaTime</param>
        void LateUpdateCallBack(float deltaTime);

        /// <summary>
        /// FixedUpdate的回调
        /// </summary>
        void FixedUpdateCallBack( );

        /// <summary>
        /// 添加过渡
        /// </summary>
        /// <param name="t">需要添加的过渡</param>
        void AddTransition(IFSMTransition t);
    }

}
