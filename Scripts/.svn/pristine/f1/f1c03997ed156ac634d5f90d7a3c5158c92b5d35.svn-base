
namespace GameFSM
{

    public delegate bool FSMTransitionDelegate();

    public class FSMTransition: ITransition
    {
        private string m_name;
        private IState m_fromState;                                 //原状态状态
        private IState m_toState;                                   //目标状态

        public event FSMTransitionDelegate OnTransition;            //过渡过程中执行的事件 如果没有 则直接过渡
        public event FSMTransitionDelegate OnCheck;                 //检测过渡条件

        public IState From
        {
            get { return m_fromState; }
            set { m_fromState = value; }
        }

        public IState To
        {
            get { return m_toState; }
            set { m_toState = value; }
        }

        public string name
        {
            get { return m_name; }
        }

        public FSMTransition(string name,IState from,IState to)
        {
            m_name = name;
            m_fromState = from;
            m_toState = to;
        }

        public bool TransitionCallBack()
        {
            if(OnTransition != null)
            {
                return OnTransition();
            }
            return Transitioning();
        }

        public bool ShouldBeginTransition()
        {
            if(OnCheck != null)
            {
                return OnCheck();
            }
            return CanTrainsition();
        }


        protected virtual bool CanTrainsition()
        {
            return false;
        }

        protected virtual bool Transitioning()
        {
            return true;
        }

        public void Reset()
        {
            m_name = "";
            m_fromState = null;
            m_toState = null;
        }
    }
}

