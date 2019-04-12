using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager:MonoBehaviour
{

    private static CoroutineManager m_instance;

    public static CoroutineManager Instance
    {
        get
        {
            if(m_instance == null)
            {
                GameObject go = new GameObject("[TaskManager]");
                m_instance = go.AddComponent<CoroutineManager>();
                GameObject.DontDestroyOnLoad(go);
            }
            return m_instance;
        }
    }

    public CoroutiuneState CreateCoroutiune(IEnumerator coroutine)
    {
        return new CoroutiuneState(coroutine);
    }

}
