﻿using System;
using College.Research.Data;
using UnityEngine;
using UnityEngine.UI;

namespace College.Research.View
{
    public class EnchanteResearchItem: MonoBehaviour
    {
        private GameObject m_end;
        private GameObject m_select;

        private Image m_icon;
        //private Image m_quility;
        private Image m_slider;
        private Text m_progress;
        private Text m_exp;

        private bool m_hasInit;

        private string m_id;

        private Action<string> m_clickAction;

        private void InitComponent()
        {
            m_end = transform.Find("End").gameObject;
            m_end.SetActive(false);
            m_select = transform.Find("Select").gameObject;
            m_select.SetActive(false);

            m_icon = transform.Find("Equip/Icon").GetComponent<Image>();
            //  m_quility = transform.Find("Equip/Quility").GetComponent<Image>();
            m_slider = transform.Find("Slider/Slider").GetComponent<Image>();
            m_progress = transform.Find("Slider/Progress").GetComponent<Text>();
            m_exp = transform.Find("Slider/Exp").GetComponent<Text>();

            Utility.AddButtonListener(transform.Find("Btn"),Click);

            m_hasInit = true;
        }

        public void InitInfo(ResearchingInfo info,Action<string> clickAction)
        {
            m_id = info.Id;
            m_clickAction = clickAction;
            if(!m_hasInit)
            {
                InitComponent();
            }
            m_end.SetActive(false);
            m_select.SetActive(false);
            Item_instance item = Item_instanceConfig.GetItemInstance(info.Data.instanceID);
            m_icon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemIcon,item.itemIcon.Count > 0 ? item.itemIcon[0] : "");
            //  m_quility.sprite = ResourceLoadUtil.LoadSprite(ResourceType.ItemQuility,info.Data.itemQuality.ToString());

            UpdateSlider(info.HaveUseTime / info.NeedTime);
            UpdateEndShow(info.HaveUseTime >= info.NeedTime);
            UpdateExp((int)info.Exp);
        }


        private void UpdateSlider(float fillAmount)
        {
            m_slider.fillAmount = fillAmount;
        }

        private void UpdateExp(int exp)
        {
            m_exp.text = string.Format("+{0}",exp);
        }

        public void UpdateSlider(float allTime,int haveUseTime,int exp)
        {
            float value = haveUseTime / allTime;
            UpdateSlider(value);
            m_progress.text = ((int)(value * 100)) + "%";
            UpdateExp(exp);
        }


        public void UpdateEndShow(bool show)
        {
            m_end.SetActive(show);
            m_progress.gameObject.SetActive(!show);
        }

        public void UpdateSelectShow(bool show)
        {
            m_select.SetActive(show);
        }

        private void Click()
        {
            if(m_clickAction != null)
            {
                m_clickAction(m_id);
            }
        }
    }
}
