using System.Collections.Generic;
using ProtoBuf;


namespace College.Research.Data
{
    /// <summary>
    /// 研究室系统 
    /// </summary>
    public class ResearchLabSystem: Building
    {
        public static ResearchLabSystem Instance { get; private set; }

        private const string ResearchLabPath = "ResearchLab";

        private ResearchLabData m_researchLabData;

        public ResearchLabSystem()
        {
            Instance = this;
        }

        public override void Init()
        {
            if(m_researchLabData == null) m_researchLabData = new ResearchLabData();
        }

        /// <summary>
        /// 读取存档信息
        /// </summary>
        /// <param name="parentPath"></param>
        public override void ReadData(string parentPath)
        {
            this.parentPath = parentPath;
            m_researchLabData = GameDataManager.ReadData<ResearchLabData>(parentPath + ResearchLabPath) as ResearchLabData;
        }


        public override void SaveData(string parentPath)
        {
            GameDataManager.SaveData(parentPath,ResearchLabPath,m_researchLabData);
        }

        /// <summary>
        /// 建立
        /// </summary>
        /// <param name="info"></param>
        public void AddRearch(ResearchingInfo info)
        {
            m_researchLabData.researchingInfo.Add(info);
        }

        /// <summary>
        /// 取消研究 有两种方式 一种是研究时间到了 一种是手动取消
        /// </summary>
        /// <param name="id">取消的id</param>
        /// <param name="isComplate">是否是研究完成了</param>
        /// <returns></returns>
        public void RemoveRearch(string id)
        {
            ResearchingInfo info = GetResearchingInfo(id);
            if(info == null)
            {
                LogHelperLSK.LogError("info i null");
            }
            else
            {
                m_researchLabData.researchingInfo.Remove(info);
            }
        }

        public void AddResearchingsTime(int time)
        {
            for(int i = 0; i < m_researchLabData.researchingInfo.Count; i++)
            {
                m_researchLabData.researchingInfo[i].HaveUseTime += time;
            }
        }

        /// <summary>
        /// 获取正在研究的列表信息
        /// </summary>
        /// <returns></returns>
        public List<ResearchingInfo> GetResearchingInfoList()
        {
            return m_researchLabData.researchingInfo;
        }

        public ResearchingInfo GetResearchingInfo(string id)
        {
            for(int i = 0; i < m_researchLabData.researchingInfo.Count; i++)
            {
                if(m_researchLabData.researchingInfo[i].Id == id)
                {
                    return m_researchLabData.researchingInfo[i];
                }
            }
            return null;
        }


        /// <summary>
        /// 更新等级
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="changeLevel"></param>
        public void UpdateReseachLevel(int type,int nowLevel)
        {
            nowLevel = nowLevel <= 1 ? 1 : nowLevel;
            if(m_researchLabData.EnchantInfoDict.ContainsKey(type))
            {
                m_researchLabData.EnchantInfoDict[type].Level = nowLevel;
            }
            else
            {
                EnchantInfo enchantInfo = new EnchantInfo();
                enchantInfo.EnchantType = type;
                enchantInfo.Level = nowLevel;
                m_researchLabData.EnchantInfoDict.Add(type,enchantInfo);
            }
        }

        public float GetReseachLvel(int type)
        {
            int level = 0;
            if(m_researchLabData.EnchantInfoDict.ContainsKey(type))
            {
                level = (int)m_researchLabData.EnchantInfoDict[type].Level;
            }
            return level < 1 ? 1 : level;
        }

        public void UpdateExp(int type,int addExp)
        {
            if(m_researchLabData.EnchantInfoDict.ContainsKey(type))
            {
                m_researchLabData.EnchantInfoDict[type].Exp += addExp;
            }
            else
            {
                EnchantInfo enchantInfo = new EnchantInfo();
                enchantInfo.EnchantType = type;
                enchantInfo.Exp = addExp;
                m_researchLabData.EnchantInfoDict.Add(type,enchantInfo);
            }
        }

        public void AddMinLevel(int type,int level)
        {
            m_researchLabData.EnchantInfoDict[type].MinEnchantLevel += level;
        }

        public int GetMinLeve(int type)
        {
            if(m_researchLabData.EnchantInfoDict.ContainsKey(type))
            {
                return m_researchLabData.EnchantInfoDict[type].MinEnchantLevel;
            }
            return 0;
        }

        public int GetReseachExp(int type)
        {
            if(m_researchLabData.EnchantInfoDict.ContainsKey(type))
            {
                return m_researchLabData.EnchantInfoDict[type].Exp;
            }
            return 0;
        }
    }


    /// <summary>
    /// 正在研究的信息
    /// </summary>
    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public class ResearchingInfo
    {
        /// <summary>
        /// 研究序列Id 
        /// </summary>
        public string Id;

        /// <summary>
        /// 需要的时间
        /// </summary>
        public float NeedTime;

        /// <summary>
        /// 已经使用了的时间
        /// </summary>
        public int HaveUseTime;

        /// <summary>
        /// 是否扣除每日消耗成功
        /// </summary>
        public bool SubManaSuc;

        /// <summary>
        /// 物品数据
        /// </summary>
        public ItemData Data;

        public int Level;
        public int MinLevel;
        public float Exp;
        public int EffectId;
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
    public class EnchantInfo
    {
        /// <summary>
        /// 类型
        /// </summary>
        public int EnchantType;

        /// <summary>
        /// 等级
        /// </summary>
        public float Level;

        /// <summary>
        /// 累计经验
        /// </summary>
        public int Exp;

        /// <summary>
        /// 累计的最小附魔等级
        /// </summary>
        public int MinEnchantLevel;
    }

}
