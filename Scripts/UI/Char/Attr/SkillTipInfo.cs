using EventCenter;
using UnityEngine;
using UnityEngine.UI;


public class SkillTipInfo: MonoBehaviour
{
    private Text m_skillName;
    private Text m_cost;
    private Text m_coolDown;
    private Text m_initialCoolDown;
    private Text m_skillDes;
    private Text m_jili;

    private bool m_hasInit = false;
    private string m_info = "{0}回合";

    private string m_format = "<color=#FAB41EFF>【{0}】</color>{1}";


    private void OnEnable()
    {
        EventManager.Instance.RegEventListener(EventSystemType.UI,EventTypeNameDefine.HideCharAttrTip,HideObj);
    }

    private void OnDisable()
    {
        EventManager.Instance.UnRegEventListener(EventSystemType.UI,EventTypeNameDefine.HideCharAttrTip,HideObj);
    }

    private void InitCompont()
    {
        m_skillName = transform.Find("Name").GetComponent<Text>();
        m_cost = transform.Find("Cost/Num").GetComponent<Text>();
        m_coolDown = transform.Find("Cool/Num").GetComponent<Text>();
        m_initialCoolDown = transform.Find("InitialCooldown/Num").GetComponent<Text>();
        m_jili = transform.Find("Jili/Text").GetComponent<Text>();
        m_skillDes = transform.Find("Des2").GetComponent<Text>();

        Utility.AddButtonListener(transform.parent.Find("Mask"),()=> transform.parent.gameObject.SetActive(false));
    }


    public void UpdateInfo(int skillId,CharAttribute charAttr)
    {
        if(!m_hasInit)
        {
            InitCompont();
            m_hasInit = true;
        }

        Combatskill_template skill = Combatskill_templateConfig.GetCombatskill_template(skillId);
        m_skillName.text = skill.skillName;
        m_cost.text =skill.energyCost.ToString();
        m_coolDown.text = string.Format(m_info,skill.Cooldown > 0 ? skill.Cooldown : 0);
        m_initialCoolDown.text = string.Format(m_info,skill.initialCooldown > 0 ? skill.initialCooldown : 0);
        m_skillDes.text = SkillDesEx.GetDes(skillId,charAttr);

        int newSkillId = skill.alternativeSkill;
        m_jili.gameObject.SetActive(newSkillId != 0);
        if(newSkillId != 0)
        {
            skill = Combatskill_templateConfig.GetCombatskill_template(newSkillId);
            m_jili.text = string.Format(m_format,"激励",skill.skillDescription1);
        }
    }

    private void HideObj()
    {
        if(transform.parent.gameObject.activeSelf)
            transform.parent.gameObject.SetActive(false);
    }

}

