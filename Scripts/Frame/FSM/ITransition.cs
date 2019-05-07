
namespace GameFSM
{
    public interface ITransition
    {
        IState From { get; set; }

        IState To { get; set; }

        string name { get; }

        /// <summary>
        /// 过渡时的回调
        /// </summary>
        /// <returns>返回true 过渡结束  返回false 继续进行过渡</returns>
        bool TransitionCallBack();

        /// <summary>
        /// 是否已经开始过渡
        /// </summary>
        /// <returns></returns>
        bool ShouldBeginTransition();

        void Reset();
    }
}