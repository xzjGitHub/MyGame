using System;
using System.Collections.Generic;
using UnityEngine;

public enum NpcType
{
    Npc1,
    Npc2
}

public class NpcUtil: Singleton<NpcUtil>
{
    private Dictionary<string,GameObject> m_dict;

    private NpcUtil()
    {
        m_dict = new Dictionary<string,GameObject>();
    }


    public GameObject GetNpc(NpcType npcType,string npcName)
    {
        GameObject npcPrefab = null;
        if(m_dict.ContainsKey(npcName))
        {
            npcPrefab = m_dict[npcName];
        }
        else
        {
            npcPrefab = ResourceLoadUtil.LoadNpc(npcName);
            m_dict.Add(npcName,npcPrefab);
        }
        string poolName = GetNpcPoolName(npcType);
        GameObject npc = GameObjectPool.Instance.GetObject(poolName,npcPrefab);
        return npc;
    }

    public void FreeNpc(NpcType npcType,GameObject obj)
    {
        GameObjectPool.Instance.FreeGameObjectByObj(GetNpcPoolName(npcType),obj);
    }


    private string GetNpcPoolName(NpcType npcType)
    {
        switch(npcType)
        {
            case NpcType.Npc1:
                return StringDefine.ObjectPooItemKey.UINpc1Name;
            case NpcType.Npc2:
                return StringDefine.ObjectPooItemKey.UINpc2Name;
            default:
                return "";
        }
    }

    public void Clear()
    {
        m_dict.Clear();
    }

}
