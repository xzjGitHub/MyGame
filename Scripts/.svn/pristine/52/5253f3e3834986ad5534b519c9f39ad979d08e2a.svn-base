using System;
using System.Collections.Generic;


namespace EventCenter
{
    public partial class EventCenter
    {
        private void UnRegister(EventSystemType eventSystemType,
            string eventType,int actionHashCode)
        {
            if(!HasIn(eventSystemType,eventType))
            {
                return;
            }

            List<Delegate> list = m_allEvent[eventSystemType][eventType];
            for(int i = list.Count - 1; i >= 0; i--)
            {
                if(list[i].GetHashCode() == actionHashCode)
                {
                    list.RemoveAt(i);
                    break;
                }
            }

            if(m_allEvent[eventSystemType][eventType].Count == 0)
            {
                m_allEvent[eventSystemType].Remove(eventType);
                if(m_allEvent[eventSystemType].Count == 0)
                {
                    m_allEvent.Remove(eventSystemType);
                }
            }
        }

        /// <summary>
        /// 注销事件
        /// </summary>
        /// <param name="eventSystemType">事件所在系统</param>
        /// <param name="eventType">事件类型</param>
        /// <param name="handler">注销的方法</param>
        public void UnRegEventListener(EventSystemType eventSystemType,
            string eventType,Action handler)
        {
            UnRegister(eventSystemType,eventType,handler.GetHashCode());
        }

        public void UnRegEventListener<T>(EventSystemType eventSystemType,
            string eventType,Action<T> handler)
        {
            UnRegister(eventSystemType,eventType,handler.GetHashCode());
        }

        public void UnRegEventListener<T, TU>(EventSystemType eventSystemType,
            string eventType,Action<T,TU> handler)
        {
            UnRegister(eventSystemType,eventType,handler.GetHashCode());
        }

        public void UnRegEventListener<T, TU, TV>(EventSystemType eventSystemType,
            string eventType,Action<T,TU,TV> handler)
        {
            UnRegister(eventSystemType,eventType,handler.GetHashCode());
        }

        public void UnRegEventListener<T, TU, TV, TW>(EventSystemType eventSystemType,
            string eventType,Action<T,TU,TV,TW> handler)
        {
            UnRegister(eventSystemType,eventType,handler.GetHashCode());
        }
    }
}
