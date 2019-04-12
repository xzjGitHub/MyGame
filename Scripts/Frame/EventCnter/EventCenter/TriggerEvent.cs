using System;
using System.Collections.Generic;

namespace EventCenter
{
    public partial class EventCenter
    {
        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="eventSystemType">事件所在系统</param>
        /// <param name="eventType">事件类型</param>
        public void TriggerEvent(EventSystemType eventSystemType,string eventType)
        {
            if(!HasIn(eventSystemType,eventType))
            {
                return;
            }

            List<Delegate> list = m_allEvent[eventSystemType][eventType];
            for(int i = 0; i < list.Count; i++)
            {
                Action action = list[i] as Action;
                if(action != null)
                {
                    action();
                }
                else
                {
                    LogHelperLSK.LogError(string.Format("TriggerEvent {0} error: types of parameters are not match.",eventType));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventSystemType"></param>
        /// <param name="eventType"></param>
        /// <param name="arg1">参数</param>
        public void TriggerEvent<T>(EventSystemType eventSystemType,string eventType,T arg1)
        {
            if(!HasIn(eventSystemType,eventType))
            {
                return;
            }

            List<Delegate> list = m_allEvent[eventSystemType][eventType];
            for(int i = 0; i < list.Count; i++)
            {
                Action<T> action = list[i] as Action<T>;
                if(action != null)
                {
                    action(arg1);
                }
                else
                {
                    LogHelperLSK.LogError(string.Format("TriggerEvent {0} error: types of parameters are not match.",eventType));
                }
            }
        }


        public void TriggerEvent<T,TU>(EventSystemType eventSystemType,string eventType,
            T arg1,TU arg2)
        {
            if(!HasIn(eventSystemType,eventType))
            {
                return;
            }

            List<Delegate> list = m_allEvent[eventSystemType][eventType];
            for(int i = 0; i < list.Count; i++)
            {
                Action<T,TU> action = list[i] as Action<T,TU>;
                if(action != null)
                {
                    action(arg1,arg2);
                }
                else
                {
                    LogHelperLSK.LogError(string.Format("TriggerEvent {0} error: types of parameters are not match.",eventType));
                }
            }
        }

        public void TriggerEvent<T, TU,TV>(EventSystemType eventSystemType,string eventType,
        T arg1,TU arg2,TV arg3)
        {
            if(!HasIn(eventSystemType,eventType))
            {
                return;
            }

            List<Delegate> list = m_allEvent[eventSystemType][eventType];
            for(int i = 0; i < list.Count; i++)
            {
                Action<T,TU,TV> action = list[i] as Action<T,TU,TV>;
                if(action != null)
                {
                    action(arg1,arg2,arg3);
                }
                else
                {
                    LogHelperLSK.LogError(string.Format("TriggerEvent {0} error: types of parameters are not match.",eventType));
                }
            }
        }

        public void TriggerEvent<T, TU, TV,TW>(EventSystemType eventSystemType,string eventType,
        T arg1,TU arg2,TV arg3,TW arg4)
        {
            if(!HasIn(eventSystemType,eventType))
            {
                return;
            }

            List<Delegate> list = m_allEvent[eventSystemType][eventType];
            for(int i = 0; i < list.Count; i++)
            {
                Action<T,TU,TV,TW> action = list[i] as Action<T,TU,TV,TW>;
                if(action != null)
                {
                    action(arg1,arg2,arg3,arg4);
                }
                else
                {
                    LogHelperLSK.LogError(string.Format("TriggerEvent {0} error: types of parameters are not match.",eventType));
                }
            }
        }

    }
}
