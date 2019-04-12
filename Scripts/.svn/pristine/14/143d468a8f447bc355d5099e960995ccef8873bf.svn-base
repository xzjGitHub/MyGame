using System.Collections.Generic;

namespace FSM
{
    public delegate void StateDelegate();
    public delegate void StateDelegateState(IFSMState state);
    public delegate void StateDelegateFloat(float f);
    public delegate void StateDelegateFloat1(float f,object param);

    /// <summary>
    /// 状态类
    /// </summary>
    public class FSMState : IFSMState
    {
        /// <summary>
        /// 当进入状态时调用的事件
        /// </summary>
        public event StateDelegateState OnEnter;

        /// <summary>
        /// 当离开状态时调用的事件
        /// </summary>
        public event StateDelegateState OnExit;

        /// <summary>
        /// 当Update调用的事件
        /// </summary>
        public event StateDelegateFloat1 OnUpdate;

        /// <summary>
        /// 当LateUpdate调用的事件
        /// </summary>
        public event StateDelegateFloat OnLateUpdate;

        /// <summary>
        /// 当FixedUpdate调用的事件
        /// </summary>
        public event StateDelegate OnFixedUpdate;

        /// <summary>
        /// 状态名
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// 状态标签
        /// </summary>
        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        /// <summary>
        /// 当前状态的状态机
        /// </summary>
        public IFSMStateMachine Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        /// <summary>
        /// 从进入状态开始计算的时长
        /// </summary>
        public float Timer
        {
            get { return timer; }
        }

        /// <summary>
        /// 状态过渡列表
        /// </summary>
        public List<IFSMTransition> Transitions { get { return transitions; } }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_name">状态名</param>
        public FSMState(string _name)
        {
            name = _name;
            transitions = new List<IFSMTransition>();
        }

        /// <summary>
        /// 添加过渡
        /// </summary>
        /// <param name="t">需要添加的过渡</param>
        public void AddTransition(IFSMTransition t)
        {
            if (t != null && !transitions.Contains(t))
            {
                transitions.Add(t);
            }
        }

        /// <summary>
        /// 进入状态时回调
        /// </summary>
        /// <param name="prevState">上一个状态</param>
        public virtual void EnterCallBack(IFSMState prevState)
        {
            timer = 0;
            if (OnEnter == null) return;
            OnEnter(prevState);
        }

        /// <summary>
        /// 退出状态时回调
        /// </summary>
        /// <param name="nextState">下一个状态</param>
        public virtual void ExitCallBack(IFSMState nextState)
        {
            timer = 0;
            if (OnExit == null) return;
            OnExit(nextState);
        }

        /// <summary>
        /// Update的回调
        /// </summary>
        /// <param name="deltaTime">Time.deltaTime</param>
        public virtual void UpdateCallBack(float deltaTime,object param)
        {
            //累加计时器
            timer += deltaTime;
            if (OnUpdate == null) return;
            OnUpdate(deltaTime, param);
        }

        /// <summary>
        /// LateUpdate的回调
        /// </summary>
        /// <param name="deltaTime">Time.deltaTime</param>
        public virtual void LateUpdateCallBack(float deltaTime)
        {
            if (OnLateUpdate == null) return;
            OnLateUpdate(deltaTime);
        }

        /// <summary>
        /// FixedUpdate的回调
        /// </summary>
        public virtual void FixedUpdateCallBack()
        {
            if (OnFixedUpdate == null) return;
            OnFixedUpdate();
        }

        /// <summary>
        /// 状态名
        /// </summary>
        private string name;

        /// <summary>
        /// 状态标签
        /// </summary>
        private string tag;

        /// <summary>
        /// 当前状态的状态机
        /// </summary>
        private IFSMStateMachine parent;

        /// <summary>
        /// 计时器
        /// </summary>
        private float timer;

        /// <summary>
        /// 状态过渡
        /// </summary>
        private List<IFSMTransition> transitions;
    }
}
