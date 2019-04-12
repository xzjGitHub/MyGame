using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PreLoadUti: Singleton<PreLoadUti>
{
    private PreLoadUti()
    {
    }

    public IEnumerator PreLoad()
    {
        List<string> panelList = GetPreLoadPanelList();
        for(int i = 0; i < panelList.Count; i++)
        {
            GameObject obj = UIPanelManager.Instance.GetPanelObj(panelList[i]); 
            UIPanelManager.Instance.AddPanelObj(panelList[i],obj);
            GameObjectPool.Instance.AddChild(obj);
            yield return null;
        }

        List<string> prefabList = GetPreLoadPrefabList();
        for(int i = 0; i < prefabList.Count; i++)
        {
            GameObjectPool.Instance.GetObjectByPrefabPath(prefabList[i],prefabList[i]);
            GameObjectPool.Instance.FreePool(prefabList[i]);
            yield return null;
        }

        List<string> effList = GetPreLoadEffList();
        for(int i = 0; i < effList.Count; i++)
        {
            UIEffectFactory.Instance.PreLoad(effList[i]);
            yield return null;
        }

        SceneManager.Instance.PreLoadEnd();
    }
}

