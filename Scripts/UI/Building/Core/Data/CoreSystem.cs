

namespace Core.Data
{
    public class CoreSystem : Building
    {
        public static CoreSystem Instance;

        private const string m_path = "Core";

        private CoreData m_coreData;

        public CoreSystem()
        {
            Instance = this;
        }

        public override void SaveData(string parentPath)
        {
            GameDataManager.SaveData(parentPath, m_path, m_coreData);
        }

        public override void ReadData(string parentPath)
        {
            this.parentPath = parentPath;
            m_coreData = GameDataManager.ReadData<CoreData>(parentPath + m_path) as CoreData;

        }

        public override void Init()
        {
            if (m_coreData == null)
            {
                Core_config core = Core_configConfig.GetCore_Config();
                m_coreData = new CoreData();
                m_coreData.Power = core.baseCorePower;
                m_coreData.Level = 1;
                m_coreData.NeedShowDialog = true;

                float random = UnityEngine.Random.Range(1 - core.deviation, 1 + core.deviation);
                m_coreData.Efficiency = random;
            }
        }

        public void Upgrade()
        {
            m_coreData.Level++;
        }


        public void UpdatePower(float power)
        {
            m_coreData.Power += power;
        }

        public float GetPower()
        {
            return m_coreData.Power;
        }

        public void UpdateEfficiency(float efficiency)
        {
            m_coreData.Efficiency = efficiency;
        }

        public float GetEfficiency()
        {
            return m_coreData.Efficiency;
        }

        public void UpdateMana(float mana)
        {
            m_coreData.CurrentMana += mana;
        }

        public float GetMana()
        {
            return m_coreData.CurrentMana;
        }

        public void ResetMana()
        {
            m_coreData.CurrentMana = 0;
        }

        public void UpdateHasIn()
        {
            m_coreData.HasIn = true;
        }

        public bool HasIn()
        {
            return m_coreData.HasIn;
        }

        public bool GetNeddShow()
        {
          //  return false;
            return m_coreData.NeedShowDialog;
        }

        public void UpdateNeedShowDia(bool show)
        {
            m_coreData.NeedShowDialog = show;
        }

        public int GetLevel()
        {
            return m_coreData.Level;
        }

        public Core_lvup GetCoreLvup()
        {
            return Core_lvupConfig.GetCore_lvup(m_coreData.Level);
        }
    }
}

