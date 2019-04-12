using UnityEngine;
using UnityEngine.UI;

public class SkillTipPanel: UIPanelBehaviour
{
    private Text m_skillName;
    private Text m_skillDes;
    private Text m_coolDown;
    private Text m_initialCoolDown;
    private Transform m_target;
    private Canvas m_canvas;

    private bool m_hasInit = false;
    private string m_info = "{0}回合";

    private void InitCompont()
    {
        m_skillName = transform.Find("Parent/Name").GetComponent<Text>();
        m_skillDes = transform.Find("Parent/Des2").GetComponent<Text>();
        m_coolDown = transform.Find("Parent/Cool/Num").GetComponent<Text>();
        m_initialCoolDown = transform.Find("Parent/InitialCooldown/Num").GetComponent<Text>();
        m_target = transform.Find("Parent");
        m_canvas = transform.parent.GetComponent<Canvas>();
    }

    public void UpdateInfo(int skillId,CharAttribute charAttr)
    {
        if(!m_hasInit)
        {
            InitCompont();
            m_hasInit = true;
        }

        Combatskill_template skill = Combatskill_templateConfig.GetCombatskill_template(skillId);
        if(skill != null)
        {
            m_skillName.text = skill.skillName;
            m_coolDown.text = string.Format(m_info,skill.Cooldown>0?skill.Cooldown:0);
            m_initialCoolDown.text = string.Format(m_info,skill.initialCooldown>0 ? skill.initialCooldown : 0);
            m_skillDes.text = SkillDesEx.GetDes(skillId,charAttr);
        }
    }

    public void UpdatePos(GameObject currentClickObj)
    {
        // TipPanelPosUtil.UpdatePanelPos(m_canvasScaler,transform,m_target,currentClickObj);

        TipPanelPosUtil.UpdatePanelPos(m_canvas,m_target,currentClickObj);
    }

}
