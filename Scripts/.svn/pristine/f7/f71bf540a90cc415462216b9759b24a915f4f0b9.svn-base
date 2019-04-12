using System.Collections.Generic;
using UnityEngine;

public enum CavasType
{
    Three,
    SpecialUI,
    PopUI
}

public partial class UIPanelManager: Singleton<UIPanelManager>
{

    public Transform ThreeUIParent { get; set; }

    public Transform SpeicalUIParent { get; set; }

    public Transform PopUiParent { get; set; }

    private Dictionary<string,UIPanelBehaviour> m_allPanels;

    private Dictionary<string,GameObject> m_allPanelObj;


    private UIPanelManager()
    {
        m_allPanels = new Dictionary<string,UIPanelBehaviour>();
        m_allPanelObj = new Dictionary<string,GameObject>();
        InitPath();
    }


    public void AddPanelObj(string name,GameObject obj)
    {
        if(!m_allPanelObj.ContainsKey(name))
            m_allPanelObj.Add(name,obj);
    }

    public GameObject GetPanelObj(string panelName)
    {
        GameObject panelGameObject = null;
        if(m_allPanelObj.ContainsKey(panelName))
        {
            panelGameObject = m_allPanelObj[panelName];
        }
        else
        {
            string panelPath = GetPanelPath(panelName);
            panelGameObject = ResourceLoadUtil.LoadPanelByPath(panelPath);
        }
        return panelGameObject;
    }

    private void SetPanelParent(CavasType type,GameObject obj)
    {
        if(type == CavasType.PopUI)
        {
            Utility.AddChild(PopUiParent,obj);
        }
        else if(type == CavasType.SpecialUI)
        {
            Utility.AddChild(SpeicalUIParent,obj);
        }
        else
        {
            Utility.AddChild(ThreeUIParent,obj);
        }
        //   obj.transform.SetAsLastSibling();
    }

    /// <summary>
    /// 打开界面
    /// </summary>
    /// <typeparam name="T">界面名字</typeparam>
    /// <param name="type">所属于Ui类型</param>
    /// <param name="parmers">参数</param>
    /// <returns></returns>
    public T Show<T>(CavasType type = CavasType.Three,
        List<System.Object> parmers = null) where T : UIPanelBehaviour
    {
        UIPanelBehaviour panel = null;
        string typeName = typeof(T).Name;
        GameObject panelGameObject = null;
        if(m_allPanels.ContainsKey(typeName) && m_allPanels[typeName] != null)
        {
            panelGameObject = GetPanelObj(typeName);
            SetPanelParent(type,panelGameObject);
            panel = m_allPanels[typeName];
            panel.Reactive();
        }
        else
        {
            panelGameObject = GetPanelObj(typeName);
            SetPanelParent(type,panelGameObject);
            panel = Utility.RequireComponent<T>(panelGameObject);
            panel.Show(parmers);

            if(!m_allPanels.ContainsKey(typeName))
                m_allPanels.Add(typeName,panel);
            else
            {
                m_allPanels[typeName] = panel;
            }
            if(!m_allPanelObj.ContainsKey(typeName))
                m_allPanelObj.Add(typeName,panelGameObject);
            else
            {
                m_allPanelObj[typeName] = panelGameObject;
            }
        }
        //   SetPanelParent(type,panelGameObject);
        //  panel.Show(parmers);
        return panel as T;
    }

    /// <summary>
    /// 隐藏物体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="needDestroy">是否需要销毁物体</param>
    /// <param name="needRemoveComponent">是否需要移除组件</param>
    public void Hide<T>(bool needDestroy = true,bool needRemoveComponent = false)
        where T : UIPanelBehaviour
    {
        string typeName = typeof(T).Name;
        if(m_allPanels.ContainsKey(typeName))
        {
            m_allPanels[typeName].Hide();
            if(needDestroy)
            {
                Object.DestroyImmediate(m_allPanelObj[typeName]);
                //Utility.Destroy(m_allPanelObj[typeName]);
                m_allPanels.Remove(typeName);
                m_allPanelObj.Remove(typeName);
            }
            else
            {
                if(needRemoveComponent)
                {
                    UIPanelBehaviour p = m_allPanelObj[typeName].GetComponent<UIPanelBehaviour>();
                    Object.DestroyImmediate(p);
                    m_allPanels.Remove(typeName);
                }
            }
        }
    }


    public void RemoveKey<T>() where T : UIPanelBehaviour
    {
        string typeName = typeof(T).Name;
        if(m_allPanels.ContainsKey(typeName))
        {
            m_allPanels[typeName].Hide(true);
            m_allPanels.Remove(typeName);
            m_allPanelObj.Remove(typeName);

            // Debug.LogError("destroy:  " + typeName);
        }
    }

    public T GetUiPanelBehaviour<T>() where T : UIPanelBehaviour
    {
        string key = typeof(T).Name;
        if(m_allPanels.ContainsKey(key))
        {
            return m_allPanels[key] as T;
        }
        return null;
    }


    public void DestroyAllPanelNotContain(List<string> list)
    {
        List<string> keys = new List<string>();
        keys.AddRange(m_allPanels.Keys);
        for(int i = 0; i < keys.Count; i++)
        {
            if(list != null && list.Contains(keys[i]))
            {
                continue;
            }
            m_allPanels.Remove(keys[i]);
        }

        List<string> objKeys = new List<string>();
        objKeys.AddRange(m_allPanelObj.Keys);
        for(int i = 0; i < objKeys.Count; i++)
        {
            if(list != null && !list.Contains(objKeys[i]))
            {
                continue;
            }
            Object.DestroyImmediate(m_allPanelObj[objKeys[i]]);
            m_allPanelObj.Remove(objKeys[i]);
        }
    }
}
