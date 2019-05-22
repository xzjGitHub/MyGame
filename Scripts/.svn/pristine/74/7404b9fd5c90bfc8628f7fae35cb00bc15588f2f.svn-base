
//--------------------------------------------------------------
//Creator： XZJ
//Data：    2019/5/21 16:07:25
//Note:     
//--------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PrefabPoolUtil
{
    private GameObject m_poolRoot;
    private GameObject m_otherObjParent;
    private GameObject m_charParent;
    private Dictionary<string,Transform> m_dict;

    public PrefabPoolUtil()
    {
        m_poolRoot = new GameObject("[PrefabPoolRoot]");
        m_otherObjParent = new GameObject("[OtherHideObjParent]");
        Utility.SetParent(m_otherObjParent,m_poolRoot);
        m_dict = new Dictionary<string,Transform>();
    }

    public void AddToPool(string poolName,GameObject obj)
    {
        if(!m_dict.ContainsKey(poolName))
        {
            GameObject temp = new GameObject(string.Format("[{0} Pool]",poolName));
            if(poolName.Contains("Anima"))
            {
                if(m_charParent == null)
                {
                    m_charParent = new GameObject("[CharPoolParent]");
                    Utility.SetParent(m_charParent,m_poolRoot);
                }
                Utility.SetParent(temp,m_charParent);
            }
            else
            {
                Utility.SetParent(temp,m_poolRoot);
            }
            m_dict.Add(poolName,temp.transform);
        }
        Utility.SetParent(obj,m_dict[poolName],false);
    }

    public void AddChild(GameObject obj)
    {
        if(obj != null)
        {
            Utility.SetParent(obj,m_otherObjParent,false);
        }
    }
}

