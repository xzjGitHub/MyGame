using UnityEngine;
using UnityEngine.UI;
using WorkShop.EquipResearch.Controller;

namespace WorkShop.EquipResearch.View
{
    public class ResEndTipPanel: MonoBehaviour
    {
        private GameObject m_levelObj;

        private Text m_type;
        private Text m_addExp;
        private Text m_exp;
        private Text m_beforeLevel;
        private Text m_nowLevel;
        private Text m_beforeMinLevel;
        private Text m_nowMinLevel;
        private Text m_beforeMaxLevel;
        private Text m_nowMaxLevel;
        private Image m_slider;

        private EquipResearchInfo m_info;

        public void InitComponent()
        {
            m_levelObj = transform.Find("Center/Level").gameObject;


            m_type = transform.Find("Center/ResType/Type").GetComponent<Text>();
            m_addExp = transform.Find("Center/Exp/Exp").GetComponent<Text>();
            m_beforeLevel = transform.Find("Center/Level/ResLevel/BeforeLevel").GetComponent<Text>();
            m_nowLevel = transform.Find("Center/Level/ResLevel/BeforeLevel/Arrow/NowLevel").GetComponent<Text>();
            m_beforeMinLevel = transform.Find("Center/Level/MinLevel/BeforeLevel").GetComponent<Text>();
            m_nowMinLevel = transform.Find("Center/Level/MinLevel/NowLevel").GetComponent<Text>();
            m_beforeMaxLevel = transform.Find("Center/Level/MaxLevel/BeforeLevel").GetComponent<Text>();
            m_nowMaxLevel = transform.Find("Center/Level/MaxLevel/NowLevel").GetComponent<Text>();
            m_exp = transform.Find("Center/Slider/Exp").GetComponent<Text>();
            m_slider = transform.Find("Center/Slider/Slider").GetComponent<Image>();

            Utility.AddButtonListener(transform.Find("SureBtn/Btn"),ClickSure);
        }

        public void UpdateInfo(EquipResearchInfo info)
        {
            m_info = info;

            EquipAttribute equip = ItemSystem.Instance.GetEquipAttribute(info.EquipId);
            Item_instance item = Item_instanceConfig.GetItemInstance(equip.instanceID);
            Equip_template eq = Equip_templateConfig.GetEquip_template(equip.instanceID);

            m_type.text = EquipResearchController.GetName(eq.REType);
            m_addExp.text = Mathf.FloorToInt(info.Exp).ToString();

            int nowLevel = WorkshopSystem.Instance.GetResearchLevel(eq.REType);
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
                m_beforeMaxLevel.text = "Lv." + (beforeRes.addItemLevel.Count > 1 ?
                    beforeRes.addItemLevel[1] : 0);
                m_nowMaxLevel.text = "Lv." + (nowRes.addItemLevel.Count > 1 ?
                    beforeRes.addItemLevel[1] : 0);
            }

            int nowminLevel = WorkshopSystem.Instance.GetEquipMakeMinLevel(eq.REType);
            if(levelChange)
            {
                m_beforeMinLevel.text = "Lv." + (info.Level +
                    beforeRes.addItemLevel.Count > 0 ? beforeRes.addItemLevel[0] : 0);
                m_nowMinLevel.text = "Lv." + (nowminLevel +
                    nowRes.addItemLevel.Count > 0 ? nowRes.addItemLevel[0] : 0);
            }

            float nowExp = WorkshopSystem.Instance.GetExp(eq.REType);
            m_exp.text = "经验：" + nowExp + "/" + nowRes.lvupExp;
            m_slider.fillAmount = nowExp / nowRes.lvupExp;

            m_levelObj.SetActive(levelChange);
        }

        private void ClickSure()
        {
            ControllerCenter.Instance.EquipResearchController.ResearchEnd(m_info);
            gameObject.SetActive(false);
        }
    }
}
