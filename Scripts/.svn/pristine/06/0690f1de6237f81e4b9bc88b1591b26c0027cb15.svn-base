using System;
using UnityEngine;
using UnityEngine.UI;

namespace Barrack.View
{
    public class CharItem: MonoBehaviour
    {
        private Text m_level;
        private Image m_icon;

        private GameObject m_select;
        private GameObject m_occupy;
        private GameObject m_removeBtn;

        private GameObject m_training;
        private GameObject m_gold;
        private GameObject m_enchant;
        private GameObject m_research;

        private GameObject m_manaNotEnough;

        private Action<CharAttribute> m_handleClick;
        private Action<CharAttribute> m_sysClick;
        private CharAttribute m_charAttr;

        private bool m_initComponent;

        private void InitComponent()
        {
            m_select = transform.Find("Info/Select").gameObject;
            m_select.SetActive(false);
            m_occupy = transform.Find("Occupy").gameObject;
            m_occupy.SetActive(false);

            m_training = transform.Find("Train").gameObject;
            m_training.SetActive(false);

            m_gold = transform.Find("Gold").gameObject;
            m_gold.SetActive(false);

            m_enchant = transform.Find("Enchant").gameObject;
            m_enchant.SetActive(false);

            m_research = transform.Find("Research").gameObject;
            m_research.SetActive(false);

            m_manaNotEnough = transform.Find("ManaNotEnough").gameObject;
            m_manaNotEnough.SetActive(false);

            m_removeBtn = transform.Find("Remove").gameObject;

            m_level = transform.Find("Info/Level").GetComponent<Text>();
            m_icon = transform.Find("Info/Icon").GetComponent<Image>();

            Utility.AddButtonListener(m_icon.transform,OnClick);
          //  Utility.AddButtonListener(m_removeBtn.transform,ClickRemove);
        }

        public void InitInfo(CharAttribute info,Action<CharAttribute> handleClick,
            Action<CharAttribute> sysClick,bool clickBtn = false,bool showRemoveBtn=false)
        {
            m_charAttr = info;
            m_handleClick = handleClick;
            m_sysClick = sysClick;
            InitInfo();
            UpdateRoveShowStatus(showRemoveBtn);
            //if(clickBtn)
            //{
            //    OnClick();
            //}
        }


        private void InitInfo()
        {
            if(!m_initComponent)
            {
                InitComponent();
                m_initComponent = true;
            }

            m_level.text = "Lv." + m_charAttr.charLevel;
            m_icon.sprite = ResourceLoadUtil.LoadSprite(ResourceType.CharHeadIcon,m_charAttr.char_template.HeadIcon);
            UpdateCharStatus(m_charAttr.charID,m_charAttr.Status);
        }

        public void UpdateCharStatus(int charId,CharStatus charStatus)
        {
            if(charId == m_charAttr.charID)
            {
                m_training.SetActive(charStatus == CharStatus.Train);
                m_gold.SetActive(charStatus == CharStatus.GoldProduce);
                m_research.SetActive(charStatus == CharStatus.EquipResearch);
                m_enchant.SetActive(charStatus == CharStatus.EnchantResearch);
                m_select.SetActive(false);
            }
        }

        public void UpdateSelectInfo(bool select)
        {
            m_select.SetActive(select);
        }


        public void OnClick()
        {
            if(m_handleClick != null)
            {
                m_handleClick(m_charAttr);
            }
        }

        public void SysClick()
        {
            if(m_sysClick != null)
            {
                m_sysClick(m_charAttr);
            }
        }

        public void UpdateManaNotEnoughShowInfo(bool isWorking)
        {
            if(m_charAttr.Status == CharStatus.GoldProduce)
            {
                m_gold.SetActive(isWorking);
            }
            if(m_charAttr.Status == CharStatus.EnchantResearch)
            {
                m_enchant.SetActive(isWorking);
            }
            if(m_charAttr.Status == CharStatus.EquipResearch)
            {
                m_research.SetActive(isWorking);
            }
            m_manaNotEnough.SetActive(!isWorking);
        }

        public void UpdateLevel(int level)
        {
            m_level.text = "Lv." + level;
        }

        public void UpdateRoveShowStatus(bool show)
        {
            if(m_removeBtn != null)
                m_removeBtn.SetActive(show);
        }
    }
}
