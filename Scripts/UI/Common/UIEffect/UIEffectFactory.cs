using System.Collections.Generic;
using UnityEngine;

public class UIEffectFactory: Singleton<UIEffectFactory>
{

    private GameObject m_poolParent;

    private Dictionary<string,GameObject> m_effDict;

    public List<string> m_preLoadList;

    private UIEffectFactory()
    {
        m_effDict = new Dictionary<string,GameObject>();
        m_poolParent = new GameObject("[UI_Effect_Pool]");

        m_preLoadList = new List<string>()
        {
            StringDefine.UIEffectNameDefine.BarrackEff,
            StringDefine.UIEffectNameDefine.CollegeEff,
            StringDefine.UIEffectNameDefine.CoreEff,
            StringDefine.UIEffectNameDefine.HallEff,
            StringDefine.UIEffectNameDefine.ShopEff,
            StringDefine.UIEffectNameDefine.WorkShopEff
        };
    }

    public GameObject GetEffectObj(string effectName,bool needCache = true)
    {
        if(m_effDict.ContainsKey(effectName))
        {
            return m_effDict[effectName];
        }
        GameObject effect = ResourceLoadUtil.LoadUIEffect(effectName);
        if(needCache)
        {
            m_effDict[effectName] = effect;
        }
        return effect;
    }

    public void FreeEffect(string effectName,bool needDestroy = false)
    {
        if(m_effDict.ContainsKey(effectName))
        {
            if(needDestroy)
            {
                GameObject.DestroyImmediate(m_effDict[effectName]);
                m_effDict.Remove(effectName);
            }
            else
            {
                Utility.SetParent(m_effDict[effectName],m_poolParent,false);
            }
        }
    }

    public void FreeAll(bool needDestroy)
    {
        List<string> list = new List<string>();
        list.AddRange(m_effDict.Keys);

        for(int i = 0; i < list.Count; i++)
        {
            GameObject.DestroyImmediate(m_effDict[list[i]]);
        }
        m_effDict.Clear();
    }

    public void AddEffObj(string name,GameObject obj)
    {
        if(m_effDict.ContainsKey(name))
            return;
        m_effDict.Add(name,obj);
        Utility.SetParent(obj,m_poolParent,false);
    }

    public void PreLoad(string name)
    {
        if(m_effDict.ContainsKey(name))
            return;
        GameObject effect = ResourceLoadUtil.LoadUIEffect(name);
        m_effDict[name] = effect;
        Utility.SetParent(effect,m_poolParent,false);
    }
}

