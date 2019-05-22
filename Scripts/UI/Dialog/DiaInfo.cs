﻿using System.Collections.Generic;

public enum DiaNpcType
{
    Left = 1,
    Right,
    Center,
    None = 100
}

public class DiaDetial
{
    private string m_npcIconName;
    public string NpcIconName { get { return m_npcIconName; } }

    private DiaNpcType m_pos;
    public DiaNpcType Pos { get { return m_pos; } }

    private string m_content;
    public string Content { get { return m_content; } }

    public DiaDetial(int id)
    {
        Text_template text = Text_templateConfig.GetText_config(id);
        m_npcIconName = text.charIcon;
        m_pos = (DiaNpcType)text.position;
        m_content = text.text;
    }
}

public class DiaInfo
{
    private int m_currentIndex;
    private List<DiaDetial> m_List;

    public DiaInfo(int diaId)
    {
        m_currentIndex = 0;
        m_List = new List<DiaDetial>();
        InitDia(diaId);
    }

    public List<List<string>> GetAllChat(out string leftNpc,out string rightNpc)
    {
        string leftIcon = "";
        string rightIcon = "";

        List<List<string>> allList = new List<List<string>>();
        List<string> left = new List<string>();
        List<string> right = new List<string>();
        List<string> center = new List<string>();
        for(int i = 0; i < m_List.Count; i++)
        {
            if(m_List[i].Pos == DiaNpcType.Left)
            {
                left.Add(m_List[i].Content);
                leftIcon = m_List[i].NpcIconName;
            }
            else if(m_List[i].Pos == DiaNpcType.Right)
            {
                right.Add(m_List[i].Content);
                rightIcon = m_List[i].NpcIconName;
            }
            else
            {
                center.Add(m_List[i].Content);
            }
        }
        allList.Add(left);
        allList.Add(right);
        allList.Add(center);

        leftNpc = leftIcon;
        rightNpc = rightIcon;
        return allList;
    }

    private void InitDia(int diaId)
    {
        Dialog_template dia = Dialog_templateConfig.GetDialog_template(diaId);
        for(int i = 0; i < dia.textSet.Count; i++)
        {
            m_List.Add(new DiaDetial(dia.textSet[i]));
        }
    }

    public DiaDetial GetDiaInfo()
    {
        if(m_currentIndex >= m_List.Count)
            return null;
        DiaDetial info = m_List[m_currentIndex];
        m_currentIndex++;
        return info;
    }
}
