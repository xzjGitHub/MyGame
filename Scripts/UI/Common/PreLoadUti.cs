﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PreLoadUti
{
    public static IEnumerator PreLoad(Action action)
    {
        //List<string> panelList = GetPreLoadPanelList();
        //for(int i = 0; i < panelList.Count; i++)
        //{
        //    GameObject obj = UIPanelManager.Instance.GetPanelObj(panelList[i]); 
        //    UIPanelManager.Instance.AddPanelObj(panelList[i],obj);
        //    PrefabPool.Instance.AddChild(obj);
        //    yield return null;
        //}
        yield return null;
        action();
    }
}

