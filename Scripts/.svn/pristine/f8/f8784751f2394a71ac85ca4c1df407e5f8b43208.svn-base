namespace FSM
{
    /// <summary>
    /// 用于状态过渡的接口
    /// </summary>
    public interface IFSMTransition
    {
        /// <summary>
        /// 从哪个状态开始过渡
        /// </summary>
        IFSMState From { get; set; }

        /// <summary>
        /// 要过渡到哪个状态
        /// </summary>
        IFSMState To { get; set; }

        /// <summary>
        /// 过渡名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 过渡时的回调
        /// </summary>
        /// <returns>true=过渡结束，false=继续进行过渡 </returns>
        bool TransitionCallback();

        /// <summary>
        /// 能否开始过渡_默认不带参数
        /// </summary>
        /// <returns>true=开始进行过渡 false=不进行过渡</returns>
        bool ShouldBengin(object param=null);
    }
}
