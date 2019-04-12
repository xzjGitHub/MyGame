using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class TitleFactory: Singleton<TitleFactory>
{
    private const string m_path = "Title";
    private GameObject m_prefab;

    private Queue<GameObject> m_queue = new Queue<GameObject>();

    private TitleFactory() { }

    public GameObject CreateTitle(Transform parent,string name)
    {
        GameObject temp = GetGameObject();
        Title title=Utility.RequireComponent<Title>(temp);
      //  title.SetName(name);
        Utility.SetParent(temp, parent, true, Vector3.one, new Vector3(0, 272, 0));
        return temp;
    }

    public void Release(GameObject gameObject)
    {
        if(gameObject == null)
            return;
        m_queue.Enqueue(gameObject);
        GameObjectPool.Instance.AddChild(gameObject);
        gameObject.SetActive(false);
    }

    private GameObject GetGameObject()
    {
        GameObject gameObject = null;
        if(m_queue.Count > 0)
        {
            gameObject = m_queue.Dequeue();
        }
        else
        {
            if(m_prefab == null)
            {
                m_prefab = ResourceLoadUtil.LoadPrefab(m_path,false);
            }
            gameObject = GameObject.Instantiate(m_prefab);
        }
        return gameObject;
    }

    public void Clear()
    {
        while(m_queue.Count > 0)
        {
            GameObject temp = m_queue.Dequeue();
            GameObject.DestroyImmediate(temp);
        }
    }
}

