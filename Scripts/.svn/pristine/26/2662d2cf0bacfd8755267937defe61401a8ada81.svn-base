
//--------------------------------------------------------------
//Creator： xzj
//Data：    5/29/2019
//Note:     
//--------------------------------------------------------------


namespace Guide
{
    /// <summary>
    /// 
    /// </summary>
    public class GuideSys: ScriptBase
    {
        public static GuideSys Instance;

        private GuideData m_data;

        private const string m_pathName = "GuideSysData";

        public GuideSys()
        {
            Instance = this;
        }

        public override void Init()
        {
            if(m_data == null)
                m_data = new GuideData();
        }

        public override void ReadData(string parentPath = null)
        {
            m_data = GameDataManager.ReadData<GuideData>(parentPath + m_pathName) as GuideData;
        }

        public override void SaveData(string parentPath = null)
        {
            GameDataManager.SaveData(parentPath,m_pathName,m_data);
        }

        public bool HaveEnd(GuideStep step)
        {
            if(GetAllHaveEndGuide())
                return true;
            return (m_data.value & (int)step) > 0;
        }

        public void SetHaveEnd(GuideStep step)
        {
            m_data.value |= (int)step;
        }

        public bool GetEndGuideFirstToCorePanel()
        {
            return m_data.EndGuideFirstToCorePanel;
        }

        public void SetEndGuideFirstToCorePanel(bool value)
        {
            m_data.EndGuideFirstToCorePanel = value;
        }

        public bool GetAllHaveEndGuide()
        {
            return m_data.GuideEnd;
        }

        public void SetAllHaveEnd(bool end)
        {
            m_data.GuideEnd = end;
        }
    }
}