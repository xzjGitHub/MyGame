using UnityEngine;
using UnityEngine.UI;

namespace Char.View
{
    public class PassiveSkill: MonoBehaviour
    {
        private Text m_xg;
        private Text m_passiveSkillDes;
        private Text m_commander;
        private Text m_des1;
        private Text m_des2;
        private Text m_des3;
        private Text m_passName;

        private GameObject m_suo1;
        private GameObject m_suo2;
       // private GameObject m_suo3;


       // private string m_info = "<color=#FAC81EFF>{0}:</color>\u3000{1}";

        public void InitComponent()
        {
            m_xg = transform.Find("XG/Des").GetComponent<Text>();
            m_passiveSkillDes = transform.Find("GameObject/Pass").GetComponent<Text>();
            m_passName = transform.Find("GameObject/Pass/Text").GetComponent<Text>();
            m_commander = transform.Find("GameObject/Commander").GetComponent<Text>();
            m_des1 = transform.Find("GameObject/Des1").GetComponent<Text>();
            m_des2 = transform.Find("GameObject/Des2").GetComponent<Text>();
            m_des3 = transform.Find("GameObject/Des3").GetComponent<Text>();

            m_suo1 = transform.Find("GameObject/Des1/Image").gameObject;
            m_suo2 = transform.Find("GameObject/Des2/Image").gameObject;
            //m_suo3 = transform.Find("GameObject/Des3/Image").gameObject;
        }

       // string space = "\u00A0";


        private string GetSpace(string s)
        {
         //   Debug.LogError("length: " + s.Length);
            switch(s.Length)
            {
                case 5:
                    // return "";
                    return "\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0";
                case 4:
                    //return "";
                    return "\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0";
                case 3:
                    return "\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0";
                case 2:
                    return "\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0";
                default:
                    return "\u00A0";

            }
        }

        public void UpdateInfo(CharAttribute attr)
        {
            Personality_template per = Personality_templateConfig.GetTemplate(attr.AttitudeID);
            m_xg.text = per.personalityName;

            PassiveSkill_template pass = PassiveSkill_templateConfig.GetPassiveSkill_Template(attr.PersonalityAddPassiveSkill);
            m_passName.text = pass.skillName + "\u00A0:";
            m_passiveSkillDes.text = GetSpace(pass.skillName) + pass.description;
            //m_passName.text = "慢条" + "\u00A0:";
            // m_passiveSkillDes.text = GetSpace("慢条") + pass.description;

            m_commander.gameObject.SetActive(attr.IsCommander);
            if(attr.IsCommander)
            {
                PowerUp_template power = PowerUp_templateConfig.GeTemplate(attr.PowerUpIDs[0]);
                m_commander.text = "\u00A0\u00A0\u00A0" + power.description;
            }

            Combatskill_template skill = Combatskill_templateConfig.GetCombatskill_template(attr.char_template.commonSkillList);
            if(skill == null)
                return;
            m_suo1.SetActive(attr.charLevel < 15);
            if(attr.charLevel < 15)
            {
                m_des1.text = "\u00A0\u00A0\u00A0\u00A0【\u00A0Lv.15\u00A0】\u00A0" + skill.skillDescription2;
            }
            else
            {
                m_des1.text = skill.skillDescription2;
            }

            m_des2.gameObject.SetActive(attr.IsCommander);
            if(attr.IsCommander)
            {
                PowerUp_template power = PowerUp_templateConfig.GeTemplate(attr.PowerUpIDs[1]);

                m_suo2.SetActive(attr.charLevel < 30);
                if(attr.charLevel < 15)
                {
                    m_des2.text = "\u00A0\u00A0\u00A0\u00A0【\u00A0Lv.30\u00A0】\u00A0" + power.description;
                }
                else
                {
                    m_des1.text = power.description;
                }
            }

            m_suo2.SetActive(attr.charLevel < 30);
            if(attr.IsCommander)
            {
                PowerUp_template power = PowerUp_templateConfig.GeTemplate(attr.PowerUpIDs[2]);

                if(attr.charLevel < 30)
                {
                    m_des3.text = "\u00A0\u00A0\u00A0\u00A0【\u00A0Lv.30\u00A0】\u00A0" + power.description;
                }
                else
                {
                    m_des3.text = power.description;
                }
            }
            else
            {
                PowerUp_template power = PowerUp_templateConfig.GeTemplate(attr.PowerUpIDs[0]);
                if(attr.charLevel < 30)
                {
                    m_des3.text = "\u00A0\u00A0\u00A0\u00A0【\u00A0Lv.30\u00A0】\u00A0" + power.description;
                }
                else
                {
                    m_des3.text = power.description;
                }
            }
        }
    }
}
