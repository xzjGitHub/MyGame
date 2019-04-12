﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TipManager: Singleton<TipManager>
{

    private Queue<string> m_tipQuene = new Queue<string>();
    private Queue<GameObject> m_tipPool = new Queue<GameObject>();

    private Transform m_tipParent;

    private TipManager()
    {
        m_tipParent = new GameObject("TipPool").transform;
        Utility.SetParent(m_tipParent.gameObject,UIPanelManager.Instance.PopUiParent);
    }

    public void ShowTip(string message)
    {
        m_tipQuene.Enqueue(message);
    }

    public void FreeTip(GameObject tip)
    {
        tip.SetActive(false);
        m_tipPool.Enqueue(tip);
    }

    private GameObject GetTip()
    {
        GameObject tip = null;
        if(m_tipPool.Count > 0)
        {
            tip = m_tipPool.Dequeue();
        }
        else
        {
            tip = ResourceLoadUtil.LoadPrefab(StringDefine.PrefabNameDefine.TipPrefab);
        }
        tip.SetActive(true);
        return tip;
    }

    public void Tick()
    {
        if(m_tipQuene.Count > 0)
        {
            GameObject tip = GetTip();
            Utility.SetParent(tip,m_tipParent,true,Vector3.one,new Vector3(0,180,0));
            tip.transform.SetAsLastSibling();

            Utility.RequireComponent<Tip>(tip).SetInfo(m_tipQuene.Dequeue());

            tip.transform.DOLocalMoveY(425,3.5f);
        }
    }
}
