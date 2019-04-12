using UnityEngine;
using UnityEngine.UI;

namespace Char.View
{
    public class DetialInfo: MonoBehaviour
    {
        private Text m_exp;
        private Image m_slider;

        private Attr m_attr;
        private SkillInfo m_baseSkill;
        private PassiveSkill m_passiveSkill;
        private SliderPos m_sliderPos;


        private CharAttribute m_char;

        private void OnEnable()
        {
            ControllerCenter.Instance.BarrackController.ExpChange += OnExpChange;
        }

        private void OnDisable()
        {
            ControllerCenter.Instance.BarrackController.ExpChange -= OnExpChange;
        }

        public void InitComponent()
        {
            m_exp = transform.Find("Scroll/Content/Level/Exp/Num").GetComponent<Text>();
            m_slider = transform.Find("Scroll/Content/Level/Exp/Fore").GetComponent<Image>();

            m_attr = Utility.RequireComponent<Attr>(transform.Find("Scroll/Content/Attr").gameObject);
            m_attr.InitComponent();

            m_baseSkill = Utility.RequireComponent<SkillInfo>(transform.Find("Scroll/Content/Skill").gameObject);
            m_baseSkill.InitComponent();

            m_passiveSkill = Utility.RequireComponent<PassiveSkill>(transform.Find("Scroll/Content/PassiveSkill").gameObject);
            m_passiveSkill.InitComponent();

            m_sliderPos = transform.Find("Scroll/Content/Level/Exp/Mask/Eff").GetComponent<SliderPos>();
        }

        public void UpdateInfo(CharAttribute attr)
        {
            m_char = attr;
            Free();
            UpdateLevelInfo(attr);
            m_attr.UpdateInfo(attr);
            m_baseSkill.UpdateInfo(CharSystem.Instance.GetCharShowActiveSkill(attr),attr);
            m_passiveSkill.UpdateInfo(attr);
        }

        private void UpdateLevelInfo(CharAttribute attr)
        {
            m_exp.text = attr.charExp + "/" + (int)attr.char_lvup.levelupExp;
            float value = attr.charExp / attr.char_lvup.levelupExp;
            m_slider.fillAmount = value;

            if(value != 0)
            {
                m_sliderPos.UpdatePos();
            }
        }

        private void OnExpChange(int id)
        {
            if(id == m_char.charID)
            {
                UpdateLevelInfo(m_char);
            }
        }

        public void Free()
        {
            GameObjectPool.Instance.FreePool(StringDefine.ObjectPooItemKey.SkillItem);
            m_attr.Free();
        }
    }
}
