namespace FSM
{
    /// <summary>
    /// 状态机接口
    /// </summary>
    public interface IFSMStateMachine
    {
        /// <summary>
        /// 当前状态
        /// </summary>
        IFSMState CurrentState { get; }

        /// <summary>
        /// 默认状态
        /// </summary>
        IFSMState DefaultState { set; get; }

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="state"></param>
        void AddState(IFSMState state);

        /// <summary>
        /// 移除状态
        /// </summary>
        /// <param name="state"></param>
        void Remove(IFSMState state);

        /// <summary>
        /// 通过指定Tag获得状态
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        IFSMState GetStateWithTag(string tag);
    }
}
