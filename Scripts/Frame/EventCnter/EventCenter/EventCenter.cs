﻿using System;
using System.Collections.Generic;

namespace EventCenter
{
    public partial class EventCenter
    {
        //[事件系统 [事件名称,事件列表]]
        private readonly Dictionary<EventSystemType,Dictionary<string,List<Delegate>>> m_allEvent;

        public EventCenter()
        {
            m_allEvent = new Dictionary<EventSystemType,Dictionary<string,List<Delegate>>>();
        }

        /// <summary>
        /// 从某个事件类型系统里面移除某个类型的事件
        /// </summary>
        /// <param name="eventSystemType">事件系统</param>
        /// <param name="eventType">事件类型</param>
        public void RemoveEventTypeInEventSystemType(EventSystemType eventSystemType,
            string eventType)
        {
            if(!HasIn(eventSystemType,eventType))
            {
                return;
            }
            m_allEvent[eventSystemType].Remove(eventType);
        }

        /// <summary>
        /// 移除某个事件系统
        /// </summary>
        /// <param name="eventSystemType"></param>
        public void RemoveEventSystemType(EventSystemType eventSystemType)
        {
            if(!m_allEvent.ContainsKey(eventSystemType))
            {
                LogHelper_MC.LogError(string.Format("m_allEvent not Contains EventSystemType: {0}",eventSystemType));
                return;
            }
            m_allEvent.Remove(eventSystemType);
        }

        /// <summary>
        /// 移除所有事件系统
        /// </summary>
        public void RemoveAllEventSystemType()
        {
            m_allEvent.Clear();
        }

        private bool HasIn(EventSystemType eventSystemType,string eventType)
        {
            if(!m_allEvent.ContainsKey(eventSystemType))
            {
               // LogHelper_MC.LogError(string.Format("m_allEvent not Contains EventSystemType: {0}",eventSystemType));
                return false;
            }
            if(!m_allEvent[eventSystemType].ContainsKey(eventType))
            {
              //  LogHelper_MC.LogError(string.Format("eventSystemType :{0} not contains eventType: {1}",eventSystemType,eventType));
                return false;
            }
            return true;
        }
    }
}
