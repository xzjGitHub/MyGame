﻿using System.Collections;
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

        private ContentSizeFitter m_sizeFitter;
        private CoroutineUtil m_cor;

        private void OnEnable()
        {
            ControllerCenter.Instance.BarrackController.ExpChange += OnExpChange;
            //UpdatePosInfo();
        }

        private void OnDisable()
        {
            ControllerCenter.Instance.BarrackController.ExpChange -= OnExpChange;
            if(m_cor != null)
            {
                if(m_cor.Running)
                {
                    m_cor.Stop();
                }
            }
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

            m_sizeFitter = transform.Find("Scroll/Content").GetComponent<ContentSizeFitter>();
        }

        public void UpdateInfo(CharAttribute attr)
        {
            if(m_char != null && m_char.charID == attr.charID)
                return;
            m_char = attr;
            Free();
            UpdateLevelInfo(attr);
            //   m_attr.UpdateInfo(attr);
            StartCoroutine(m_attr.UpdateInfo(attr));
            // m_baseSkill.UpdateInfo(CharSystem.Instance.GetCharShowActiveSkill(attr),attr);
            StartCoroutine(m_baseSkill.UpdateInfo(CharSystem.Instance.GetCharShowActiveSkill(attr),attr));
            m_passiveSkill.UpdateInfo(attr);

            //UpdatePosInfo();
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
            PrefabPool.Instance.Free(StringDefine.ObjectPooItemKey.SkillItem);
            m_attr.Free();
        }

        private void UpdatePosInfo()
        {
            if(m_cor != null)
            {
                if(m_cor.Running)
                {
                    m_cor.Stop();
                }
            }
            m_cor = new CoroutineUtil(UpdatePos(),false);
            m_cor.Start();
        }

        private IEnumerator UpdatePos()
        {
            yield return null;
            m_sizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;

            m_sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
    }
}
