using System.Collections.Generic;
using UnityEngine;

public class ItemCostFactory: Singleton<ItemCostFactory>
{
    private Queue<GameObject> m_queue = new Queue<GameObject>();
    private GameObject m_prefab;

    private ItemCostFactory() { }

    public GameObject CreateItemCost(int itemId,int needNum,Transform parent)
    {
        GameObject temp = GetGameObject();
        ItemCost itemCost = Utility.RequireComponent<ItemCost>(temp);
        itemCost.Init(itemId,needNum);
        Utility.AddChild(parent,temp);
        return temp;
    }

    public GameObject CreateItemCostByTemplate(int templateId,int needNum,Transform parent = null)
    {
        GameObject temp = GetGameObject();
        ItemCost itemCost = Utility.RequireComponent<ItemCost>(temp);
        itemCost.InitByTemplateId(templateId,needNum);
        if(parent != null)
            Utility.AddChild(parent,temp);
        return temp;
    }

    public GameObject CreateItemCost(ItemData itemData,int needNum,Transform parent = null)
    {
        GameObject temp = GetGameObject();
        ItemCost itemCost = Utility.RequireComponent<ItemCost>(temp);
        itemCost.Init(itemData,needNum);
        if(parent != null)
            Utility.AddChild(parent,temp);
        return temp;
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
                m_prefab = ResourceLoadUtil.LoadPrefab(StringDefine.PrefabNameDefine.ItemCost);
            }
            gameObject = GameObject.Instantiate(m_prefab);
        }
        return gameObject;
    }

    public void Release(GameObject gameObject)
    {
        if(gameObject != null)
        {
            m_queue.Enqueue(gameObject);
            GameObjectPool.Instance.AddChild(gameObject);
            gameObject.SetActive(false);
        }
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
