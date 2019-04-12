using System.Collections.Generic;
using UnityEngine;

public class UICharStateManager
{

    public void AddState(int stateID, UIPlayEffect playEffect)
    {
        RemoveState(stateID);
        states.Add(stateID, playEffect);
    }

    /// <summary>
    /// 移除状态
    /// </summary>
    /// <param name="stateID"></param>
    public void RemoveState(int stateID)
    {
        if (!states.ContainsKey(stateID))
        {
            return;
        }
        states[stateID].DestroyRes();
        states.Remove(stateID);
    }

    private Dictionary<int, UIPlayEffect> states = new Dictionary<int, UIPlayEffect>();
}
