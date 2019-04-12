using Core.Controller;
using Core.Data;
using UnityEngine.UI;
using UnityEngine;

namespace Core.View
{
    public class CoreInfoPanel: MonoBehaviour
    {
        private Text m_level;
        private Text m_manaCL;
        private Text m_xy;

        private Text m_corePower;
        private Text m_manaGet;
        private Text m_power;

        public void InitComponent()
        {
            m_level = transform.Find("Level/Level").GetComponent<Text>();
            m_manaCL = transform.Find("Mana/Level").GetComponent<Text>();
            m_xy = transform.Find("XiaoNu/Level").GetComponent<Text>();
            m_corePower = transform.Find("CorePower/Level").GetComponent<Text>();
            m_manaGet = transform.Find("ManaGet/Level").GetComponent<Text>();
            m_power = transform.Find("Power/Level").GetComponent<Text>();

            Utility.AddButtonListener(transform.Find("Mask"), () => gameObject.SetActive(false));

            CoreEventCenter.Instance.ShowCoreInfo += ShowCoreInfo;

            //Building_template bui = Building_templateConfig.GetBuildingTemplate((int) BuildingType.Core);
            //Text diaInfo = transform.Find("Dia/Text").GetComponent<Text>();
            //diaInfo.text = bui.buildingInfo;

            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            CoreEventCenter.Instance.ShowCoreInfo -= ShowCoreInfo;
        }


        private void ShowCoreInfo()
        {
            gameObject.SetActive(true);
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            int coreLevel = CoreSystem.Instance.GetLevel();

            Core_lvup core_Lvup = Core_lvupConfig.GetCore_lvup(coreLevel);

            m_level.text = "Lv." + coreLevel;
            m_manaCL.text = core_Lvup.resourceOutPutBonus*100 + "%";
            m_xy.text = core_Lvup.buildingEfficiency*100 + "%";

            float currentPower = CoreSystem.Instance.GetPower();
            m_corePower.text = ((int)(currentPower*100)/100f).ToString();

            float manaGet = BuildingAttribute.Building.GetbaseRewardValue(
                Building_templateConfig.GetBuildingTemplate((int) BuildingType.Core),
                currentPower);
            m_manaGet.text =((int) (manaGet*100)/100f).ToString();
            m_power.text = BuildingAttribute.Building.GetcombatBonus().ToString();
        }
    }
}
