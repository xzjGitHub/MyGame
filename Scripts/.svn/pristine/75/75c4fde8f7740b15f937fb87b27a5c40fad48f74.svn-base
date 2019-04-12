using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[Serializable]
public class TogGroupInfo
{
    /// <summary>
    /// 面板
    /// </summary>
    [SerializeField]
    private GameObject panel = null;
    public GameObject Panel { get { return panel; } }
}

public class TogGroupControl: MonoBehaviour
{
    [SerializeField]
    private List<TogGroupInfo> all;  //要确保拖拽赋值的时候的顺序 

    /// <summary>
    /// 记录当前选择的 便于下一次更新 避免遍历列表
    /// </summary>
    private TogGroupInfo m_currentShowInfo;

    private void Awake()
    {
        for (int i = 0; i < all.Count; i++)
        {
            all[i].Panel.SetActive(false);
        }
    }

    /// <summary>
    /// Tog点击更新
    /// </summary>
    /// <param name="index">索引</param>
    public void UpdateInfo(int index)
    {
        if(m_currentShowInfo != null)
        {
            m_currentShowInfo.Panel.SetActive(false);
        }
        m_currentShowInfo = all[index];
        m_currentShowInfo.Panel.SetActive(true);
    }
}

