﻿using Char.View;
using UnityEngine;
using UnityEngine.UI;


public class DetialInfo2: MonoBehaviour
{
    private Text m_weijie;
    private Text m_zizhi;
    private Text m_xg;

    private Attr m_attr;
    private SkillInfo m_baseSkill;

    public void InitComponent()
    {
        m_weijie = transform.Find("Scroll/Content/Top/weijie/weijie").GetComponent<Text>();
        m_zizhi = transform.Find("Scroll/Content/Top/zizhi/zizhi").GetComponent<Text>();
        m_xg = transform.Find("Scroll/Content/Top/xg/xg").GetComponent<Text>();

        m_attr = Utility.RequireComponent<Attr>(transform.Find("Scroll/Content/Attr").gameObject);
        m_attr.InitComponent();

        m_baseSkill = Utility.RequireComponent<SkillInfo>(transform.Find("Scroll/Content/Skill").gameObject);
        m_baseSkill.InitComponent();
    }


    public void UpdateInfo(CharAttribute attr,bool isGray)
    {
        Free();
        InitBaseInfo(attr);
        m_attr.UpdateInfo(attr);
        m_baseSkill.UpdateInfo(CharSystem.Instance.GetCharShowActiveSkill(attr),attr);
    }

    private void InitBaseInfo(CharAttribute attr) {
        string des = "【{0}】";
        m_weijie.text = string.Format(des,CharaterUti.GetRankName(attr.CharRank));
        m_zizhi.text =attr.charQuality.ToString();
        Personality_template per = Personality_templateConfig.GetTemplate(attr.AttitudeID);
        m_xg.text =per.personalityName;
    }

    public void Free()
    {
        PrefabPool.Instance.Free(StringDefine.ObjectPooItemKey.SkillItem);
        m_attr.Free();
    }

}