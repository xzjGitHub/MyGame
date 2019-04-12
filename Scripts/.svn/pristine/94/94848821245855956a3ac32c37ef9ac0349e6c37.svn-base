using Core.Controller;
using Core.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Core.View
{
    public class CoreLevelUpInfo: MonoBehaviour
    {

        private Text m_levelBefore;
        private Text m_manaCLBefore;
        private Text m_xyBefore;
        private Text m_corePowerBefore;
        private Text m_manaGetBefore;
        private Text m_powerBefore;

        private Text m_levelNow;
        private Text m_manaCLNow;
        private Text m_xyNow;
        private Text m_corePowerNow;
        private Text m_manaGetNow;
        private Text m_powerNow;

        public void InitComponent()
         {
            m_levelBefore = transform.Find("Level/Before").GetComponent<Text>();
            m_levelNow = transform.Find("Level/Now").GetComponent<Text>();

            m_manaCLBefore = transform.Find("Mana/Before").GetComponent<Text>();
            m_manaCLNow = transform.Find("Mana/Now").GetComponent<Text>();

            m_xyBefore = transform.Find("XiaoNu/Before").GetComponent<Text>();
            m_xyNow = transform.Find("XiaoNu/Now").GetComponent<Text>();

            m_corePowerBefore = transform.Find("CorePower/Before").GetComponent<Text>();
            m_corePowerNow = transform.Find("CorePower/Now").GetComponent<Text>();

            m_manaGetBefore = transform.Find("ManaGet/Before").GetComponent<Text>();
            m_manaGetNow = transform.Find("ManaGet/Now").GetComponent<Text>();

            m_powerBefore = transform.Find("Power/Before").GetComponent<Text>();
            m_powerNow = transform.Find("Power/Now").GetComponent<Text>();

            Utility.AddButtonListener(transform.Find("Mask"),() => gameObject.SetActive(false));

            CoreEventCenter.Instance.CoreChange += UpdateInfo;

            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            CoreEventCenter.Instance.CoreChange -= UpdateInfo;
        }

        public void UpdateInfo()
        {
            gameObject.SetActive(true);
            UpdateBeforeInfo();
            UpdateNownfo();
        }

        private void UpdateBeforeInfo()
        {
            int coreLevel = ControllerCenter.Instance.CoreController.LastLevel;

            Core_lvup core_Lvup = Core_lvupConfig.GetCore_lvup(coreLevel);

            m_levelBefore.text = coreLevel.ToString();
            m_manaCLBefore.text = core_Lvup.resourceOutPutBonus * 100 + "%";
            m_xyBefore.text = core_Lvup.buildingEfficiency * 100 + "%";

            float currentPower = ControllerCenter.Instance.CoreController.LastPower;
            m_corePowerBefore.text = ((int)(currentPower*100)/100).ToString();

            float mamaGet = BuildingAttribute.Building.GetbaseRewardValue(
                Building_templateConfig.GetBuildingTemplate((int) BuildingType.Core),
                currentPower);

            m_manaGetBefore.text = ((int) (mamaGet*100)/100).ToString();
            m_powerBefore.text = BuildingAttribute.Building.GetcombatBonus().ToString();
        }

        private void UpdateNownfo()
        {
            int coreLevel = CoreSystem.Instance.GetLevel();

            Core_lvup core_Lvup = Core_lvupConfig.GetCore_lvup(coreLevel);

            m_levelNow.text =  coreLevel.ToString();
            m_manaCLNow.text = core_Lvup.resourceOutPutBonus * 100 + "%";
            m_xyNow.text = core_Lvup.buildingEfficiency * 100 + "%";

            float currentPower = CoreSystem.Instance.GetPower();
            m_corePowerNow.text = ((int)(currentPower*100)/100f).ToString();

            float manaGet = BuildingAttribute.Building.GetbaseRewardValue(
                Building_templateConfig.GetBuildingTemplate((int) BuildingType.Core),
                currentPower);
            m_manaGetNow.text =((int)(manaGet*100)/100f).ToString();
            m_powerNow.text = BuildingAttribute.Building.GetcombatBonus().ToString();
        }
    }
}
