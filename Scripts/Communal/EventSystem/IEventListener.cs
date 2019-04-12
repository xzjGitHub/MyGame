using UnityEngine;
using System.Collections;

namespace GameEventDispose
{
    /// <summary>
    /// 事件监听接口
    /// </summary>
    interface IEventListener
    {
        void RegisterEvent();
        void UnRegisterEvent();
    } 
}

