using System.Collections.Generic;

public class DiaDetial
{
    private string m_npcIconName;
    public string NpcIconName { get { return m_npcIconName; } }

    private int m_pos;
    public int Pos { get { return m_pos; } }

    private string m_content;
    public string Content { get { return m_content; } }

    public DiaDetial(int id)
    {
        Text_template text = Text_templateConfig.GetText_config(id);
        m_npcIconName = text.charIcon;
        m_pos = text.position;
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
        return m_List[m_currentIndex];
    }
}
