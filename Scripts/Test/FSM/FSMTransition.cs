namespace FSM
{
    public delegate bool TransitionDelegate(object param = null);

    /// <summary>
    /// 用于进行状态过渡
    /// </summary>
    public class FSMTransition : IFSMTransition
    {
        /// <summary>
        /// 过渡时需要做的事
        /// </summary>
        public event TransitionDelegate OnTransition;

        /// <summary>
        /// 检查是否在过渡
        /// </summary>
        public event TransitionDelegate OnCheck;

        /// <summary>
        /// 从哪个状态开始过渡
        /// </summary>
        public IFSMState From
        {
            get { return from; }
            set { from = value; }
        }

        /// <summary>
        /// 要过渡到哪个状态
        /// </summary>
        public IFSMState To
        {
            get { return to; }
            set { to = value; }
        }

        /// <summary>
        /// 过渡名
        /// </summary>
        public string Name { get { return name; } }

        /// <summary>
        /// 过渡时的回调
        /// </summary>
        /// <returns>true=过渡结束，false=继续进行过渡 </returns>
        public bool TransitionCallback()
        {
            if (OnTransition != null)
            {
                return OnTransition();
            }
            return true;
        }

        /// <summary>
        /// 能否开始过渡
        /// </summary>
        /// <returns>true=开始进行过渡 false=不进行过渡</returns>
        public bool ShouldBengin(object param = null)
        {
            if (OnCheck != null)
            {
                return OnCheck(param);
            }
            return false;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_name">过渡名</param>
        /// <param name="_from">源状态</param>
        /// <param name="_to">目标状态</param>
        public FSMTransition(string _name, IFSMState _from, IFSMState _to)
        {
            name = _name;
            from = _from;
            to = _to;
        }

        /// <summary>
        /// 源状态
        /// </summary>
        private IFSMState from;

        /// <summary>
        /// 目标状态
        /// </summary>
        private IFSMState to;

        /// <summary>
        /// 过渡名
        /// </summary>
        private string name;
    }
}
