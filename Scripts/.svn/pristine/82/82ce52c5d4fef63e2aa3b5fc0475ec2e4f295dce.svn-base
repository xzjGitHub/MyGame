
//--------------------------------------------------------------
//Creator： XZJ
//Data：    2019/5/28 10:09:33
//Note:     
//--------------------------------------------------------------


using UnityEngine;

/// <summary>
/// 
/// </summary>
public abstract class MonoSingleton<T>: MonoBehaviour where T : MonoSingleton<T>
{
    private static readonly object m_lock = new object();

    private static T m_instance;
    public static T Instance
    {
        get
        {
            if(!Application.isPlaying)
            {
                Debug.LogError("Application.isPlaying," + Application.isPlaying);
                return null;
            }
            lock(m_lock)
            {
                if(m_instance == null)
                {
                    m_instance = Object.FindObjectOfType<T>();
                    if(FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("More than one," + typeof(T));
                    }
                    if(m_instance == null)
                    {
                        GameObject obj = new GameObject(string.Format("(singleton)+{0}",typeof(T)));
                        m_instance = obj.AddComponent<T>();
                        DontDestroyOnLoad(obj);
                    }
                }
                return m_instance;
            }
        }
    }
}

