using System;
using System.Collections.Generic;

namespace EventCenter
{
    public partial class EventCenter
    {
        private bool Check(EventSystemType eventSystemType,
            string eventType,int hashCode)
        {
            if(!m_allEvent.ContainsKey(eventSystemType))
            {
                m_allEvent.Add(eventSystemType,new Dictionary<string,List<Delegate>>());
            }

            if(!m_allEvent[eventSystemType].ContainsKey(eventType))
            {
                m_allEvent[eventSystemType].Add(eventType,new List<Delegate>());
            }

            List<Delegate> list = m_allEvent[eventSystemType][eventType];
            for(int i = 0; i < list.Count; i++)
            {
                if(list[i].GetHashCode() == hashCode)
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="eventSystemType">事件所在系统</param>
        /// <param name="eventType">事件类型名称</param>
        /// <param name="handler">注册的方法</param>
        public void RegEventListener(EventSystemType eventSystemType,
            string eventType,Action handler)
        {
            bool result = Check(eventSystemType,eventType,handler.GetHashCode());
            if(result)
                m_allEvent[eventSystemType][eventType].Add(handler);
        }

        public void RegEventListener<T>(EventSystemType eventSystemType,
            string eventType,Action<T> handler)
        {
            bool result = Check(eventSystemType,eventType,handler.GetHashCode());
            if(result)
                m_allEvent[eventSystemType][eventType].Add(handler);
        }

        public void RegEventListener<T, TU>(EventSystemType eventSystemType,
            string eventType,Action<T,TU> handler)
        {
            bool result = Check(eventSystemType,eventType,handler.GetHashCode());
            if(result)
                m_allEvent[eventSystemType][eventType].Add(handler);
        }

        public void RegEventListener<T, TU, TV>(EventSystemType eventSystemType,
            string eventType,Action<T,TU,TV> handler)
        {
            bool result = Check(eventSystemType,eventType,handler.GetHashCode());
            if(result)
                m_allEvent[eventSystemType][eventType].Add(handler);
        }

        public void RegEventListener<T, TU, TV, TW>(EventSystemType eventSystemType,
            string eventType,Action<T,TU,TV,TW> handler)
        {
            bool result = Check(eventSystemType,eventType,handler.GetHashCode());
            if(result)
                m_allEvent[eventSystemType][eventType].Add(handler);
        }
    }
}
