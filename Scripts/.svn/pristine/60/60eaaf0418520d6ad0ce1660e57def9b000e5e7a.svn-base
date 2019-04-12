using UnityEngine;
using UnityEngine.UI;

namespace WorkShop.EquipResearch.View
{
    public class ViewItem: MonoBehaviour
    {
        private Text m_level;
        private Text m_level1;
        private Text m_exp;
        private Image m_image;

        public void InitComponent(string name)
        {
            m_image = transform.Find("Slider/Slider").GetComponent<Image>();
            m_level = transform.Find("Level").GetComponent<Text>();
            m_level1 = transform.Find("Level1").GetComponent<Text>();
            m_exp = transform.Find("Slider/Exp").GetComponent<Text>();
            transform.Find("Name").GetComponent<Text>().text = name;
        }

        public void UpdateInfo(int type)
        {
            int level = WorkshopSystem.Instance.GetResearchLevel(type);
            m_level.text = "Lv."+level;

            int minLevel = WorkshopSystem.Instance.GetEquipMakeMinLevel(type);
            Research_lvup lvup = Research_lvupConfig.GetResearch_lvup(level);
            int maxLevel = lvup.addItemLevel.Count > 1 ? lvup.addItemLevel[1] : 0;
            m_level1.text = (minLevel + lvup.addItemLevel.Count > 0 ?
                lvup.addItemLevel[0] : 0) + "-" + maxLevel;

            Research_lvup lv = Research_lvupConfig.GetResearch_lvup(level);
            int maxResLevel = Research_lvupConfig.GetMaxLevel();
            if(level >= maxResLevel)
            {
                m_exp.text = "经验:" + lv.lvupExp + "/" + lv.lvupExp;
                m_image.fillAmount = 1;
            }
            else
            {
                float currentExp = WorkshopSystem.Instance.GetExp(type);
                m_exp.text = "经验:" + currentExp + "/" + lv.lvupExp;
                m_image.fillAmount = currentExp / lv.lvupExp;
            }
        }
    }
}
