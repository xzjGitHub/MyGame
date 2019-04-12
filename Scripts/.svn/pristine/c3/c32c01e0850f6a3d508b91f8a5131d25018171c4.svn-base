using System;

namespace EventCenter
{
    public class EventManager: Singleton<EventManager>
    {
        private EventCenter m_eventCeneter;
        private EventManager()
        {
            m_eventCeneter = new EventCenter();
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="eventType">类型</param>
        /// <param name="handler">回调</param>
        public void RegEventListener(EventSystemType eventSystemType,string eventType,
            Action handler)
        {
            m_eventCeneter.RegEventListener(eventSystemType,eventType,handler);
        }

        public void RegEventListener<T>(EventSystemType eventSystemType,string eventType,
            Action<T> handler)
        {
            m_eventCeneter.RegEventListener(eventSystemType,eventType,handler);
        }

        public void RegEventListener<T, TU>(EventSystemType eventSystemType,string eventType,
         Action<T,TU> handler)
        {
            m_eventCeneter.RegEventListener(eventSystemType,eventType,handler);
        }

        public void RegEventListener<T, TU, TV>(EventSystemType eventSystemType,string eventType,
         Action<T,TU,TV> handler)
        {
            m_eventCeneter.RegEventListener(eventSystemType,eventType,handler);
        }

        public void RegEventListener<T, TU, TV,TW>(EventSystemType eventSystemType,string eventType,
         Action<T,TU,TV,TW> handler)
        {
            m_eventCeneter.RegEventListener(eventSystemType,eventType,handler);
        }


        /// <summary>
        /// 注销事件
        /// </summary>
        /// <param name="eventSystemType"></param>
        /// <param name="eventType"></param>
        /// <param name="handler"></param>
        public void UnRegEventListener(EventSystemType eventSystemType,string eventType,
            Action handler)
        {
            m_eventCeneter.UnRegEventListener(eventSystemType,eventType,handler);
        }

        public void UnRegEventListener<T>(EventSystemType eventSystemType,string eventType,
            Action<T> handler)
        {
            m_eventCeneter.UnRegEventListener(eventSystemType,eventType,handler);
        }

        public void UnRegEventListener<T,TU>(EventSystemType eventSystemType,string eventType,
         Action<T,TU> handler)
        {
            m_eventCeneter.UnRegEventListener(eventSystemType,eventType,handler);
        }

        public void UnRegEventListener<T, TU,TV>(EventSystemType eventSystemType,string eventType,
         Action<T,TU,TV> handler)
        {
            m_eventCeneter.UnRegEventListener(eventSystemType,eventType,handler);
        }

        public void UnRegEventListener<T, TU, TV,TW>(EventSystemType eventSystemType,string eventType,
        Action<T,TU,TV,TW> handler)
        {
            m_eventCeneter.UnRegEventListener(eventSystemType,eventType,handler);
        }


        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="eventSystemType"></param>
        /// <param name="eventType"></param>
        public void TriggerEvent(EventSystemType eventSystemType,string eventType)
        {
            m_eventCeneter.TriggerEvent(eventSystemType,eventType);
        }

        public void TriggerEvent<T>(EventSystemType eventSystemType,string eventType,T arg1)
        {
            m_eventCeneter.TriggerEvent(eventSystemType,eventType,arg1);
        }

        public void TriggerEvent<T,TU>(EventSystemType eventSystemType,string eventType,
            T arg1,TU arg2)
        {
            m_eventCeneter.TriggerEvent(eventSystemType,eventType,arg1,arg2);
        }

        public void TriggerEvent<T, TU,TV>(EventSystemType eventSystemType,string eventType,
         T arg1,TU arg2,TV arg3)
        {
            m_eventCeneter.TriggerEvent(eventSystemType,eventType,arg1,arg2,arg3);
        }

        public void TriggerEvent<T, TU, TV,TW>(EventSystemType eventSystemType,string eventType,
        T arg1,TU arg2,TV arg3,TW arg4)
        {
            m_eventCeneter.TriggerEvent(eventSystemType,eventType,arg1,arg2,arg3,arg4);
        }

        /// <summary>
        /// 从某个事件类型系统里面移除某个类型的事件
        /// </summary>
        /// <param name="eventSystemType">事件系统</param>
        /// <param name="eventType">事件类型</param>
        public void RemoveEventTypeInEventSystemType(EventSystemType eventSystemType,
            string eventType)
        {
            m_eventCeneter.RemoveEventTypeInEventSystemType(eventSystemType,eventType);
        }

        /// <summary>
        /// 移除某个事件系统
        /// </summary>
        /// <param name="eventSystemType"></param>
        public void RemoveEventSystemType(EventSystemType eventSystemType)
        {
            m_eventCeneter.RemoveEventSystemType(eventSystemType);
        }

        public void RemoveAllEvent()
        {
            m_eventCeneter.RemoveAllEventSystemType();
        }
    }
}
