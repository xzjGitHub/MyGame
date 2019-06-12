
//--------------------------------------------------------------
//Creator： xzj
//Data：    5/31/2019
//Note:     
//--------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对话
/// </summary>
public class Dia: MonoBehaviour
{
    private DiaNpc m_left;
    private DiaNpc m_right;
    private DiaNpc m_center;

    private DiaInfo m_diaInfo;

    /// <summary>
    /// 初始化组件
    /// </summary>
    /// <param name="action">点击的回调 需要传</param>
    public void InitComponent(Action action)
    {
        m_left = Utility.RequireComponent<DiaNpc>(transform.Find("LeftNpc").gameObject);
        m_right = Utility.RequireComponent<DiaNpc>(transform.Find("RightNpc").gameObject);
        m_center = Utility.RequireComponent<DiaNpc>(transform.Find("CenterNpc").gameObject);

        m_left.InitComponent(action);
        m_right.InitComponent(action);
        m_center.InitComponent(action);

        HideAllNpc();
    }

    /// <summary>
    /// 设置对话的内容
    /// </summary>
    /// <param name="diaId"></param>
    public void SetDiaInfo(int diaId)
    {
        if(m_diaInfo == null)
            m_diaInfo = new DiaInfo(diaId);
        else
            m_diaInfo.UpdateInfo(diaId);


        List<List<string>> list = m_diaInfo.GetAllChat();
        m_left.InitInfo(list[0]);
        m_right.InitInfo(list[1]);
        m_center.InitInfo(list[2]);
    }

    /// <summary>
    /// 对话是否结束
    /// </summary>
    /// <returns></returns>
    public bool DiaIsEnd()
    {
        return m_diaInfo.IsLast();
    }

    /// <summary>
    /// 更新对话
    /// </summary>
    public void UpdateInfo()
    {
        if(m_diaInfo == null)
        {
            Debug.LogError("m_diaInfo is null,please check!!");
            return;
        }

        DiaDetial info = m_diaInfo.GetDiaInfo();
        if(info == null)
        {
            Debug.LogError("info is null,please check!!");
            return;
        }

        UpdateNpcShow(info.Pos);
        bool isLast = m_diaInfo.IsLast();
        switch(info.Pos)
        {
            case DiaNpcType.Left:
                m_left.UpdateTextInfo(info.NpcIconName);
                m_left.UpdateTagShow(m_left.CheckAllShowEnd() && isLast);
                break;
            case DiaNpcType.Right:
                m_right.UpdateTextInfo(info.NpcIconName);
                m_right.UpdateTagShow(m_right.CheckAllShowEnd() && isLast);
                break;
            case DiaNpcType.Center:
                m_center.UpdateTextInfo(info.NpcIconName);
                m_left.UpdateTagShow(m_center.CheckAllShowEnd() && isLast);
                break;
            default:
                break;
        }
    }

    public void HideAllNpc()
    {
        UpdateNpcShow(DiaNpcType.None);
    }

    private void UpdateNpcShow(DiaNpcType npctype)
    {
        m_left.gameObject.SetActive(npctype == DiaNpcType.Left);
        m_right.gameObject.SetActive(npctype == DiaNpcType.Right);
        m_center.gameObject.SetActive(npctype == DiaNpcType.Center);
    }
}
