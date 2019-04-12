using UnityEngine;
using System.Collections;

namespace GameEventDispose
{
    public class EventDispatcher
    {
        public static EventDispatcher Instance
        {
            get
            {
                if (instance == null) instance = new EventDispatcher();
                return instance;
            }
        }
        /// <summary>
        /// 系统事件
        /// </summary>
        public EventManager SystemEvent
        {
            get { return this._eventSystem; }
        }
        /// <summary>
        /// 战斗事件
        /// </summary>
        public EventManager CombatEvent
        {
            get { return this.eventCombat; }
        }
        /// <summary>
        /// 战斗特效
        /// </summary>
        public EventManager CombatEffect
        {
            get { return this.eventCombatEffect; }
        }
        /// <summary>
        /// 探索事件
        /// </summary>
        public EventManager ExploreEvent
        {
            get { return this.eventExplore; }
        }
        /// <summary>
        /// 剧本时间事件
        /// </summary>
        public EventManager ScriptTimeEvent
        {
            get { return this.eventScriptTime; }
        }
        /// <summary>
        /// 入侵事件
        /// </summary>
        public EventManager InvasionEvent
        {
            get { return this.eventInvasion; }
        }
        /// <summary>
        /// 远征事件
        /// </summary>
        public EventManager FortEvent
        {
            get { return this.eventFort; }
        }
        /// <summary>
        /// 角色事件
        /// </summary>
        public EventManager CharEvent
        {
            get { return this.eventChar; }
        }
        /// <summary>
        /// 悬赏事件
        /// </summary>
        public EventManager BountyEvent
        {
            get
            {
                return eventBounty;
            }
        }

        private static EventDispatcher instance;

        // 系统事件
        private EventManager _eventSystem = new EventManager();
        // 战斗事件
        private EventManager eventCombat = new EventManager();
        //战斗特效
        private EventManager eventCombatEffect = new EventManager();
        //探索事件
        private EventManager eventExplore = new EventManager();
        //剧本时间事件
        private EventManager eventScriptTime = new EventManager();
        //入侵事件
        private EventManager eventInvasion = new EventManager();
        //要塞事件
        private EventManager eventFort = new EventManager();
        //角色事件
        private EventManager eventChar = new EventManager();
        //悬赏事件
        private EventManager eventBounty = new EventManager();
    }
}