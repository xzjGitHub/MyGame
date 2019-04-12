using College.Research.Controller;
using College.Research.Data;
using UnityEngine;
using UnityEngine.UI;

namespace College.Research.View
{
    public class EnchantEndTipPanel:MonoBehaviour
    {

        private GameObject m_levelObj;
        private GameObject m_effObj;

        private Image m_slider;
        private Text m_type;
        private Text m_addExp;
        private Text m_exp;
        private Text m_beforeLevel;
        private Text m_nowLevel;
        private Text m_beforeMinLevel;
        private Text m_nowMinLevel;
        private Text m_beforeMaxLevel;
        private Text m_nowMaxLevel;

        private Text m_effectName;
        private Text m_effDes;

        private ResearchingInfo m_info;

        public void InitComponent()
        {
            m_levelObj = transform.Find("Center/Level").gameObject;
            m_effObj = transform.Find("UnLockEff").gameObject;

            m_slider = transform.Find("Center/Slider/Slider").GetComponent<Image>();
            m_type = transform.Find("Center/ResType/Type").GetComponent<Text>();
            m_addExp = transform.Find("Center/Exp/Exp").GetComponent<Text>();
            m_beforeLevel = transform.Find("Center/Level/ResLevel/BeforeLevel").GetComponent<Text>();
            m_nowLevel = transform.Find("Center/Level/ResLevel/BeforeLevel/Arrow/NowLevel").GetComponent<Text>();
            m_beforeMinLevel = transform.Find("Center/Level/MinLevel/BeforeLevel").GetComponent<Text>();
            m_nowMinLevel = transform.Find("Center/Level/MinLevel/NowLevel").GetComponent<Text>();
            m_beforeMaxLevel = transform.Find("Center/Level/MaxLevel/BeforeLevel").GetComponent<Text>();
            m_nowMaxLevel = transform.Find("Center/Level/MaxLevel/NowLevel").GetComponent<Text>();
            m_exp = transform.Find("Center/Slider/Exp").GetComponent<Text>();
            m_effectName = transform.Find("UnLockEff/Name").GetComponent<Text>();
            m_effDes = transform.Find("UnLockEff/Des").GetComponent<Text>();

            Utility.AddButtonListener(transform.Find("SureBtn/Btn"),ClickSure);
        }

        public void UpdateInfo(ResearchingInfo info)
        {
            m_info = info;

            MR_template rare = MR_templateConfig.GetTemplate(info.Data.instanceID);
            m_type.text = EnchanteResearchController.GetName(rare.enchantType);      
            m_addExp.text = ((int)info.Exp).ToString();

            int nowLevel =(int) ResearchLabSystem.Instance.GetReseachLvel(rare.enchantType);
            bool levelChange = nowLevel - info.Level > 0;
            if(levelChange)
            {
                m_beforeLevel.text = "Lv." + info.Level;
                m_nowLevel.text = "Lv." + nowLevel;
            }

            Research_lvup beforeRes = Research_lvupConfig.GetResearch_lvup(info.Level);
            Research_lvup nowRes = Research_lvupConfig.GetResearch_lvup(nowLevel);
            if(levelChange)
            {
                m_beforeMaxLevel.text = "Lv." + (beforeRes.addEnchantLevel.Count > 1 ?
                    beforeRes.addEnchantLevel[1] : 0);
                m_nowMaxLevel.text = "Lv." + (nowRes.addEnchantLevel.Count > 1 ?
                    beforeRes.addEnchantLevel[1] : 0);
            }

            int nowminLevel = ResearchLabSystem.Instance.GetMinLeve(rare.enchantType);
            if(levelChange)
            {
                m_beforeMinLevel.text = "Lv." + (info.Level +
                    beforeRes.addEnchantLevel.Count > 0 ? beforeRes.addEnchantLevel[0] : 0);
                m_nowMinLevel.text = "Lv." + (nowminLevel +
                    nowRes.addEnchantLevel.Count > 0 ? nowRes.addEnchantLevel[0] : 0);
            }

            float nowExp = ResearchLabSystem.Instance.GetReseachExp(rare.enchantType);
            m_exp.text = "经验：" + nowExp + "/" + nowRes.lvupExp;
            m_slider.fillAmount = nowExp / nowRes.lvupExp;

            m_levelObj.SetActive(levelChange);
            UpdateEffInfo();
        }

        private void UpdateEffInfo()
        {
            m_effObj.SetActive(m_info.EffectId != -1);
            if (m_info.EffectId != -1)
            {
                Enchant_template enchant = Enchant_templateConfig.GetEnchant_Template(m_info.EffectId);
                m_effectName.text = enchant.enchantName;
                m_effDes.text = "^^^";
            }
        }

        private void ClickSure()
        {
            ControllerCenter.Instance.EnchanteResearchController.EndResearch(m_info);
            gameObject.SetActive(false);
        }
    }
}
