using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GameEventDispose
{

    /// <summary>
    /// Event System With C# Delegate
    /// </summary>
    public class EventManager
    {
        private Dictionary<int, List<Delegate>> dicEvents = new Dictionary<int, List<Delegate>>();

        #region 添加监听
        public void AddEventListener(EventId eventId, Action listener)
        {
            AddEvent(eventId, listener);
        }
        public void AddEventListener<T>(EventId eventId, Action<T> listener)
        {
            AddEvent(eventId, listener);
        }
        public void AddEventListener<T0, T1>(EventId eventId, Action<T0, T1> listener)
        {
            AddEvent(eventId, listener);
        }
        public void AddEventListener<T0, T1, T2>(EventId eventId, Action<T0, T1, T2> listener)
        {
            AddEvent(eventId, listener);
        }
        public void AddEventListener<T0, T1, T2, T3>(EventId eventId, Action<T0, T1, T2, T3> listener)
        {
            AddEvent(eventId, listener);
        }

        #endregion

        #region 移除监听
        public void RemoveEventListener(EventId eventId, Action listener)
        {
            RemoveEvent(eventId, listener);
        }
        public void RemoveEventListener<T>(EventId eventId, Action<T> listener)
        {
            RemoveEvent(eventId, listener);
        }
        public void RemoveEventListener<T0, T1>(EventId eventId, Action<T0, T1> listener)
        {
            RemoveEvent(eventId, listener);
        }
        public void RemoveEventListener<T0, T1, T2>(EventId eventId, Action<T0, T1, T2> listener)
        {
            RemoveEvent(eventId, listener);
        }
        public void RemoveEventListener<T0, T1, T2, T3>(EventId eventId, Action<T0, T1, T2, T3> listener)
        {
            RemoveEvent(eventId, listener);
        }
        #endregion

        #region 派发监听
        public void DispatchEvent(EventId eventId)
        {
            //判断是否有该Id  是否为空
            if (!dicEvents.ContainsKey((int)eventId) || dicEvents[(int)eventId] == null) return;
            for (int i = 0; i < dicEvents[(int)eventId].Count; i++)
            {
                if (!IsActionTypeSame(dicEvents[(int)eventId][i], typeof(Action))) continue;
                ExecuteAction(dicEvents[(int)eventId][i].GetInvocationList());
            }
        }
        public void DispatchEvent<T>(EventId eventId, T p)
        {
            //判断是否有该Id  是否为空
            if (!dicEvents.ContainsKey((int)eventId) || dicEvents[(int)eventId] == null) return;
            for (int i = 0; i < dicEvents[(int)eventId].Count; i++)
            {
                if (!IsActionTypeSame(dicEvents[(int)eventId][i], typeof(Action<T>))) continue;
                ExecuteAction<T>(dicEvents[(int)eventId][i].GetInvocationList(), p);
            }
        }
        public void DispatchEvent<T0, T1>(EventId eventId, T0 p0, T1 p1)
        {
            //判断是否有该Id  是否为空
            if (!dicEvents.ContainsKey((int)eventId) || dicEvents[(int)eventId] == null) return;
            for (int i = 0; i < dicEvents[(int)eventId].Count; i++)
            {
                if (!IsActionTypeSame(dicEvents[(int)eventId][i], typeof(Action<T0, T1>))) continue;
                ExecuteAction<T0, T1>(dicEvents[(int)eventId][i].GetInvocationList(), new object[] { p0, p1 });
            }
        }
        public void DispatchEvent<T0, T1, T2>(EventId eventId, T0 p0, T1 p1, T2 p2)
        {
            //判断是否有该Id  是否为空
            if (!dicEvents.ContainsKey((int)eventId) || dicEvents[(int)eventId] == null) return;
            for (int i = 0; i < dicEvents[(int)eventId].Count; i++)
            {
                if (!IsActionTypeSame(dicEvents[(int)eventId][i], typeof(Action<T0, T1, T2>))) continue;
                ExecuteAction<T0, T1, T2>(dicEvents[(int)eventId][i].GetInvocationList(), new object[] { p0, p1, p2 });
            }
        }
        public void DispatchEvent<T0, T1, T2, T3>(EventId eventId, T0 p0, T1 p1, T2 p2, T3 p3)
        {
            //判断是否有该Id  是否为空
            if (!dicEvents.ContainsKey((int)eventId) || dicEvents[(int)eventId] == null) return;
            for (int i = 0; i < dicEvents[(int)eventId].Count; i++)
            {
                if (!IsActionTypeSame(dicEvents[(int)eventId][i], typeof(Action<T0, T1, T2, T3>))) continue;
                ExecuteAction<T0, T1, T2, T3>(dicEvents[(int)eventId][i].GetInvocationList(), new object[] { p0, p1, p2, p3 });
            }
        }
        #endregion

        #region 执行委托

        /// <summary>
        /// 执行委托
        /// </summary>
        /// <param name="_delegates"></param>
        private void ExecuteAction(Delegate[] _delegates)
        {
            for (int i = 0; i < _delegates.Length; i++)
            {
                if (_delegates[i].GetType() != typeof(Action)) continue;
                //
                Action action = _delegates[i] as Action;
                if (action != null) action();
            }
        }
        private void ExecuteAction<T>(Delegate[] _delegates, object _objects)
        {
            for (int i = 0; i < _delegates.Length; i++)
            {
                if (_delegates[i].GetType() != typeof(Action<T>)) continue;
                //
                Action<T> action = _delegates[i] as Action<T>;
                if (action != null) action((T)_objects);
            }
        }
        private void ExecuteAction<T0, T1>(Delegate[] _delegates, object[] _objects)
        {
            for (int i = 0; i < _delegates.Length; i++)
            {
                if (_delegates[i].GetType() != typeof(Action<T0, T1>)) continue;
                Action<T0, T1> action = _delegates[i] as Action<T0, T1>;
                if (action != null) action((T0)_objects[0], (T1)_objects[1]);
            }
        }
        private void ExecuteAction<T0, T1, T2>(Delegate[] _delegates, object[] _objects)
        {
            for (int i = 0; i < _delegates.Length; i++)
            {
                if (_delegates[i].GetType() != typeof(Action<T0, T1, T2>)) continue;
                //
                Action<T0, T1, T2> action = _delegates[i] as Action<T0, T1, T2>;
                if (action != null) action((T0)_objects[0], (T1)_objects[1], (T2)_objects[2]);
            }
        }
        private void ExecuteAction<T0, T1, T2, T3>(Delegate[] _delegates, object[] _objects)
        {
            for (int i = 0; i < _delegates.Length; i++)
            {
                if (_delegates[i].GetType() != typeof(Action<T0, T1, T2, T3>)) continue;
                //
                Action<T0, T1, T2, T3> action = _delegates[i] as Action<T0, T1, T2, T3>;
                if (action != null) action((T0)_objects[0], (T1)_objects[1], (T2)_objects[2], (T3)_objects[3]);
            }
        }
        #endregion


        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="del"></param>
        private void AddEvent(EventId eventId, Delegate del)
        {
            //判断是否有该Id
            if (dicEvents.ContainsKey((int)eventId))
            {
                bool isHave = false;
                for (int i = 0; i < dicEvents[(int)eventId].Count; i++)
                {
                    //判断是否为相同类型
                    if (dicEvents[(int)eventId][i] == null || dicEvents[(int)eventId][i].GetType() != del.GetType()) continue;
                    isHave = true;
                    //判断该类型中是否有该委托
                    if (dicEvents[(int)eventId][i].GetInvocationList().Contains(del)) return;
                    dicEvents[(int)eventId][i] = Delegate.Combine(dicEvents[(int)eventId][i], del);
                }
                //没有该类型的委托
                if (!isHave)
                {
                    dicEvents[(int)eventId].Add(del);
                }
                return;
            }
            dicEvents.Add((int)eventId, new List<Delegate> { del });
        }
        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="del"></param>
        private void RemoveEvent(EventId eventId, Delegate del)
        {
            //判断是否有该Id  是否为空
            if (!dicEvents.ContainsKey((int)eventId) || dicEvents[(int)eventId] == null) return;
            for (int i = 0; i < dicEvents[(int)eventId].Count; i++)
            {
                //判断是否为相同类型
                if (!IsActionTypeSame(dicEvents[(int)eventId][i], del.GetType())) continue;
                //判断该类型中是否有该委托
                if (!dicEvents[(int)eventId][i].GetInvocationList().Contains(del)) return;
                dicEvents[(int)eventId][i] = Delegate.Remove(dicEvents[(int)eventId][i], del);
                return;
            }
        }

        /// <summary>
        /// 获得委托类型是否相同
        /// </summary>
        /// <param name="_action"></param>
        /// <returns></returns>
        private bool IsActionTypeSame(object _action, Type _type)
        {
            return _action != null && _action.GetType() == _type;
        }


    }
}
