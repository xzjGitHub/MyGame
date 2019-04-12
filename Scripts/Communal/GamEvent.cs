using System;
using System.Collections.Generic;
using System.Linq;

namespace EventDispatch
{
    public delegate void EventHandle(object param);
    /// <summary>
    /// 游戏事件
    /// </summary>
    public class GameEvent
    {
        private Dictionary<object, List<Delegate>> mEventTableList = new Dictionary<object, List<Delegate>>();

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="eventHandle"></param>
        public void Add(EventHandle eventHandle)
        {
            if (mEventTableList.ContainsKey(eventHandle.Target))
            {
                if (mEventTableList[eventHandle.Target].Any(item => item.Method == eventHandle.Method)) return;
                mEventTableList[eventHandle.Target].Add(eventHandle);
                return;
            }
            mEventTableList.Add(eventHandle.Target, new List<Delegate> { eventHandle });
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="eventHandle"></param>
        public void Remove(EventHandle eventHandle)
        {
            if (!mEventTableList.ContainsKey(eventHandle.Target)) return;
            for (int i = 0; i < mEventTableList[eventHandle.Target].Count; i++)
            {
                if (mEventTableList[eventHandle.Target][i].Method != eventHandle.Method) continue;
                mEventTableList[eventHandle.Target].Remove(mEventTableList[eventHandle.Target][i]);
                return;
            }
        }

        /// <summary>
        /// 广播事件
        /// </summary>
        /// <param name="param"></param>
        public void Dispatch(object param)
        {
            foreach (var item in mEventTableList)
            {
                foreach (var temp in item.Value)
                {
                    ((EventHandle)temp)(param);
                }
            }
        }
    }


}
