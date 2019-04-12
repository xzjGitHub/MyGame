using System.Collections.Generic;
using System.Linq;

namespace FSM
{
    /// <summary>
    /// 状态机类_需要继承于状态类、实现状态机接口
    /// </summary>
    public class FSMStateMachine : FSMState, IFSMStateMachine
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_name">状态机名</param>
        /// <param name="_defaultState">默认状态</param>
        public FSMStateMachine(string _name, IFSMState _defaultState = null) : base(_name)
        {
            states = new List<IFSMState>();
            DefaultState = _defaultState;
        }

        /// <summary>
        /// 当前状态
        /// </summary>
        public IFSMState CurrentState
        {
            get { return currentState; }
        }

        /// <summary>
        /// 默认状态
        /// </summary>
        public IFSMState DefaultState
        {
            get { return defaultState; }
            set
            {
                AddState(value);
                defaultState = value;
            }
        }

        /// <summary>
        /// 所有状态
        /// </summary>
        public List<IFSMState> States { get { return states; } }

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="state"></param>
        public void AddState(IFSMState state)
        {
            if (states == null || states.Contains(state)) return;
            states.Add(state);
            //设置当前状态机
            state.Parent = this;
            if (defaultState == null)
            {
                defaultState = state;
            }
        }

        /// <summary>
        /// 移除状态
        /// </summary>
        /// <param name="state"></param>
        public void Remove(IFSMState state)
        {
            //在状态机运行过程中，不能删除状态
            if (currentState == state) return;
            if (states == null || !states.Contains(state)) return;
            states.Remove(state);
            //移除当前状态机
            state.Parent = null;
            if (defaultState == state)
            {
                defaultState = states.Count > 1 ? states[0] : null;
            }
        }

        /// <summary>
        /// 通过指定Tag获得状态
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public IFSMState GetStateWithTag(string tag)
        {
            return null;
        }

        /// <summary>
        /// 设置当前状态
        /// </summary>
        /// <param name="_name">状态名字</param>
        public void SetCurrentState(string _name)
        {
            IFSMState _state = states.Find(a => a.Name == _name);
            if (_state==null) return;
            isTransition = false;
            currentState = _state;
            currentState.EnterCallBack(null);
        }

        public override void UpdateCallBack(float deltaTime, object param = null)
        {
            if (isTransition)
            {
                if (!t.TransitionCallback()) return;
                DoTransition(t);
                isTransition = false;
                return;
            }
            //没有在过渡
            base.UpdateCallBack(deltaTime, param);
            if (currentState == null) currentState = defaultState;
            tsList = currentState.Transitions;
            int count = tsList.Count;
            for (int i = 0; i < count; i++)
            {
                t = tsList[i];
                if (!t.ShouldBengin(param)) continue;
                isTransition = true;
                return;
            }
            currentState.UpdateCallBack(deltaTime, param);
        }

        public override void LateUpdateCallBack(float deltaTime)
        {
            base.LateUpdateCallBack(deltaTime);
            currentState.LateUpdateCallBack(deltaTime);
        }

        public override void FixedUpdateCallBack()
        {
            base.FixedUpdateCallBack();
            currentState.FixedUpdateCallBack();
        }

        /// <summary>
        /// 开始进行过渡
        /// </summary>
        private void DoTransition(IFSMTransition _t)
        {
            currentState.ExitCallBack(_t.To);
            currentState = _t.To;
            currentState.EnterCallBack(_t.From);
        }

        /// <summary>
        /// 当前状态
        /// </summary>
        private IFSMState currentState;

        /// <summary>
        /// 默认状态
        /// </summary>
        private IFSMState defaultState;

        /// <summary>
        /// 所有状态
        /// </summary>
        private List<IFSMState> states;

        /// <summary>
        /// 临时
        /// </summary>
        private List<IFSMTransition> tsList;

        /// <summary>
        /// 临时过渡
        /// </summary>
        private IFSMTransition t;

        /// <summary>
        /// 是否在进行过渡
        /// </summary>
        private bool isTransition = false;

    }
}
