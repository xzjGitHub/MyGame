using Char.View;
using UnityEngine;
using UnityEngine.UI;


public class DetialInfo3: MonoBehaviour
{
    private Attr1 m_attr;
    private SkillInfo1 m_baseSkill;

  //  private Text m_qui;

    public void InitComponent()
    {
       // m_qui = transform.Find("Scro/Content/Top/Text1").GetComponent<Text>();

        m_attr = Utility.RequireComponent<Attr1>(transform.Find("Scro/Content/Attr").gameObject);
        m_attr.InitComponent();

        m_baseSkill = Utility.RequireComponent<SkillInfo1>(transform.Find("Scro/Content/Skill").gameObject);
        m_baseSkill.InitComponent();
    }

    public void UpdateInfo(CharAttribute attr,bool hasCall)
    {
        m_attr.UpdateInfo(attr,hasCall);
        m_baseSkill.UpdateInfo(CharSystem.Instance.GetCharShowActiveSkill(attr),attr,hasCall);
    }

    public void Free()
    {
        m_attr.Free();
        m_baseSkill.Free();
    }
}